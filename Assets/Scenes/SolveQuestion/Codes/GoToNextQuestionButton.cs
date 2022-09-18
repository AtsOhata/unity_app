using UnityEngine;
using UnityEngine.UI;

/// <summary>答え合わせボタン</summary>
public class GoToNextQuestionButton : MonoBehaviour
{
    TouchDetect TD;

    public void Initialize(int ButtonType)
    {
        // コンポーネントの取得
        TD = transform.Find("Button").GetComponent<TouchDetect>();

        // ボタンタイプにより文言の変更
        if(ButtonType == 1)
        {
            transform.GetComponentInChildren<Text>().text = "次へ";
        }
    }

    /// <summary>タッチされたらtrue</summary>
    public bool CheckTouch()
    {
        return TD.DetectTouch();
    }
}
