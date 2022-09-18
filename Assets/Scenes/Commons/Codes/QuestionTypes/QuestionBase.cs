using System.Xml.Serialization;

/// <summary>問題の抽象クラス</summary>
[XmlInclude(typeof(OneQuestionOneAnswer))]
[XmlInclude(typeof(MultipleChoice))]
[XmlInclude(typeof(Description))]
[XmlInclude(typeof(ChooseOrder))]
public abstract class QuestionBase
{
    /// <summary>問題</summary>
    public string Question;

    /// <summary>問題文字揃え<br/>null -> MiddleCenter<br/>UpperLeft UpperCenter UpperRight<br/>MiddleLeft MiddleCenter MiddleRight<br/>LowerLeft LowerCenter LowerRight</summary>
    public UnityEngine.TextAnchor QuestionAlignment = UnityEngine.TextAnchor.MiddleCenter;

    /// <summary>
    /// 注意書き<br></br>
    /// 問題ごとになにか注意や注釈が必要な場合に付加する
    /// 例: 記述する際に用語間は,で区切れ
    /// </summary>
    public string Caution;

    /// <summary>プレイヤー選択量</summary>
    public int PlayerChooseQuantity;

    /// <summary>問題の初期化（選択肢の作成など）
    /// <returns>エラーならfalse</returns>
    public abstract bool Initialize();

}
