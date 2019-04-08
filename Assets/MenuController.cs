using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour {


	FadeEffect fadeEffect;

	void Start()
	{
		fadeEffect = (FadeEffect)FindObjectOfType(typeof(FadeEffect));
	}

    public void Play()
    {
		fadeEffect.LoadScene("Play");
		//fadeEffect.Show(SceneManager.LoadSceneAsync("Play"));
	}

    public void Instructions()
    {
		fadeEffect.LoadScene("Instructions");

		//fadeEffect.Show(SceneManager.LoadSceneAsync("Instructions"));
	}

	public void Expansions()
    {
		fadeEffect.LoadScene("Expansions");
		//fadeEffect.Show(SceneManager.LoadSceneAsync("Expansions"));
    }

    public void Exit()
    {
        Application.Quit();
    }
}
