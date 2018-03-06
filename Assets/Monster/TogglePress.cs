using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePress : MonoBehaviour
{
    public void Pressed(int chordID)
    {
        print("Se ha pulsado " + chordID);
        MonsterQuestLogic.MQLinstance.ChangeChordToSelection(chordID);
    }
}
