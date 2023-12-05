using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using LevelData;

public class LevelText : MonoBehaviour {
    public void Start() {
        gameObject.GetComponent<TMP_Text>().text = LevelTexts.levelNames[SceneManager.GetActiveScene().buildIndex];
    }
}