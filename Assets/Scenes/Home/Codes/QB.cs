using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QB : MonoBehaviour
{
    TouchDetect TD;

    /// <summary>次の画面名</summary>
    public string NextScreenName;

    public void Initialize(List<string> values)
    {
        TD = GetComponent<TouchDetect>();

        transform.Find("Text").GetComponent<Text>().text = values[0];

        NextScreenName = values[1];
    }

    /// <summary>タッチされたら次の画面名を返す</summary>
    public bool CheckTouch()
    {
        return TD.DetectTouch();
    }
}
