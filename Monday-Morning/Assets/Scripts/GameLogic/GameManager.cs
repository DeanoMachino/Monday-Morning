using UnityEngine;
using System.Collections;

[AddComponentMenu("Game Logic/Game Manager")]

public class GameManager : MonoBehaviour {

	public static GameManager Instance;
	public Objectives gameObjectives;

	public bool tasksCompleted = false;

	void Start(){
		gameObjectives = new Objectives ();
	}

	// Update is called once per frame
	void Update () {
		tasksCompleted = gameObjectives.MainObjectivesCompleted ();

		if (tasksCompleted) {
			// DO SOMETHING FOR WINNING THE GAME
		}
	}

	void CompleteTask(GameObjectives objective_){
		switch (objective_) {
		case GameObjectives.WASH_TOWEL:
			gameObjectives.washedTowel = true;
			break;
		case GameObjectives.FIX_SHOWER:
			gameObjectives.fixedShower = true;
			break;
		case GameObjectives.HAVE_SHOWER:
			gameObjectives.hadShower = true;
			break;
		case GameObjectives.WASH_CLOTHES:
			gameObjectives.washedClothes = true;
			break;
		case GameObjectives.GET_DRESSED:
			gameObjectives.gotDressed = true;
			break;
		case GameObjectives.DO_DISHES:
			gameObjectives.doneDishes = true;
			break;
		case GameObjectives.DRY_DISHES:
			gameObjectives.driedDishes = true;
			break;
		case GameObjectives.PLACE_BOWL:
			gameObjectives.placedBowl = true;
			break;
		case GameObjectives.PLACE_SPOON:
			gameObjectives.placedSpoon = true;
			break;
		case GameObjectives.EAT_BREAKFAST:
			gameObjectives.ateBreakfast = true;
			break;
		case GameObjectives.FIND_KEYS:
			gameObjectives.foundKeys = true;
			break;
		default:

			break;
		}
	}

	void Awake(){
		Instance = this;
	}
}

public class Objectives {
	// MAJOR OBJECTIVES //
	// ================ //
	public bool hadShower = false;
	public bool gotDressed = false;
	public bool ateBreakfast = false;
	public bool takenKeys = false;

	// MINOR OBJECTIVES //
	// ================ //
	// HaveShower
	public bool washedTowel = false;
	public bool fixedShower = false;
	// GetDressed
	public bool washedClothes = false;
	// EatBreakfast
	public bool doneDishes = false;
	public bool driedDishes = false;
	public bool placedBowl = false;
	public bool placedSpoon = false;
	// TakeKeys
	public bool foundKeys = false;
	
	public bool MainObjectivesCompleted(){
		if (hadShower && gotDressed && ateBreakfast && takenKeys) {
			return true;
		} else {
			return false;
		}
	}

	public void ResetMainObjectives(){
		hadShower = false;
		gotDressed = false;
		ateBreakfast = false;
		takenKeys = false;
	}
}

public enum GameObjectives {
	NONE,
	WASH_TOWEL,
	FIX_SHOWER,
	HAVE_SHOWER,		// MAJOR
	WASH_CLOTHES,
	GET_DRESSED,		// MAJOR
	DO_DISHES,
	DRY_DISHES,
	PLACE_BOWL,
	PLACE_SPOON,
	EAT_BREAKFAST,		// MAJOR
	FIND_KEYS			// MAJOR
}

public enum InteractableObjects {
	NONE,
	CEREAL_BOX,
	BOWL,
	MILK,
	SPOON,
	SPANNER,
	TOWEL_DIRTY,
	TOWEL_CLEAN,
	BUNDLE_OF_CLOTHES,
	KEY,
	KITCHEN_SINK,
	DRYING_RACK,
	KITCHEN_TABLE,
	WASHING_MACHINE,
	SHOWER_UNIT,
	DOOR,
	CUPBOARD_DOOR,
	DRAWER,
	WARDROBE_DOOR
}


