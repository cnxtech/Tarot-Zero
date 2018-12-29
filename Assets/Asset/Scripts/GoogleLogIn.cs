using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;

public class GoogleLogIn : MonoBehaviour {

	void Start(){
		if (!Social.localUser.authenticated) {
			Social.localUser.Authenticate ((bool success) => {
				if (success) {
					Debug.Log (success.ToString () + "OK");
				}
			});
		}

	}
}
