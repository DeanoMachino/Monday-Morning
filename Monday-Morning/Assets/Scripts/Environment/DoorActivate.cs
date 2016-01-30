using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Environment/Door/Activate")]

public class DoorActivate : MonoBehaviour {

	private bool doorOpen;			// Holds if door is open or closed
	private bool doorRotating;		// Holds if door is currently opening or closing, or not
	private float rotationCounter;	// Counts how far door has opened so far
	private GameObject pivotPoint;	// Pivot point for rotating door around (Hinges)

	public float doorMaxRotation;	// Sets how far door can open in degrees
	public float rotationVelocity;	// Velocity value for opening/closing door

	// Use this for initialization
	void Start ()
	{
		doorRotating = false;	// Start door as not rotating

		doorOpen = false;		// Start door in closed postion
		rotationCounter = 0;	// Init rotation counter to 0

		// Set pivot point on left of door
		pivotPoint = new GameObject("pivot");
		pivotPoint.transform.position = this.transform.position;
		pivotPoint.transform.parent = this.transform;
		pivotPoint.transform.localPosition = new Vector3(-0.5f, 0.0f, 0.0f);
	}

	void Rotate(float rotationVelocity_)
	{
		// Check if door is open/closed and apply appropriate rotation
		if (doorOpen == false)
		{
			Debug.Log ("Door opening");

			// Rotate around pivot point to open position
			this.transform.RotateAround (pivotPoint.transform.position, Vector3.up, rotationVelocity);
			rotationCounter += rotationVelocity;
		}
		else if (doorOpen == true)
		{
			Debug.Log ("Door closing");

			// Rotate around pivot point back to closed position
			this.transform.RotateAround (pivotPoint.transform.position, Vector3.up, -rotationVelocity);
			rotationCounter += rotationVelocity;
		}

		// When door finishes opening/closing
		if (rotationCounter >= doorMaxRotation)
		{
			doorRotating = false;	// Stops door from rotating
			doorOpen = !doorOpen;	// Toggle doorOpen
			rotationCounter = 0;	// Reset counter
		}
	}

	// Called on object activation
	void Activate()
	{
		// Sets door to rotate when activated
		Debug.Log ("Door activated");
		doorRotating = true;
	}

	// Called once per frame
	void Update()
	{
		// Remove this key press when activate is finished - Stuart
		if(Input.GetKeyDown("z"))
		{
			doorRotating = true;
		}

		// Rotate door if doorRotating == true
		if(doorRotating == true)
		{
			Rotate(rotationVelocity);
		}
	}
}
