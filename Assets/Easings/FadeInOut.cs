using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour {

    public enum Easings { expo, circ, quint, quart, quad, sine, back, bounce, elastic };
    public Easings easings; // Instacio un objeto de la clase Easings

    public float currentTime; // Es el valor de tiempo de la animación en un momento concreto 
    public float timeDuration; // Duración total de la animación

    public float iniValue; // Valor de inicio de la animación
    public float finalValue; // Valor final de la animación
    private float deltaValue; // Incremento o variación de los valores, final - inicial

    public bool pingPong;

    public float startDelay;

    public Image blackScreen;

    private void Start()
    {
        deltaValue = finalValue - iniValue;
    }
    public void Update()
    {            // Al final un easing se hace con un contador de tiempo
                 // Easing - hay que calcularlos cada frame, porque si no nos daría un solo valor

        if (currentTime <= timeDuration) // Mientras el momento actual sea menor o igual se hará esto
        {
            // Do easing
            float easingValue = iniValue;
            if (startDelay > 0) //Cuenta atras
            {
                startDelay -= Time.deltaTime;
                return;
            }

            switch (easings)
            {
                case Easings.expo:
                    easingValue = (Easing.ExpoEaseInOut(currentTime, iniValue, deltaValue, timeDuration));
                                 
                    break;
                case Easings.circ:
                    easingValue = (Easing.CircEaseInOut(currentTime, iniValue, deltaValue, timeDuration));
                    break;
                case Easings.quint:
                    easingValue = (Easing.QuintEaseInOut(currentTime, iniValue, deltaValue, timeDuration));
                    break;
                case Easings.quart:
                    easingValue = (Easing.QuartEaseInOut(currentTime, iniValue, deltaValue, timeDuration));
                    break;
                case Easings.quad:
                    easingValue = (Easing.QuadEaseInOut(currentTime, iniValue, deltaValue, timeDuration));
                    break;
                case Easings.sine:
                    easingValue = (Easing.SineEaseInOut(currentTime, iniValue, deltaValue, timeDuration));
                    break;
                case Easings.back:
                    easingValue = (Easing.BackEaseInOut(currentTime, iniValue, deltaValue, timeDuration));
                    break;
                case Easings.bounce:
                    easingValue = (Easing.BounceEaseInOut(currentTime, iniValue, deltaValue, timeDuration));
                    break;
                case Easings.elastic:
                    easingValue = (Easing.ElasticEaseInOut(currentTime, iniValue, deltaValue, timeDuration));
                    break;
                default:
                    break;
            }
            Debug.Log("easing value = " + easingValue);
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, easingValue);

            // Contador tiempo
            currentTime += Time.deltaTime;

            if (currentTime > timeDuration) // En este momento se ha de acabar el easing
            {
                Debug.Log("El easing acaba de terminar justo ahora");
                blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, finalValue);

                // Esto para que haga el efecto de ping pong, si no no hace falta, ya ha acabado
                if (pingPong)
                {
                    currentTime = 0;
                   float ini = iniValue;
                    iniValue = finalValue;
                    finalValue = ini;
                    deltaValue = finalValue - iniValue;
                }
            }

        }

        else Debug.Log("Easing terminado");
    }
}
