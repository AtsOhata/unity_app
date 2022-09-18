using UnityEngine;
using UnityEngine.UI;

/// <summary>Home�V�[����Option��ʂ̖���</summary>
public class QLQuestionAmount : MonoBehaviour, OptionInterface
{
    /// <summary>�{�{�^��</summary>
    TouchDetect Plus;
    /// <summary>�|�{�^��</summary>
    TouchDetect Minus;
    /// <summary>����</summary>
    Text AmountText;

    int Amount;

    readonly int MaxAmount = 50;

    public void Initialize()
    {
        Plus = transform.Find("Plus").gameObject.GetComponent<TouchDetect>();
        Minus = transform.Find("Minus").gameObject.GetComponent<TouchDetect>();
        AmountText = transform.Find("Amount").GetComponent<Text>();

        // �ŏ��ɕ\������l
        Amount = UserOption.Instance.QLQuestionAmount;
        AmountText.text = Amount + "";
        
    }

    /// <summary>�{/�|�{�^���扟�ɂĖ��ʂ̏㉺</summary>
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
