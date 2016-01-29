using UnityEngine;
using System.Collections;

public class PlayerActivate : MonoBehaviour {

	// Use this for initialization
	void Start () {
		RaycastHit hit;
		Ray ray;
		Transform camera;
		int activateRange = 10;
	}
	
	// Update is called once per frame
	void Update () {
	
		// Press E to activate object, this may open doors/windows, pick up an object or carry out a task
		if(Input.GetKeyDown("e"))
		{
			var select = GameObject.FindWithTag("select").transform;
			camera = Camera.current.transform;
			ray = new Ray(camera.position, camera.forward);

			// If the raycast hits then activate the hit object
			if(Physics.Raycast(ray, hit, activateRange))
			{
				hit.collider.transform.tag = "select";
				// Activate selected object here
				Debug.log("Object activated");
			}
		}
		// De-select currently selected object
		select.tag = "none";
	}
}
