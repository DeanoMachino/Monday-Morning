using UnityEngine;
using System.Collections;

[AddComponentMenu("Player/Action/Player Activate")]

public class PlayerActivate : MonoBehaviour {
	
	public float range;
	private string label;
	private bool visible = false;

	void Update(){
		RaycastHit hit;
		Ray activationRay = new Ray (transform.position, transform.forward);
		visible = false;
		// Check if object has been hit
		if (Physics.Raycast (activationRay, out hit, range)) {
			visible = true;
			label = "";
			switch(hit.collider.tag){
				case "Cube":
					label = "Cube";
					// Show text tooltip
					break;
				default:
					Debug.Log ("Tag hit: " + hit.collider.tag);
					break;
			}
			if (Input.GetButtonDown ("Action")) {
				Debug.Log ("Object Hit");
				ActivateObject(hit);
			}

		}
	}

	void ActivateObject(RaycastHit hit){

	}

	void OnGUI(){
		if (visible && label != "") {
			GUI.Box(new Rect(Screen.width / 3, Screen.height * 0.6f, Screen.width / 3, Screen.height * 0.05f), label);
		}
	}
}
