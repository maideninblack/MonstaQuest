using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transitions : MonoBehaviour {

    
    public float currentTime = 0;   
    public float iniAlpha = 0;
    public float endAlpha = 1;
    public float deltaAlpha;
    public int  cicle = 0;
    public Image blackScreen;

    private void Start()
    {
        deltaAlpha = endAlpha - iniAlpha;
    }
    public void FadeInOut(Image blackScreen, float fadeTime)
    {
              
        if (currentTime <= fadeTime)
        {
            deltaAlpha = endAlpha - iniAlpha;
            float alpha = Easing.SineEaseOut(currentTime, iniAlpha, deltaAlpha, fadeTime);
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, alpha);
            currentTime += Time.deltaTime;

            if (currentTime >= fadeTime)
            {
                cicle++;
                blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, endAlpha);
                currentTime = 0;
                float ini = iniAlpha;
                iniAlpha = endAlpha;
                endAlpha = ini;
                deltaAlpha = endAlpha - iniAlpha;
              
            }
        }

        

    }

    public Vector3 PositionBounceInOut(Vector3 iniPosition, Vector3 finalPosition, float duration)
    {
        Vector3 easingValue = finalPosition;
        
        
        if(currentTime <= duration)
        {           
            Vector3 deltaValue = finalPosition - iniPosition;
            easingValue = new Vector3(Easing.BounceEaseInOut(currentTime, iniPosition.x, deltaValue.x, duration),
                                       Easing.BounceEaseInOut(currentTime, iniPosition.y, deltaValue.y, duration),
                                       Easing.BounceEaseInOut(currentTime, iniPosition.z, deltaValue.z, duration));
            currentTime += Time.deltaTime;
           
            if(currentTime > duration)
            {
                easingValue = finalPosition;
                return easingValue;
            }
            return easingValue;
        }

        return easingValue;
    }
    
    public Vector3 PositionExpoInOut(Vector3 iniPosition, Vector3 finalPosition, float duration)
    {
        Vector3 easingValue = finalPosition;


        if (currentTime <= duration)
        {
            Vector3 deltaValue = finalPosition - iniPosition;
            easingValue = new Vector3(Easing.ExpoEaseInOut(currentTime, iniPosition.x, deltaValue.x, duration),
                                       Easing.ExpoEaseInOut(currentTime, iniPosition.y, deltaValue.y, duration),
                                       Easing.ExpoEaseInOut(currentTime, iniPosition.z, deltaValue.z, duration));
            currentTime += Time.deltaTime;

            if (currentTime > duration)
            {
                easingValue = finalPosition;
                return easingValue;
            }
            return easingValue;
        }

        return easingValue;
    }
    public Vector3 PositionExpoOut(Vector3 iniPosition, Vector3 finalPosition, float duration)
    {
        Vector3 easingValue = finalPosition;


        if (currentTime <= duration)
        {
            Vector3 deltaValue = finalPosition - iniPosition;
            easingValue = new Vector3(Easing.ExpoEaseOut(currentTime, iniPosition.x, deltaValue.x, duration),
                                       Easing.ExpoEaseOut(currentTime, iniPosition.y, deltaValue.y, duration),
                                       Easing.ExpoEaseOut(currentTime, iniPosition.z, deltaValue.z, duration));
            currentTime += Time.deltaTime;

            if (currentTime > duration)
            {
                easingValue = finalPosition;
                return easingValue;
            }
            return easingValue;
        }

        return easingValue;
    }
    public Vector3 PositionCircInOut(Vector3 iniPosition, Vector3 finalPosition, float duration)
    {
        Vector3 easingValue = finalPosition;


        if (currentTime <= duration)
        {
            Vector3 deltaValue = finalPosition - iniPosition;
            easingValue = new Vector3(Easing.CircEaseInOut(currentTime, iniPosition.x, deltaValue.x, duration),
                                       Easing.CircEaseInOut(currentTime, iniPosition.y, deltaValue.y, duration),
                                       Easing.CircEaseInOut(currentTime, iniPosition.z, deltaValue.z, duration));
            currentTime += Time.deltaTime;

            if (currentTime > duration)
            {
                easingValue = finalPosition;
                return easingValue;
            }
            return easingValue;
        }

        return easingValue;
    }
    public Vector3 PositionBackInOut(Vector3 iniPosition, Vector3 finalPosition, float duration)
    {
        Vector3 easingValue = finalPosition;


        if (currentTime <= duration)
        {
            Vector3 deltaValue = finalPosition - iniPosition;
            easingValue = new Vector3(Easing.BackEaseInOut(currentTime, iniPosition.x, deltaValue.x, duration),
                                       Easing.BackEaseInOut(currentTime, iniPosition.y, deltaValue.y, duration),
                                       Easing.BackEaseInOut(currentTime, iniPosition.z, deltaValue.z, duration));
            currentTime += Time.deltaTime;

            if (currentTime > duration)
            {
                easingValue = finalPosition;
                return easingValue;
            }
            return easingValue;
        }

        return easingValue;
    }
    public Vector3 PositionBounceOut(Vector3 iniPosition, Vector3 finalPosition, float duration)
    {
        Vector3 easingValue = finalPosition;


        if (currentTime <= duration)
        {
            Vector3 deltaValue = finalPosition - iniPosition;
            easingValue = new Vector3(Easing.BounceEaseOut(currentTime, iniPosition.x, deltaValue.x, duration),
                                       Easing.BounceEaseOut(currentTime, iniPosition.y, deltaValue.y, duration),
                                       Easing.BounceEaseOut(currentTime, iniPosition.z, deltaValue.z, duration));
            currentTime += Time.deltaTime;

            if (currentTime > duration)
            {
                easingValue = finalPosition;
                return easingValue;
            }
            return easingValue;
        }

        return easingValue;
    }
    public Vector3 PositionElasticOut(Vector3 iniPosition, Vector3 finalPosition, float duration)
    {
        Vector3 easingValue = finalPosition;


        if (currentTime <= duration)
        {
            Vector3 deltaValue = finalPosition - iniPosition;
            easingValue = new Vector3(Easing.ElasticEaseOut(currentTime, iniPosition.x, deltaValue.x, duration),
                                       Easing.ElasticEaseOut(currentTime, iniPosition.y, deltaValue.y, duration),
                                       Easing.ElasticEaseOut(currentTime, iniPosition.z, deltaValue.z, duration));
            currentTime += Time.deltaTime;

            if (currentTime > duration)
            {
                easingValue = finalPosition;
                return easingValue;
            }
            return easingValue;
        }

        return easingValue;
    }

    public bool CicleCheck()
    {
        if (cicle <= 1) return true;
        else return false;
    }
    public void ResetCicle()
    {
        cicle = 0;
    }
    public void ResetCurrentTime()
    {
        currentTime = 0;
    }
                
}
