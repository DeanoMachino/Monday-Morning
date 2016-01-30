using UnityEngine;
using System.Collections;

public class ItemPickup : MonoBehaviour {



	// Use this for initialization
	void Start ()
	{
	
	}

	void RemoveFromWorld()
	{
		// Kills the game object
		Destroy (gameObject);
		
		// Removes this script instance from the game object
		Destroy (this);
	}

	// Called on object activation
	void Activate()
	{
		// Sets door to rotate when activated
		Debug.Log ("Item activated");
		RemoveFromWorld();
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Possibly add animation code here - Stuart

		// Remove this when activation code is finished - Stuart
		if(Input.GetKeyDown("x"))
		{
			RemoveFromWorld();
		}
	}
}
