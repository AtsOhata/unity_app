using System.Collections.Generic;

/// <summary>
/// QL�̒��̑S�Ă̖��
/// </summary>
[System.Serializable]
public class QuestionSet
{
    /// <summary>�����_���t���O<br></br>
    /// true�Ȃ烉���_���ɏo��@false�Ȃ珇�ɏo��</summary>
    public bool RandomFlag;

    /// <summary>�ݒ�̖�����l���t���O<br></br>
    /// 1�Ȃ��������K�p�����@0�Ȃ�K�p����Ȃ����ߐ����ȂǂɎg����</summary>
    public bool UpperLimitFlag;

    /// <summary>���Ɋւ�����Q</summary>
    public List<QuestionPack> QuestionPacks = new List<QuestionPack>();

    /// <summary>���Ɋւ�����</summary>
    public class QuestionPack
    {
        /// <summary>�ʂ���</summary>
        public string Alias;
        /// <summary>�摜</summary>
        public string Image;
        /// <summary>�ǂݕ�(�����ǉ��Ȃ�)</summary>
        public string ReadingMaterial;
        /// <summary>�����J�ڃ{�^���̎��<br></br>
        /// �f�t�H���g(�܂���1) -�u�����v �����Ɠ������킹���Ď���ʂɑJ�ڂ���<br></br>
        /// 2 - �u���ցv�@���̉�ʂɑJ�ڂ���
        /// </summary>
        public int GoToNextQuestionButtonType;
        /// <summary>���</summary>
        public List<QuestionBase> QuestionBases = new List<QuestionBase>();
    }

    /// <summary>�f�[�^��ǂݍ���</summary>
    /// <returns>xml�̏����^�ɕϊ���������</returns>
    static public QuestionSet Load(string FileName)
    {
        return FileUtility.LoadXml<QuestionSet>(FileName);
    }
}
