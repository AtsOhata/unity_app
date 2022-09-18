using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAchievementsButton : MonoBehaviour
{
    TouchDetect TD;

    public string ScreenName;

    public void Initialize(string achievementScreenName)
    {
        TD = GetComponent<TouchDetect>();

        ScreenName = achievementScreenName;
    }

    /// <summary>ƒ^ƒbƒ`‚³‚ê‚½‚çŽŸ‚Ì‰æ–Ê–¼‚ð•Ô‚·</summary>
    public bool CheckTouch()
    {
        return TD.DetectTouch();
    }
}
