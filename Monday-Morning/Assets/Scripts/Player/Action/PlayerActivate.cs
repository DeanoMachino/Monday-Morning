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

			SetTooltipLabel(hit);

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

	void SetTooltipLabel(RaycastHit hit){
		switch(hit.collider.tag){
			// INVENTORY ITEMS //
			// =============== //
		case "CerealBox":
			label = "Take Box of Cereal";
			break;
		case "Bowl":
			label = "Take Bowl";
			break;
		case "Milk":
			label = "Take Milk";
			break;
		case "Spoon":
			label = "Take Spoon";
			break;
		case "Spanner":
			label = "Take Spanner";
			break;
		case "TowelDirty":
			label = "Take Dirty Towel";
			break;
		case "TowelClean":
			label = "Take Clean Towel";
			break;
		case "BundleOfClothes":
			label = "Take Bundle of Clothes";
			break;
		case "Key":
			label = "Take Key";
			break;

			// INTERACTIVE ITEMS //
			// ================= //
		case "KitchenSink":
			// IF DISHES NOT DONE
				// SET LABEL TO "Wash dirty dishes"
			break;
		case "DryingRack":
			// IF DISHES TO BE PUT AWAY
				// SET LABEL TO "Put away dishes"
			break;
		case "KitchenTable":
			// IF HOLDING BOWL
				// SET LABEL TO "Place bowl"
			// ELSE IF HOLDING SPOON
				// SET LABEL TO "Place spoon"
			// ELSE IF BOWL AND SPOON PLACED
			break;
		case "WashingMachine":
			// IF HOLDING BUNDLE OF CLOTHES
				// SET LABEL TO "Wash dirty clothes"
			// ELSE IF HOLDING DIRTY TOWEL
				// SET LABEL TO "Wash dirty towel"
			break;
		case "WorkOutfit":
			// IF NOT HAD SHOWER YET
				// SET LABEL TO "You need to take a shower first
			// IF HAD SHOWER
				// SET LABEL TO "Get dressed for work"
			break;
		case "ShowerUnit":
			// IF SHOWER IS BROKEN AND NOT HOLDING SPANNER
				// SET LABEL TO "The shower is broken"
			// IF SHOWER IS BROKEN AND HOLDING SPANNER
				// SET LABEL TO "Fix the shower"
			// IF SHOWER IS FIXED
				// SET LABEL TO "Take a shower

			break;
			// DEFAULT ITEMS //
			// ============= //
		case "Untagged":
			label = "UNTAGGED ITEM";
			break;
		default:
			Debug.Log ("Tag hit: " + hit.collider.tag);
			break;
		}
	}
	

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
			GUI.Box(new Rect(Screen.width / 3, Screen.height * 0.6f, Screen.width / 3, Screen.height * 0.04f), label);
		}
	}
}
