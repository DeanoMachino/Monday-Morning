using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Environment/Door/Activate")]

public class DoorActivate : MonoBehaviour
{
	private bool doorOpen;			// Holds if door is open or closed
	private bool doorRotating;		// Holds if door is currently opening or closing, or not
	private float openCounter;		// Counts how far door has opened so far
	private GameObject pivotPoint;	// Pivot point for rotating door around (Hinges)
	
	private AudioSource audio;		// Holds AudioSource
	private bool audioPlayed;		// Checks if audio has been played so it only plays once (For narration)

	public float openMax;			// Sets how far door can open in degrees
	public float openVelocity;		// Velocity value for opening/closing door


	// Use this for initialization
	void Start ()
	{
		doorRotating = false;	// Start door as not rotating

		doorOpen = false;		// Start door in closed postion
		openCounter = 0;	// Init rotation counter to 0
		
		audio = GetComponent<AudioSource>();
		audioPlayed = false;

	}

	void Rotate()
	{
		// Check if door is open/closed and apply appropriate rotation
		if (doorOpen == false)
		{
			// Rotate around pivot point to open position
			if ((this.tag == "FridgeDoor") || (this.tag == "WardrobeDoorLeft"))
			{
				this.transform.RotateAround (pivotPoint.transform.position, Vector3.up, openVelocity);
			}
			else if ((this.tag == "Door") || (this.tag == "WardrobeDoorRight"))
			{
				this.transform.RotateAround (pivotPoint.transform.position, Vector3.up, -openVelocity);
			}
			else if (this.tag == "Drawers")
			{
				this.transform.Translate(new Vector3(0.0f, openVelocity, 0.0f));
			}
		}
		else if (doorOpen == true)
		{
			// Rotate around pivot point back to closed position
			if ((this.tag == "FridgeDoor") || (this.tag == "WardrobeDoorLeft"))
			{
				this.transform.RotateAround (pivotPoint.transform.position, Vector3.up, -openVelocity);
			} 
			else if ((this.tag == "Door") || (this.tag == "WardrobeDoorRight"))
			{
				this.transform.RotateAround (pivotPoint.transform.position, Vector3.up, openVelocity);
			}
			else if (this.tag == "Drawers")
			{
				this.transform.Translate(new Vector3(0.0f, -openVelocity, 0.0f));
			}
		}

		openCounter += openVelocity;

		// When door finishes opening/closing
		if (openCounter >= openMax)
		{
			doorRotating = false;	// Stops door from rotating
			doorOpen = !doorOpen;	// Toggle doorOpen
			openCounter = 0;		// Reset counter
			Destroy(pivotPoint);	// Delete pivotPoint to free memory
		}
	}
	/*
	void Animate()
	{
		if (doorOpen == false)
		{
			// Play animation forwards to open
			anim.Play(openHash, -1, 0.0f);
		}
		else if (doorOpen == true)
		{
			// Play animation bacwards to close
			anim.Play(closeHash, -1, 0.0f);
		}

		if ((anim.GetCurrentAnimatorClipInfo(0).GetHashCode() == openedHash) || (anim.GetCurrentAnimatorClipInfo(0).GetHashCode() == closedHash))
		{
			doorRotating = false;
		}
	}
	*/
	// Called on object activation
	public void Activate()
	{
		// Set pivot point on left of door
		pivotPoint = new GameObject("pivot");
		pivotPoint.transform.position = this.transform.position;
		pivotPoint.transform.parent = this.transform;
		
		if (this.tag == "Door")
		{
			pivotPoint.transform.localPosition = new Vector3 (0.1f, 0.0f, 0.0f);
		} 
		else if (this.tag == "FridgeDoor")
		{
			pivotPoint.transform.localPosition = new Vector3 (0.98f, 0.0f, 0.0f);
		}
		else if (this.tag == "WardrobeDoorLeft")
		{
			pivotPoint.transform.localPosition = new Vector3 (0.0f, 0.0f, 0.0f);
		}
		else if (this.tag == "WardrobeDoorRight")
		{
			pivotPoint.transform.localPosition = new Vector3 (0.0f, 0.0f, 0.0f);
		}
		else
		{
			pivotPoint.transform.localPosition = new Vector3 (0.0f, 0.0f, 0.0f);
		}

		// Sets door to rotate when activated if it's not already rotating
		if(doorRotating == false)
		{
			Debug.Log ("Door activated");
			doorRotating = true;

			if(!audioPlayed)
			{
				audio.Play();
				audioPlayed = true;
			}
		}
	}

	// Update is called once per frame
	void Update()
	{
		// Rotate door if doorRotating == true
		if(doorRotating == true)
		{
			Rotate();
			//Animate();
		}
	}

	/*void LateUpdate()
	{
		this.transform.position = localPos;
		//transform.localPosition += localPos;
	}*/
}
