using UnityEngine;
using System.Collections;

[AddComponentMenu("Game Logic/Game Manager")]

public class GameManager : MonoBehaviour {

	public static GameManager Instance;
	public Objectives gameObjectives;
	public ScreenFader fader;
	public GameObject player;

	// Objectives
	public GameObject cerealBox;
	public GameObject bowl;
	public GameObject milk;
	public GameObject spoon;
	public GameObject spanner;
	public GameObject towelDirty;
	public GameObject towelClean;
	public GameObject bundleOfClothes;
	public GameObject key;
	// Misc
	public GameObject dirtyDishes;
	public GameObject cleanDishes;
	public GameObject workClothes;

	public bool tasksCompleted = false;

	private string popup;
	private bool popupActive = false;

	private float startTime = 0.0f, currentTime = 0.0f, timeToWait = 1.5f;

	void Start(){
		gameObjectives = new Objectives ();
	}

	// Update is called once per frame
	void Update () {
		// Check if tasks have been completed (Goal state)
		tasksCompleted = gameObjectives.MainObjectivesCompleted ();

		if (tasksCompleted) {
			Debug.Log("Tasks Completed");
			fader.EndScene(2);
		}

		if(Input.GetKeyDown(KeyCode.C)){
			ResetDay();
		}

		// Popup message
		if (popupActive) {
			currentTime = Time.time;
			if (currentTime - startTime > timeToWait) {
				popupActive = false;
			}
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

	public void ActivateObject(InteractableObjects object_){
		switch (object_) {
		case InteractableObjects.CEREAL_BOX:
			// PICKED UP CEREAL BOX
			cerealBox.SetActive(false);
			StartPopupTimer ("Picked up cereal box");
			break;
		case InteractableObjects.BOWL:
			// PICKED UP BOWL
			bowl.SetActive(false);
			StartPopupTimer ("Picked up bowl");
			break;
		case InteractableObjects.MILK:
			// PICKED UP MILK
			milk.SetActive(false);
			StartPopupTimer ("Picked up milk");
			break;
		case InteractableObjects.SPOON:
			// PICKED UP SPOON
			spoon.SetActive(false);
			StartPopupTimer ("Picked up spoon");
			break;
		case InteractableObjects.SPANNER:
			// PICKED UP SPANNER
			spanner.SetActive(false);
			StartPopupTimer ("Picked up spanner");
			break;
		case InteractableObjects.TOWEL_DIRTY:
			// PICKED UP DIRTY TOWEL
			towelDirty.SetActive(false);
			StartPopupTimer ("Picked up dirty towel");
			break;
		case InteractableObjects.TOWEL_CLEAN:
			// PICKED UP CLEAN TOWEL
			towelClean.SetActive(false);
			StartPopupTimer ("Picked up clean towel");
			break;
		case InteractableObjects.BUNDLE_OF_CLOTHES:
			// PICKED UP BUNDLE OF CLOTHES
			bundleOfClothes.SetActive(false);
			StartPopupTimer ("Picked up bundle of clothes");
			break;
		case InteractableObjects.KEY:
			// PICKED UP KEY
			key.SetActive(false);
			StartPopupTimer ("Picked up key");
			break;
		case InteractableObjects.KITCHEN_SINK:
			// CLEANED DISHES
			dirtyDishes.SetActive (false);
			cleanDishes.SetActive (true);
			StartPopupTimer ("Cleaned dirty dishes");
			break;
		case InteractableObjects.DRYING_RACK:
			// PUT AWAY DISHES
			cleanDishes.SetActive (false);
			StartPopupTimer ("Dried dishes");
			break;
		case InteractableObjects.KITCHEN_TABLE:
			if (!gameObjectives.ateBreakfast) {
				if (InventoryManager.Instance.HoldingItem (ItemType.BOWL)) {
					// PLACE BOWL
					// move bowl to table
					StartPopupTimer ("Placed bowl");
				} else if (InventoryManager.Instance.HoldingItem (ItemType.SPOON)) {
					// PLACE SPOON
					// move spoon to table
					StartPopupTimer ("Placed spoon");
				} else if (GameManager.Instance.gameObjectives.placedBowl && GameManager.Instance.gameObjectives.placedSpoon &&
				           InventoryManager.Instance.HoldingItem (ItemType.MILK) && InventoryManager.Instance.HoldingItem (ItemType.CEREAL_BOX)) {
					// EAT BREAKFAST
					// remove bowl
					// remove spoon
					// place cereal
					// place milk
					StartPopupTimer ("Ate breakfast");
				}
			}
			break;
		case InteractableObjects.WASHING_MACHINE:
			if (InventoryManager.Instance.HoldingItem (ItemType.BUNDLE_OF_CLOTHES)) {
				// PLACE WORK CLOTHES IN WARDROBE
				workClothes.SetActive (true);
				StartPopupTimer ("Washed dirty work clothes");
			} else if (InventoryManager.Instance.HoldingItem (ItemType.TOWEL_DIRTY)) {		// ELSE IF HOLDING DIRTY TOWEL
				// PLACE CLEAN TOWEL IN BATHROOM
				towelClean.SetActive (true);
				StartPopupTimer ("Washed towel");
			}
			break;
		case InteractableObjects.WORK_OUTFIT:
			// PUT ON WORK CLOTHES
			workClothes.SetActive (false);
			StartPopupTimer ("Got dressed in work clothes");
			break;
		case InteractableObjects.SHOWER_UNIT:
			if (!GameManager.Instance.gameObjectives.fixedShower && InventoryManager.Instance.HoldingItem (ItemType.SPANNER)) {	// ELSE IF SHOWER IS BROKEN AND HOLDING SPANNER
				// FIX THE SHOWER
				StartPopupTimer ("Fixed the shower");
			} else if (GameManager.Instance.gameObjectives.fixedShower && InventoryManager.Instance.HoldingItem (ItemType.TOWEL_CLEAN)) {	// ELSE IF SHOWER IS FIXED AND HOLDING CLEAN TOWEL
				// HAVE A SHOWER
				StartPopupTimer ("Took a shower");
			}
			break;
		}
	}

	void ResetDay(){
		// Reset objectives
		gameObjectives.ResetMainObjectives();

		// Player
		player.GetComponent<SetInitial>().SetPosition();
		player.GetComponent<SetInitial>().SetRotation();

		// HaveShower
		if (gameObjectives.washedTowel) {
			//towelDirty.SetActive (false);
			//towelClean.SetActive (true);
		} else {
		}

		// GetDressed
		if(!gameObjectives.washedClothes) {

		}
		// EatBreakfast

		// TakeKeys
	}

	void StartPopupTimer(string message){
		popupActive = true;
		startTime = Time.time;
		popup = message;
	}

	void OnGUI(){
		if (popupActive) {
			GUI.Box (new Rect (Screen.width * 0.85f, Screen.height * 0.05f, Screen.width * 0.145f, Screen.height * 0.04f), popup);
		}
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
	WARDROBE_DOOR,
	WORK_OUTFIT
}


