


/// <summary>���ꓚ�N���X</summary>
[System.Serializable]
public class OneQuestionOneAnswer : QuestionBase
{
    /// <summary>����</summary>
    public string Answer;

    public override bool Initialize()
    {
        Question = Question.Trim();
        Answer = Answer.Trim();
        return true;
    }
}
