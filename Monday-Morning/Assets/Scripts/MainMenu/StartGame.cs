using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {
	
	public void EndTheScene(){
		Debug.Log ("Start clicked");
		//fader.EndScene (1);
		Application.LoadLevel ("Level01");
	}
}
