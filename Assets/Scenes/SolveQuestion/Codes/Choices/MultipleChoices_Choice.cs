using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>�I�����p�[�c</summary>
public class MultipleChoices_Choice : MonoBehaviour
{
    TouchDetect TD;
    /// <summary>���^�b�`���ꂽ��true</summary>
    bool IsChosen = false;
    /// <summary>�^�b�`�����O�̌��X�̐F</summary>
    Color ColorBeforeTouch;
    /// <summary>�I�����̕���</summary>
    string Text;

    /// <summary>��������</summary>
    /// <param name="Number">���Ԗڂ̑I������(�ꏊ���߂Ɏg�p)</param>
    /// <param name="Text">�I�����̕���</param>
    public void Initialize(int Number, string Text, float Interval, float WholeHeight)
    {
        this.Text = Text;

        // �I�����̈ʒu
        float width = GetComponent<RectTransform>().sizeDelta.x;
        float horizontaInterval = 20;
        float height = GetComponent<RectTransform>().sizeDelta.y;
        float x;
        if(Number % 2 == 0) x = - (width + horizontaInterval) / 2;
        else x = (width + horizontaInterval) / 2;
        float y = WholeHeight / 2 - (Interval / 2 + height / 2) - (Interval + height) * (Number / 2);
        GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);

        transform.Find("Text").GetComponent<Text>().text = Text;  // �e�L�X�g���f
        TD = GetComponentInChildren<TouchDetect>();
        ColorBeforeTouch = GetComponentInChildren<Image>().color;
    }
    /// <summary>�^�b�`������</summary>
    public void CheckTouch(List<string> Answers)
    {
        // �^�b�`���ꂽ��I�����̐F���ς��I�����̃e�L�X�g���o��
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