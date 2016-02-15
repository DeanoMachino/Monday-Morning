using UnityEngine;
using System.Collections;

public class SetInitial : MonoBehaviour {
	
	public Vector3 startPosition;
	public Quaternion startRotation;
	public bool visible = true;
	private bool lastVisible;

	public void SetPosition(){
		this.transform.position = startPosition;
	}

	public void SetRotation(){
		this.transform.rotation = startRotation;
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.B)){
			SetPosition();
		}
			
		//if (visible != lastVisible) {
		gameObject.SetActive(visible);
		//}

		//lastVisible = visible;
	}
}
