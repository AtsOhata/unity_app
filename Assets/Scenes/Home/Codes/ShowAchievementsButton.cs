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

    /// <summary>タッチされたら次の画面名を返す</summary>
    public bool CheckTouch()
    {
        return TD.DetectTouch();
    }
}
