using UnityEngine;
using UnityEngine.UI;

/// <summary>HomeシーンのOption画面の問題量</summary>
public class QLQuestionAmount : MonoBehaviour, OptionInterface
{
    /// <summary>＋ボタン</summary>
    TouchDetect Plus;
    /// <summary>－ボタン</summary>
    TouchDetect Minus;
    /// <summary>問題量</summary>
    Text AmountText;

    int Amount;

    readonly int MaxAmount = 50;

    public void Initialize()
    {
        Plus = transform.Find("Plus").gameObject.GetComponent<TouchDetect>();
        Minus = transform.Find("Minus").gameObject.GetComponent<TouchDetect>();
        AmountText = transform.Find("Amount").GetComponent<Text>();

        // 最初に表示する値
        Amount = UserOption.Instance.QLQuestionAmount;
        AmountText.text = Amount + "";
        
    }

    /// <summary>＋/－ボタン捺押にて問題量の上下</summary>
    public void Act()
    {
        if(Plus.DetectTouch() && Amount < MaxAmount)
        {
            Amount += 10;
            AmountText.text = Amount + "";
            UserOption.Instance.QLQuestionAmount = Amount;
            UserOption.Save();
        }
        if(Minus.DetectTouch() && Amount > 10)
        {
            Amount -= 10;
            AmountText.text = Amount + "";
            UserOption.Instance.QLQuestionAmount = Amount;
            UserOption.Save();
        }
    }
}
