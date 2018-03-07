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

    bool canValidateTest = false; // To trigger ValidateTest() method

    List<int> doneTests;

    public Hashtable testPerIteration; // Con esta hashtable tendré un mapa de los tests que hay en cada una de las iteraciones (a cada una le corresponde un test)

    // Array para los scores que tiene cada chord en cada test
    int[] melancholicTest;
    int[] naughtyTest;
    int[] tenderTest;
    int[] cheerfulTest;
    int[] bizarreTest;
    int[] sadTest;

    void Start()
    {
        playerScore = 0;

        testNumber = 0; // Nunca funcionará valiendo 0 (ha de ser 1-6 que se lo daré con Random.Range)

        doTest = false;

        chords = new List<Chord>();

        chordsSelection = new Dictionary<int, Chord>();

        doneTests = new List<int>();

        testPerIteration = new Hashtable();
    }

	void Update ()
    {
		if (doTest)
        {
            Debug.Log("Checking if test is already done!");
            bool newTest = false;
        }
	}

    void TestGenerator(int testNumber)
    {
        // Array de ints variable que usaré para machacarlo con los scores de los chords del tipo de test que toque
        int[] defaultTest = new int[MAX_CHORDS];

        testNumber = Random.Range(1, 6);

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
            case 6:
                defaultTest = sadTest;
                break;
        }

        // Los scores de cada chord (20) se cambian a los del tipo de test que haya tocado en el switch
        for (int i = 0; i < MAX_CHORDS; i++)
        {
            chords[i].score = defaultTest[i];
        }

        doTest = true;
    }

    private void Reset()
    {
        testNumber = 0;
    }
}
