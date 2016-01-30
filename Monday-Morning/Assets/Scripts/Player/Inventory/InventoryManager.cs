using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Player/Inventory/Inventory Manager")]

public class InventoryManager : MonoBehaviour {

	public static InventoryManager Instance;
	public GameObject inventoryBar;
	public int inventorySpaces = 9;
	public List<ItemType> itemList;

	// Use this for initialization
	void Start() {
		itemList = new List<ItemType>();
		itemList.Clear();
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
				}
			}
		}
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
	TOWEL,
	BUNDLE_OF_CLOTHES,
	WORK_OUTFIT,
	KEY
}
