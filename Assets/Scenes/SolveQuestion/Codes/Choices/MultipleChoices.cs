using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>多肢選択問題</summary>
public class MultipleChoices : MonoBehaviour
{
    /// <summary>選択肢プレハブ</summary>
    public GameObject Choice;

    /// <summary>選択肢間の距離</summary>
    public float Interval = 10f;

    /// <summary>選択肢スクリプトリスト</summary>
    List<MultipleChoices_Choice> ChoiceList = new List<MultipleChoices_Choice>();

    /// <summary>答え</summary>
    List<string> Answers = new List<string>();

    public void Initialize(List<string> texts)
    {
        RectTransform r = transform.GetComponent<RectTransform>();

        // 与えられた選択肢テキストの分選択肢プレハブを設置する
        for (int i = 0; i < texts.Count; i++)
        {
            GameObject choice = Instantiate(Choice, transform);  // 選択肢の作成
            choice.GetComponent<MultipleChoices_Choice>().Initialize(i, texts[i], Interval, r.sizeDelta.y);  // 選択肢の初期化
            choice.transform.localScale = Vector3.Scale(Choice.transform.localScale, new Vector3(1.0f, 1.0f, 1.0f));  // 実機でサイズが勝手に変更されてしまう対策
            ChoiceList.Add(choice.GetComponent<MultipleChoices_Choice>());
        }
    }

    /// <summary>挙動</summary>
    public List<string> CheckTouch()
    {
        ChoiceList.ForEach(x => x.CheckTouch(Answers));  // タッチ時
        return Answers;
    }

    /// <summary>フィードバック</summary>
    public void Feedback(List<string> Answers)
    {
        ChoiceList.ForEach(x => x.Feedback(Answers));
    }
}
