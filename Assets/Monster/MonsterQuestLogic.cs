using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MonsterQuestLogic
{
    public  const int MAX_CHORDS = 20;
    public  const int MAX_SELECTED_CHORDS = 5;
    public  const int MAX_TESTS = 3;
    public const int MINIMUM_SCORE = 7;


    public static int testNumber; // Variable que se randomizará para después generar el test

    public static int playerScore;

    public static List<Chord> chords; //  Chords ha de ser una List para poder usar el método Add()

    public static List<Chord> chordsSelection; // Diccionario de chords seleccionados, con su key (int)

    public static bool doTest;

    public static bool areAllSelected;

    static bool canValidateTest = false; // To trigger ValidateTest() method

    static List<int> doneTests;


    // Array para los scores que tiene cada chord en cada test
    static int[] melancholicTest;
    static int[] naughtyTest;
    static int[] tenderTest;
    static int[] cheerfulTest;
    static int[] bizarreTest;

    public static void Initialize()
    {
        playerScore = 0;

        testNumber = 0; // Nunca funcionará valiendo 0 (ha de ser 1-6 que se lo daré con Range)

        doTest = false;

        areAllSelected = false;
   
        chordsSelection = new List<Chord>();
        Debug.Log("ChordSelection initialized");

        doneTests = new List<int>();

        // esto le llegará a defaultTest, de momento ponemos estos valores y luego quizá los inicializamos desde editor, mejor...
        melancholicTest = new int[] { 1, 1, 1, 1, 5, 5, 5, 5, 4, 4, 4, 4, 3, 3, 3, 3, 2, 2, 2, 2 };
        naughtyTest = new int[] { 2, 2, 2, 2, 1, 1, 1, 1, 5, 5, 5, 5, 4, 4, 4, 4, 3, 3, 3, 3 };
        tenderTest = new int[] { 3, 3, 3, 3, 2, 2, 2, 2, 1, 1, 1, 1, 5, 5, 5, 5, 4, 4, 4, 4 };
        cheerfulTest = new int[] { 4, 4, 4, 4, 3, 3, 3, 3, 2, 2, 2, 2, 1, 1, 1, 1, 5, 5, 5, 5 };
        bizarreTest = new int[] { 5, 5, 5, 5, 4, 4, 4, 4, 3, 3, 3, 3, 2, 2, 2, 2, 1, 1, 1, 1 };

    }

	public static void MonsterQuestUpdate ()
    {
        if(chordsSelection.Count < 5)
        {
            areAllSelected = false;
            for (int i = 0; i < chords.Count; i++)
            {
                chords[i].SwitchToggleOn();     
            }
        }
		if (doTest)
        {
            Debug.Log("Checking if test is already done!");

            bool newTest = false; // Para controlar que el test que se le asigne no haya aparecido ya antes

            while (newTest == false) // Mientras no sea un test nuevo seguiré intentando generar uno nuevo
            {
                testNumber = Random.Range(1, 6);

                if(doneTests.Count > 0)
                {
                    foreach(int test in doneTests) // Recorro la List de doneTests
                    {
                        if(testNumber != test) // Si el test generado no está en doneTests
                        {
                            newTest = true; // Entonces es un nuevo test!
                        }
                        else // Pero si encuentra una coincidencia
                        {
                            newTest = false; // Ya se ha usado!
                            break; // Y break del foreach para que empiece de nuevo
                        }
                    }
                }
                else
                    newTest = true;
            }

            doneTests.Add(testNumber);
            doTest = false;

            // Deshabilitar chords NO seleccionados cuando la selección está llena
            if (areAllSelected)
            {
                foreach (Chord chord in chords)
                    {
                    if (!chord.isPressed)
                    {
                        chord.SwitchToggleOff();
                        Debug.Log("Toggles disabled!");
                    }
                    else chord.SwitchToggleOn();
                    }  
            }
        }
        if (areAllSelected)
        {
            Debug.Log("Toggling all off: starting");
            for(int i = 0; i < chords.Count; i++)
            {
                chords[i].SwitchToggleOff();
               
            }
            Debug.Log("Toggling all off: done");


            for (int i = 0; i < chords.Count; i++)
            {
                if(chords[i].isPressed)
                chords[i].SwitchToggleOn();      
            }
        }
	}

    public static void TestGenerator(int testNumber)
    {
        // Array de ints variable que usaré para machacarlo con los scores de los chords del tipo de test que toque
        int[] defaultTest = new int[MAX_CHORDS];
        Debug.Log("Starting case " + testNumber);
        // Switch de cases de testNumber: un case por cada número de test (es decir, por cada iteración)
        switch (testNumber)
        {
            case 1:
                defaultTest = melancholicTest;
                break;
            case 2:
                defaultTest = naughtyTest;
                break;
            case 3:
                defaultTest = tenderTest;
                break;
            case 4:
                defaultTest = cheerfulTest;
                break;
            case 5:
                defaultTest = bizarreTest;
                break;
        }

        // Los scores de cada chord (20) se cambian a los del tipo de test que haya tocado en el switch
        for (int i = 0; i < MAX_CHORDS; i++)
        {
            chords[i].score = defaultTest[i];
        }

        doTest = true;
    }

    public static void ValidateTest()
    {
        // Meter la corrutina que esta en el audiomanager que playea todos los sonidos(al final de todo esto)
        if (areAllSelected)
        {
            for (int i = 0; i < chordsSelection.Count; i++)
            {
                chordsSelection[i].score = chords[i].score;
                playerScore += chordsSelection[i].score;
            }
            if (playerScore >= MINIMUM_SCORE)
            {
                Debug.Log("Congratulations!!! You passed!!");
            }
            else Debug.Log("Sorry, you are too bad...");
            Debug.Log("Score: " + playerScore.ToString());
        }
        else Debug.Log("There are not 5 selected");

     
    }

    public static bool ChangeChordSelection(Chord chord)
    {     
        if (!chord.isPressed/*chordsSelection.Contains(chord)*/)
        {
            chordsSelection.Remove(chord);
            Debug.Log("Se quita: " + chord.id);
        }

       if (!areAllSelected)
       {
            if (chord.isPressed)
            {
                chordsSelection.Add(chord);
                Debug.Log("Se añade: " + chord.id);
            }
       }

        if (chordsSelection.Count > 4)
        {
            areAllSelected = true;
            Debug.Log("All are selected");
        }
            
        else areAllSelected = false;

        return areAllSelected;
    }

    public static void Reset()
    {
        testNumber = 0;
        // TODO
        // Empty the selection dictionary
        // Reset variables (including areAllSelected)
        // Reload scene¿?
    }

    public static void ResetButtons()
    {
        Debug.Log(chordsSelection.Count);
        for (int i = 0; i < chords.Count; i++)
        {
            chords[i].chordToggle.isOn = false;
            
        }
        chordsSelection.Clear();
        Debug.Log("Selection cleared");
    }

    
}
