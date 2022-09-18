using UnityEngine;
using UnityEngine.UI;

/// <summary>�ݒ�:�b���\�����邩�ǂ���
public class HomeOptionSecondShowFlag : MonoBehaviour, OptionInterface
{
    /// <summary>�{�^����TouchDetect</summary>
    TouchDetect TD;

    /// <summary>ON/OFF</summary>
    Text OnOffText;

    public void Initialize()
    {
        TD = transform.Find("Button").GetComponent<TouchDetect>();

        OnOffText = transform.Find("Button").GetComponentInChildren<Text>();
    }

    public void Act()
    { 
        // �{�^���ɐG�ꂽ��t���O�؂�ւ�
        if(TD.DetectTouch())
        {
            UserOption.Instance.SecondShowFlag = !UserOption.Instance.SecondShowFlag;
            OnOffText.text = UserOption.Instance.SecondShowFlag ? "ON" : "OFF";
            UserOption.Save();
        }
    }
}
