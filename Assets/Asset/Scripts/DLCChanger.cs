using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class DLCChanger : MonoBehaviour {

	public InAPPPurchase m_Compras;
	public Main m_CartasSinDLC;
	public MainExp1 m_CartasConDLC1;
	public Button m_SuerteBoton;

	// Use this for initialization
	void Start () {
#if UNITY_WP8 || UNITY_IOS || UNITY_ANDROID
        m_Compras.Cargar ();
		if (m_Compras.m_CompradaExp1 == true) {
			m_CartasConDLC1.enabled = true;
			m_CartasSinDLC.enabled = false;
			m_SuerteBoton.onClick.AddListener (() => m_CartasConDLC1.RandomCards ());
			m_SuerteBoton.onClick.RemoveListener (() => m_CartasSinDLC.RandomCards ());
		}

		if(m_Compras.m_CompradaExp1 == false){
			m_CartasSinDLC.enabled = true;
			m_CartasConDLC1.enabled = false;
			m_SuerteBoton.onClick.AddListener (() => m_CartasSinDLC.RandomCards ());
			m_SuerteBoton.onClick.RemoveListener (() => m_CartasConDLC1.RandomCards ());
		}
#endif
	}
	
	// Update is called once per frame
	void Update () {
	}
}
