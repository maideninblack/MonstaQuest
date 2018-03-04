using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePress : MonoBehaviour
{
    public MonsterQuestLogic MQLinstance;
	public void Pressed(int chordID)
    {
        print("Se ha pulsado " + chordID);

        MQLinstance.AddChordToSelection(chordID);
    }
}
