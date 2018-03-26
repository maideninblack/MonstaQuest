using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ini : MonoBehaviour {

    public List<Chord> publicChords;
    public MonsterDialiogue monster;
    public bool dialogueIni = false;
	// Use this for initialization
	void Start () {
        
        MonsterQuestLogic.chords = publicChords;
        MonsterQuestLogic.Initialize();
        Debug.Log("Monster Quest initialized");
        MonsterQuestLogic.TestGenerator(Random.Range(1, 5));
        
	}

    private void Update()
    {
        if (!dialogueIni)
        {
            monster.TriggerStartDialogue();
            dialogueIni = true;
        }
        MonsterQuestLogic.MonsterQuestUpdate();
    }

    public void IniDialogue()
    {
        monster.TriggerStartDialogue();
    }

    public void ValidateTest()
    {
        MonsterQuestLogic.ValidateTest();  
        StartCoroutine(Test());
        
    }
    public void ResetButtons()
    {
        MonsterQuestLogic.ResetButtons();
    }

    public void Restart()
    {
        MonsterQuestLogic.Initialize();
        MonsterQuestLogic.TestGenerator(Random.Range(1, 5));
        IniDialogue();
        dialogueIni = false;
    }

    IEnumerator Test()
    {
        yield return new WaitForSeconds(3);
        MonsterQuestLogic.ValidateTest();
        if(MonsterQuestLogic.chordsSelection.Count == 5)
        {
            monster.TriggerEndDialogue();
        }
        
    }
}
