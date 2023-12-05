using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelData {
    public static class LevelTexts {
        public readonly static string[] levelNames = { "", "Level 1-1", "Level 1-2", "Level 1-3", "Level 2-1", "Level 2-2", "Level 2-3" };
    }

    public static class RetryTexts {
        public readonly static string[,] retryButton = {{"", "", "", "", ""}, {"重玩 從第一關開始", "提示： DF旋轉 JK左右", "提示： 一直按著 -> 一直死", "第一關沒啥好提示的...", "加油..."}, {"我會乖乖看路", "提示：碰到門就過關囉", "提示：F=ma （之類的）", "提示：（插入國中物理）", "加油..."}, {"提示：站得直，跳得高", "跳高高！", "你需要一點兔子腳嗎", "累了就靠牆休息一下吧...", "跳啊跳啊跳..."}, {"地面溼滑", "Caution Wet Floor", "插入日文翻譯", "建議：不要碰到水", "水啦！"}, {"我調過了，你得從第一根柱子跳到第二根", "兩根柱子中間可以休息", "休息完怎麽出來是你的事", "聽說直接跳過水坑比較快", "加油..."}, {"跳啊跳", "跳啊跳啊跳", "直接跳過去就行囉", "跳啊跳啊跳跳", "跳啊跳啊跳跳跳！"}};
    }
}
