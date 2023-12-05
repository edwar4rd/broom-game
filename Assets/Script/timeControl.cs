using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeControl : MonoBehaviour
{
    private bool isSlowed = false;
    private float originalTimeScale;

    void Start()
    {
        originalTimeScale = 1.0f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U) && !isSlowed)
        {
            // 减慢游戏时间一半
            Time.timeScale = 0.5f;
            isSlowed = true;

            // 在1秒钟后恢复正常游戏速度
            Invoke(nameof(ResetTimeScale), 1.0f);
        }
    }

    void ResetTimeScale()
    {
        // 恢复正常游戏速度
        Time.timeScale = originalTimeScale;
        isSlowed = false;
    }
}
