using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanMonsterQuest : MonoBehaviour
{
    public const int MAX_CHORDS = 20;
    public const int MAX_SELECTED_CHORDS = 5;
    public const int MAX_TESTS = 3;

    public int testNumber;

    public int playerScore;

    List<Chord> chords; //  Chords ha de ser una List para poder usar el método Add()

    public Dictionary<int, Chord> chordsSelection; // Diccionario de chords seleccionados, con su key (int)

    bool canValidateTest = false;

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

        chords = new List<Chord>();

        chordsSelection = new Dictionary<int, Chord>();

        doneTests = new List<int>();

        testPerIteration = new Hashtable();
    }

	void Update ()
    {
		
	}

    void TestGenerator(int testBumber)
    {
        // Array de ints variable que usaré para machacarlo con los scores de los chords del tipo de test que toque
        int[] defaultTest = new int[MAX_CHORDS];

        // Switch de cases de testNumber: un case por cada número de test 
    }
}
