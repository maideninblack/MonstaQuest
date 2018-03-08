using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleBehavior : MonoBehaviour
{
    public Toggle chordToggle;

	void Start ()
    {
        chordToggle = GetComponent<Toggle>();
    }
	

	void Update ()
    {
        if (MonsterQuestLogic.areAllSelected)
        {
            SwitchToggleOff();
        }
        else SwitchToggleOn();
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
