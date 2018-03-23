using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

    public Dialogue dialogue;        
    //public Sprite characterSprite;
    public Dialogue_Manager dManager;
    //public Sprite nameSprite;

    private void Start()
    {
       
       dManager = GetComponent<Dialogue_Manager>();
    }
    

   

    public virtual void TriggerDialogue()
    {
        FindObjectOfType<Dialogue_Manager>().StartDialogue(dialogue);
        //Dialogue.textImage.sprite = characterSprite;
        //dialogue.nameImage = nameSprite;
    }

}
