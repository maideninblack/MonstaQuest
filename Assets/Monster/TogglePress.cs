using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TogglePress : MonoBehaviour
{
    public void Pressed(int chordID)
    {
        print("Se ha pulsado " + chordID);
    }
}
