using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Bought : MonoBehaviour {
	public Text m_text;
	public Button m_BotonCartas;
	private InAPPPurchase m_Compras;


	// Use this for initialization
	void Start () {
		m_Compras = GameObject.FindGameObjectWithTag ("Compras").GetComponent<InAPPPurchase> ();
		m_Compras.Cargar ();
		if (m_Compras.m_CompradaExp1 == true) {
			m_text.text = "Comprado";
			m_text.fontSize = 38;
			m_BotonCartas.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
}
