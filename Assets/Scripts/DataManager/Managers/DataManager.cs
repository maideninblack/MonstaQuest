using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;

public static class Data
{
    public static string LoadTextFromResources(string pathfile) // Read 1 text file and return it like textAsset
    {
        TextAsset textAsset = Resources.Load(pathfile) as TextAsset;
        return textAsset.text;
    }
    public static List<string> ReadAllLinesFromString(string text) // Read all lines with stringReader and store them into a list
    {
        StringReader strReader = new StringReader(text);
        List<string> lineList = new List<string>();

        while(true)
        {
            string line = strReader.ReadLine();
            if(line != null) lineList.Add(line);
            else break;
        }
        return lineList;
    }

    public static object ReadBinaryPersistentPath<T>(string fileName)
    {
        Debug.Log("PersistentDataPath: " + Application.persistentDataPath);

        string pathFile = Application.persistentDataPath + "/Data/Slots/" + fileName;
        T data;

        using(Stream stream = File.Open(pathFile, FileMode.Open))
        {
            var bformatter = new BinaryFormatter();
            data = (T)bformatter.Deserialize(stream);
        }
        return data;
    } // READ Save Files

    public static void WriteBinaryPersistentPath(object data, string fileName) // SAVE Save Files
    {
        string path = Application.persistentDataPath + "/Data/Slots/";
        if(!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        using(Stream stream = File.Open(path + fileName, FileMode.Create))
        {
            var bformater = new BinaryFormatter();
            bformater.Serialize(stream, data);
        }
    }
}

public static class GameData
{
    [Serializable]
    public struct GameState
    {
        public float playerPosition;
        public bool hasKey;
        public int score;
        public string playerName;
    }
    public static GameState gameState;

    public static void SaveGame(int slot)
    {
        Debug.Log("Saving");
        Data.WriteBinaryPersistentPath(gameState, "SaveGame_" + slot + ".save");
        Debug.Log("Saved");
    }
    public static void NewGame(int slot)
    {
        gameState = new GameState();
        Debug.Log("New game");

        gameState.playerPosition = 0;
        gameState.hasKey = false;
        gameState.score = 0;
        gameState.playerName = "NoName";

        SaveGame(slot);
    }

    public static void DeleteGame(int slot) { }
    public static GameState LoadGame(int slot)
    {
        Debug.Log("LoadGame");
        gameState = new GameState();

        try
        {
            gameState = (GameState)Data.ReadBinaryPersistentPath<GameState>("SaveGame_" + slot + ".save");
            Debug.Log("Game loaded");
        }
        catch(Exception e)
        {
            Debug.LogError("Loading error: " + e);
            NewGame(slot);
        }

        return gameState;
    }
}

public static class TextData
{
    public static Dictionary<string, string> mainTitleData;    // Dictionary for storing the keys + the value of the text. Probably want to create 1 for each external text file in the game
    public static Dictionary<string, string> scene1Data;      // Scene 1 dialogue text
    public static Dictionary<string, string> scene2Data;      // Scene 2 dialogue text
    public static Dictionary<string, string> scene3Data;      // Scene 3 dialogue text
    public static Dictionary<string, string> ui;              // Gameplay UI text
    public static Dictionary<string, string> endScene;        // End scene text

    public static Dictionary<int, string> testing;       // Joker dictionary for testing
    /* Dictionary<TKey, TValue>
     * TValue: es el tipo de variable que almacena.
     * TKey: es el identificador con el que guardo el value.
     */

    public static void Initialize()
    {
        #region MainTitle Dictionary
        mainTitleData = new Dictionary<string, string>();
        //Leyendo el texto entero
        string mainTitleText = Data.LoadTextFromResources("Data/Main_Menu_Text");
        //Separando el texto en lineas
        List<string> allLinesTitle = Data.ReadAllLinesFromString(mainTitleText);
        //Separando las columnas de cada linea
        for (int line = 1; line < allLinesTitle.Count; line++)
        {
            string[] colText = allLinesTitle[line].Split('\t');

            if (Language.language == Language.Lang.esES) mainTitleData.Add(colText[0], colText[1]);
            else mainTitleData.Add(colText[0], colText[2]);
            //Si hay mas idiomas, añadir aquí un if por cada uno de ellos.
        }
        #endregion
        #region Test Dictionary
        testing = new Dictionary<int, string>();
        string testingText = Data.LoadTextFromResources("Data/testingText");
        List<string> allLinesTest = Data.ReadAllLinesFromString(testingText);
        for (int line = 1; line < allLinesTest.Count; line++)
        {
            string[] colText = allLinesTest[line].Split('\t');     
            if (Language.language == Language.Lang.esES) testing.Add(int.Parse(colText[0]), colText[1]);
            else if(Language.language == Language.Lang.enUS) testing.Add(int.Parse(colText[0]), colText[2]);

            Debug.Log(colText[2]);
            //Si hay mas idiomas, añadir aquí un if por cada uno de ellos.
        }
#endregion
        UpdateUIText();
        UpdateDialogueText();
    }

    public static string GetTextTitle(string key)
    {
        string value = "";
        mainTitleData.TryGetValue(key, out value);
        return value;
    }
    public static string GetTextDialogue(int key)
    {
        string value = "";
        testing.TryGetValue(key, out value);
        Debug.Log(value);
        return value;
    }
    //UI TEXT
    public static List<LoadUIText> uiText;
    public static List<Dialogue> dialogueText;
    public static void AddUIText(LoadUIText ui)
    {
        if(uiText == null) uiText = new List<LoadUIText>();
        uiText.Add(ui);
        ui.LoadText();
    }
    public static void AddDialogueText(Dialogue dialogue)
    {
        if (dialogueText == null) dialogueText = new List<Dialogue>();
        dialogueText.Add(dialogue);
        dialogue.LoadText();
    }

    public static void UpdateUIText()
    {
        if(uiText == null) return;
        /*foreach(LoadUIText ui in uiText)
        {
            ui.LoadText();
        }*/
        for(int i = 0; i < uiText.Count; i++)
        {
            uiText[i].LoadText();
        }
    }  
    public static void UpdateDialogueText()
    {
        if (dialogueText == null) return;
        for(int i = 0; i < dialogueText.Count; i++)
        {
            dialogueText[i].LoadText();
        }
    }
}

public static class Language
{
    public enum Lang { none, esES, enUS};
    public static Lang language;

    public static void Initialize()
    {
        if(language == Lang.none)
        {
            if(Application.systemLanguage == SystemLanguage.English)
            {
                language = Lang.enUS;
            }
            else if(Application.systemLanguage == SystemLanguage.Spanish)
            {
                language = Lang.esES;
            }
            else language = Lang.enUS;
        }
        UpdateTextLagunage();
    }

    public static void SetLanguage(Lang newLanguage)
    {
        language = newLanguage;
        UpdateTextLagunage();
    }

    public static void UpdateTextLagunage()
    {
        TextData.Initialize();        
    }
}
