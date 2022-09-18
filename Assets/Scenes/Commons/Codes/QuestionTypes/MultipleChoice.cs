using System;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
/// <summary>多肢選択問題クラス</summary>
public class MultipleChoice : QuestionBase
{
    /// <summary>正答プール</summary>
    public List<string> CorrectChoicePool;
    /// <summary>間違いプール</summary>
    public List<string> WrongChoicePool;
    /// <summary>間違い選択肢数</summary>
    public int WrongChoiceCount;
    /// <summary>選択肢</summary>
    public List<string> Choices;

    /// <summary>選択肢を自動的に作成する</summary>
    /// <returns>成功でtrue　エラーでfalse</returns>
    public override bool Initialize()
    {
        if (Question != null)
            Question = Question.Trim();
        Choices = Choices.Select(x => x.Trim()).ToList();

        // 正答選択肢数が未設定か多すぎる時正答選択肢の数に合わせる
        if (PlayerChooseQuantity < 1 || CorrectChoicePool.Count < PlayerChooseQuantity)
        {
            PlayerChooseQuantity = CorrectChoicePool.Count;
        }
        // 間違い選択肢数が未設定か多すぎる時間違い選択肢の数に合わせる
        if (WrongChoiceCount < 1 || WrongChoicePool.Count < WrongChoiceCount)
        {
            WrongChoiceCount = WrongChoicePool.Count;
        }
        // 選択肢がxmlファイルに記述されていない場合、または不正である場合に新たに選択肢を作る
        if (Choices.Count < 1 || Choices.Count > PlayerChooseQuantity + WrongChoiceCount)
        {
            Choices = new List<string>();  // 初期化
            CorrectChoicePool = CorrectChoicePool.OrderBy(i => Guid.NewGuid()).ToList();  // 正答プールの中身を乱数で入れ替える
            for (int i = 0; i < PlayerChooseQuantity; i++)
            {
                Choices.Add(CorrectChoicePool[i]);
            }
            WrongChoicePool = WrongChoicePool.OrderBy(i => Guid.NewGuid()).ToList();  // 間違いプールの中身を乱数で入れ替える
            for (int i = 0; i < WrongChoiceCount; i++)
            {
                Choices.Add(WrongChoicePool[i]);
            }
            Choices = Choices.OrderBy(i => Guid.NewGuid()).ToList();  // 選択肢の中身を乱数で入れ替える
        }
        // 選択肢の数が１以下の場合falseを返す
        if (Choices.Count <= 1) return false;
        return true;
    }

}
