using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QB : MonoBehaviour
{
    TouchDetect TD;

    /// <summary>���̉�ʖ�</summary>
    public string NextScreenName;

    public void Initialize(List<string> values)
    {
        TD = GetComponent<TouchDetect>();

        transform.Find("Text").GetComponent<Text>().text = values[0];

        NextScreenName = values[1];
    }

    /// <summary>�^�b�`���ꂽ�玟�̉�ʖ���Ԃ�</summary>
    public bool CheckTouch()
    {
        return TD.DetectTouch();
    }
}
