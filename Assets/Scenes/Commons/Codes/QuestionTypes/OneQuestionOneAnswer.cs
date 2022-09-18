


/// <summary>ˆê–âˆê“šƒNƒ‰ƒX</summary>
[System.Serializable]
public class OneQuestionOneAnswer : QuestionBase
{
    /// <summary>“š‚¦</summary>
    public string Answer;

    public override bool Initialize()
    {
        Question = Question.Trim();
        Answer = Answer.Trim();
        return true;
    }
}
