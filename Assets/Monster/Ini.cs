using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ini : MonoBehaviour {

    public List<Chord> publicChords;
	// Use this for initialization
	void Start () {
        
        MonsterQuestLogic.chords = publicChords;
        MonsterQuestLogic.Initialize();
        Debug.Log("Monster Quest initialized");
        MonsterQuestLogic.TestGenerator(Random.Range(1, 5));
	}

    private void Update()
    {
        MonsterQuestLogic.MonsterQuestUpdate();
    }

    public void ValidateTest()
    {
        MonsterQuestLogic.ValidateTest();
    }
    public void ResetButtons()
    {
        MonsterQuestLogic.ResetButtons();
    }
}
