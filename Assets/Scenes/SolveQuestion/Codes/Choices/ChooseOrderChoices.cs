using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>���בւ����</summary>
public class ChooseOrderChoices : MonoBehaviour
{
    /// <summary>�I�����v���n�u</summary>
    public GameObject Choice;

    /// <summary>�I�����X�N���v�g���X�g</summary>
    List<ChooseOrderChoices_Choice> ChoiceList = new List<ChooseOrderChoices_Choice>();

    /// <summary>����</summary>
    List<string> Answers = new List<string>();

    public void Initialize(List<string> texts)
    {
        float Interval = 14f;  // �I�����ԗ]��
        float UpperLowerMargin = 7f;  // �㉺�̗]��
        float ChoiceHeight = Choice.transform.GetComponent<RectTransform>().sizeDelta.y;  // �I�����̍���

        // �I��������5,9,13...�ƂȂ�ɂ�č����������Ȃ�
        RectTransform r = transform.GetComponent<RectTransform>();
        r.sizeDelta = new Vector2(r.sizeDelta.x, ChoiceHeight + UpperLowerMargin * 2 + ((texts.Count - 1) / 4) * (ChoiceHeight + Interval * 2));

        // �^����ꂽ�I�����e�L�X�g�̕��I�����v���n�u��ݒu����
        for (int i = 0; i < texts.Count; i++)
        {
            GameObject choice = Instantiate(Choice, transform);  // �I�����̍쐬
            choice.GetComponent<ChooseOrderChoices_Choice>().Initialize(i, texts[i], Interval, r.sizeDelta.y);  // �I�����̏�����
            choice.transform.localScale = Vector3.Scale(Choice.transform.localScale, new Vector3(1.0f, 1.0f, 1.0f));  // ���@�ŃT�C�Y������ɕύX����Ă��܂��΍�
            ChoiceList.Add(choice.GetComponent<ChooseOrderChoices_Choice>());
        }
    }

    /// <summary>����</summary>
    public List<string> CheckTouch()
    {
        bool orderChangeFlag = false;
        ChoiceList.ForEach(x => x.CheckTouch(Answers, ref orderChangeFlag));  // �^�b�`��
        if (orderChangeFlag) ChoiceList.ForEach(x => x.ChangeOrder(Answers));  // �ǂꂩ���^�b�`���ꂽ�珇���ύX
        return Answers;
    }
 
    /// <summary>�t�B�[�h�o�b�N</summary>
    public void Feedback(List<string> Answers)
    {
        ChoiceList.ForEach(x =>
        {
            x.Feedback(Answers);
            x.ChangeOrder(Answers);
        });
    }
}
