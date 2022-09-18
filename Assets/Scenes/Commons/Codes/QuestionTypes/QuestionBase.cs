using System.Xml.Serialization;

/// <summary>���̒��ۃN���X</summary>
[XmlInclude(typeof(OneQuestionOneAnswer))]
[XmlInclude(typeof(MultipleChoice))]
[XmlInclude(typeof(Description))]
[XmlInclude(typeof(ChooseOrder))]
public abstract class QuestionBase
{
    /// <summary>���</summary>
    public string Question;

    /// <summary>��蕶������<br/>null -> MiddleCenter<br/>UpperLeft UpperCenter UpperRight<br/>MiddleLeft MiddleCenter MiddleRight<br/>LowerLeft LowerCenter LowerRight</summary>
    public UnityEngine.TextAnchor QuestionAlignment = UnityEngine.TextAnchor.MiddleCenter;

    /// <summary>
    /// ���ӏ���<br></br>
    /// ��育�ƂɂȂɂ����ӂ⒍�߂��K�v�ȏꍇ�ɕt������
    /// ��: �L�q����ۂɗp��Ԃ�,�ŋ�؂�
    /// </summary>
    public string Caution;

    /// <summary>�v���C���[�I���</summary>
    public int PlayerChooseQuantity;

    /// <summary>���̏������i�I�����̍쐬�Ȃǁj
    /// <returns>�G���[�Ȃ�false</returns>
    public abstract bool Initialize();

}
