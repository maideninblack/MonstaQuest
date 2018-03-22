using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public enum Easings { expo, circ, quint, quart, quad, sine, back, bounce, elastic };
    public Easings easings; // Instacio un objeto de la clase Easings

    public float currentTime; // Es el valor de tiempo de la animación en un momento concreto 
    public float timeDuration; // Duración total de la animación

    public Vector3 iniValue; // Valor de inicio de la animación
    public Vector3 finalValue; // Valor final de la animación
    private Vector3 deltaValue; // Incremento o variación de los valores, final - inicial

    public bool pingPong;

    public float startDelay;

    private bool start;


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
            if (currentTime <= timeDuration)
            {

                // Do easing
                Quaternion easingValue = Quaternion.Euler(iniValue); // Hay que convertirlo a Quaternion

                if (startDelay > 0) //Cuenta atras
                {
                    startDelay -= Time.deltaTime;
                    return;
                }

                switch (easings)
                {
                    case Easings.expo:
                        easingValue = Quaternion.Euler(finalValue.x, finalValue.y, Easing.ExpoEaseInOut(currentTime, iniValue.z, deltaValue.z, timeDuration));
                        break;
                    case Easings.circ:
                        easingValue = Quaternion.Euler(finalValue.x, finalValue.y, Easing.CircEaseInOut(currentTime, iniValue.z, deltaValue.z, timeDuration));
                        break;
                    case Easings.quint:
                        easingValue = Quaternion.Euler(finalValue.x, finalValue.y, Easing.QuintEaseInOut(currentTime, iniValue.z, deltaValue.z, timeDuration));
                        break;
                    case Easings.quart:
                        easingValue = Quaternion.Euler(finalValue.x, finalValue.y, Easing.QuartEaseInOut(currentTime, iniValue.z, deltaValue.z, timeDuration));
                        break;
                    case Easings.quad:
                        easingValue = Quaternion.Euler(finalValue.x, finalValue.y, Easing.QuadEaseInOut(currentTime, iniValue.z, deltaValue.z, timeDuration));
                        break;
                    case Easings.sine:
                        easingValue = Quaternion.Euler(finalValue.x, finalValue.y, Easing.SineEaseInOut(currentTime, iniValue.z, deltaValue.z, timeDuration));
                        break;
                    case Easings.back:
                        easingValue = Quaternion.Euler(finalValue.x, finalValue.y, Easing.BackEaseInOut(currentTime, iniValue.z, deltaValue.z, timeDuration));
                        break;
                    case Easings.bounce:
                        easingValue = Quaternion.Euler(finalValue.x, finalValue.y, Easing.BounceEaseInOut(currentTime, iniValue.z, deltaValue.z, timeDuration));
                        break;
                    case Easings.elastic:
                        easingValue = Quaternion.Euler(finalValue.x, finalValue.y, Easing.ElasticEaseInOut(currentTime, iniValue.z, deltaValue.z, timeDuration));
                        break;
                    default:
                        break;
                }

                transform.localRotation = easingValue;

                currentTime += Time.deltaTime;

                if (currentTime > timeDuration)
                {
                    Debug.Log("El easing acaba de terminar justo ahora");
                    transform.localRotation = Quaternion.Euler(finalValue);

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
