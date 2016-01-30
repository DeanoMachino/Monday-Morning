using UnityEngine;
using System.Collections;

[RequireComponent(typeof(InventoryManager))]
[AddComponentMenu("Inventory/Inventory Item")]

public class InventoryItem : MonoBehaviour {

	public ItemType itemType = ItemType.NONE;

	// Use this for initialization
	public InventoryItem(ItemType type_){
		itemType = type_;
	}
	

}

public enum ItemType {
	NONE,
	HAMMER
}