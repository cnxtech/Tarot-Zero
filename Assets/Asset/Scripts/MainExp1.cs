using UnityEngine;
using UnityEngine.SocialPlatforms;
using System.Collections;
using UnityEngine.UI;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
public class MainExp1 : MonoBehaviour {

	public int m_NumberOne;
	public int m_NumberTwo;
	public int m_NumberThree;
	public GameObject m_CartaBack1;
	public GameObject m_CartaBack2;
	public Button m_GodluckButton;
	public Transform m_Spawn1;
	public Transform m_Spawn2;
	private float m_count = 7f;
	private DateTime _CurretnTime;
	private DateTime _OldTime;
	public Text m_TextNumber1;
	public Text m_TextNumber2;
	public Text m_TextNumber3;
	private float m_Number1;
	private float m_Number2;
	private float m_Number3;
	public ParticleSystem m_Particulas;
	private InAPPPurchase m_ComprasIntegradas;
	private TimeSpan _remain;
	private bool _pressed;
	public GameObject[] m_Card;
	private GameObject _One;
	private GameObject _Two;

	void Awake(){
		m_ComprasIntegradas = GameObject.FindGameObjectWithTag("Compras").GetComponent<InAPPPurchase> ();
	}

	// Use this for initialization
	void Start () {
		m_ComprasIntegradas.Cargar ();
		if (PlayerPrefs.HasKey ("timer")) {
			CalculateDifferenceOfTime ();
		}
	}

	// Update is called once per frame
	void Update () {
		if (_pressed) {
			m_count -= Time.deltaTime;
			m_TextNumber1.text = UnityEngine.Random.Range (0, m_NumberOne).ToString();
			m_TextNumber2.text = UnityEngine.Random.Range (0, m_NumberTwo).ToString();
			m_TextNumber3.text = UnityEngine.Random.Range (0, m_NumberThree).ToString();
		}



		if (PlayerPrefs.HasKey ("timer")) {
			CalculateDifferenceOfTime ();
		}

		if (m_count <= 0) {
			m_TextNumber1.text = m_Number1.ToString();
			m_TextNumber2.text = m_Number2.ToString();
			m_TextNumber3.text = m_Number3.ToString();
			m_Particulas.gameObject.SetActive(true);
		}
	}

	public void Notifications(){
		LocalNotification.SendNotification (0, TimeSpan.FromDays (4), "Tarot Zero", "Las cartas te esperan");
	}

	public void Check(){
		SceneManager.LoadScene (7);
	}

	public void RandomCards() {
		_pressed = true;
		m_CartaBack1.gameObject.SetActive (false);
		m_CartaBack2.gameObject.SetActive (false);
		_One = Instantiate(m_Card[UnityEngine.Random.Range(0, m_Card.Length)], m_Spawn1.transform.position, m_Spawn1.transform.rotation) as GameObject;
		PlayerPrefs.SetString ("_One", _One.ToString ());
		PlayerPrefs.Save ();
		_Two = Instantiate(m_Card[UnityEngine.Random.Range(0, m_Card.Length)], m_Spawn2.transform.position, m_Spawn2.transform.rotation) as GameObject;
		PlayerPrefs.SetString ("_Two", _Two.ToString ());
		PlayerPrefs.Save ();
		PlayerPrefs.SetString ("timer", DateTime.Now.ToBinary ().ToString ());
		PlayerPrefs.Save ();

		m_Card[0].name.Replace("(Clone)", "").Trim();
		m_Card[1].name.Replace("(Clone)", "").Trim();
		m_Card[2].name.Replace("(Clone)", "").Trim();
		m_Card[3].name.Replace("(Clone)", "").Trim();
		m_Card[4].name.Replace("(Clone)", "").Trim();
		m_Card[5].name.Replace("(Clone)", "").Trim();
		m_Card[6].name.Replace("(Clone)", "").Trim();
		m_Card[7].name.Replace("(Clone)", "").Trim();
		m_Card[8].name.Replace("(Clone)", "").Trim();
		m_Card[9].name.Replace("(Clone)", "").Trim();
		m_Card[10].name.Replace("(Clone)", "").Trim();
		m_Card[11].name.Replace("(Clone)", "").Trim();
		m_Card[12].name.Replace("(Clone)", "").Trim();
		m_Card[13].name.Replace("(Clone)", "").Trim();
		m_Card[14].name.Replace("(Clone)", "").Trim();
		m_Card[15].name.Replace("(Clone)", "").Trim();
		m_Card[16].name.Replace("(Clone)", "").Trim();
		m_Card[17].name.Replace("(Clone)", "").Trim();
		m_Card[18].name.Replace("(Clone)", "").Trim();
		m_Card[19].name.Replace("(Clone)", "").Trim();
		m_Card[20].name.Replace("(Clone)", "").Trim();

		if (_One.ToString () == _Two.ToString ()) {
			Destroy (_One);
			Destroy (_Two);
		} else {
			m_GodluckButton.enabled = false;
			m_Number1 = UnityEngine.Random.Range (0, m_NumberOne);
			m_Number2 = UnityEngine.Random.Range (0, m_NumberTwo);
			m_Number3 = UnityEngine.Random.Range (0, m_NumberThree);
		}	
	}

	public void EnableButton(){
		m_GodluckButton.enabled = true;
	}

	public void Instrucciones(){
		SceneManager.LoadScene (5);
	}

	public void Expansiones(){
		SceneManager.LoadScene (4);
	}

	public void Actualizaciones(){
		SceneManager.LoadScene (6);
	}

	public void CalculateDifferenceOfTime(){
		_CurretnTime = DateTime.Now;
		long temp = Convert.ToInt64 (PlayerPrefs.GetString ("timer"));
		_OldTime = DateTime.FromBinary (temp);
		TimeSpan _Diference = _CurretnTime - _OldTime;

		if (_Diference > TimeSpan.FromDays(0)) {
			m_GodluckButton.enabled = true;
			_pressed = false;

		} else {
			m_GodluckButton.enabled = false;
			Notifications ();
		}
	}
}