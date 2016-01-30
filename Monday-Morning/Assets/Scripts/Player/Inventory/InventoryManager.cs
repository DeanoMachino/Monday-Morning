using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Inventory/Inventory Manager")]

public class InventoryManager : MonoBehaviour {

	private int inventorySpaces = 8;
	List<InventoryItem> itemList = new List<InventoryItem>();
	// Use this for initialization
	void Start() {
		itemList.Clear();
	}
	
	// Update is called once per frame
	void Update() {
	
	}

	void ItemPickedUp(ItemType type_){
		itemList.Add(new InventoryItem (type_));
	}

	void ItemDropped(ItemType type_){
		foreach (InventoryItem item in itemList) {
			if(item.itemType == type_){
				itemList.RemoveAll(i => i.itemType == type_);
			}
		}
	}

	//void Item
}
