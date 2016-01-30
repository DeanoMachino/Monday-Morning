using UnityEngine;
using System.Collections;

public class PlayerActivate : MonoBehaviour {

	public GameObject player;		// Player GameObject
			
	public float activateRange;		// Range of player activation

	private Ray ray;				// Ray cast from player
	private RaycastHit hit;			// Information for object hit by ray
	private new Camera camera;		// Player camera
	private Transform select;		// Object selected by ravcast

	// Use this for initialization
	void Start ()
	{
		camera = Camera.main;
		ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)); // Ray from centre of screen
		activateRange = 2;
	}
	
	// Update is called once per frame
	void Update ()
	{	
		// Press E to activate object, this may open doors/windows, pick up an object or carry out a task
		if(Input.GetKeyDown("e"))
		{
			Debug.Log("Key pressed");
			//camera = Camera.current.transform;
			//ray = new Ray(camera.position, camera.forward);

			// If the raycast hits then activate the hit object
			if(Physics.Raycast(ray, out hit, activateRange))
			{
				// Add logic to check which object type has been hit using tags (Door, pickup or other)

				// Activate selected object here
				Debug.Log("Object activated");
			}
		}
	}
}
