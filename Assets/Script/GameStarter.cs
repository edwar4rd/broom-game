using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using PlayerStats;
using UnityEngine.InputSystem;

public class GameStarter : MonoBehaviour {
    public InputActionAsset actions;

    public void Awake() {
        actions["StartGame"].started += StartGame;
        actions["StartGame"].Enable();
    }

    public void StartGame() {
        actions["StartGame"].started -= StartGame;
        actions["StartGame"].Disable();
        UIStats.playerSeenInstruction = false;
        GameStats.totalRestart = 0;
        GameStats.totalFail = 0;
        GameStats.levelRestart = new List<uint>(SceneManager.sceneCountInBuildSettings);
        GameStats.levelFail = new List<uint>(SceneManager.sceneCountInBuildSettings);
        GameStats.startTimeStamps = new List<float>(SceneManager.sceneCountInBuildSettings);
        GameStats.restartTimeStamps = new List<float>(SceneManager.sceneCountInBuildSettings);
        GameStats.finishTimeStamps = new List<float>(SceneManager.sceneCountInBuildSettings);
        // 使用 SceneManager.LoadScene 來讀取 "Level1" 場景
        SceneManager.LoadScene("Level1");
    }
    private void StartGame(InputAction.CallbackContext context) {
        if (this == null)
            return;
        StartGame();
    }
}
