using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 実績ボタン<br></br>
/// タッチされたら実績概要を伝達箱に伝える
/// </summary>
public class Achievement : MonoBehaviour
{
    /// <summary>実績名テキストコンポーネント</summary>
    public Text AchievementNameComponent;
    /// <summary>実績概要テキストコンポーネント</summary>
    public Text AchievementSummaryComponent;

    TouchDetect TD;

    /// <summary>実績名</summary>
    string AchievementName;
    /// <summary>実績概要</summary>
    string AchievementSummary;

    /// <summary>実績の初期化</summary>
    /// <param name="values">[0] = 実績名　[1] = 実績概要</param>
    public void Initialize(List<string> values, Text achievementNameComponent, Text achievementSummaryComponent)
    {
        // 最初にSavedataに実績があるかどうかを見に行き、なければSavedataに足す
        if (!Savedata.Instance.AchievementDatas.Exists(x => x.Name == values[2]))
        {
            Savedata.Instance.AchievementDatas.Add(new Savedata.AchievementData(values[2], 0));
            Savedata.Save(Savedata.Instance);
        }

        TD = GetComponent<TouchDetect>();

        Savedata.AchievementData achievement = Savedata.Instance.AchievementDatas.Find(x => x.Name == values[2]);
        // 達成率の算出
        achievement.CalcPercent(Savedata.Instance.LearnedWords);

        AchievementName = values[0];
        AchievementSummary = values[1] + " 現在" + achievement.Percent.ToString("F3") + "％";
        AchievementNameComponent = achievementNameComponent;
        AchievementSummaryComponent = achievementSummaryComponent;

        // 実績率 < 実績達成目標　なら透明度を下げる
        float a = achievement.Percent < achievement.TargetPercent ? 0.5f : 1f;
        transform.GetComponentInChildren<Image>().color = new Color(1.0f, 1.0f, 1.0f, a);
    }

    /// <summary>タッチされたら実績概要名と実績概要を伝達箱に伝える</summary>
    public void CheckTouch()
    {
       if (TD.DetectTouch())
       {
            AchievementNameComponent.text = AchievementName;  // 実績名
            AchievementSummaryComponent.text = AchievementSummary;  // 実績概要
       }
    }
}
