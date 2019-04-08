using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;


public class FadeEffect : MonoBehaviour {


	public string NextSceneName = "Menu";

	public void Awake()
	{
		DontDestroyOnLoad(transform.parent.gameObject);
	}

	public void FadeIn()
	{
		animator.SetBool("loaded", true);
		animator.SetBool("loadNextScene", false);
	}

	public void FadeOut()
	{
		animator.SetBool("loaded", false);
		animator.SetBool("loadNextScene", true);
	}


	public void ShowLoading()
	{
		loading.SetActive(true);
	}

	public void HideLoading()
	{
		loading.SetActive(false);
	}

	/// <summary>
	/// Raw scene loading, this loads without fading out
	/// </summary>
	public void LoadNextScene()
	{
		SceneManager.LoadSceneAsync(NextSceneName).completed += (x) => { FadeIn(); };
	}

	/// <summary>
	/// This is not the same as <see cref="LoadNextScene"/>. Performs the procedure of loading a scene of this name (Includes fading out effect)
	/// </summary>
	/// <param name="name"></param>
	public void LoadScene(string name)
	{
		NextSceneName = name;
		FadeOut();
	}


	Animator animator;
	GameObject loading;
	void Start()
	{
		loading = GameObject.Find("LoadingCanvas");
		HideLoading();
		DontDestroyOnLoad(loading);
		animator = GetComponent<Animator>();
	}
}
