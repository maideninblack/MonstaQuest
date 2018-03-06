using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IterationsManager : MonoBehaviour
{
    public static IterationsManager iterationInstance = null; // Static instance de IterationsManager que obliga a que sólo exista esta instancia, en este script, y que permite que pueda accederse a esta clase desde cualquier parte del código

    public int iterationNumber;
    public int lastIterationPlayed;

    // Esto sería un singleton adaptado a nuestra lógica
    void Awake()
    {
        //Check if instance already exists
        if (iterationInstance == null)

            //if not, set instance to this
            iterationInstance = this;

        //If instance already exists and it's not this:
        else if (iterationInstance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        iterationNumber = 1;
        lastIterationPlayed = 1;
    }
	
	void Update ()
    {
		
	}
}
