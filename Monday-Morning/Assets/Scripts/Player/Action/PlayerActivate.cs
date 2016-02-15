using UnityEngine;
using System.Collections;

[AddComponentMenu("Player/Action/Player Activate")]

public class PlayerActivate : MonoBehaviour {
	
	public float range;
	private GameObject objectHit;

	private string tooltip;
	private bool tooltipVisible = false;

	void Update(){
		RaycastHit hit;
		Ray activationRay = new Ray (transform.position, transform.forward);
		tooltipVisible = false;

		// Check if object has been hit
		if (Physics.Raycast (activationRay, out hit, range)) {
			tooltipVisible = true;
			tooltip = "";

			SetTooltipLabel(hit);

			if (Input.GetButtonDown ("Action")) {
				Debug.Log ("Object Hit");
				ActivateObject(hit);
			}
		}
	}

	void SetTooltipLabel(RaycastHit hit){
		switch(hit.collider.tag){
			// INVENTORY ITEMS //
			// =============== //
			case "CerealBox":
				tooltip = "Take Box of Cereal";
				break;
			case "Bowl":
				tooltip = "Take Bowl";
				break;
			case "Milk":
				tooltip = "Take Milk";
				break;
			case "Spoon":
				tooltip = "Take Spoon";
				break;
			case "Spanner":
				tooltip = "Take Spanner";
				break;
			case "TowelDirty":
				tooltip = "Take Dirty Towel";
				break;
			case "TowelClean":
				tooltip = "Take Clean Towel";
				break;
			case "BundleOfClothes":
				tooltip = "Take Bundle of Clothes";
				break;
			case "Key":
				tooltip = "Take Key";
				break;

			// INTERACTIVE ITEMS //
			// ================= //
			case "KitchenSink":
				// IF DISHES NOT DONE
				if(!GameManager.Instance.gameObjectives.doneDishes){
					// SET TOOLTIP TO "Wash dirty dishes"
					tooltip = "Wash dirty dishes";
				}
				break;
			case "DryingRack":
				// IF DISHES DONE AND DISHES TO BE PUT AWAY
				if(GameManager.Instance.gameObjectives.doneDishes){
					if(!GameManager.Instance.gameObjectives.driedDishes){
						// SET TOOLTIP TO "Put away dishes"
						tooltip = "Put away dishes";
					}
				}
				break;
			case "KitchenTable":
				// IF HOLDING BOWL
				if(InventoryManager.Instance.HoldingItem(ItemType.BOWL)){
					// SET TOOLTIP TO "Place bowl"
					tooltip = "Place bowl";
				}else if(InventoryManager.Instance.HoldingItem(ItemType.SPOON)){	// ELSE IF HOLDING SPOON
					// SET TOOLTIP TO "Place spoon"
					tooltip = "Place spoon";
				}else if((GameManager.Instance.gameObjectives.placedBowl && GameManager.Instance.gameObjectives.placedSpoon) &&
				         !(InventoryManager.Instance.HoldingItem(ItemType.MILK) && InventoryManager.Instance.HoldingItem(ItemType.CEREAL_BOX)) &&
						 !(GameManager.Instance.gameObjectives.ateBreakfast)){	// ELSE IF ( BOWL AND SPOON PLACED ) AND NOT ( HOLDING MILK AND CEREAL BOX )
					// SET TOOLTIP TO "You need something to eat"
					tooltip = "You need something to eat";
				}else if(GameManager.Instance.gameObjectives.placedBowl && GameManager.Instance.gameObjectives.placedSpoon &&
				         InventoryManager.Instance.HoldingItem(ItemType.MILK) && InventoryManager.Instance.HoldingItem(ItemType.CEREAL_BOX)){		// ELSE IF BOWL AND SPOON PLACED AND HOLDING MILK AND CEREAL BOX
					// SET TOOLTIP TO "Eat breakfast"
					tooltip = "Eat breakfast";
				}
				break;
			case "WashingMachine":
				// IF HOLDING BUNDLE OF CLOTHES
				if(InventoryManager.Instance.HoldingItem(ItemType.BUNDLE_OF_CLOTHES)){
					// SET TOOLTIP TO "Wash dirty clothes"
					tooltip = "Wash dirty clothes";
				}else if(InventoryManager.Instance.HoldingItem(ItemType.TOWEL_DIRTY)){		// ELSE IF HOLDING DIRTY TOWEL
					// SET TOOLTIP TO "Wash dirty towel"
					tooltip = "Wash dirty towel";
				}
				break;
			case "WorkOutfit":
				// IF NOT HAD SHOWER YET
				if(!GameManager.Instance.gameObjectives.hadShower){
					// SET TOOLTIP TO "You need to take a shower first"
					tooltip = "You need to take a shower first";
				}else{	// IF HAD SHOWER
					// SET TOOLTIP TO "Get dressed for work"
					tooltip = "Get dressed for work";
				}
				break;
			case "ShowerUnit":
				if(!GameManager.Instance.gameObjectives.hadShower){
					// IF SHOWER IS BROKEN AND NOT HOLDING SPANNER
					if(!GameManager.Instance.gameObjectives.fixedShower && !InventoryManager.Instance.HoldingItem(ItemType.SPANNER)){
						// SET TOOLTIP TO "The shower is broken"
						tooltip = "The shower is broken";
					}else if(!GameManager.Instance.gameObjectives.fixedShower && InventoryManager.Instance.HoldingItem(ItemType.SPANNER)){	// ELSE IF SHOWER IS BROKEN AND HOLDING SPANNER
						// SET TOOLTIP TO "Fix the shower"
						tooltip = "Fix the shower";
					}else if(GameManager.Instance.gameObjectives.fixedShower &&
					         (!InventoryManager.Instance.HoldingItem(ItemType.TOWEL_DIRTY) && !InventoryManager.Instance.HoldingItem(ItemType.TOWEL_CLEAN))){	// ELSE IF SHOWER IS FIXED AND NOT HOLDING TOWEL
						// SET TOOLTIP TO "You need a towel"
						tooltip = "You need a towel";
					}else if(GameManager.Instance.gameObjectives.fixedShower && InventoryManager.Instance.HoldingItem(ItemType.TOWEL_DIRTY)){	// ELSE IF SHOWER IS FIXED AND HOLDING DIRTY TOWEL
						// SET TOOLTIP TO "You need a CLEAN towel"
						tooltip = "You need a CLEAN towel";
					}else if(GameManager.Instance.gameObjectives.fixedShower && InventoryManager.Instance.HoldingItem(ItemType.TOWEL_CLEAN)){	// ELSE IF SHOWER IS FIXED AND HOLDING CLEAN TOWEL
						// SET TOOLTIP TO "Take a shower"
						tooltip = "Have a shower";
					}
				}
				break;

				// DEFAULT ITEMS //
				// ============= //
			case "Untagged":
				//tooltip = "[UNTAGGED ITEM]";
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
				InventoryManager.Instance.SendMessage ("ItemPickedUp", ItemType.CEREAL_BOX, SendMessageOptions.DontRequireReceiver);
				GameManager.Instance.ActivateObject (InteractableObjects.CEREAL_BOX);
				break;
			case "Bowl":
				Debug.Log ("ActivateObject -- Bowl");
				InventoryManager.Instance.SendMessage("ItemPickedUp", ItemType.BOWL, SendMessageOptions.DontRequireReceiver);
				GameManager.Instance.ActivateObject (InteractableObjects.BOWL);
				break;
			case "Milk":
				Debug.Log ("ActivateObject -- Milk");
				InventoryManager.Instance.SendMessage("ItemPickedUp", ItemType.MILK, SendMessageOptions.DontRequireReceiver);
			GameManager.Instance.ActivateObject (InteractableObjects.MILK);
				break;
			case "Spoon":
				Debug.Log ("ActivateObject -- Spoon");	
				InventoryManager.Instance.SendMessage("ItemPickedUp", ItemType.SPOON, SendMessageOptions.DontRequireReceiver);
			GameManager.Instance.ActivateObject (InteractableObjects.SPOON);
				break;
			case "Spanner":
				Debug.Log ("ActivateObject -- Spanner");
				InventoryManager.Instance.SendMessage("ItemPickedUp", ItemType.SPANNER, SendMessageOptions.DontRequireReceiver);
			GameManager.Instance.ActivateObject (InteractableObjects.SPANNER);
				break;
			case "TowelDirty":
				Debug.Log ("ActivateObject -- TowelDirty");
				InventoryManager.Instance.SendMessage("ItemPickedUp", ItemType.TOWEL_DIRTY, SendMessageOptions.DontRequireReceiver);
			GameManager.Instance.ActivateObject (InteractableObjects.TOWEL_DIRTY);
				break;
			case "TowelClean":
				Debug.Log ("ActivateObject -- TowelClean");
				InventoryManager.Instance.SendMessage("ItemPickedUp", ItemType.TOWEL_CLEAN, SendMessageOptions.DontRequireReceiver);
			GameManager.Instance.ActivateObject (InteractableObjects.TOWEL_CLEAN);
				break;
			case "BundleOfClothes":
				Debug.Log ("ActivateObject -- BundleOfClothes");
				InventoryManager.Instance.SendMessage("ItemPickedUp", ItemType.BUNDLE_OF_CLOTHES, SendMessageOptions.DontRequireReceiver);
			GameManager.Instance.ActivateObject (InteractableObjects.BUNDLE_OF_CLOTHES);
				break;
			case "Key":
				Debug.Log ("ActivateObject -- Key");
				InventoryManager.Instance.SendMessage("ItemPickedUp", ItemType.KEY, SendMessageOptions.DontRequireReceiver);
				GameManager.Instance.gameObjectives.takenKeys = true;
			GameManager.Instance.ActivateObject (InteractableObjects.KEY);
				break;
			
			// INTERACTIVE ITEMS //
			// ================= //
			case "KitchenSink":
				Debug.Log ("ActivateObject -- KitchenSink");
				// IF DISHES NOT DONE
				if(!GameManager.Instance.gameObjectives.doneDishes){
					GameManager.Instance.SendMessage("CompleteTask", GameObjectives.DO_DISHES, SendMessageOptions.DontRequireReceiver);
					GameManager.Instance.ActivateObject (InteractableObjects.KITCHEN_SINK);
				}
				break;
			case "DryingRack":
				Debug.Log ("ActivateObject -- DryingRack");
				// IF DISHES DONE AND DISHES TO BE PUT AWAY
				if(GameManager.Instance.gameObjectives.doneDishes){
					if(!GameManager.Instance.gameObjectives.driedDishes){
						// Dry dishes
						GameManager.Instance.SendMessage("CompleteTask", GameObjectives.DRY_DISHES, SendMessageOptions.DontRequireReceiver);
						GameManager.Instance.ActivateObject (InteractableObjects.DRYING_RACK);
					}
				}
				break;
			case "KitchenTable":
				Debug.Log ("ActivateObject -- KitchenTable");
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
				         InventoryManager.Instance.HoldingItem(ItemType.MILK) && InventoryManager.Instance.HoldingItem(ItemType.CEREAL_BOX)){		// ELSE IF BOWL AND SPOON PLACED AND HOLDING MILK AND CEREAL BOX
					// Eat breakfast and remove cereal and milk from inventory
					GameManager.Instance.SendMessage("CompleteTask", GameObjectives.EAT_BREAKFAST, SendMessageOptions.DontRequireReceiver);
					InventoryManager.Instance.SendMessage("ItemDropped", ItemType.CEREAL_BOX, SendMessageOptions.DontRequireReceiver);
					InventoryManager.Instance.SendMessage("ItemDropped", ItemType.MILK, SendMessageOptions.DontRequireReceiver);
				}
				break;
			case "WashingMachine":
				Debug.Log ("ActivateObject -- WashingMachine");
				// IF HOLDING BUNDLE OF CLOTHES
				if(InventoryManager.Instance.HoldingItem(ItemType.BUNDLE_OF_CLOTHES)){
					// Wash dirty clothes and remove from inventory
					GameManager.Instance.SendMessage("CompleteTask", GameObjectives.WASH_CLOTHES, SendMessageOptions.DontRequireReceiver);
					InventoryManager.Instance.SendMessage("ItemDropped", ItemType.BUNDLE_OF_CLOTHES, SendMessageOptions.DontRequireReceiver);
					GameManager.Instance.ActivateObject (InteractableObjects.WASHING_MACHINE);
				}else if(InventoryManager.Instance.HoldingItem(ItemType.TOWEL_DIRTY)){		// ELSE IF HOLDING DIRTY TOWEL
					// Wash dirty towel and remove from inventory
					GameManager.Instance.SendMessage("CompleteTask", GameObjectives.WASH_TOWEL, SendMessageOptions.DontRequireReceiver);
					InventoryManager.Instance.SendMessage("ItemDropped", ItemType.TOWEL_DIRTY, SendMessageOptions.DontRequireReceiver);
					GameManager.Instance.ActivateObject (InteractableObjects.WASHING_MACHINE);
				}
				break;
			case "WorkOutfit":
				Debug.Log ("ActivateObject -- WorkOutfit");
				// IF NOT HAD SHOWER YET
				if(!GameManager.Instance.gameObjectives.hadShower){
					// do nothing
				}else{	// IF HAD A SHOWER
					// Get dressed for work
					GameManager.Instance.SendMessage("CompleteTask", GameObjectives.GET_DRESSED, SendMessageOptions.DontRequireReceiver);;
				}
				break;
			case "ShowerUnit":
				Debug.Log ("ActivateObject -- ShowerUnit");
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
			case "Door":
				Debug.Log ("ActivateObject -- Door");
				// Open the door
				hit.collider.GetComponentInParent<DoorActivate>().Activate();
				break;
			case "FrontDoor":
				Debug.Log ("ActivateObject -- FrontDoor");
				// Finish if all other objectives are complete - Stuart
				break;
			case "FridgeDoor":
				Debug.Log ("ActivateObject -- FridgeDoor");
				// Open the fridge
				hit.collider.GetComponentInParent<DoorActivate>().Activate();
				break;
			case "WardrobeDoorLeft":
				Debug.Log ("ActivateObject -- WardrobeDoorLeft");
				// Open the wardrobe
				hit.collider.GetComponentInParent<DoorActivate>().Activate();
				break;
			case "WardrobeDoorRight":
				Debug.Log ("ActivateObject -- WardrobeDoorRight");
				// Open the wardrobe
				hit.collider.GetComponentInParent<DoorActivate>().Activate();
				break;
			case "Drawers":
				Debug.Log ("ActivateObject -- Drawers");
				// Open the drawers
				hit.collider.GetComponentInParent<DoorActivate>().Activate();
				break;
			
			// DEFAULT ITEMS //
			// ============= //
			case "Untagged":
				tooltip = "[UNTAGGED ITEM]";
				break;
			default:
				Debug.Log ("Tag hit: " + hit.collider.tag);
				break;
		}
	}

	void OnGUI(){
		if (tooltipVisible && tooltip != "") {
			GUI.Box(new Rect(Screen.width / 3, Screen.height * 0.6f, Screen.width / 3, Screen.height * 0.04f), tooltip);
		}
	}
}
