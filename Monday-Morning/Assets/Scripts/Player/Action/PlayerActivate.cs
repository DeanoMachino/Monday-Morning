using UnityEngine;
using System.Collections;

[AddComponentMenu("Player/Action/Player Activate")]

public class PlayerActivate : MonoBehaviour {
	
	public float range;

	void Update(){
		RaycastHit hit;
		Ray activationRay = new Ray (transform.position, transform.forward);

		if (Input.GetButtonDown ("Action")) {

			// Check if object has been hit
			if (Physics.Raycast (activationRay, out hit, range)) {
				Debug.Log ("Object Hit");
			}
		}
	}
}
