using System.Collections.Generic;

/// <summary>
/// Scene_ShowAnswer ��VO�N���X
/// Scene_ShowAnswer�N���X�ɓn�������ϐ��������ɓo�^����
/// </summary>
public class ShowAnswer_DTO
{
    /// <summary>���񓚐�</summary>
    public int GrossAnswerQuantity;

    /// <summary>QL����</summary>
    public string QLTitle;

    /// <summary>���񓚏��N���X</summary>
    public class QuestionResult
    {
        /// <summary>���� true=���� false=�ԈႢ</summary>
        public bool IsCorrect;
        /// <summary>���</summary>
        public string Question;
        /// <summary>��蕶������</summary>
        public UnityEngine.TextAnchor QuestionAlignment;
        /// <summary>�摜</summary>
        public string Picture;
        /// <summary>�v���C���[�̉�</summary>
        public List<string> PlayerAnswers;
        /// <summary>����I����</summary>
        public List<string> CorrectAnswers;
        /// <summary>�I����</summary>
        public List<string> Choices;
        /// <summary>�����Ɋ|����������</summary>
        public float TimeForAnswer;
    }

    /// <summary>���񓚏��</summary>
    public List<QuestionResult> QR = new List<QuestionResult>();

}
