using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Player/Inventory/Inventory Manager")]

public class InventoryManager : MonoBehaviour {

	public static InventoryManager Instance;
	public int inventorySpaces = 9;
	public List<ItemType> itemList;

	public InventoryIcon cerealBoxIcon;
	public InventoryIcon bowlIcon;
	public InventoryIcon milkIcon;
	public InventoryIcon spoonIcon;
	public InventoryIcon spannerIcon;
	public InventoryIcon towelDirtyIcon;
	public InventoryIcon towelCleanIcon;
	public InventoryIcon bundleOfClothesIcon;
	public InventoryIcon keyIcon;

	// Use this for initialization
	void Start() {
		itemList = new List<ItemType>();
		itemList.Clear();

		//cerealBoxIcon
	}

	// Receives message
	void ItemPickedUp(ItemType type_){
		// Adds item to inventory list
		if (itemList.Count < inventorySpaces) {
			// Checks the item isn't already in the list
			bool exists = false;
			foreach(ItemType item in itemList){
				if(item == type_){
					exists = true;
				}
				break;
			}
			if(!exists){
				Debug.Log ("Item Picked Up: " + type_.ToString ());
				itemList.Add (type_);

				switch(type_){
					case ItemType.CEREAL_BOX:
						cerealBoxIcon.held = true;
						break;
					case ItemType.BOWL:
						bowlIcon.held = true;
						break;
					case ItemType.MILK:
						milkIcon.held = true;
						break;
					case ItemType.SPOON:
						spoonIcon.held = true;
						break;
					case ItemType.SPANNER:
						spannerIcon.held = true;
						break;
					case ItemType.TOWEL_DIRTY:
						towelDirtyIcon.held = true;
						break;
					case ItemType.TOWEL_CLEAN:
						towelCleanIcon.held = true;
						break;
					case ItemType.BUNDLE_OF_CLOTHES:
						bundleOfClothesIcon.held = true;
						break;
					case ItemType.KEY:
						keyIcon.held = true;
						break;
				}
			}
		}
	}

	// Receives message
	void ItemDropped(ItemType type_){
		// Removes item from inventory list
		if (itemList.Count > 0) {
			// Loops backwards through the list and removes any items of the same type
			for(int index = itemList.Count - 1; index >= 0; --index){
				if(itemList[index] == type_){
					itemList.RemoveAt(index);

					switch(type_){
					case ItemType.CEREAL_BOX:
						cerealBoxIcon.held = false;
						break;
					case ItemType.BOWL:
						bowlIcon.held = false;
						break;
					case ItemType.MILK:
						milkIcon.held = false;
						break;
					case ItemType.SPOON:
						spoonIcon.held = false;
						break;
					case ItemType.SPANNER:
						spannerIcon.held = false;
						break;
					case ItemType.TOWEL_DIRTY:
						towelDirtyIcon.held = false;
						break;
					case ItemType.TOWEL_CLEAN:
						towelCleanIcon.held = false;
						break;
					case ItemType.BUNDLE_OF_CLOTHES:
						bundleOfClothesIcon.held = false;
						break;
					case ItemType.KEY:
						keyIcon.held = false;
						break;
					}
				}
			}
		}
	}

	public bool HoldingItem(ItemType type_){
		for (int index = itemList.Count - 1; index >= 0; --index) {
			if(itemList[index] == type_){
				return true;
			}
		}
		return false;
	}

	void EmptyInventory(){
		itemList.Clear ();
	}

	void Awake(){
		Instance = this;
	}
}

public enum ItemType {
	NONE,
	CEREAL_BOX,
	BOWL,
	MILK,
	SPOON,
	SPANNER,
	TOWEL_DIRTY,
	TOWEL_CLEAN,
	BUNDLE_OF_CLOTHES,
	KEY
}
