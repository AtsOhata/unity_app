using System;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
/// <summary>���בւ����N���X</summary>
public class ChooseOrder : QuestionBase
{
    /// <summary>���������я�</summary>
    public string CorrectOrder;
    /// <summary>�_�~�[�I����</summary>
    public string DummyChoices;
    /// <summary>�I������</summary>
    public int ChoiceQuantity;

    /// <summary>���ۂ̑I����(xml�ɂ͋L�q�Ȃ�)</summary>
    public List<string> Choices;

    /// <summary>�I�������쐬����</summary>
    /// <returns>������true�@�G���[��false</returns>
    public override bool Initialize()
    {
        // �L�q���Ȃ��ꍇ�̗v�f�̏����l
        if (Question == null)
            Question = "";
        else
            Question = Question.Trim();

        if (ChoiceQuantity == 0)
            ChoiceQuantity = 9999;

        // ���������я�(CorrectOrder)�ƃ_�~�[�I����(DummyChoices)����w�萔(ChoiceQuantity)�I�������쐬����
        Choices.AddRange(CorrectOrder.Split(','));
        if (DummyChoices != null)
        {
            List<string> dummyChoiceList = Utility.Randomize(DummyChoices.Split(',').ToList());
            int i = 0;
            while (ChoiceQuantity > Choices.Count && i < dummyChoiceList.Count)
            {
                Choices.Add(dummyChoiceList[i]);
                i++;
            }
        }
        Choices = Utility.Randomize(Choices);  // �I�����̒��g�𗐐��œ���ւ���

        // �v���C���[�񓚐��ɋL�q���Ȃ���ΐ����𐳂������ɕ��בւ�����ɂ���
        if (PlayerChooseQuantity == 0)
        {
            PlayerChooseQuantity = CorrectOrder.Split(',').ToList().Count;
        }

        return true;
    }
}
