using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Transitions trans;
    public Image blackscreen;
    public int index = 0;
    public bool loading = false;
    // Use this for initialization
    void Start()
    {
        index = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            NextScene();
        }
        if (loading == true)
        {
            trans.FadeInOut(blackscreen, 2);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            if (index == 0) index = 4;
            else
            {
                index--;
            }
            LoadLast(index);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload(index);
        }
    }

    public void LoadNext(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void LoadLast(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void Reload(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void NextScene()
    {
        StartCoroutine(WaitForTime());
 

    }

    IEnumerator WaitForTime()
    {
        loading = true;
        yield return new WaitForSeconds(2);
        loading = false;
        if (index + 1 >= SceneManager.sceneCountInBuildSettings) index = 0;
        SceneManager.LoadScene(index + 1);



    }
}