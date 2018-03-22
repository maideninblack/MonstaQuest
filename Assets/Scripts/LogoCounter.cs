using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogoCounter : MonoBehaviour {

	public float counter;
    public Transitions transitions;
    public Image blackScreen;

    void Start ()
    {
        transitions = new Transitions();
    }
	
	// Update is called once per frame
	void Update () {
        counter += Time.deltaTime;
        if(counter >= 4)
        {
            transitions.FadeInOut(blackScreen, 1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            
        }
	}
}
