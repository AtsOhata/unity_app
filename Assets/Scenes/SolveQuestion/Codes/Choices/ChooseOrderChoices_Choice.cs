using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>並べ替え選択肢</summary>
public class ChooseOrderChoices_Choice : MonoBehaviour
{
    TouchDetect TD;

    /// <summary>選ばれているかどうか<br></br>奇数回タッチされたらtrue</summary>
    bool IsChosen = false;

    /// <summary>選択肢の文言</summary>
    string Text;

    /// <summary>タッチされる前の元々の色</summary>
    Color ColorBeforeTouch;

    /// <summary>初期処理</summary>
    /// <param name="Number">何番目の選択肢か</param>
    /// <param name="Text">選択肢の文言</param>
    public void Initialize(int Number, string Text, float Interval, float WholeHeight)
    {
        this.Text = Text;
        GetComponent<RectTransform>().anchoredPosition = new Vector2(-60 + Number % 4 * 40, WholeHeight / 2 - (Interval / 2 + GetComponent<RectTransform>().sizeDelta.y / 2) - (Interval + GetComponent<RectTransform>().sizeDelta.y) * (Number / 4));  // 選択肢の位置
        transform.Find("Text").GetComponent<Text>().text = Text;  // テキスト反映
        TD = GetComponentInChildren<TouchDetect>();
        ColorBeforeTouch = transform.Find("Button").GetComponent<Image>().color;
    }

    /// <summary>タッチ時挙動</summary>
    public void CheckTouch(List<string> Answers, ref bool OrderChangeFlag)
    {
        if (TD.DetectTouch())
        {
            OrderChangeFlag = true;
            IsChosen = !IsChosen;
            
            if (IsChosen)
            {
                transform.Find("Button").GetComponent<Image>().color = Color.yellow;
                Answers.Add(Text);
            }
            else
            {
                transform.Find("Button").GetComponent<Image>().color = ColorBeforeTouch;
                Answers.RemoveAt(Answers.FindLastIndex(x => x == Text));
            }
        }
    }

    /// <summary>順序変更</summary>
    public void ChangeOrder(List<string> Answers)
    {
        if (!Answers.Contains(Text))
            transform.Find("Order").GetComponent<Text>().text = "";
        else
            transform.Find("Order").GetComponent<Text>().text = StringUtility.ConvertToFullWidth((Answers.IndexOf(Text) + 1).ToString());
    }

    public void Feedback(List<string> Answers)
    {
        // 正解の中に含まれていたら黄色にし、それ以外は元々の色にする
        if (Answers.Contains(Text))
        {
            GetComponentInChildren<Image>().color = Color.yellow;
        }
        else
        {
            GetComponentInChildren<Image>().color = ColorBeforeTouch;
        }
    }
}
