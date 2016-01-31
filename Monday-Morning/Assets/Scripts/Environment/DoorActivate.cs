using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Environment/Door/Activate")]

public class DoorActivate : MonoBehaviour {

	private bool doorOpen;										// Holds if door is open or closed
	private bool doorRotating;									// Holds if door is currently opening or closing, or not
	private float rotationCounter;								// Counts how far door has opened so far
	private GameObject pivotPoint;								// Pivot point for rotating door around (Hinges)
	
	private AudioSource audio;									// Holds AudioSource
	private bool audioPlayed;									// Checks if audio has been played so it only plays once (For narration)

	public float doorMaxRotation;								// Sets how far door can open in degrees
	public float rotationVelocity;								// Velocity value for opening/closing door

	private Animator anim;										// Holds the animator component

	private int openedHash, closedHash, openHash, closeHash;	// Hash maps for animation clips

	Vector3 localPos;											// Fix for animated objects assets moving to parent position
	bool wasPlaying;
	
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
		pivotPoint.transform.localPosition = new Vector3(0.1f, 0.0f, 0.0f);

		audio = GetComponent<AudioSource>();
		audioPlayed = false;

		//anim = GetComponent<Animator>();

		//openedHash = Animator.StringToHash ("Opened");
		//closedHash = Animator.StringToHash ("Closed");
		//openHash = Animator.StringToHash ("Open");
		//closeHash = Animator.StringToHash ("Close");

		//localPos = this.transform.position;
		//anim.Play(closedHash, -1, 0.0f);
	}

	void Rotate()
	{
		// Check if door is open/closed and apply appropriate rotation
		if (doorOpen == false)
		{
			// Rotate around pivot point to open position
			this.transform.RotateAround (pivotPoint.transform.position, Vector3.up, -rotationVelocity);
			rotationCounter += rotationVelocity;
		}
		else if (doorOpen == true)
		{
			// Rotate around pivot point back to closed position
			this.transform.RotateAround (pivotPoint.transform.position, Vector3.up, rotationVelocity);
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
