using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

	//public SceneFadeInOut screenFader;

	private float minutes, seconds;	// Minutes and seconds
									// (Real time minutes used to display hours and seconds to display minutes)

	private float totalTimePassed;	// Total time passed in seconds since level
	public float levelTime;			// Time to complete level in seconds

	private string timeString;		// Time string for display

	// Use this for initialization
	void Start ()
	{
		//screenFader = GameObject.FindObjectOfType(typeof(SceneFadeInOut)) as SceneFadeInOut;

		minutes = 0;
		seconds = 0;
		totalTimePassed = 0;
	}

	// Handles display time for GUI
	void DisplayTime()
	{
		// Remove this and display as GUI
		Debug.Log (timeString);
	}

	// Handles resetting the level
	void ResetLevel()
	{
		// Call screen fader here and move player back to start
		//screenFader.EndScene();
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Add delta time to time passed every frame
		totalTimePassed += Time.deltaTime;

		// Minutes and seconds passed for display
		minutes = Mathf.Floor (totalTimePassed / 60);
		seconds = Mathf.Floor(totalTimePassed % 60);	
		
		timeString = string.Format("{0:00}:{1:00} AM", minutes + 7, seconds);

		//DisplayTime();

		// Reset totalTimePassed to 0 once allocated levelTime has elapsed
		if (totalTimePassed >= levelTime)
		{
			totalTimePassed = 0;

			// Reset level here - Stuart
			// ResetLevel();
		}
		
	}

	void OnGUI()
	{
		GUI.Box(new Rect(0.0f, 0.0f, 75.0f, 25.0f), timeString);
	}
}
