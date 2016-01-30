using UnityEngine;
using System.Collections;

[RequireComponent(typeof(InventoryManager))]
[AddComponentMenu("Player/Inventory/Inventory Item")]

public class InventoryItem : MonoBehaviour {

	public ItemType itemType = ItemType.NONE;

	// Use this for initialization
	public InventoryItem(ItemType type_){
		itemType = type_;
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