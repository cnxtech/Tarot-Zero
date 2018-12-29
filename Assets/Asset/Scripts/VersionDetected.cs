using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class VersionDetected : MonoBehaviour {
	public Text m_Text1;
	// Use this for initialization
	void Start () {
		m_Text1.text = Application.version.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
