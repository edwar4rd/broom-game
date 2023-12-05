using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using PlayerStats;
using System;

public class HUDTimer : MonoBehaviour {
    private DateTime startTime;
    public void Start() {
        startTime = DateTime.Now;
        gameObject.GetComponent<TMP_Text>().text = "00:00.00";
    }

    public void Update() {
        gameObject.GetComponent<TMP_Text>().text = (DateTime.Now-startTime).ToString("hh\\:mm\\:ss\\.ff");
    }
}