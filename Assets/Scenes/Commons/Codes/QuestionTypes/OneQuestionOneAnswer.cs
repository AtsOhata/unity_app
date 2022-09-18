


/// <summary>一問一答クラス</summary>
[System.Serializable]
public class OneQuestionOneAnswer : QuestionBase
{
    /// <summary>答え</summary>
    public string Answer;

    public override bool Initialize()
    {
        Question = Question.Trim();
        Answer = Answer.Trim();
        return true;
    }
}
