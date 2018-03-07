using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OkPress : MonoBehaviour
{
    public void Pressed()
    {
        print("Se ha pulsado OK");
        MonsterQuestLogic.ValidateTest();
    }
}
