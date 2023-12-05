using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerStats;

public class DeathUIManager : MonoBehaviour {
    public GameObject DeathUI;
    public PlayerManager player;
    // Start is called before the first frame update
    void Start() {
        DeathUI.SetActive(false);
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
