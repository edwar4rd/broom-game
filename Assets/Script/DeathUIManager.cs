using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerStats;
using TMPro;
using LevelData;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class DeathUIManager : MonoBehaviour {
    public GameObject DeathUI;
    public TMP_Text RestartButtonText;
    public PlayerManager player;
    // Start is called before the first frame update
    void Start() {
        DeathUI.SetActive(false);
        uint failCount = GameStats.levelFail.Count > SceneManager.GetActiveScene().buildIndex ? GameStats.levelFail[SceneManager.GetActiveScene().buildIndex] : 0;
        RestartButtonText.text = RetryTexts.retryButton[SceneManager.GetActiveScene().buildIndex, failCount < 5 ? failCount : 4];
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Debug.Log("Closing Death UI");
            CloseDeathUI();
        }
    }

    public void CloseDeathUI() {
        DeathUI.SetActive(false);
    }

    public void CloseDeathUIWithClick() {
        DeathUI.SetActive(false);
        player.restartScene();
    }
}
