using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlayerStats {
    public static class UIStats {
        public static bool playerSeenInstruction { get; set; } = false;
    }
    public static class GameStats {
        public static uint totalRestart { get; set; } = 0;
        public static uint totalFail { get; set; } = 0;
        public static List<uint> levelRestart { get; set; } = new List<uint>(SceneManager.sceneCountInBuildSettings);
        public static List<uint> levelFail { get; set; } = new List<uint>(SceneManager.sceneCountInBuildSettings);

        public static List<float> startTimeStamps { get; set; } = new List<float>(SceneManager.sceneCountInBuildSettings);
        public static List<float> restartTimeStamps { get; set; } = new List<float>(SceneManager.sceneCountInBuildSettings);
        public static List<float> finishTimeStamps { get; set; } = new List<float>(SceneManager.sceneCountInBuildSettings);
        public static string scoreStr = "SCORE IS VERY GOOD";
    }
}
