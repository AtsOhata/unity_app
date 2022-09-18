using System.Collections.Generic;

/// <summary>
/// �z�[����ʂ̃p�[�c��񂪋L�ڂ���Ă���
/// </summary>lkmn 
[System.Serializable]
public class Home_DTO
{
    /// <summary>���݂̉�ʖ�</summary>
    public string CurrentScreenName;

    /// <summary>��ʐݒ胊�X�g</summary>
    public List<ScreenConfig> ScreenConfigs = new List<ScreenConfig>();

    /// <summary>��ʕϑJ����<br></br>���Ƀz�[����ʂɗ���Ƃ��Ɍ��̔z�u�ɖ߂��p</summary>
    public List<string> PreviousScreenNames = new List<string>();

    /// <summary>�p�[�c���X�g</summary>
    public List<Part> Parts = new List<Part>();

    public class ScreenConfig
    {
        /// <summary>
        /// �o�������ʖ�<br></br>
        /// ���X�̉�ʂɖ��̂��t�����Ă���A�Ή�������ʂɉ����ăp�[�c���o������<br></br>
        /// �ŏ��̉�ʂ̉�ʖ���""<br></br>
        /// </summary>
        public string ScreenName;
        /// <summary>
        /// ��ʃ^�C�v 1-�؂�ւ� 2-�X�N���[��
        /// </summary>
        public int ScreenType = 2;

        /// <summary>
        /// �X�N���[���l
        /// </summary>
        public float ScreenValue = 1f;
    }

    /// <summary>�p�[�c</summary>
    public class Part
    {
        /// <summary>
        /// �o�������ʖ�<br></br>
        /// ScreenConfig.ScreenName�ƈ�v����
        /// </summary>
        public string ScreenName;

        /// <summary>
        /// �p�[�c�^�C�v<br></br>
        /// �g�p����v���n�u���w�肷��<br></br>
        /// </summary>
        public string PartType;

        /// <summary>
        /// �p�[�c���g�p����l<br></br>
        /// �p�[�c�ו��̎d�l�m�����`�B�Ɏg�p����<br></br>
        /// </summary>
        public List<string> Values;
    }

    /// <summary>�f�[�^��ǂݍ���</summary>
    /// <returns>Home_DTO.xml�̏���Home_DTO�N���X�ɕϊ���������</returns>
    static public Home_DTO Load()
    {
        return FileUtility.LoadXml<Home_DTO>("Home_DTO", true);
    }
}






