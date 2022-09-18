using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>選択肢パーツ</summary>
public class MultipleChoices_Choice : MonoBehaviour
{
    TouchDetect TD;
    /// <summary>奇数回タッチされたらtrue</summary>
    bool IsChosen = false;
    /// <summary>タッチされる前の元々の色</summary>
    Color ColorBeforeTouch;
    /// <summary>選択肢の文言</summary>
    string Text;

    /// <summary>初期処理</summary>
    /// <param name="Number">何番目の選択肢か(場所決めに使用)</param>
    /// <param name="Text">選択肢の文言</param>
    public void Initialize(int Number, string Text, float Interval, float WholeHeight)
    {
        this.Text = Text;

        // 選択肢の位置
        float width = GetComponent<RectTransform>().sizeDelta.x;
        float horizontaInterval = 20;
        float height = GetComponent<RectTransform>().sizeDelta.y;
        float x;
        if(Number % 2 == 0) x = - (width + horizontaInterval) / 2;
        else x = (width + horizontaInterval) / 2;
        float y = WholeHeight / 2 - (Interval / 2 + height / 2) - (Interval + height) * (Number / 2);
        GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);

        transform.Find("Text").GetComponent<Text>().text = Text;  // テキスト反映
        TD = GetComponentInChildren<TouchDetect>();
        ColorBeforeTouch = GetComponentInChildren<Image>().color;
    }
    /// <summary>タッチ時挙動</summary>
    public void CheckTouch(List<string> Answers)
    {
        // タッチされたら選択肢の色が変わり選択肢のテキストを出力
        if(TD.DetectTouch())
        {
            IsChosen = !IsChosen;

            if (IsChosen)
            {
                GetComponentInChildren<Image>().color = Color.gray;
                Answers.Add(Text);
            }
            else
            {
                GetComponentInChildren<Image>().color = ColorBeforeTouch;
                Answers.RemoveAt(Answers.FindLastIndex(x => x == Text));
            }
        }
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