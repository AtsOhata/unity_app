using System.Collections.Generic;

[System.Serializable]
/// <summary>�L�q���N���X</summary>
public class Description : QuestionBase
{
    /// <summary>�����v�[��</summary>
    public List<string> CorrectChoicePool;

    public override bool Initialize()
    {
        Question = Question.Trim();
        return true;
    }
}
