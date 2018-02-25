using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour {
	[System.Serializable]
	public class GameModeData{
		public string mode;
		[Tooltip("Objects and all scripts attached to this objects will be disabled, if you want to run some of the public script before you can subscribe to onDsiable func")]
		public GameObject[] objects;
		public UnityEvent onEnable;
		public UnityEvent onDisable;
	}
		
	public static GameManager Instance;
	public string defaultMode;

	string gameMode = "Play";
	string gameModePrev = "Play"; // to return to previous mode

	public GameModeData[] gameModeData;


	// Use this for initialization
	void Start () {
		if (Instance == null)
			Instance = this;

		SetMode (defaultMode);
	}

	public void SetMode(string mode){
		EnableNext (mode);
	}

	public void SetToPreviousMode(){
		foreach (GameModeData g in gameModeData) {
			if (g.mode == gameModePrev) {
				foreach (GameObject o in g.objects) {
					o.SetActive (true);
				}
				gameMode = gameModePrev;
				print ("Game mode: SetToPreviousMode: " + gameMode);
				g.onEnable.Invoke ();
			}
		}
	}

	// it will run onDisable on previous active Mode
	public void EnableNext(string mode){
		// disable curMode
		if(mode != gameMode){ // prevent first or same game mode launch
			foreach (GameModeData g in gameModeData) {
				if (g.mode == gameMode) {
					g.onDisable.Invoke ();
					foreach (GameObject o in g.objects) {
						o.SetActive (false);
					}
				}
			}
		}
		gameModePrev = gameMode;

		// enable next
		foreach (GameModeData g in gameModeData) {
			if (g.mode == mode) {
				foreach (GameObject o in g.objects) {
					o.SetActive (true);
				}
				gameMode = mode;
				print ("Game mode: EnableNext: " + gameMode);
				g.onEnable.Invoke ();
			}
		}
	}

	// will disable all other active mode
	public void EnableOnly(string mode){
		foreach (GameModeData g in gameModeData) {
			if (g.mode == mode) {
				foreach (GameObject o in g.objects) {
					o.SetActive (true);
				}
				g.onEnable.Invoke ();
			} else {
				foreach (GameObject o in g.objects) {
					o.SetActive (false);
				}
			}
		}
	}
	// Update is called once per frame
	void Update () {
//		if (Input.GetKeyUp (KeyCode.A)) {
//			SetMode (GameMode.Play);
//		}
//		if (Input.GetKeyUp (KeyCode.S)) {
//			SetMode (GameMode.RecordSample);
//		}
	}
}
