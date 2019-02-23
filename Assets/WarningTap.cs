using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;


public class WarningTap : MonoBehaviour {

	public void SkipWarning()
    {
        SceneManager.LoadSceneAsync("Menu");
    }
}
