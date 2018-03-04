using UnityEngine;
using System.Collections;

// Esta clase me va a cargar en memoria nada más iniciar la escena de la iteración a la que esté asociada los scripts IterationsManager y MonsterQuestLogic, creandome instancias de los objs que llevan dentro dichos scripts
// El sr. del tutorial se lo metió dentro a la Main Camera de la scene
public class Loader : MonoBehaviour
{
    public GameObject iterationsManager;          //IterationsManager prefab to instantiate.
    public GameObject monsterQuestLogic;          //IterationsManager prefab to instantiate.

    void Awake()
    {
        //Check if an IterationsManager has already been assigned to static variable IterationsManager.instance or if it's still null
        if (IterationsManager.instance == null)

            //Instantiate IterationsManager prefab
            Instantiate(iterationsManager);

        //Check if a MonsterQuestLogic has already been assigned to static variable MonsterQuestLogic.instance or if it's still null
        if (MonsterQuestLogic.MQLinstance == null)

            //Instantiate MonsterQuestLogic prefab
            Instantiate(monsterQuestLogic);
    }
}