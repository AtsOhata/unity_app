using UnityEngine;

public class UI_StartButton : MonoBehaviour
{
    TouchDetect td;

    /// <summary>初期化する</summary>
    public void Initialize()
    {
        td = GetComponent<TouchDetect>();
    }

    /// <summary>タッチされたら開始ボタン音を出して消える</summary>
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
