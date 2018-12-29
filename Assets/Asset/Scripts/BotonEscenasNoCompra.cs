using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BotonEscenasNoCompra : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Main(){
		SceneManager.LoadScene (3);
	}
}
