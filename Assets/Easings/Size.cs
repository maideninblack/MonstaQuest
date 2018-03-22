using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Size : MonoBehaviour
{
    public enum Easings { expo, circ, quint, quart, quad, sine, back, bounce, elastic };
    public Easings easings; // Instacio un objeto de la clase Easings

    public float currentTime; // Es el valor de tiempo de la animación en un momento concreto 
    public float timeDuration; // Duración total de la animación

    public Vector3 iniValue; // Valor de inicio de la animación
    public Vector3 finalValue; // Valor final de la animación
    private Vector3 deltaValue; // Incremento o variación de los valores, final - inicial
    //private Vector3 easingValue; // Incremento o variación de los valores, final - inicial

    public bool pingPong;

    public float startDelay;

    public bool start;


    private void Start()
    {
        deltaValue = finalValue - iniValue;
        start = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) start = true;
        if (start)
        {
            // Al final un easing se hace con un contador de tiempo
            // Easing - hay que calcularlos cada frame, porque si no nos daría un solo valor

            if (currentTime <= timeDuration) // Mientras el momento actual sea menor o igual se hará esto
            {
                // Do easing
                Vector3 easingValue = iniValue;

                if (startDelay > 0) //Cuenta atras
                {
                    startDelay -= Time.deltaTime;
                    return;
                }

                switch (easings)
                {
                    case Easings.expo:
                        easingValue = new Vector3(Easing.ExpoEaseInOut(currentTime, iniValue.x, deltaValue.x, timeDuration),
                                        Easing.ExpoEaseInOut(currentTime, iniValue.y, deltaValue.y, timeDuration),
                                        Easing.ExpoEaseInOut(currentTime, iniValue.z, deltaValue.z, timeDuration));
                        break;
                    case Easings.circ:
                        easingValue = new Vector3(Easing.CircEaseInOut(currentTime, iniValue.x, deltaValue.x, timeDuration),
                                        Easing.CircEaseInOut(currentTime, iniValue.y, deltaValue.y, timeDuration),
                                        Easing.CircEaseInOut(currentTime, iniValue.z, deltaValue.z, timeDuration));
                        break;
                    case Easings.quint:
                        easingValue = new Vector3(Easing.QuintEaseInOut(currentTime, iniValue.x, deltaValue.x, timeDuration),
                                        Easing.QuintEaseInOut(currentTime, iniValue.y, deltaValue.y, timeDuration),
                                        Easing.QuintEaseInOut(currentTime, iniValue.z, deltaValue.z, timeDuration));
                        break;
                    case Easings.quart:
                        easingValue = new Vector3(Easing.QuartEaseInOut(currentTime, iniValue.x, deltaValue.x, timeDuration),
                                        Easing.QuartEaseInOut(currentTime, iniValue.y, deltaValue.y, timeDuration),
                                        Easing.QuartEaseInOut(currentTime, iniValue.z, deltaValue.z, timeDuration));
                        break;
                    case Easings.quad:
                        easingValue = new Vector3(Easing.QuadEaseInOut(currentTime, iniValue.x, deltaValue.x, timeDuration),
                                        Easing.QuadEaseInOut(currentTime, iniValue.y, deltaValue.y, timeDuration),
                                        Easing.QuadEaseInOut(currentTime, iniValue.z, deltaValue.z, timeDuration));
                        break;
                    case Easings.sine:
                        easingValue = new Vector3(Easing.SineEaseInOut(currentTime, iniValue.x, deltaValue.x, timeDuration),
                                        Easing.SineEaseInOut(currentTime, iniValue.y, deltaValue.y, timeDuration),
                                        Easing.SineEaseInOut(currentTime, iniValue.z, deltaValue.z, timeDuration));
                        break;
                    case Easings.back:
                        easingValue = new Vector3(Easing.BackEaseInOut(currentTime, iniValue.x, deltaValue.x, timeDuration),
                                        Easing.BackEaseInOut(currentTime, iniValue.y, deltaValue.y, timeDuration),
                                        Easing.BackEaseInOut(currentTime, iniValue.z, deltaValue.z, timeDuration));
                        break;
                    case Easings.bounce:
                        easingValue = new Vector3(Easing.BounceEaseInOut(currentTime, iniValue.x, deltaValue.x, timeDuration),
                                        Easing.BounceEaseInOut(currentTime, iniValue.y, deltaValue.y, timeDuration),
                                        Easing.BounceEaseInOut(currentTime, iniValue.z, deltaValue.z, timeDuration));
                        break;
                    case Easings.elastic:
                        easingValue = new Vector3(Easing.ElasticEaseInOut(currentTime, iniValue.x, deltaValue.x, timeDuration),
                                        Easing.ElasticEaseInOut(currentTime, iniValue.y, deltaValue.y, timeDuration),
                                        Easing.ElasticEaseInOut(currentTime, iniValue.z, deltaValue.z, timeDuration));
                        break;
                    default:
                        break;
                }

                transform.localScale = easingValue;

                // Contador tiempo
                currentTime += Time.deltaTime;

                if (currentTime > timeDuration) // En este momento se ha de acabar el easing
                {
                    Debug.Log("El easing acaba de terminar justo ahora");
                    transform.localScale = finalValue;

                    // Esto para que haga el efecto de ping pong, si no no hace falta, ya ha acabado
                    if (pingPong)
                    {
                        currentTime = 0;
                        Vector3 ini = iniValue;
                        iniValue = finalValue;
                        finalValue = ini;
                        deltaValue = finalValue - iniValue;
                    }
                }

            }

            else Debug.Log("Easing terminado");
        }
    }
}
