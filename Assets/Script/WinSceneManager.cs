using System.Collections;
using System.Collections.Generic;
using PlayerStats;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;
using TMPro;

public class WinSceneManager : MonoBehaviour {
    [DllImport("__Internal")]
    private static extern void CopyToClipboard(string textToCopy);

    public InputActionAsset actions;
    public TMP_Text info_text;
    private InputActionMap winActionMap;

    public List<ParticleSystem> particleSystems;
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
        Debug.Log(GameStats.totalTime);
        Debug.Log(GameStats.totalRestart);
        info_text.text = "Total Time: " + GameStats.totalTime + "s\nTotal Restart: " + GameStats.totalRestart + " time(s)";
    }

    public void BackToMenu(InputAction.CallbackContext context) {
        if (this == null)
            return;
        winActionMap.Disable();
        Celebrate();
        Debug.Log("Going back to menu...");
        SceneManager.LoadScene(0);
    }

    public void CopyScore(InputAction.CallbackContext context) {
        Debug.Log("Copy score to clipboard...");
        GUIUtility.systemCopyBuffer = GameStats.scoreStr.Trim();
        CopyToClipboard(GameStats.scoreStr.Trim());
    }
    public void Celebrate(InputAction.CallbackContext context) {
        if (this == null)
            return;
        Celebrate();
    }

    public void Celebrate() {
        Debug.Log("YOU WIN!!!");
        foreach (ParticleSystem particleSystem in particleSystems) {
            particleSystem.Play();
        }
        Debug.Log("Celebrate!!!");
    }
}