using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �V�[���Ԃŕێ����Ă��������ϐ����i�[����N���X
/// </summary>
public class GlobalVariables : MonoBehaviour
{
    /// <summary>�A�v���̐ݒ�(Resources���擾)</summary>
    static public Dictionary<string, string> AppConstants = new Dictionary<string, string>();

    /// <summary>��ʑJ�ڂɕK�v�ȏ��</summary>
    static public Home_DTO Home_DTO = new Home_DTO();
    /// <summary>��ʑJ�ڂɕK�v�ȏ��</summary>
    static public LoadQuestion_DTO LoadQuestion_DTO = new LoadQuestion_DTO();
    /// <summary>��ʑJ�ڂɕK�v�ȏ��</summary>
    static public SolveQuestion_DTO SolveQuestion_DTO = new SolveQuestion_DTO();
    /// <summary>��ʑJ�ڂɕK�v�ȏ��</summary>
    static public ShowAnswer_DTO ShowAnswer_DTO = new ShowAnswer_DTO();

    void Start()
    {
        // �V�[���ύX���Ă��j������Ȃ�
        DontDestroyOnLoad(this);
    }

}
