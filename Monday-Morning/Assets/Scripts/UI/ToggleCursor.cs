﻿using UnityEngine;
using System.Collections;

public class ToggleCursor : MonoBehaviour {

	public bool activated = false;

	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("ToggleCursor")){
			activated = !activated;
			Cursor.visible = activated;
		}
	}
}
