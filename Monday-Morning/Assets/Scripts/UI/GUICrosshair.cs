using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUICrosshair : MonoBehaviour {

	public Texture2D crosshair;
	private Vector2 position;

	// Use this for initialization
	void Start () {
		//position = new Vector2((
	}
	
	void OnGUI(){
		float xMin = (Screen.width/2) - (crosshair.width/2);
		float yMin = (Screen.height/2) - (crosshair.height/2);
		GUI.DrawTexture (new Rect (xMin, yMin, crosshair.width, crosshair.height), crosshair);
	}
}
