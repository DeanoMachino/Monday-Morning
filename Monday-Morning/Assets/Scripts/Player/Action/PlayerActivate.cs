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
				if(!GameManager.Instance.gameObjectives.doneDishes){
					// SET LABEL TO "Wash dirty dishes"
					label = "Wash dirty dishes";
				}
				break;
			case "DryingRack":
				// IF DISHES DONE AND DISHES TO BE PUT AWAY
				if(GameManager.Instance.gameObjectives.doneDishes){
					if(!GameManager.Instance.gameObjectives.driedDishes){
						// SET LABEL TO "Put away dishes"
						label = "Put away dishes";
					}
				}
				break;
			case "KitchenTable":
				// IF HOLDING BOWL
				if(InventoryManager.Instance.HoldingItem(ItemType.BOWL)){
					// SET LABEL TO "Place bowl"
					label = "Place bowl";
				}else if(InventoryManager.Instance.HoldingItem(ItemType.SPOON)){	// ELSE IF HOLDING SPOON
					// SET LABEL TO "Place spoon"
					label = "Place spoon";
				}else if((GameManager.Instance.gameObjectives.placedBowl && GameManager.Instance.gameObjectives.placedSpoon) &&
				         !(InventoryManager.Instance.HoldingItem(ItemType.MILK) && InventoryManager.Instance.HoldingItem(ItemType.CEREAL_BOX))){	// ELSE IF ( BOWL AND SPOON PLACED ) AND NOT ( HOLDING MILK AND CEREAL BOX )
					// SET LABEL TO "You need something to eat"
					label = "You need something to eat";
				}else if(GameManager.Instance.gameObjectives.placedBowl && GameManager.Instance.gameObjectives.placedSpoon &&
				         !InventoryManager.Instance.HoldingItem(ItemType.MILK) && InventoryManager.Instance.HoldingItem(ItemType.CEREAL_BOX)){		// ELSE IF BOWL AND SPOON PLACED AND HOLDING MILK AND CEREAL BOX
					// SET LABEL TO "Eat breakfast"
					label = "Eat breakfast";
				}
				break;
			case "WashingMachine":
				// IF HOLDING BUNDLE OF CLOTHES
				if(InventoryManager.Instance.HoldingItem(ItemType.BUNDLE_OF_CLOTHES)){
					// SET LABEL TO "Wash dirty clothes"
					label = "Wash dirty clothes";
				}else if(InventoryManager.Instance.HoldingItem(ItemType.TOWEL_DIRTY)){		// ELSE IF HOLDING DIRTY TOWEL
					// SET LABEL TO "Wash dirty towel"
					label = "Wash dirty towel";
				}
				break;
			case "WorkOutfit":
				// IF NOT HAD SHOWER YET
				if(!GameManager.Instance.gameObjectives.hadShower){
					// SET LABEL TO "You need to take a shower first"
					label = "You need to take a shower first";
				}else{	// IF HAD SHOWER
					// SET LABEL TO "Get dressed for work"
					label = "Get dressed for work";
				}
				break;
			case "ShowerUnit":
				// IF SHOWER IS BROKEN AND NOT HOLDING SPANNER
				if(!GameManager.Instance.gameObjectives.fixedShower && !InventoryManager.Instance.HoldingItem(ItemType.SPANNER)){
					// SET LABEL TO "The shower is broken"
					label = "The shower is broken";
				}else if(!GameManager.Instance.gameObjectives.fixedShower && InventoryManager.Instance.HoldingItem(ItemType.SPANNER)){	// ELSE IF SHOWER IS BROKEN AND HOLDING SPANNER
					// SET LABEL TO "Fix the shower"
					label = "Fix the shower";
				}else if(GameManager.Instance.gameObjectives.fixedShower &&
				         (!InventoryManager.Instance.HoldingItem(ItemType.TOWEL_DIRTY) && !InventoryManager.Instance.HoldingItem(ItemType.TOWEL_CLEAN))){	// ELSE IF SHOWER IS FIXED AND NOT HOLDING TOWEL
					// SET LABEL TO "You need a towel"
					label = "You need a towel";
				}else if(GameManager.Instance.gameObjectives.fixedShower && InventoryManager.Instance.HoldingItem(ItemType.TOWEL_DIRTY)){	// ELSE IF SHOWER IS FIXED AND HOLDING DIRTY TOWEL
					// SET LABEL TO "You need a CLEAN towel"
					label = "You need a CLEAN towel";
				}else if(GameManager.Instance.gameObjectives.fixedShower && InventoryManager.Instance.HoldingItem(ItemType.TOWEL_CLEAN)){	// ELSE IF SHOWER IS FIXED AND HOLDING CLEAN TOWEL
					// SET LABEL TO "Take a shower"
					label = "Have a shower";
				}
				break;

				// DEFAULT ITEMS //
				// ============= //
			case "Untagged":
				label = "[UNTAGGED ITEM]";
				break;
			default:
				Debug.Log ("Tag hit: " + hit.collider.tag);
				break;
		}
	}
	

	void ActivateObject(RaycastHit hit){
		switch (hit.collider.tag) {
			// INVENTORY ITEMS //
			// =============== //
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
			case "TowelDirty":
				InventoryManager.Instance.SendMessage("ItemPickedUp", ItemType.TOWEL_DIRTY, SendMessageOptions.DontRequireReceiver);
				break;
			case "TowelClean":
				InventoryManager.Instance.SendMessage("ItemPickedUp", ItemType.TOWEL_CLEAN, SendMessageOptions.DontRequireReceiver);
				break;
			case "BundleOfClothes":
				InventoryManager.Instance.SendMessage("ItemPickedUp", ItemType.BUNDLE_OF_CLOTHES, SendMessageOptions.DontRequireReceiver);
				break;
			case "Key":
				InventoryManager.Instance.SendMessage("ItemPickedUp", ItemType.KEY, SendMessageOptions.DontRequireReceiver);
				break;
			
			// INTERACTIVE ITEMS //
			// ================= //
			case "KitchenSink":
				// IF DISHES NOT DONE
				if(!GameManager.Instance.gameObjectives.doneDishes){
					GameManager.Instance.SendMessage("CompleteTask", GameObjectives.DO_DISHES, SendMessageOptions.DontRequireReceiver);
					// do nothing
				}
				break;
			case "DryingRack":
				// IF DISHES DONE AND DISHES TO BE PUT AWAY
				if(GameManager.Instance.gameObjectives.doneDishes){
					if(!GameManager.Instance.gameObjectives.driedDishes){
						// Dry dishes
						GameManager.Instance.SendMessage("CompleteTask", GameObjectives.DRY_DISHES, SendMessageOptions.DontRequireReceiver);
					}
				}
				break;
			case "KitchenTable":
				// IF HOLDING BOWL
				if(InventoryManager.Instance.HoldingItem(ItemType.BOWL)){
					// Place bowl on table and remove from inventory
					GameManager.Instance.SendMessage("CompleteTask", GameObjectives.PLACE_BOWL, SendMessageOptions.DontRequireReceiver);
					InventoryManager.Instance.SendMessage("ItemDropped", ItemType.BOWL, SendMessageOptions.DontRequireReceiver);
				}else if(InventoryManager.Instance.HoldingItem(ItemType.SPOON)){	// ELSE IF HOLDING SPOON
					// Place spoon on table and remove from inventory
					GameManager.Instance.SendMessage("CompleteTask", GameObjectives.PLACE_SPOON, SendMessageOptions.DontRequireReceiver);
					InventoryManager.Instance.SendMessage("ItemDropped", ItemType.SPOON, SendMessageOptions.DontRequireReceiver);
				}else if((GameManager.Instance.gameObjectives.placedBowl && GameManager.Instance.gameObjectives.placedSpoon) &&
				         !(InventoryManager.Instance.HoldingItem(ItemType.MILK) && InventoryManager.Instance.HoldingItem(ItemType.CEREAL_BOX))){	// ELSE IF ( BOWL AND SPOON PLACED ) AND NOT ( HOLDING MILK AND CEREAL BOX )
					// do nothing
				}else if(GameManager.Instance.gameObjectives.placedBowl && GameManager.Instance.gameObjectives.placedSpoon &&
				         !InventoryManager.Instance.HoldingItem(ItemType.MILK) && InventoryManager.Instance.HoldingItem(ItemType.CEREAL_BOX)){		// ELSE IF BOWL AND SPOON PLACED AND HOLDING MILK AND CEREAL BOX
					// Eat breakfast and remove cereal and milk from inventory
					GameManager.Instance.SendMessage("CompleteTask", GameObjectives.EAT_BREAKFAST, SendMessageOptions.DontRequireReceiver);
					InventoryManager.Instance.SendMessage("ItemDropped", ItemType.CEREAL_BOX, SendMessageOptions.DontRequireReceiver);
					InventoryManager.Instance.SendMessage("ItemDropped", ItemType.MILK, SendMessageOptions.DontRequireReceiver);
				}
				break;
			case "WashingMachine":
				// IF HOLDING BUNDLE OF CLOTHES
				if(InventoryManager.Instance.HoldingItem(ItemType.BUNDLE_OF_CLOTHES)){
					// Wash dirty clothes and remove from inventory
					GameManager.Instance.SendMessage("CompleteTask", GameObjectives.WASH_CLOTHES, SendMessageOptions.DontRequireReceiver);
					InventoryManager.Instance.SendMessage("ItemDropped", ItemType.BUNDLE_OF_CLOTHES, SendMessageOptions.DontRequireReceiver);
				}else if(InventoryManager.Instance.HoldingItem(ItemType.TOWEL_DIRTY)){		// ELSE IF HOLDING DIRTY TOWEL
					// Wash dirty towel and remove from inventory
					GameManager.Instance.SendMessage("CompleteTask", GameObjectives.WASH_TOWEL, SendMessageOptions.DontRequireReceiver);
					InventoryManager.Instance.SendMessage("ItemDropped", ItemType.TOWEL_DIRTY, SendMessageOptions.DontRequireReceiver);
				}
				break;
			case "WorkOutfit":
				// IF NOT HAD SHOWER YET
				if(!GameManager.Instance.gameObjectives.hadShower){
					// do nothing
				}else{	// IF HAD A SHOWER
					// Get dressed for work
					GameManager.Instance.SendMessage("CompleteTask", GameObjectives.GET_DRESSED, SendMessageOptions.DontRequireReceiver);;
				}
				break;
			case "ShowerUnit":
				// IF SHOWER IS BROKEN AND NOT HOLDING SPANNER
				if(!GameManager.Instance.gameObjectives.fixedShower && !InventoryManager.Instance.HoldingItem(ItemType.SPANNER)){
					// do nothing
				}else if(!GameManager.Instance.gameObjectives.fixedShower && InventoryManager.Instance.HoldingItem(ItemType.SPANNER)){	// ELSE IF SHOWER IS BROKEN AND HOLDING SPANNER
					// Fix the shower and remove spanner from inventory
					GameManager.Instance.SendMessage("CompleteTask", GameObjectives.FIX_SHOWER, SendMessageOptions.DontRequireReceiver);
					InventoryManager.Instance.SendMessage("ItemDropped", ItemType.SPANNER, SendMessageOptions.DontRequireReceiver);
				}else if(GameManager.Instance.gameObjectives.fixedShower &&
				        (!InventoryManager.Instance.HoldingItem(ItemType.TOWEL_DIRTY) && !InventoryManager.Instance.HoldingItem(ItemType.TOWEL_CLEAN))){	// ELSE IF SHOWER IS FIXED AND NOT HOLDING TOWEL
					// do nothing
				}else if(GameManager.Instance.gameObjectives.fixedShower && InventoryManager.Instance.HoldingItem(ItemType.TOWEL_DIRTY)){	// ELSE IF SHOWER IS FIXED AND HOLDING DIRTY TOWEL
					// do nothing
				}else if(GameManager.Instance.gameObjectives.fixedShower && InventoryManager.Instance.HoldingItem(ItemType.TOWEL_CLEAN)){	// ELSE IF SHOWER IS FIXED AND HOLDING CLEAN TOWEL
					// Have shower and remove clean towel from inventory
					GameManager.Instance.SendMessage("CompleteTask", GameObjectives.HAVE_SHOWER, SendMessageOptions.DontRequireReceiver);
					InventoryManager.Instance.SendMessage("ItemDropped", ItemType.TOWEL_CLEAN, SendMessageOptions.DontRequireReceiver);
				}
				break;

			// DEFAULT ITEMS //
			// ============= //
			case "Untagged":
				label = "[UNTAGGED ITEM]";
				break;
			default:
				Debug.Log ("Tag hit: " + hit.collider.tag);
				break;
		}
	}

	void OnGUI(){
		if (visible && label != "") {
			GUI.Box(new Rect(Screen.width / 3, Screen.height * 0.6f, Screen.width / 3, Screen.height * 0.04f), label);
		}
	}
}
