using UnityEngine;
using System.Collections;

public class Aspect : MonoBehaviour {

	// Use this for initialization
	void Start () {

		GetComponent<Camera> ().aspect = 16f / 10f;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
