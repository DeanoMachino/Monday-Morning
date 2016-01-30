using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("GUI/Crosshair")]

public class GUICrosshair : MonoBehaviour {

	public Texture2D crosshair;
	private Vector2 position = new Vector2(0, 0);

	void OnGUI(){
		position.x = (Screen.width/2) - (crosshair.width/2);
		position.y = (Screen.height/2) - (crosshair.height/2);
		GUI.DrawTexture (new Rect (position.x, position.y, crosshair.width, crosshair.height), crosshair);
	}
}
