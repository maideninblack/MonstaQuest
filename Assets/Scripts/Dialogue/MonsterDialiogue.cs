using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDialiogue : DialogueTrigger {
    public Dialogue startDialogue1;
    public Dialogue startDialogue2;
    public Dialogue startDialogue3;
    public Dialogue startDialogue4;
    public Dialogue startDialogue5;

    public Dialogue endDialogueOk;
    public Dialogue endDialogueBad;

    private enum testNumber { ONE, TWO, THREE, FOUR, FIVE}
    private testNumber test;


    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update () {
        if (MonsterQuestLogic.testNumber == 1) test = testNumber.ONE;
        else if (MonsterQuestLogic.testNumber == 2) test = testNumber.TWO;
        else if (MonsterQuestLogic.testNumber == 3) test = testNumber.THREE;
        else if (MonsterQuestLogic.testNumber == 4) test = testNumber.FOUR;
        else if (MonsterQuestLogic.testNumber == 5) test = testNumber.FIVE;
    }

    public void TriggerStartDialogue()
    {
            //base.TriggerDialogue();
            switch (test)
            {
                case testNumber.ONE:
                    FindObjectOfType<Dialogue_Manager>().StartDialogue(startDialogue1);
                    break;
                case testNumber.TWO:
                    FindObjectOfType<Dialogue_Manager>().StartDialogue(startDialogue2);
                    break;
                case testNumber.THREE:
                    FindObjectOfType<Dialogue_Manager>().StartDialogue(startDialogue3);
                    break;
                case testNumber.FOUR:
                    FindObjectOfType<Dialogue_Manager>().StartDialogue(startDialogue4);
                    break;
                case testNumber.FIVE:
                    FindObjectOfType<Dialogue_Manager>().StartDialogue(startDialogue5);
                    break;
                default:
                    break;
            }
    }

    public void TriggerEndDialogue()
    {
        if (MonsterQuestLogic.success)
        {
            FindObjectOfType<Dialogue_Manager>().StartDialogue(endDialogueOk);
        }
        else FindObjectOfType<Dialogue_Manager>().StartDialogue(endDialogueBad);
    }
}
