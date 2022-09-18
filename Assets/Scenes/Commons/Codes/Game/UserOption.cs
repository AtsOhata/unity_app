/// <summary>���[�U�[�ݒ�</summary>
[System.Serializable]
public class UserOption
{
    /// <summary>���[�U�[�ݒ肪�����Ă���C���X�^���X</summary>
    public static UserOption Instance;

    /// <summary>���[�U�[�ݒ�t�@�C����</summary>
    readonly static string OptionFileName = "UserOption.xml";

    /// <summary>�P�x�ɏo������</summary>
    public int QLQuestionAmount;

    /// <summary>����</summary>
    public float SoundValue;

    /// <summary>�񓚎��b���\���t���O</summary>
    public bool SecondShowFlag;

    public UserOption()
    {
        QLQuestionAmount = 10;
        SoundValue = 1.0f;
        SecondShowFlag = true;
    }

    /// <summary>�ݒ��ǂݍ���</summary>
    /// <returns>�ݒ�̏���Option�N���X�ɕϊ���������</returns>
    static public UserOption Load()
    {
        UserOption data = FileUtility.LoadXml<UserOption>(OptionFileName, false);
        // �ݒ�f�[�^��������΍쐬����
        if (data == null)
        {
            data = new UserOption();
            FileUtility.SaveXml(OptionFileName, data);
        }
        return data;
    }

    /// <summary>�Z�[�u����</summary>
    /// <param name="optionXml">�Z�[�u����Option�N���X</param>
    static public void Save()
    {
        FileUtility.SaveXml(OptionFileName, Instance);
    }

}
