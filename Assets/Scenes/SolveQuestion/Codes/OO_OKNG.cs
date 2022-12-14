using UnityEngine;

public class OO_OKNG : MonoBehaviour
{
    // ＯＫ／ＮＧ
    GameObject OK, NG;
    TouchDetect OKTD, NGTD;

    // サウンド
    GameObject Sound;

    /// <summary>初期化する</summary>
    public void Initialize(GameObject Sound)
    {
        // ゲームオブジェクトとコンポーネントの取得
        OK = transform.Find("OK").gameObject;  // ＯＫ画像
        NG = transform.Find("NG").gameObject;  // ＮＧ画像
        OKTD = OK.GetComponent<TouchDetect>();  // ＯＫTouchDetect
        NGTD = NG.GetComponent<TouchDetect>();  // ＮＧTouchDetect

        this.Sound = Sound;
    }

    /// <summary>ＯＫがタッチされたら"OK"、ＮＧがタッチされたら"NG"と返す</summary>
    /// <returns>"OK"・"NG"・""のいずれか</returns>
    public string CheckTouch()
    {
        if (OKTD.DetectTouch())
        {
            // ＯＫ音を鳴らしてＯＫと返す
            Sound.GetComponent<SolveQuestion_Sound>().PlaySound(SolveQuestion_Sound.SOUND.OK);
            return "OK";
        }
        if (NGTD.DetectTouch())
        {
            // ＮＧ音を鳴らしてＮＧと返す
            Sound.GetComponent<SolveQuestion_Sound>().PlaySound(SolveQuestion_Sound.SOUND.NG);
            return "NG";
        }
        return "";
    }
}
