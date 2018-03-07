using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanMonsterQuest : MonoBehaviour
{
    public const int MAX_CHORDS = 20;
    public const int MAX_SELECTED_CHORDS = 5;
    public const int MAX_TESTS = 3;

    public int testNumber; // Variable que se randomizará para después generar el test

    public int playerScore;

    List<Chord> chords; //  Chords ha de ser una List para poder usar el método Add()

    public Dictionary<int, Chord> chordsSelection; // Diccionario de chords seleccionados, con su key (int)

    public bool doTest;

    public bool areAllSelected;

    bool canValidateTest = false; // To trigger ValidateTest() method

    List<int> doneTests;

    // Array para los scores que tiene cada chord en cada test
    int[] melancholicTest;
    int[] naughtyTest;
    int[] tenderTest;
    int[] cheerfulTest;
    int[] bizarreTest;

    void Start()
    {
        playerScore = 0;

        testNumber = 0; // Nunca funcionará valiendo 0 (ha de ser 1-6 que se lo daré con Range)

        doTest = false;

        areAllSelected = false;

        chords = new List<Chord>();

        chordsSelection = new Dictionary<int, Chord>();

        doneTests = new List<int>();

        // esto le llegará a defaultTest, de momento ponemos estos valores y luego quizá los inicializamos desde editor, mejor...
        melancholicTest = new int[] { 1, 1, 1, 1, 5, 5, 5, 5, 4, 4, 4, 4, 3, 3, 3, 3, 2, 2, 2, 2 };
        naughtyTest = new int[] { 2, 2, 2, 2, 1, 1, 1, 1, 5, 5, 5, 5, 4, 4, 4, 4, 3, 3, 3, 3 };
        tenderTest = new int[] { 3, 3, 3, 3, 2, 2, 2, 2, 1, 1, 1, 1, 5, 5, 5, 5, 4, 4, 4, 4 };
        cheerfulTest = new int[] { 4, 4, 4, 4, 3, 3, 3, 3, 2, 2, 2, 2, 1, 1, 1, 1, 5, 5, 5, 5 };
        bizarreTest = new int[] { 5, 5, 5, 5, 4, 4, 4, 4, 3, 3, 3, 3, 2, 2, 2, 2, 1, 1, 1, 1 };
    }

	void Update ()
    {
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
        }
	}

    void TestGenerator(int testNumber)
    {
        // Array de ints variable que usaré para machacarlo con los scores de los chords del tipo de test que toque
        int[] defaultTest = new int[MAX_CHORDS];

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

    public void ValidateTest()
    {
        areAllSelected = true;

        for (int i = 0; i < MAX_SELECTED_CHORDS; i++)
        {
            if (!chordsSelection.ContainsKey(i)) // En el momento en el que encuentre uno que no esté presente, expresado en la función ContainsKey(i)...
            {
                areAllSelected = false; // ... Pues pongo areAllSelected a false, porque hay al menos un hueco y por tanto se puede rellenar con otro valor
            }
        }

        if (areAllSelected)
        {
            for (int i = 0; i < MAX_SELECTED_CHORDS; i++)
            {
                playerScore += chordsSelection[i].score;
            }

            print("Score: " + playerScore.ToString());
        }
    }

    private void Reset()
    {
        testNumber = 0;
    }
}
