using UnityEngine;
using System.Collections;

[AddComponentMenu("Player/Action/Player Activate")]

public class PlayerActivate : MonoBehaviour {
	
	public float range;
	private string label;
	private bool visible = false;

	void Update(){
		RaycastHit hit;
		Ray activationRay = new Ray (transform.position, transform.forward);
		visible = false;
		// Check if object has been hit
		if (Physics.Raycast (activationRay, out hit, range)) {
			visible = true;
			label = "";
			switch(hit.collider.tag){
				case "Cube":
					label = "Cube";
					// Show text tooltip
					break;
				case "CerealBox":
					label = "CerealBox";
					// Show text tooltip
					break;
				case "Bowl":
					label = "Bowl";
					// Show text tooltip
					break;
				default:
					Debug.Log ("Tag hit: " + hit.collider.tag);
					break;
			}
			if (Input.GetButtonDown ("Action")) {
				Debug.Log ("Object Hit");
				ActivateObject(hit);
			}

			// TEMP -- Testing removing objects from the inventory
			if (Input.GetKeyDown ("q")) {
				InventoryManager.Instance.SendMessage("ItemDropped", ItemType.CEREAL_BOX, SendMessageOptions.DontRequireReceiver);
			}

		}
	}
	/* 

Can be collected for inventory:
- Cereal Box
- Bowl
- Milk
- Spoon
- Spanner
- Towel
- Bundle of clothes
- Work outfit (e.g. suit)
- Keys

	 */
	

	void ActivateObject(RaycastHit hit){
		switch (hit.collider.tag) {
		case "CerealBox":
			Debug.Log ("ActivateObject -- CerealBox");
			InventoryManager.Instance.SendMessage("ItemPickedUp", ItemType.CEREAL_BOX, SendMessageOptions.DontRequireReceiver);
			break;
		case "Bowl":
			InventoryManager.Instance.SendMessage("ItemPickedUp", ItemType.BOWL, SendMessageOptions.DontRequireReceiver);
			break;
		case "Milk":
			InventoryManager.Instance.SendMessage("ItemPickedUp", ItemType.MILK, SendMessageOptions.DontRequireReceiver);
			break;
		case "Spoon":
			InventoryManager.Instance.SendMessage("ItemPickedUp", ItemType.SPOON, SendMessageOptions.DontRequireReceiver);
			break;
		case "Spanner":
			InventoryManager.Instance.SendMessage("ItemPickedUp", ItemType.SPANNER, SendMessageOptions.DontRequireReceiver);
			break;
		case "Towel":
			InventoryManager.Instance.SendMessage("ItemPickedUp", ItemType.TOWEL, SendMessageOptions.DontRequireReceiver);
			break;
		case "BundleOfClothes":
			InventoryManager.Instance.SendMessage("ItemPickedUp", ItemType.BUNDLE_OF_CLOTHES, SendMessageOptions.DontRequireReceiver);
			break;
		case "WorkOutfit":
			InventoryManager.Instance.SendMessage("ItemPickedUp", ItemType.WORK_OUTFIT, SendMessageOptions.DontRequireReceiver);
			break;
		case "Key":
			InventoryManager.Instance.SendMessage("ItemPickedUp", ItemType.KEY, SendMessageOptions.DontRequireReceiver);
			break;
		}
	}

	void OnGUI(){
		if (visible && label != "") {
			GUI.Box(new Rect(Screen.width / 3, Screen.height * 0.6f, Screen.width / 3, Screen.height * 0.05f), label);
		}
	}
}
