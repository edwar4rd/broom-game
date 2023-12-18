using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PlayerStats;

public class DoorManager : MonoBehaviour {
	private bool touchingDoor = false;
	public bool isWin = false;
	public bool isLastLevel;
	// Start is called before the first frame update
	void Start() {
	}

	// Update is called once per frame
	void Update() {
		// if (Input.GetKey("r")) {
		// 	isWin = false;
		// 	if (winningSign != null)
		// 		winningSign.SetActive(false);
		// }
		if (touchingDoor && !isWin)
			FinishLevel();
		// if (Input.GetKeyDown(KeyCode.F)) {
		// 	Debug.Log("F be pressed");
		// 	//check for player's collision with game object tagged Dock
		// 	if (touchingDoor)
		// 		FinishLevel();
		// }
	}

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log("Door touched");
		if (other.tag == "Player") {
			touchingDoor = true;
			Debug.Log("Door touched by Player!");
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Player") {
			touchingDoor = false;
		}
	}

	void FinishLevel() {
		while (SceneManager.GetActiveScene().buildIndex >= GameStats.finishTimeStamps.Count) {
			GameStats.finishTimeStamps.Add(0);
		}
		GameStats.finishTimeStamps[SceneManager.GetActiveScene().buildIndex] = Time.time;

		string winGameDataDump = "Failed " + GameStats.totalFail + " time(s), restarted " + GameStats.totalRestart + " time(s).\n";
		// Level0 => menu
		for (int i = 1; i < GameStats.levelFail.Count; i++) {
			winGameDataDump += "Failed Level " + i + " " + GameStats.levelFail[i] + " time(s) \n";
		}
		for (int i = 1; i < GameStats.levelRestart.Count; i++) {
			winGameDataDump += "Restarted Level " + i + " " + GameStats.levelRestart[i] + " time(s) \n";
		}
		for (int i = 1; i < GameStats.startTimeStamps.Count; i++) {
			winGameDataDump += "Level " + i + ":\n";
			winGameDataDump += "\tStart:   " + GameStats.startTimeStamps[i] + "\n";
			winGameDataDump += "\tRestart: " + GameStats.restartTimeStamps[i] + "\n";
			winGameDataDump += "\tFinish:  " + GameStats.finishTimeStamps[i] + "\n";
		}
		if (isLastLevel)
			winGameDataDump += "\nYOU WIN!!!\n";
		Debug.Log(winGameDataDump);
		GameStats.scoreStr = winGameDataDump;

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		isWin = true;

		Time.timeScale = 0;
	}
}
