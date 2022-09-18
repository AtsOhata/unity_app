using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_QuestionCounter : MonoBehaviour
{
    Text t1, t2;

    public int CurrentQuestion;
    public int QuestionQuantity;

    /// <summary>���݂̖�萔�^��萔�@�̂t�h�p�[�c</summary>
    /// <param name="QuestionQuantity">�S�Ă̖�萔</param>
    public void Initialize(int QuestionQuantity)
    {
        this.QuestionQuantity = QuestionQuantity;

        // Text�R���|�[�l���g�擾
        t1 = transform.Find("CurrentQuestion").gameObject.GetComponent<Text>();
        t2 = transform.Find("QuestionQuantity").gameObject.GetComponent<Text>();

        // ���݂̖�萔���O�ɁA��萔�����߂�ꂽ���ɂ���
        t1.text = "0";
        t2.text = this.QuestionQuantity + "";
    }

    /// <summary>���݂̖�萔���e�L�X�g���f����</summary>
    public void AddOne()
    {
        t1.text = ++CurrentQuestion + "";
    }

}
