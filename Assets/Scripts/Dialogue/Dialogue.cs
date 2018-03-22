using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue {

    public string name;
    public int numberOfSentences;

    [TextArea(3, 10)]
    public List<string> sentences;
    public Sprite textImage;
    public Sprite nameImage;
    
    public void Initialize()
    {
        sentences = new List<string>();
        LoadText();
        TextData.AddDialogueText(this); 
    }

    public void LoadText()
    {
        if (sentences == null) sentences = new List<string>();
        if (sentences.Count >= numberOfSentences) return;
        for(int i = 0; i < numberOfSentences; i++)
        {          
            sentences.Add(TextData.GetTextDialogue(i));
        }
        

    }
}

