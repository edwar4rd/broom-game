using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerStats;
using TMPro;
using LevelData;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class DeathUIManager : MonoBehaviour {
    public GameObject DeathUI;
    public TMP_Text RestartButtonText;
    public PlayerManager player;
    public InputActionAsset actions;
    // Start is called before the first frame update
    void Start() {
        DeathUI.SetActive(false);
        actions["CloseDeathUI"].started += CloseDeathUI;
        actions["CloseDeathUI"].Enable();

        uint failCount = GameStats.levelFail.Count > SceneManager.GetActiveScene().buildIndex ? GameStats.levelFail[SceneManager.GetActiveScene().buildIndex] : 0;
        RestartButtonText.text = RetryTexts.retryButton[SceneManager.GetActiveScene().buildIndex, failCount < 5 ? failCount : 4];
    }

    public void CloseDeathUI() {
        if (DeathUI.activeInHierarchy) {
            Debug.Log("Closing Death UI");
            actions["CloseDeathUI"].started -= CloseDeathUI;
            actions["CloseDeathUI"].Disable();
            DeathUI.SetActive(false);
        }
    }

    public void CloseDeathUI(InputAction.CallbackContext context) {
        if (this == null)
            return;
        CloseDeathUI();
    }

    public void CloseDeathUIWithClick() {
        DeathUI.SetActive(false);
        player.restartScene();
    }
}
