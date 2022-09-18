using System.Collections.Generic;
using UnityEngine;

public class ReturnButton : MonoBehaviour
{
    TouchDetect TD;

    /// <summary>遷移前画面名</summary>
    public List<string> PreviousScreenNames;

    public void Initialize()
    {
        TD = GetComponent<TouchDetect>();
        if (GlobalVariables.Home_DTO.PreviousScreenNames.Count < 1)
            PreviousScreenNames = new List<string>() { };  // ダミーを加える
        else
            PreviousScreenNames = GlobalVariables.Home_DTO.PreviousScreenNames;
    }

    /// <summary>遷移前画面名を加える</summary>
    /// <param name="ScreenName">遷移前画面名</param>
    public void AddPreviouScreenName(string ScreenName)
    {
        PreviousScreenNames.Add(ScreenName);
    }

    /// <summary>タッチされたらtrue</summary>
    public bool CheckTouch()
    {
        if (PreviousScreenNames.Count >= 1)
            return TD.DetectTouch();
        else
            return false;
    }

    /// <summary>前の画面に戻る時に使う</summary>
    /// <returns>前の画面の名称</returns>
    public string ReturnScreen()
    {
        string result = PreviousScreenNames[PreviousScreenNames.Count - 1];
        PreviousScreenNames.RemoveAt(PreviousScreenNames.Count - 1);
        return result;
    }
}
