using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>並べ替え問題</summary>
public class ChooseOrderChoices : MonoBehaviour
{
    /// <summary>選択肢プレハブ</summary>
    public GameObject Choice;

    /// <summary>選択肢スクリプトリスト</summary>
    List<ChooseOrderChoices_Choice> ChoiceList = new List<ChooseOrderChoices_Choice>();

    /// <summary>答え</summary>
    List<string> Answers = new List<string>();

    public void Initialize(List<string> texts)
    {
        float Interval = 14f;  // 選択肢間余白
        float UpperLowerMargin = 7f;  // 上下の余白
        float ChoiceHeight = Choice.transform.GetComponent<RectTransform>().sizeDelta.y;  // 選択肢の高さ

        // 選択肢数が5,9,13...となるにつれて高さが高くなる
        RectTransform r = transform.GetComponent<RectTransform>();
        r.sizeDelta = new Vector2(r.sizeDelta.x, ChoiceHeight + UpperLowerMargin * 2 + ((texts.Count - 1) / 4) * (ChoiceHeight + Interval * 2));

        // 与えられた選択肢テキストの分選択肢プレハブを設置する
        for (int i = 0; i < texts.Count; i++)
        {
            GameObject choice = Instantiate(Choice, transform);  // 選択肢の作成
            choice.GetComponent<ChooseOrderChoices_Choice>().Initialize(i, texts[i], Interval, r.sizeDelta.y);  // 選択肢の初期化
            choice.transform.localScale = Vector3.Scale(Choice.transform.localScale, new Vector3(1.0f, 1.0f, 1.0f));  // 実機でサイズが勝手に変更されてしまう対策
            ChoiceList.Add(choice.GetComponent<ChooseOrderChoices_Choice>());
        }
    }

    /// <summary>挙動</summary>
    public List<string> CheckTouch()
    {
        bool orderChangeFlag = false;
        ChoiceList.ForEach(x => x.CheckTouch(Answers, ref orderChangeFlag));  // タッチ時
        if (orderChangeFlag) ChoiceList.ForEach(x => x.ChangeOrder(Answers));  // どれかがタッチされたら順序変更
        return Answers;
    }
 
    /// <summary>フィードバック</summary>
    public void Feedback(List<string> Answers)
    {
        ChoiceList.ForEach(x =>
        {
            x.Feedback(Answers);
            x.ChangeOrder(Answers);
        });
    }
}
