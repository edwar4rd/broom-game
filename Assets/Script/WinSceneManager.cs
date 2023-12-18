using System.Collections;
using System.Collections.Generic;
using PlayerStats;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class WinSceneManager : MonoBehaviour {
    public InputActionAsset actions;
    private InputActionMap winActionMap;
    // Start is called before the first frame update
    void Start() {
        foreach (InputActionMap actionmap in actions.actionMaps) {
            actionmap.Disable();
        }
        winActionMap = actions.FindActionMap("WinInput");
        winActionMap["BackToMenu"].performed += BackToMenu;
        winActionMap["CopyScore"].performed += CopyScore;
        winActionMap["Celebrate"].performed += Celebrate;
        winActionMap.Enable();
    }

    public void BackToMenu(InputAction.CallbackContext context) {
        if (this == null)
            return;
        winActionMap.Disable();
        Debug.Log("Going back to menu...");
        SceneManager.LoadScene(0);
    }
    public void CopyScore(InputAction.CallbackContext context) {
        Debug.Log("Copy score to clipboard...");
        CopyText(GameStats.scoreStr);
    }
    public void Celebrate(InputAction.CallbackContext context) {
        Debug.Log("YOU WIN!!!");
    }

    private void CopyText(string textToCopy) {
        GUIUtility.systemCopyBuffer = textToCopy;
    }
}