using UnityEngine;
using UnityEngine.SocialPlatforms;
using System.Collections;
using UnityEngine.UI;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
public class Main : MonoBehaviour {

    public GameObject[] m_Card;
	public int m_NumberOne;
	public int m_NumberTwo;
	public int m_NumberThree;
    public GameObject m_CartaBack1;
    public GameObject m_CartaBack2;
    public Button m_GodluckButton;
	public Transform m_Spawn1;
	public Transform m_Spawn2;
	private float m_count = 7f;
	public Text m_TextNumber1;
	public Text m_TextNumber2;
	public Text m_TextNumber3;
	private float m_Number1;
	private float m_Number2;
	private float m_Number3;
	public ParticleSystem m_Particulas;
	private InAPPPurchase m_ComprasIntegradas;
	private DateTime _CurretnTime;
	private DateTime _OldTime;
	private bool _pressed;
	[HideInInspector] public GameObject _One;
	[HideInInspector]public GameObject _Two;

	void Awake(){
		m_ComprasIntegradas = GameObject.FindGameObjectWithTag("Compras").GetComponent<InAPPPurchase> ();
	}

	// Use this for initialization
	void Start () {
		m_ComprasIntegradas.Cargar ();
		if (PlayerPrefs.HasKey ("timer")) {
			CalculateDifferenceOfTime ();
		}
		Cargar ();
		m_CartaBack1.GetComponent<SpriteRenderer> ().sprite = _One.GetComponent<SpriteRenderer> ().sprite;
		m_CartaBack2.GetComponent<SpriteRenderer> ().sprite = _Two.GetComponent<SpriteRenderer> ().sprite;
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

	public void RandomCards() {
		_pressed = true;
		m_CartaBack1.gameObject.SetActive (false);
		m_CartaBack2.gameObject.SetActive (false);
		 _One = Instantiate(m_Card[UnityEngine.Random.Range(0, m_Card.Length)], m_Spawn1.transform.position, m_Spawn1.transform.rotation) as GameObject;
		_Two = Instantiate(m_Card[UnityEngine.Random.Range(0, m_Card.Length)], m_Spawn2.transform.position, m_Spawn2.transform.rotation) as GameObject;
		Guardar ();
		PlayerPrefs.SetString ("timer", DateTime.Now.ToBinary ().ToString ());
		PlayerPrefs.Save ();

		if (_One.ToString () == _Two.ToString ()) {
			Destroy (_One);
			Destroy (_Two);
		} else {
			m_GodluckButton.enabled = false;

			Debug.Log(m_count.ToString());
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

		if (_Diference > TimeSpan.FromDays(4)) {
			m_GodluckButton.enabled = true;
			_pressed = false;

		} else {
			m_GodluckButton.enabled = false;
			Notifications ();
		}
	}

	public void Cargar(){
		BinaryFormatter _Binary = new BinaryFormatter ();
		FileStream _File = File.Open (Application.persistentDataPath + "/data002.dat", FileMode.Open);
		DatosGameObject _Datos = (DatosGameObject)_Binary.Deserialize (_File);

		_One = _Datos._One;
		_Two = _Datos._Two;
		_File.Close ();
	}

	public void Guardar(){
		BinaryFormatter _Binary = new BinaryFormatter ();
		FileStream _File = File.Create (Application.persistentDataPath + "/data002.dat");
		DatosGameObject _Datos = new DatosGameObject ();

		_Datos._One = _One;
		_Datos._Two = _Two;
		_Binary.Serialize (_File, _Datos);
		_File.Close ();
	}

	[System.Serializable]
	public class DatosGameObject{
		public GameObject _One;
		public GameObject _Two;
	}
}
