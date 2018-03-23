using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chord : MonoBehaviour
{
    public int id;
    public int score;
    public bool isPressed = false;

   

    public Toggle chordToggle;

    public void Pressed()
    {
        isPressed = !isPressed;
        Debug.Log("Se ha pulsado " + id);
        MonsterQuestLogic.ChangeChordSelection(this);  
    }

    public void SwitchToggleOff()
    {
        chordToggle.interactable = false;
        
    }

    public void SwitchToggleOn()
    {
        chordToggle.interactable = true;
        
    }
}
