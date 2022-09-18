using UnityEngine;
using UnityEngine.UI;

/// <summary>�������킹�{�^��</summary>
public class GoToNextQuestionButton : MonoBehaviour
{
    TouchDetect TD;

    public void Initialize(int ButtonType)
    {
        // �R���|�[�l���g�̎擾
        TD = transform.Find("Button").GetComponent<TouchDetect>();

        // �{�^���^�C�v�ɂ�蕶���̕ύX
        if(ButtonType == 1)
        {
            transform.GetComponentInChildren<Text>().text = "����";
        }
    }

    /// <summary>�^�b�`���ꂽ��true</summary>
    public bool CheckTouch()
    {
        return TD.DetectTouch();
    }
}
