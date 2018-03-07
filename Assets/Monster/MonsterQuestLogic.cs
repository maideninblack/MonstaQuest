using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterQuestLogic : MonoBehaviour
{
    public static MonsterQuestLogic MQLinstance = null; // Static instance of MonsterQuestLogic which allows it to be accessed by any other script.

    public const int MAX_CHORDS = 20;
    public const int MAX_TESTS = 5;
    public const int MAX_SELECTED_CHORDS = 5;

    public int testNumber;

    public int playerScore; // este será el score del player, la suma, y luego tenemos el Chord.score

    // Declaración array de Chord
    List <Chord> chords;

    public Dictionary<int, Chord> chordsSelection; // diccionario de chords seleccionados con su key (int)

    // Array para los scores que tiene cada chord en cada test
    int[] melancholicTest;
    int[] naughtyTest;
    int[] tenderTest;
    int[] cheerfulTest;
    int[] bizarreTest;

    // Declaración lista de ints donde almacenemos las pruebas que ya se han hecho para que no se repitan
    List<int> doneTests;
    
    // Hashtable es una versión avanzada de los Dictionaries, con esto tengo un mapa de los tests que hay en cada una de las iteraciones, 
    // respectivamente (a cada iteración le corresponde un test)
    public Hashtable iterationTests; 

    bool homeworkFixControl;
    int homeworkFixFramesCounter;

    void Awake()
    {
        // SINGLETON -----------------------------------------------
        //Check if instance already exists
        if (MQLinstance == null)

            //if not, set instance to this
            MQLinstance = this;

        //If instance already exists and it's not this:
        else if (MQLinstance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Set this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
        // SINGLETON -----------------------------------------------

        chords = new List<Chord>(); // Inicializo la lista de chords

        // esto le llegará a defaultTest, de momento ponemos estos valores y luego quizá los inicializamos desde editor, mejor...
        melancholicTest = new int[] {1, 1, 1, 1, 5, 5, 5, 5, 4, 4, 4, 4, 3, 3, 3, 3, 2, 2, 2, 2};
        naughtyTest = new int[]     {2, 2, 2, 2, 1, 1, 1, 1, 5, 5, 5, 5, 4, 4, 4, 4, 3, 3, 3, 3};
        tenderTest = new int[]      {3, 3, 3, 3, 2, 2, 2, 2, 1, 1, 1, 1, 5, 5, 5, 5, 4, 4, 4, 4};
        cheerfulTest = new int[]    {4, 4, 4, 4, 3, 3, 3, 3, 2, 2, 2, 2, 1, 1, 1, 1, 5, 5, 5, 5};
        bizarreTest = new int[]     {5, 5, 5, 5, 4, 4, 4, 4, 3, 3, 3, 3, 2, 2, 2, 2, 1, 1, 1, 1};

        // Necesito que doneTests sea un dato tipo lista para poder usar el método Add
        doneTests = new List<int>();

        iterationTests = new Hashtable(); // Inicializo la hashtable de tests

        chordsSelection = new Dictionary<int, Chord>();

        // llamo así (tag "homeworkFix") a las cosas que habrá que cambiar de cara a la implementación del juego final, de momento son un parche
        homeworkFixControl = true;
        homeworkFixFramesCounter = 0;
    }
	
	void Update ()
    {
        if (homeworkFixControl)
        {
            homeworkFixFramesCounter++;
            if (homeworkFixFramesCounter > 30)
            {
                print("Se llama a la inicialización del test");
                HomeworkFix();
                homeworkFixControl = false;
            }
        }
    }

    void TestGenerator(int testNumber)
    {
        // Me creo una variable volátil, defaultTest, donde almacenaré el array de scores de los acordes de cada test, en función del tipo del testNumber y del tipo de test que toque en él
        int[] defaultTest = new int[MAX_CHORDS]; 

        // Hago un switch de cases de testNumber, que tendrá un case por cada número de test (es decir, por cada iteración)
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

            default:
                break;
        }
        
        // Los scores de cada acorde (20 en total) se cambian a los del tipo de test que haya tocado en el switch de arriba
        for (int i = 0; i < MAX_CHORDS; i++)
        {
            chords[i].score = defaultTest[i];
        }
    }

    // Esto lo cambiaré cuando decidamos cómo marcamos cuándo un test ha sido ya triggereado. P.e. después de diálogo con Monster. 
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // Además del trigger por defecto (que el collider tenga tag Player) aquí se hace la pregunta de que la key 
            // IterationsManager.iterationNumber de la hashtable iterationTests no exista, como requisito para el if
            // Si este valor de la hashtable es null quiere decir que no hay test asignado en la iteración actual
            if (iterationTests[IterationsManager.iterationInstance.iterationNumber] == null)
            {
                Debug.Log("Monster encounter!");

                bool newTest = false; // Este bool lo usaré para controlar que el test que va a dársele al player no lo ha efectuado ya antes

                while (newTest == false) // Mientras no sea un test nuevo... Yo voy a seguir intentando generar un test nuevo (para que no se repita)
                {
                    testNumber = Random.Range(1, 6); // Genero un número de test aleatorio para intentar que no se repita

                    if (doneTests.Count > 0)
                    {
                        foreach (int test in doneTests) // Este foreach me recorre el array doneTests, y por cada test (cada uno de los tests realizados)...
                        {
                            if (testNumber != test) // Si el test generado no está en el array doneTests...
                            {
                                newTest = true; // Entonces es un nuevo test!
                            }
                            else // PERO si encuentra una coincidencia
                            {
                                newTest = false; // No es un nuevo test y me devuelve newTest a false
                                break; // Y se hace un break del foreach
                            }
                        }
                    }
                    else

                    newTest = true;
                }
                // Necesito que doneTests sea un dato tipo lista para usar el método Add
                //Ahora tengo que actualizar con el testNumber nuevo la lista de tests doneTest, que el jugador ya ha hecho
                doneTests.Add(testNumber);


                // Añadimos a la hashtable IterationTests el número de test que ha salido (1-5) y luego se le asigna este valor a la key IterationsManager.iterationNumber, que es la iteración actual
                // !!!!!!! NOTA: hay que programar la lógica de que cada cambio de iteración actualice IterationsManager.iterationNumber según en la que estemos
                iterationTests.Add(IterationsManager.iterationInstance.iterationNumber, testNumber); 

                TestGenerator(testNumber);

            }
            /* else
             * Aquí habrá que meter la lógica de interacción y la validación del test...
            {
                ValidateTest((int)iterationTests[IterationsManager.iterationNumber]); // IterationsManager.iterationNumber es en realidad la iteración actual, quizá cambie el iterationNumber a currentIteration...
            }*/
        }
    }


    // Tenemos que llamar esta funcion en un script que lleve el botón OK
    public void ValidateTest()
    {
        bool areAllSelected = true;

        // Primero hay que comprobar que estén los 5 seleccionados
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



    // Aquí haré tanto el añadir chord to selection (antiguo AddChordToSelecteion()) como el remove -> TODO!!!
    public void ChangeChordToSelection(int chordID)
    {
        bool isPressed = true;

        foreach (KeyValuePair<int, Chord> chordPair in chordsSelection)
        {
            if(chordPair.Value.id.Equals(chordID))
            {
                chordsSelection.Remove(chordPair.Key);
                isPressed = false;
                Debug.Log("Se borra: " + chordID);
            }
        }

        if (isPressed)
        {
            for (int i = 0; i < MAX_SELECTED_CHORDS; i++)
            {
                if (!chordsSelection.ContainsKey(i))
                {
                    foreach (Chord chord in chords) // por cada elemento o pointer del array... (el nombre chord se lo doy yo aquí en el foreach)
                    {
                        if (chord.id.Equals(chordID)) // cuando el ID del chord actual (el que está recorriendo el foreach in that moment) es igual que el que ha elegido el player
                        {
                            chordsSelection.Add(i, chord); // i es la key (0, 1, 2, 3 o 4) y chord el value
                            Debug.Log("Se añade: " + chordID);
                            break;
                        }
                    }
                    break;
                }
            }
        }

        Debug.Log("Actualmente pulsados:");
        foreach (KeyValuePair<int, Chord> chord in chordsSelection)
        {
            Debug.Log(chord.Value.id);
        }
        Debug.Log("====================================");
    }



    public void HomeworkFix()
    {
        print("Se empieza la inicialización del test");
        if (chords != null)
        {
            print("Hay chords");
            for (int i = 0; i < MAX_CHORDS; i++)
            {
                chords.Add(new Chord());
                if (chords[i] != null)
                    chords[i].id = i + 1;
            }
        }
        else
            print("NO hay chords");


        if (IterationsManager.iterationInstance != null)
        {
            // Además del trigger por defecto (que el collider tenga tag Player) aquí se hace la pregunta de que la key IterationsManager.iterationNumber de la hashtable iterationTests no exista, como requisito para el if
            // Si este valor de la hashtable es null quiere decir que no hay test asignado en la iteración actual
            if (iterationTests[IterationsManager.iterationInstance.iterationNumber] == null)
            {
                Debug.Log("Monster encounter!");

                bool newTest = false; // Este bool lo usaré para controlar que el test que va a dársele al player no lo ha efectuado ya antes

                while (newTest == false) // Mientras no sea un test nuevo... Yo voy a seguir intentando generar un test nuevo (para que no se repita)
                {
                    testNumber = Random.Range(1, 6); // Genero un número de test aleatorio para intentar que no se repita

                    if (doneTests.Count > 0)
                    {
                        foreach (int test in doneTests) // Este foreach me recorre el array doneTests, y por cada test (cada uno de los tests realizados)...
                        {
                            if (testNumber != test) // Si el test generado no está en el array doneTests...
                            {
                                newTest = true; // Entonces es un nuevo test!
                            }
                            else // PERO si encuentra una coincidencia
                            {
                                newTest = false; // No es un nuevo test y me devuelve newTest a false
                                break; // Y se hace un break del foreach
                            }
                        }
                    }
                    else
                        newTest = true;
                }
                // Necesito que doneTests sea un dato tipo lista para usar el método Add
                //Ahora tengo que actualizar con el testNumber nuevo la lista de tests doneTest, que el jugador ya ha hecho
                doneTests.Add(testNumber);

                iterationTests.Add(IterationsManager.iterationInstance.iterationNumber, testNumber); // Añadimos a la hashtable IterationTests el número de test que ha salido (1-5) y luego se le asigna este valor a la key IterationsManager.iterationNumber, que es la iteración actual
                                                                                                     // !!!!!!! NOTA: hay que programar la lógica de que cada cambio de iteración actualice IterationsManager.iterationNumber según en la que estemos

                Debug.Log("Llamamos al test generator");
                TestGenerator(testNumber);
            }
            print("Se ha finalizado la inicialización del test");
        }
        else
            print("NO se ha finalizado la inicialización del test");
    }
}
