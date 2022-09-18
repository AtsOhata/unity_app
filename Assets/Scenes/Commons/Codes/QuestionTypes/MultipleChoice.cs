using System;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
/// <summary>�����I����N���X</summary>
public class MultipleChoice : QuestionBase
{
    /// <summary>�����v�[��</summary>
    public List<string> CorrectChoicePool;
    /// <summary>�ԈႢ�v�[��</summary>
    public List<string> WrongChoicePool;
    /// <summary>�ԈႢ�I������</summary>
    public int WrongChoiceCount;
    /// <summary>�I����</summary>
    public List<string> Choices;

    /// <summary>�I�����������I�ɍ쐬����</summary>
    /// <returns>������true�@�G���[��false</returns>
    public override bool Initialize()
    {
        if (Question != null)
            Question = Question.Trim();
        Choices = Choices.Select(x => x.Trim()).ToList();

        // �����I�����������ݒ肩�������鎞�����I�����̐��ɍ��킹��
        if (PlayerChooseQuantity < 1 || CorrectChoicePool.Count < PlayerChooseQuantity)
        {
            PlayerChooseQuantity = CorrectChoicePool.Count;
        }
        // �ԈႢ�I�����������ݒ肩�������鎞�ԈႢ�I�����̐��ɍ��킹��
        if (WrongChoiceCount < 1 || WrongChoicePool.Count < WrongChoiceCount)
        {
            WrongChoiceCount = WrongChoicePool.Count;
        }
        // �I������xml�t�@�C���ɋL�q����Ă��Ȃ��ꍇ�A�܂��͕s���ł���ꍇ�ɐV���ɑI���������
        if (Choices.Count < 1 || Choices.Count > PlayerChooseQuantity + WrongChoiceCount)
        {
            Choices = new List<string>();  // ������
            CorrectChoicePool = CorrectChoicePool.OrderBy(i => Guid.NewGuid()).ToList();  // �����v�[���̒��g�𗐐��œ���ւ���
            for (int i = 0; i < PlayerChooseQuantity; i++)
            {
                Choices.Add(CorrectChoicePool[i]);
            }
            WrongChoicePool = WrongChoicePool.OrderBy(i => Guid.NewGuid()).ToList();  // �ԈႢ�v�[���̒��g�𗐐��œ���ւ���
            for (int i = 0; i < WrongChoiceCount; i++)
            {
                Choices.Add(WrongChoicePool[i]);
            }
            Choices = Choices.OrderBy(i => Guid.NewGuid()).ToList();  // �I�����̒��g�𗐐��œ���ւ���
        }
        // �I�����̐����P�ȉ��̏ꍇfalse��Ԃ�
        if (Choices.Count <= 1) return false;
        return true;
    }

}
