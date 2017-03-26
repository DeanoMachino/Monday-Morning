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
	public GameObject bowl2;
	public GameObject spoon2;
	public GameObject cereal2;
	public GameObject milk2;

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
		}

		// TEMP: Not for release -Dean
		if(Input.GetKeyDown(KeyCode.C)){
			// Not implemented fully yet -Dean
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

	public void EndScene(bool win){
		if (win) {
			fader.nextScene = 2;
		} else {
			fader.nextScene = 1;
		}
		fader.sceneEnding = true;
	}

	void Awake(){
		Instance = this;
	}

	public void ActivateObject(Action operation){
		switch (operation) {
		case Action.NONE:

			break;
		case Action.PICKED_UP_CEREAL:
			cerealBox.SetActive(false);
			StartPopupTimer ("Picked up cereal box");
			break;
		case Action.PICKED_UP_BOWL:
			bowl.SetActive(false);
			StartPopupTimer ("Picked up bowl");
			break;
		case Action.PICKED_UP_MILK:
			milk.SetActive(false);
			StartPopupTimer ("Picked up milk");
			break;
		case Action.PICKED_UP_SPOON:
			spoon.SetActive(false);
			StartPopupTimer ("Picked up spoon");
			break;
		case Action.PICKED_UP_SPANNER:
			spanner.SetActive(false);
			StartPopupTimer ("Picked up spanner");
			break;
		case Action.PICKED_UP_TOWEL_DIRTY:
			towelDirty.SetActive(false);
			StartPopupTimer ("Picked up dirty towel");
			break;
		case Action.PICKED_UP_TOWEL_CLEAN:
			towelClean.SetActive(false);
			StartPopupTimer ("Picked up clean towel");
			break;
		case Action.PICKED_UP_BUNDLE_OF_CLOTHES:
			bundleOfClothes.SetActive(false);
			StartPopupTimer ("Picked up bundle of clothes");
			break;
		case Action.PICKED_UP_KEY:
			key.SetActive (false);
			StartPopupTimer ("Picked up key");
			gameObjectives.foundKeys = true;
			gameObjectives.takenKeys = true;
			break;
		case Action.DONE_DISHES:
			gameObjectives.doneDishes = true;
			dirtyDishes.SetActive (false);
			cleanDishes.SetActive (true);
			StartPopupTimer ("Cleaned dirty dishes");
			break;
		case Action.DRIED_DISHES:
			gameObjectives.driedDishes = true;
			bowl.SetActive (true);
			spoon.SetActive (true);
			cleanDishes.SetActive (false);
			StartPopupTimer ("Dried dishes");
			break;
		case Action.PLACED_BOWL:
			gameObjectives.placedBowl = true;
			bowl2.SetActive (true);
			StartPopupTimer ("Placed bowl on table");
			break;
		case Action.PLACED_SPOON:
			gameObjectives.placedSpoon = true;
			spoon2.SetActive (true);
			StartPopupTimer("Placed spoon on table");
			break;
		case Action.ATE_BREAKFAST:
			gameObjectives.ateBreakfast = true;
			cereal2.SetActive (true);
			milk2.SetActive (true);
			StartPopupTimer ("Ate breakfast");
			break;
		case Action.WASHED_CLOTHES:
			gameObjectives.washedClothes = true;
			workClothes.SetActive (true);
			StartPopupTimer ("Washed dirty work clothes");
			break;
		case Action.WASHED_TOWEL:
			gameObjectives.washedTowel = true;
			towelClean.SetActive (true);
			StartPopupTimer ("Washed towel");
			break;
		case Action.GOT_DRESSED:
			gameObjectives.gotDressed = true;
			workClothes.SetActive (false);
			StartPopupTimer ("Got dressed in work clothes");
			break;
		case Action.FIXED_SHOWER:
			gameObjectives.fixedShower = true;
			StartPopupTimer ("Fixed the shower");
			break;
		case Action.HAD_SHOWER:
			gameObjectives.hadShower = true;
			StartPopupTimer ("Had a shower");
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
			towelDirty.SetActive (false);
			towelClean.SetActive (true);
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

		string shower = "";
		string dressed = "";
		string breakfast = "";
		string keys = "";

		if (gameObjectives.hadShower) {
			shower = " - Have a shower\tDONE";
		} else {
			shower = " - Have a shower";
		}

		if (gameObjectives.gotDressed) {
			dressed = " - Get dressed\tDONE";
		} else {
			dressed = " - Get dressed";
		}

		if (gameObjectives.ateBreakfast) {
			breakfast = " - Eat breakfast\tDONE";
		} else {
			breakfast = " - Eat breakfast";
		}

		if (gameObjectives.takenKeys) {
			keys = " - Find keys\tDONE";
		} else {
			keys = " - Find keys";
		}

		string objectivesText = "  TO DO by 9:00AM:\n" + shower + "\n" + dressed + "\n" + breakfast + "\n" + keys;

		GUIStyle style = new GUIStyle();
		style.alignment = TextAnchor.MiddleLeft;
		style.fontStyle = FontStyle.Bold;
		style.normal.background = Texture2D.whiteTexture;
		style.normal.textColor = Color.black;
		style.border.top = 10;
		style.border.left = 10;
		style.border.bottom = 10;
		style.border.right = 10;

		GUI.Box (new Rect (Screen.width * 0.875f, Screen.height * 0.4375f, Screen.width * 0.12f, Screen.height * 0.125f), objectivesText, style);
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
			Debug.Log ("Completed: true");
			return true;
		} else {
			Debug.Log ("Completed: false");
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

public enum Action{
	NONE,
	PICKED_UP_CEREAL,
	PICKED_UP_BOWL,
	PICKED_UP_MILK,
	PICKED_UP_SPOON,
	PICKED_UP_SPANNER,
	PICKED_UP_TOWEL_DIRTY,
	PICKED_UP_TOWEL_CLEAN,
	PICKED_UP_BUNDLE_OF_CLOTHES,
	PICKED_UP_KEY,
	DONE_DISHES,
	DRIED_DISHES,
	PLACED_BOWL,
	PLACED_SPOON,
	ATE_BREAKFAST,
	WASHED_CLOTHES,
	WASHED_TOWEL,
	GOT_DRESSED,
	FIXED_SHOWER,
	HAD_SHOWER
}
