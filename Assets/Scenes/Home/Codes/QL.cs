using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QL : MonoBehaviour
{
    TouchDetect TD;

    public string QLTitle;
    public string QLPath;

    /// <summary>QL初期化</summary>
    /// <param name="values">
    ///   values[0] = QLTitle<br></br>
    ///   values[1] = QLPath<br></br>
    /// </param>
    public void Initialize(List<string> values)
    {
        TD = GetComponent<TouchDetect>();

        // テキスト反映
        transform.Find("Text").GetComponent<Text>().text = values[0];

        // 情報取得
        QLTitle = values[0];
        QLPath = values[1];

        // 該当するQLDataがSavedataにあるかどうかを見に行き、なければSavedataに足す
        if (!Savedata.Instance.QLDatas.Exists(x => x.QLTitle == QLTitle))
        {
            Savedata.Instance.QLDatas.Add(new Savedata.QLData(QLTitle, 0));
            Savedata.Save(Savedata.Instance);
        }
        Savedata.QLData qldata = Savedata.Instance.QLDatas.Find(x => x.QLTitle == QLTitle);
        // 達成率の算出
        qldata.CalcPercent(Savedata.Instance.LearnedWords);

        // 達成率をスライダーに反映する
        // 達成率が０ならスライダーの色を紛らわしいため隠す
        if (qldata.Percent <= 0f)
            transform.Find("Slider/Fill Area/Fill").gameObject.SetActive(false);
        else
            transform.Find("Slider").GetComponent<Slider>().value = qldata.Percent;

        // 達成率が80%以上であればトロフィー表示
        if (qldata.Percent >= 0.8)
            transform.Find("Image").gameObject.SetActive(true);
        else
            transform.Find("Image").gameObject.SetActive(false);
    }

    /// <summary>タッチされたら次の画面名を返す</summary>
    public string CheckTouch()
    {
        return TD.DetectTouch() ? QLPath : "";
    }
}
