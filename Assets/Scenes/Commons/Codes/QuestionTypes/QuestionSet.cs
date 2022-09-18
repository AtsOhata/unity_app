using System.Collections.Generic;

/// <summary>
/// QLの中の全ての問題
/// </summary>
[System.Serializable]
public class QuestionSet
{
    /// <summary>ランダムフラグ<br></br>
    /// trueならランダムに出題　falseなら順に出題</summary>
    public bool RandomFlag;

    /// <summary>設定の問題上限考慮フラグ<br></br>
    /// 1なら問題上限が適用される　0なら適用されないため説明などに使える</summary>
    public bool UpperLimitFlag;

    /// <summary>問題に関する情報群</summary>
    public List<QuestionPack> QuestionPacks = new List<QuestionPack>();

    /// <summary>問題に関する情報</summary>
    public class QuestionPack
    {
        /// <summary>通し名</summary>
        public string Alias;
        /// <summary>画像</summary>
        public string Image;
        /// <summary>読み物(長文読解など)</summary>
        public string ReadingMaterial;
        /// <summary>次問題遷移ボタンの種類<br></br>
        /// デフォルト(または1) -「答合」 押すと答え合わせして次画面に遷移する<br></br>
        /// 2 - 「次へ」　次の画面に遷移する
        /// </summary>
        public int GoToNextQuestionButtonType;
        /// <summary>問題</summary>
        public List<QuestionBase> QuestionBases = new List<QuestionBase>();
    }

    /// <summary>データを読み込む</summary>
    /// <returns>xmlの情報を型に変換したもの</returns>
    static public QuestionSet Load(string FileName)
    {
        return FileUtility.LoadXml<QuestionSet>(FileName);
    }
}
