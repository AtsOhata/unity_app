using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionButton : MonoBehaviour
{
    TouchDetect TD;

    public string ScreenName;

    public void Initialize(string optionScreenName)
    {
        TD = GetComponent<TouchDetect>();

        ScreenName = optionScreenName;
    }

    /// <summary>タッチされたら次の画面名を返す</summary>
    public bool CheckTouch()
    {
        return TD.DetectTouch();
    }
}
