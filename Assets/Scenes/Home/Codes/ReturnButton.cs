using System.Collections.Generic;
using UnityEngine;

public class ReturnButton : MonoBehaviour
{
    TouchDetect TD;

    /// <summary>�J�ڑO��ʖ�</summary>
    public List<string> PreviousScreenNames;

    public void Initialize()
    {
        TD = GetComponent<TouchDetect>();
        if (GlobalVariables.Home_DTO.PreviousScreenNames.Count < 1)
            PreviousScreenNames = new List<string>() { };  // �_�~�[��������
        else
            PreviousScreenNames = GlobalVariables.Home_DTO.PreviousScreenNames;
    }

    /// <summary>�J�ڑO��ʖ���������</summary>
    /// <param name="ScreenName">�J�ڑO��ʖ�</param>
    public void AddPreviouScreenName(string ScreenName)
    {
        PreviousScreenNames.Add(ScreenName);
    }

    /// <summary>�^�b�`���ꂽ��true</summary>
    public bool CheckTouch()
    {
        if (PreviousScreenNames.Count >= 1)
            return TD.DetectTouch();
        else
            return false;
    }

    /// <summary>�O�̉�ʂɖ߂鎞�Ɏg��</summary>
    /// <returns>�O�̉�ʂ̖���</returns>
    public string ReturnScreen()
    {
        string result = PreviousScreenNames[PreviousScreenNames.Count - 1];
        PreviousScreenNames.RemoveAt(PreviousScreenNames.Count - 1);
        return result;
    }
}
