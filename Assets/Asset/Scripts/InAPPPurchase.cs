using UnityEngine;
using System.Collections.Generic;
using OnePF;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public class InAPPPurchase : MonoBehaviour {
	private const string SKU_EXPANSION_1 = "expansion1";
	private bool _isInitialized = false;
	private string _label = "";
	private Inventory _inventory;
	[HideInInspector] public bool m_CompradaExp1 = false;
	private string _RutaArchivo; 
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this);
		OpenIAB.mapSku (SKU_EXPANSION_1, OpenIAB_Android.STORE_GOOGLE, "expansion1");
		_isInitialized = true;
		var options = new OnePF.Options ();
		options.checkInventory = false;
		options.prefferedStoreNames = new string[] {OpenIAB_Android.STORE_GOOGLE};
		options.availableStoreNames = new string[] {OpenIAB_Android.STORE_GOOGLE};
		options.checkInventoryTimeoutMs = Options.INVENTORY_CHECK_TIMEOUT_MS * 2;
		options.discoveryTimeoutMs = Options.DISCOVER_TIMEOUT_MS;
		options.verifyMode = OptionsVerifyMode.VERIFY_ONLY_KNOWN;
		options.storeKeys.Add (OpenIAB_Android.STORE_GOOGLE, "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAsinEv5ZM1RZXPOMR9UYgXalYsFt/7cS+1unyGbqAAyboLnGLWTsxRGxQ+H6x1RjOxW3RoVU0KCDgpr5OlqI0UYwQL+oLgTLnExu0wQHUn/GL+ZPTbhfjb1/hGLkaiR8gw1LeV5joEKsUxrqXTM8sVbyZ9YV8JqMsijH55f7L4Dp0knX2h6e8c+fddjxO/pqD6ESlR3lpnf0lV3AqkwONE+6mqr8SzjExP1IuaR5RqSvKwoQ9vdwUpb/Zt0FHIHqxYcc9Ju/o38sXu+dgioBEOu4cmNC1Sx9yGVMA6s7rRHpzBXsLdLuqVOvwzWJ6aaSrsbdC8JFE7qFqb5n+mjn+iwIDAQAB");
		options.storeSearchStrategy = SearchStrategy.INSTALLER_THEN_BEST_FIT;
		OpenIAB.init (options);
	}

	void Awake(){
		OpenIABEventManager.billingSupportedEvent += billingSupportedEvent; 
		OpenIABEventManager.billingNotSupportedEvent += billingNotSupportedEvent; 
		OpenIABEventManager.queryInventorySucceededEvent+= queryInventorySucceededEvent; 
		OpenIABEventManager.queryInventoryFailedEvent += queryInventoryFailedEvent; 
		OpenIABEventManager.purchaseSucceededEvent += purchaseSucceededEvent; 
		OpenIABEventManager.purchaseFailedEvent += purchaseFailedEvent; 
		OpenIABEventManager.consumePurchaseSucceededEvent += consumePurchaseSucceededEvent; 
		OpenIABEventManager.consumePurchaseFailedEvent += consumePurchaseFailedEvent;
		_RutaArchivo = Application.persistentDataPath + "/data001.dat";
	}

	public void Expansion1(){
		OpenIAB.purchaseProduct (SKU_EXPANSION_1);
		m_CompradaExp1 = true;
		Guardar ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (_inventory != null && _inventory.HasPurchase (SKU_EXPANSION_1)) {
			OpenIAB.consumeProduct (_inventory.GetPurchase (SKU_EXPANSION_1));

		}
	}
	private void OnDisable(){
		OpenIABEventManager.billingSupportedEvent -= billingSupportedEvent; 
		OpenIABEventManager.billingNotSupportedEvent -= billingNotSupportedEvent; 
		OpenIABEventManager.queryInventorySucceededEvent-= queryInventorySucceededEvent; 
		OpenIABEventManager.queryInventoryFailedEvent -= queryInventoryFailedEvent; 
		OpenIABEventManager.purchaseSucceededEvent -= purchaseSucceededEvent; 
		OpenIABEventManager.purchaseFailedEvent -= purchaseFailedEvent; 
		OpenIABEventManager.consumePurchaseSucceededEvent -= consumePurchaseSucceededEvent; 
		OpenIABEventManager.consumePurchaseFailedEvent -= consumePurchaseFailedEvent; 
	}

	public void Cargar(){
		if (File.Exists (_RutaArchivo)) {
			BinaryFormatter _Binary = new BinaryFormatter ();
			FileStream _File = File.Open (_RutaArchivo, FileMode.Open);
			DatosAguardar _DatosBools = (DatosAguardar)_Binary.Deserialize (_File);

			m_CompradaExp1 = _DatosBools.m_CompradaExp1;
			_File.Close ();
		} else {
			Debug.Log ("NO EXISTE EL ARCHIVO");
		}
	}

	public void Guardar(){
		BinaryFormatter _Binary = new BinaryFormatter ();
		FileStream _File = File.Create (_RutaArchivo);
		DatosAguardar DatoBools = new DatosAguardar ();

		DatoBools.m_CompradaExp1 = m_CompradaExp1;
		_Binary.Serialize (_File, DatoBools);
		_File.Close ();
}

	private void billingSupportedEvent()
	{
		_isInitialized = true;
		Debug.Log("billingSupportedEvent");
	}
	private void billingNotSupportedEvent(string error)
	{
		Debug.Log("billingNotSupportedEvent: " + error);
	}
	private void queryInventorySucceededEvent(Inventory inventory)
	{
		Debug.Log("queryInventorySucceededEvent: " + inventory);
		if (inventory != null)
		{
			_label = inventory.ToString();
			_inventory = inventory;
		}
	}
	private void queryInventoryFailedEvent(string error)
	{
		Debug.Log("queryInventoryFailedEvent: " + error);
		_label = error;
	}
	private void purchaseSucceededEvent(Purchase purchase)
	{
		Debug.Log("purchaseSucceededEvent: " + purchase);
		_label = "PURCHASED:" + purchase.ToString();
	}
	private void purchaseFailedEvent(int errorCode, string errorMessage)
	{
		Debug.Log("purchaseFailedEvent: " + errorMessage);
		_label = "Purchase Failed: " + errorMessage;
	}
	private void consumePurchaseSucceededEvent(Purchase purchase)
	{
		Debug.Log("consumePurchaseSucceededEvent: " + purchase);
		_label = "CONSUMED: " + purchase.ToString();
	}
	private void consumePurchaseFailedEvent(string error)
	{
		Debug.Log("consumePurchaseFailedEvent: " + error);
		_label = "Consume Failed: " + error;
	}

	[System.Serializable]
	public class DatosAguardar{
		public bool m_CompradaExp1 = false;
		public bool m_CompradaExp2 = false;
	}
}
