using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionButton : MonoBehaviour
{
    TouchDetect TD;

    public string ScreenName;

    public void Initialize(string optionScreenName)
    {
        TD = GetComponent<TouchDetect>();

        ScreenName = optionScreenName;
    }

    /// <summary>�^�b�`���ꂽ�玟�̉�ʖ���Ԃ�</summary>
    public bool CheckTouch()
    {
        return TD.DetectTouch();
    }
}
