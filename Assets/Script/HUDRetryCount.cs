using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using PlayerStats;

public class HUDRetryCount : MonoBehaviour {
    public void Start() {
        uint count = GameStats.levelRestart.Count > SceneManager.GetActiveScene().buildIndex?GameStats.levelRestart[SceneManager.GetActiveScene().buildIndex]:0;
        gameObject.GetComponent<TMP_Text>().text = count + (count>1?" Restarts":" Restart");
    }
}