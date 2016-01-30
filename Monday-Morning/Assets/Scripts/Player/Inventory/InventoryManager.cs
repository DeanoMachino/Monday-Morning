using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Inventory/Inventory Manager")]

public class InventoryManager : MonoBehaviour {

	public GameObject inventoryBar;
	public int inventorySpaces = 8;
	public List<InventoryItem> itemList = new List<InventoryItem>();
	// Use this for initialization
	void Start() {
		itemList.Clear();
	}

	// Receives message
	void ItemPickedUp(ItemType type_){
		if (itemList.Count < inventorySpaces) {
			itemList.Add (new InventoryItem (type_));
		}
	}

	// Receives message
	void ItemDropped(ItemType type_){
		if (itemList.Count > 0) {
			foreach (InventoryItem item in itemList) {
				if (item.itemType == type_) {
					itemList.RemoveAll (i => i.itemType == type_);
				}
			}
		}
	}
}
