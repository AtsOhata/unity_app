using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayMode : MonoBehaviour
{
    TouchDetect TD;

    string NextScreenName;

    public void Initialize(List<string> values)
    {
        TD = GetComponent<TouchDetect>();

        transform.Find("Text").GetComponent<Text>().text = values[0];

        NextScreenName = values[1];
    }

    public string CheckTouch()
    {
        return TD.DetectTouch() ? NextScreenName : "";
    }
}
