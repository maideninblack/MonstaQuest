//This example outputs Sliders that control the red green and blue elements of a sprite's color
//Attach this to a GameObject and attach a SpriteRenderer component
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    SpriteRenderer m_SpriteRenderer;
    //The Color to be assigned to the Renderer’s Material
    Color m_NewColor;

    public float currentTime = 0;
    public float timeDuration = 0;

    private Color orangeFu = new Color(255, 147, 64);
    private Color bluFu = new Color(43, 202, 255);

    public Color iniColor = new Color(0, 0, 0);
    public Color finalColor = new Color(0, 0, 0);
    private Color deltaColor; // Incremento o variación de los valores, final - inicial

    public float startDelay;
    public bool pingPong;

    private bool start;

    //These are the values that the Color Sliders return
    //public float m_Red, m_Blue, m_Green;

    void Start()
    {
        //iniColor = orangeFu;
        //finalColor = bluFu;
        deltaColor = finalColor - iniColor;
        //Fetch the SpriteRenderer from the GameObject
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        start = false;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space)) start = true;
        if (start)
        {
            if (currentTime <= timeDuration) // Mientras el momento actual sea menor o igual se hará esto
            {
                // Do easing
                Color easingValue = iniColor;

                if (startDelay > 0) //Cuenta atras
                {
                    startDelay -= Time.deltaTime;
                    return;
                }
                easingValue = new Color(Easing.ExpoEaseInOut(currentTime, iniColor.r, deltaColor.r, timeDuration),
                                        Easing.ExpoEaseInOut(currentTime, iniColor.g, deltaColor.g, timeDuration),
                                        Easing.ExpoEaseInOut(currentTime, iniColor.b, deltaColor.b, timeDuration));

                m_SpriteRenderer.color = easingValue;

                currentTime += Time.deltaTime;


                if (currentTime > timeDuration) // En este momento se ha de acabar el easing
                {
                    if (pingPong)
                    {
                        currentTime = 0;
                        Color ini = iniColor;
                        iniColor = finalColor;
                        finalColor = ini;
                        deltaColor = finalColor - iniColor;
                    }
                }
            }
        }
    }


}
