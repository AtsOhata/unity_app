using System.Collections.Generic;

public class SolveQuestion_DTO
{
    /// <summary>QL名称</summary>
    public string QLTitle;

    /// <summary>出題セット(QS)</summary>
    public class QS
    {
        /// <summary>通し名</summary>
        public string Alias;
        /// <summary>問題タイプ</summary>
        public QT QT;
        /// <summary>画像</summary>
        public string Image;
        /// <summary>読解資料</summary>
        public string ReadingMaterial;
        /// <summary>問題</summary>
        public string Question;
        /// <summary>問題文字揃え</summary>
        public UnityEngine.TextAnchor QuestionAlignment;
        /// <summary>注意書き</summary>
        public string Caution;
        /// <summary>答えるべき数</summary>
        public int PlayerChooseQuantity;
        /// <summary>選択肢</summary>
        public List<string> Choices = new List<string>();
        /// <summary>答え</summary>
        public List<string> Answers = new List<string>();
        /// <summary>次画面遷移ボタンタイプ</summary>
        public int GoToNextQuestionButtonType;
    }
    public List<QS> QSs = new List<QS>();

    public enum QT
    {
        NON_QUESTION,  // 問題がない
        ONE_QUESTION_ONE_ANSWER,  // 一問一答
        MULTIPLE_CHOICE,  // 多肢選択
        DESCRIPTION,  // 自由記述
        CHOOSE_ORDER  // 順番選択
    }
}
