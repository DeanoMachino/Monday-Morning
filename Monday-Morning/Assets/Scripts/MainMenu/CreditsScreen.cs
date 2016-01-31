using UnityEngine;
using System.Collections;

public class CreditsScreen : MonoBehaviour {

	public void EndTheScene(){
		Debug.Log ("Credits clicked");
		//fader.EndScene (1);
		Application.LoadLevel ("Credits");
	}
}
