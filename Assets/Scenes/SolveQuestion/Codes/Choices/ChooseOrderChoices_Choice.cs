using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>���בւ��I����</summary>
public class ChooseOrderChoices_Choice : MonoBehaviour
{
    TouchDetect TD;

    /// <summary>�I�΂�Ă��邩�ǂ���<br></br>���^�b�`���ꂽ��true</summary>
    bool IsChosen = false;

    /// <summary>�I�����̕���</summary>
    string Text;

    /// <summary>�^�b�`�����O�̌��X�̐F</summary>
    Color ColorBeforeTouch;

    /// <summary>��������</summary>
    /// <param name="Number">���Ԗڂ̑I������</param>
    /// <param name="Text">�I�����̕���</param>
    public void Initialize(int Number, string Text, float Interval, float WholeHeight)
    {
        this.Text = Text;
        GetComponent<RectTransform>().anchoredPosition = new Vector2(-60 + Number % 4 * 40, WholeHeight / 2 - (Interval / 2 + GetComponent<RectTransform>().sizeDelta.y / 2) - (Interval + GetComponent<RectTransform>().sizeDelta.y) * (Number / 4));  // �I�����̈ʒu
        transform.Find("Text").GetComponent<Text>().text = Text;  // �e�L�X�g���f
        TD = GetComponentInChildren<TouchDetect>();
        ColorBeforeTouch = transform.Find("Button").GetComponent<Image>().color;
    }

    /// <summary>�^�b�`������</summary>
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

    /// <summary>�����ύX</summary>
    public void ChangeOrder(List<string> Answers)
    {
        if (!Answers.Contains(Text))
            transform.Find("Order").GetComponent<Text>().text = "";
        else
            transform.Find("Order").GetComponent<Text>().text = StringUtility.ConvertToFullWidth((Answers.IndexOf(Text) + 1).ToString());
    }

    public void Feedback(List<string> Answers)
    {
        // �����̒��Ɋ܂܂�Ă����物�F�ɂ��A����ȊO�͌��X�̐F�ɂ���
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
