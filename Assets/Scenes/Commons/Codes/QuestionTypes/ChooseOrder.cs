using System;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
/// <summary>並べ替え問題クラス</summary>
public class ChooseOrder : QuestionBase
{
    /// <summary>正しい並び順</summary>
    public string CorrectOrder;
    /// <summary>ダミー選択肢</summary>
    public string DummyChoices;
    /// <summary>選択肢数</summary>
    public int ChoiceQuantity;

    /// <summary>実際の選択肢(xmlには記述なし)</summary>
    public List<string> Choices;

    /// <summary>選択肢を作成する</summary>
    /// <returns>成功でtrue　エラーでfalse</returns>
    public override bool Initialize()
    {
        // 記述がない場合の要素の初期値
        if (Question == null)
            Question = "";
        else
            Question = Question.Trim();

        if (ChoiceQuantity == 0)
            ChoiceQuantity = 9999;

        // 正しい並び順(CorrectOrder)とダミー選択肢(DummyChoices)から指定数(ChoiceQuantity)選択肢を作成する
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
        Choices = Utility.Randomize(Choices);  // 選択肢の中身を乱数で入れ替える

        // プレイヤー回答数に記述がなければ正答を正しい順に並べ替える問題にする
        if (PlayerChooseQuantity == 0)
        {
            PlayerChooseQuantity = CorrectOrder.Split(',').ToList().Count;
        }

        return true;
    }
}
