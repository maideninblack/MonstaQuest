using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue_Manager : MonoBehaviour {

    //public Text nameText;
    public Text dialogueText;

    //public Animator animator;

    private Queue<string> sentences; // (FIFO collection, first in first out) Variable that keeps track of all the sentences in our dialogue

    public bool justEndDialogue = false;
    
    //Script_HideCursor cursor;


   // public CinemachineBehaviour cineMachine;
    //public Animator cineMachineAnim;


    

	// Use this for initialization
	void Start () {
        sentences = new Queue<string>(); // Initialize variable
        Debug.Log("sentences initialized");
        //cursor = GetComponent<Script_HideCursor>();
	}
	
	

    public void StartDialogue (Dialogue dialogue)
    {
        //cineMachineAnim.SetBool("IsActive", true);
        //cineMachine.Play();
        
        //cursor.ShowCursor();
        
        //animator.SetBool("IsOpen", true);

        //Debug.Log("Starting conversation with" + dialogue.name);

        //nameText.text = dialogue.name;

        sentences.Clear();
        Debug.Log("Cleared");

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        EndDialogue();
        /*  if (sentences.Count == 1)
          {
              justEndDialogue = true;
          }
          else justEndDialogue = false;*/

        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        else justEndDialogue = false;
    

        string sentence = sentences.Dequeue();   
        Debug.Log(sentence);
        //dialogueText.text = sentence; this way the text appears instantly
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

    }

    IEnumerator TypeSentence (string sentence)
    {
       // dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }
    
    public void EndDialogue()
    {
        dialogueText.text = null;
        justEndDialogue = true;
        //cursor.HideCursor();
        
        Debug.Log("End of conversation");
       // animator.SetBool("IsOpen", false);

        //cineMachineAnim.SetBool("IsActive", false);

    }
}
