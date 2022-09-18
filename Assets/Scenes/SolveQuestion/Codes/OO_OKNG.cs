using UnityEngine;

public class OO_OKNG : MonoBehaviour
{
    // �n�j�^�m�f
    GameObject OK, NG;
    TouchDetect OKTD, NGTD;

    // �T�E���h
    GameObject Sound;

    /// <summary>����������</summary>
    public void Initialize(GameObject Sound)
    {
        // �Q�[���I�u�W�F�N�g�ƃR���|�[�l���g�̎擾
        OK = transform.Find("OK").gameObject;  // �n�j�摜
        NG = transform.Find("NG").gameObject;  // �m�f�摜
        OKTD = OK.GetComponent<TouchDetect>();  // �n�jTouchDetect
        NGTD = NG.GetComponent<TouchDetect>();  // �m�fTouchDetect

        this.Sound = Sound;
    }

    /// <summary>�n�j���^�b�`���ꂽ��"OK"�A�m�f���^�b�`���ꂽ��"NG"�ƕԂ�</summary>
    /// <returns>"OK"�E"NG"�E""�̂����ꂩ</returns>
    public string CheckTouch()
    {
        if (OKTD.DetectTouch())
        {
            // �n�j����炵�Ăn�j�ƕԂ�
            Sound.GetComponent<SolveQuestion_Sound>().PlaySound(SolveQuestion_Sound.SOUND.OK);
            return "OK";
        }
        if (NGTD.DetectTouch())
        {
            // �m�f����炵�Ăm�f�ƕԂ�
            Sound.GetComponent<SolveQuestion_Sound>().PlaySound(SolveQuestion_Sound.SOUND.NG);
            return "NG";
        }
        return "";
    }
}
