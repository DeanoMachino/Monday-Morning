using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

	private float minutes, seconds;	// Minutes and seconds
									// (Real time minutes used to display hours and seconds to display minutes)

	private float totalTimePassed;	// Total time passed in seconds since level
	private string timeString;		// Time string for display

	public float levelTime;			// Time to complete level in seconds

	// Use this for initialization
	void Start ()
	{
		minutes = 0;
		seconds = 0;
		totalTimePassed = 0;
	}

	// Handles display time for GUI
	void DisplayTime()
	{
		Debug.Log (timeString);
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Add delta time to time passed every frame
		totalTimePassed += Time.deltaTime;

		// Minutes and seconds passed for display
		minutes = Mathf.Floor (totalTimePassed / 60);
		seconds = totalTimePassed % 60;		
		
		timeString = string.Format("{0:00}:{1:00} AM", minutes + 7, seconds);

		DisplayTime();

		// Reset totalTimePassed to 0 once allocated levelTime has elapsed
		if (totalTimePassed >= levelTime)
		{
			totalTimePassed = 0;
			// Reset level here - Stuart
		}
		
	}
}
