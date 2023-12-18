using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleColliders : MonoBehaviour
{
    private bool showColliders = false;

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Y))
        // {
        //     showColliders = !showColliders;
        //     ToggleAllColliders(showColliders);
        //     Debug.Log("顯示碰撞框："+showColliders);
        // }
    }

    void ToggleAllColliders(bool show)
    {
        Collider2D[] allColliders = FindObjectsOfType<Collider2D>();

        foreach (Collider2D collider in allColliders)
        {
            collider.enabled = show;
        }
    }
}