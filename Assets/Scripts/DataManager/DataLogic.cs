using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLogic : MonoBehaviour
{
    public int slot;
    public GameData.GameState state;
	// Use this for initialization
	void Awake ()
    {      
        Language.Initialize();        
	}

    private void Start()
    {
        //state = GameData.LoadGame(slot);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Language.SetLanguage(Language.Lang.esES);
            Debug.Log("Español");
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            Language.SetLanguage(Language.Lang.enUS);
            Debug.Log("English");
        }

        /*if(Input.GetKeyDown(KeyCode.S))
        {
            GameData.gameState = state;
            GameData.SaveGame(slot);
        }*/
    }
}
