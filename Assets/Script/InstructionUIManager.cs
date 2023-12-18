using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerStats;
using UnityEngine.InputSystem;

public class InstructionUIManager : MonoBehaviour {
    public InputActionAsset actions;
    public GameObject InstructionUI;
    // Start is called before the first frame update
    void Start() {
        if (UIStats.playerSeenInstruction) {
            InstructionUI.SetActive(false);
            Time.timeScale = 1f;
        }
        else {
            Time.timeScale = 0f;
            actions.FindActionMap("LevelInput")["CloseInstructionUI"].started += CloseInstructionUI;
            actions.FindActionMap("LevelInput")["CloseInstructionUI"].Enable();
        }
    }

    public void CloseInstructionUI() {
        if (enabled) {
            actions.FindActionMap("LevelInput")["CloseInstructionUI"].started -= CloseInstructionUI;
            actions.FindActionMap("LevelInput")["CloseInstructionUI"].Disable();
            UIStats.playerSeenInstruction = true;
            InstructionUI.SetActive(false);
            Time.timeScale = 1f;
        }
    }
    public void CloseInstructionUI(InputAction.CallbackContext context) {
        if (this == null)
            return;

        CloseInstructionUI();
    }
}
