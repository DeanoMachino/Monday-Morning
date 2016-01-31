using UnityEngine;
using System.Collections;

[AddComponentMenu("Player/Inventory/Inventory Icon")]

public class InventoryIcon : MonoBehaviour {
	public Texture2D icon;
	public Vector2 position = new Vector2(0, 0);
	public Vector2 scale = new Vector2(1, 1);
	public float xOffset = 0;
	public bool held = false;
		
	void OnGUI(){
		if (true) {
			GUI.DrawTexture (new Rect (Screen.width * 0.665f, Screen.height * 0.9275f, icon.width * scale.x, icon.height * scale.y), icon);
		}
	}
}
