using System.Collections.Generic;

/// <summary>
/// Scene_ShowAnswer のVOクラス
/// Scene_ShowAnswerクラスに渡したい変数をここに登録する
/// </summary>
public class ShowAnswer_DTO
{
    /// <summary>総回答数</summary>
    public int GrossAnswerQuantity;

    /// <summary>QL名称</summary>
    public string QLTitle;

    /// <summary>問題回答情報クラス</summary>
    public class QuestionResult
    {
        /// <summary>結果 true=正解 false=間違い</summary>
        public bool IsCorrect;
        /// <summary>問題</summary>
        public string Question;
        /// <summary>問題文字揃え</summary>
        public UnityEngine.TextAnchor QuestionAlignment;
        /// <summary>画像</summary>
        public string Picture;
        /// <summary>プレイヤーの回答</summary>
        public List<string> PlayerAnswers;
        /// <summary>正解選択肢</summary>
        public List<string> CorrectAnswers;
        /// <summary>選択肢</summary>
        public List<string> Choices;
        /// <summary>答えに掛かった時間</summary>
        public float TimeForAnswer;
    }

    /// <summary>問題回答情報</summary>
    public List<QuestionResult> QR = new List<QuestionResult>();

}
