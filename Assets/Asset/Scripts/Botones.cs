using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Botones : MonoBehaviour {
	private InAPPPurchase _InApp;
	// Use this for initialization
	void Start () {
		_InApp = GameObject.FindGameObjectWithTag ("Compras").GetComponent<InAPPPurchase> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Back(){
		_InApp.Guardar ();
		SceneManager.LoadScene (3);
	}
}
