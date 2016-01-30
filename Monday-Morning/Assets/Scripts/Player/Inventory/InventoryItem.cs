using UnityEngine;
using System.Collections;

[AddComponentMenu("Player/Inventory/Inventory Item")]

public class InventoryItem : MonoBehaviour{

	public Texture2D icon;
	public string label;
	public ItemType type;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Setup(Texture2D icon_, string label_, ItemType type_){
		icon = icon_;
		label = label_;
		type = type_;
	}

	void OnGUI(){
		GUI.DrawTexture (new Rect (0f, 0f, 30f, 30f), icon);
	}
}
