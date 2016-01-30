using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("GUI/Watermark")]

public class GUIWatermark : MonoBehaviour {
	
	public Texture2D watermark;
	public Vector2 position = new Vector2(0, 0);
	public Vector2 scale = new Vector2(0.2f, 0.2f);
	
	// Use this for initialization
	void Start () {
		//position = new Vector2((

	}
	
	void OnGUI(){
		position.x = 0;
		position.y = Screen.height - watermark.height * scale.y;
		GUI.DrawTexture (new Rect (position.x, position.y, watermark.width * scale.x, watermark.height * scale.y), watermark);
	}
}