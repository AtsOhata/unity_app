using System.Collections.Generic;

[System.Serializable]
/// <summary>記述問題クラス</summary>
public class Description : QuestionBase
{
    /// <summary>正答プール</summary>
    public List<string> CorrectChoicePool;

    public override bool Initialize()
    {
        Question = Question.Trim();
        return true;
    }
}
