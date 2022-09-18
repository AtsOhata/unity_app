using System.Collections;
using UnityEngine;

public class Feedback : MonoBehaviour
{
    // �n�j�^�m�f
    GameObject OK, NG;

    // �T�E���h
    GameObject Sound;

    // �R���[�`��
    Coroutine c_1;

    /// <summary>����������</summary>
    public void Initialize(GameObject Sound)
    {
        // �Q�[���I�u�W�F�N�g�ƃR���|�[�l���g�̎擾
        OK = transform.Find("OK").gameObject;  // �n�j�摜
        NG = transform.Find("NG").gameObject;  // �m�f�摜

        this.Sound = Sound;
        Set();
    }

    /// <summary>�t�B�[�h�o�b�N</summary>
    /// <param name="IsCorrect">�����Ȃ�^�A�ԈႢ�Ȃ�U</param>
    /// <param name="Time">�����ԈႢ�摜�̒񎦎���</param>
    public void GiveFeedback(bool IsCorrect, float Time = 0.4f)
    {
        if (c_1 != null) StopCoroutine(c_1);
        Set();
        if (IsCorrect)
        {
            c_1 = StartCoroutine(Feedback_OK(Time));
        }
        else
        {
            c_1 = StartCoroutine(Feedback_NG(Time));
        }
    }

    IEnumerator Feedback_OK(float ShowTime)
    {
        // �n�j����炷
        Sound.GetComponent<SolveQuestion_Sound>().PlaySound(SolveQuestion_Sound.SOUND.OK);
        // ShowTime���߂���܂ŉ摜�\��
        float elapsedTime = 0;
        OK.SetActive(true);
        while (elapsedTime < ShowTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        OK.SetActive(false);

        yield break;
    }

    IEnumerator Feedback_NG(float ShowTime)
    {
        // �m�f����炷
        Sound.GetComponent<SolveQuestion_Sound>().PlaySound(SolveQuestion_Sound.SOUND.NG);
        // ShowTime���߂���܂ŉ摜�\��
        float elapsedTime = 0;
        NG.SetActive(true);
        while (elapsedTime < ShowTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        NG.SetActive(false);

        yield break;
    }

    void Set()
    {
        OK.SetActive(false);
        NG.SetActive(false);
    }
}
