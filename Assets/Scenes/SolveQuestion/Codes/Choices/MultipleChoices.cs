using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>�����I����</summary>
public class MultipleChoices : MonoBehaviour
{
    /// <summary>�I�����v���n�u</summary>
    public GameObject Choice;

    /// <summary>�I�����Ԃ̋���</summary>
    public float Interval = 10f;

    /// <summary>�I�����X�N���v�g���X�g</summary>
    List<MultipleChoices_Choice> ChoiceList = new List<MultipleChoices_Choice>();

    /// <summary>����</summary>
    List<string> Answers = new List<string>();

    public void Initialize(List<string> texts)
    {
        RectTransform r = transform.GetComponent<RectTransform>();

        // �^����ꂽ�I�����e�L�X�g�̕��I�����v���n�u��ݒu����
        for (int i = 0; i < texts.Count; i++)
        {
            GameObject choice = Instantiate(Choice, transform);  // �I�����̍쐬
            choice.GetComponent<MultipleChoices_Choice>().Initialize(i, texts[i], Interval, r.sizeDelta.y);  // �I�����̏�����
            choice.transform.localScale = Vector3.Scale(Choice.transform.localScale, new Vector3(1.0f, 1.0f, 1.0f));  // ���@�ŃT�C�Y������ɕύX����Ă��܂��΍�
            ChoiceList.Add(choice.GetComponent<MultipleChoices_Choice>());
        }
    }

    /// <summary>����</summary>
    public List<string> CheckTouch()
    {
        ChoiceList.ForEach(x => x.CheckTouch(Answers));  // �^�b�`��
        return Answers;
    }

    /// <summary>�t�B�[�h�o�b�N</summary>
    public void Feedback(List<string> Answers)
    {
        ChoiceList.ForEach(x => x.Feedback(Answers));
    }
}
