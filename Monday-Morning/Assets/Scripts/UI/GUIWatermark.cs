using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUIWatermark : MonoBehaviour {
	
	public Texture2D watermark;
	public Vector2 position = new Vector2(0, 0);
	public Vector2 size = new Vector2(0, 0);
	
	// Use this for initialization
	void Start () {
		//position = new Vector2((

	}
	
	void OnGUI(){
		position.x = 0;
		position.y = Screen.height - watermark.height;

		//size.x = watermark.width / 4;
		//size.y = watermark.height / 4;
		watermark.Resize((int)size.x, (int)size.y);

		GUI.DrawTexture (new Rect (position.x, position.y, watermark.width, watermark.height), watermark);
	}
}