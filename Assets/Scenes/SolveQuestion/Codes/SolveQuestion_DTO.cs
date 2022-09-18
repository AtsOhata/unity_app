using System.Collections.Generic;

public class SolveQuestion_DTO
{
    /// <summary>QL����</summary>
    public string QLTitle;

    /// <summary>�o��Z�b�g(QS)</summary>
    public class QS
    {
        /// <summary>�ʂ���</summary>
        public string Alias;
        /// <summary>���^�C�v</summary>
        public QT QT;
        /// <summary>�摜</summary>
        public string Image;
        /// <summary>�ǉ�����</summary>
        public string ReadingMaterial;
        /// <summary>���</summary>
        public string Question;
        /// <summary>��蕶������</summary>
        public UnityEngine.TextAnchor QuestionAlignment;
        /// <summary>���ӏ���</summary>
        public string Caution;
        /// <summary>������ׂ���</summary>
        public int PlayerChooseQuantity;
        /// <summary>�I����</summary>
        public List<string> Choices = new List<string>();
        /// <summary>����</summary>
        public List<string> Answers = new List<string>();
        /// <summary>����ʑJ�ڃ{�^���^�C�v</summary>
        public int GoToNextQuestionButtonType;
    }
    public List<QS> QSs = new List<QS>();

    public enum QT
    {
        NON_QUESTION,  // ��肪�Ȃ�
        ONE_QUESTION_ONE_ANSWER,  // ���ꓚ
        MULTIPLE_CHOICE,  // �����I��
        DESCRIPTION,  // ���R�L�q
        CHOOSE_ORDER  // ���ԑI��
    }
}
