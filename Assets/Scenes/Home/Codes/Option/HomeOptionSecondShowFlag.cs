using UnityEngine;
using UnityEngine.UI;

/// <summary>設定:秒数表示するかどうか
public class HomeOptionSecondShowFlag : MonoBehaviour, OptionInterface
{
    /// <summary>ボタンのTouchDetect</summary>
    TouchDetect TD;

    /// <summary>ON/OFF</summary>
    Text OnOffText;

    public void Initialize()
    {
        TD = transform.Find("Button").GetComponent<TouchDetect>();

        OnOffText = transform.Find("Button").GetComponentInChildren<Text>();
    }

    public void Act()
    { 
        // ボタンに触れたらフラグ切り替え
        if(TD.DetectTouch())
        {
            UserOption.Instance.SecondShowFlag = !UserOption.Instance.SecondShowFlag;
            OnOffText.text = UserOption.Instance.SecondShowFlag ? "ON" : "OFF";
            UserOption.Save();
        }
    }
}
