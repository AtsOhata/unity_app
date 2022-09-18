using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QB : MonoBehaviour
{
    TouchDetect TD;

    /// <summary>ŽŸ‚Ì‰æ–Ê–¼</summary>
    public string NextScreenName;

    public void Initialize(List<string> values)
    {
        TD = GetComponent<TouchDetect>();

        transform.Find("Text").GetComponent<Text>().text = values[0];

        NextScreenName = values[1];
    }

    /// <summary>ƒ^ƒbƒ`‚³‚ê‚½‚çŽŸ‚Ì‰æ–Ê–¼‚ð•Ô‚·</summary>
    public bool CheckTouch()
    {
        return TD.DetectTouch();
    }
}
