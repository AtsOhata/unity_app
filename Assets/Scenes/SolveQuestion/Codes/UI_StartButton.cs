using UnityEngine;

public class UI_StartButton : MonoBehaviour
{
    TouchDetect td;

    /// <summary>����������</summary>
    public void Initialize()
    {
        td = GetComponent<TouchDetect>();
    }

    /// <summary>�^�b�`���ꂽ��J�n�{�^�������o���ď�����</summary>
    public bool CheckTouch(GameObject Sound)
    {
        if (td.DetectTouch())
        {
            Sound.GetComponent<SolveQuestion_Sound>().PlaySound(SolveQuestion_Sound.SOUND.START_BUTTON);
            Destroy(gameObject);
            return true;
        }
        return false;
    }
}
