using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {

    public bool isPaused = false;

    public GameObject PauseMenu;
    

	// Use this for initialization
	void Start () {
        PauseMenu = this.gameObject;
       // PauseMenu.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        /*
         if (isPaused)
         {
            this.gameObject.SetActive(true);
            Time.timeScale = 0f;
         }
         else
         {
            this.gameObject.SetActive(false);
            Time.timeScale = 1f;
         }
         
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!isPaused) SetPause();
            else if(isPaused) QuitPause();
            
        }*/
    }

    public void SetPause()
    {
        isPaused = true;
        this.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void QuitPause()
    {
        isPaused = false;
        this.gameObject.SetActive(false);
        Time.timeScale = 1f;
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
