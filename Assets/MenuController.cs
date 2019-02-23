using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour {


    public void Play()
    {
        SceneManager.LoadSceneAsync("Play");
    }

    public void Instructions()
    {
        SceneManager.LoadSceneAsync("Instructions");
    }

    public void Expansions()
    {
        SceneManager.LoadSceneAsync("Expansions");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
