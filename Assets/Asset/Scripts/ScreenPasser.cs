using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class ScreenPasser : MonoBehaviour {
	private float m_count = 4f;
	public AudioSource m_BackgroundMusic;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		DontDestroyOnLoad (m_BackgroundMusic);
		m_count -= Time.deltaTime;
		if (m_count <= 0) {
			SceneManager.LoadScene(1);
		}
	}
}
