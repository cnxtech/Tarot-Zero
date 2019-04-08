using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingController : MonoBehaviour {

	void Awake()
	{
	}
	Canvas canvas;
	FadeEffect fadeEffect;
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
		canvas = GetComponent<Canvas>();
		canvas.enabled = false;
		fadeEffect = (FadeEffect) FindObjectOfType(typeof(FadeEffect));
	}

	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// Seconds
	/// </summary>
	public int MinHideDelay = 1;


	public void Show(AsyncOperation operation)
	{
		canvas.enabled = true;

		if (operation.isDone)
		{
			Debug.Log("LoadedDone");
			StartCoroutine(HideAfter(MinHideDelay));
		}
		else
		{
			StartCoroutine(HideAfter(MinHideDelay));

			operation.completed += (x) =>
			{
				Debug.Log("Loaded");
				StartCoroutine(HideAfter(0));
			};
		}

	}

	IEnumerator HideAfter(int seconds)
	{
		yield return new WaitForSeconds(seconds);
		canvas.enabled = false;
	}


}
