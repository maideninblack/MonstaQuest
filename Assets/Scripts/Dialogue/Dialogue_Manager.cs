using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Dialogue_Manager : MonoBehaviour {
    
    public Text dialogueText;

    private Queue<string> sentences; // (FIFO collection, first in first out) Variable that keeps track of all the sentences in our dialogue

    public bool justEndDialogue = false;
    
  // Use this for initialization
	void Start () {
        sentences = new Queue<string>(); // Initialize variable
        Debug.Log("sentences initialized");
        
	}
	
	

    public void StartDialogue (Dialogue dialogue)
    {      
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
        dialogueText.text = "";
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
        
        
        Debug.Log("End of conversation");
    }

    public void ButtonNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            SceneManager.LoadScene(3);
            return;
        }
        else justEndDialogue = false;
        string sentence = sentences.Dequeue();
        Debug.Log(sentence);
        //dialogueText.text = sentence; this way the text appears instantly
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }
}
