using UnityEngine;

public class OO_OKNG : MonoBehaviour
{
    // ‚n‚j^‚m‚f
    GameObject OK, NG;
    TouchDetect OKTD, NGTD;

    // ƒTƒEƒ“ƒh
    GameObject Sound;

    /// <summary>‰Šú‰»‚·‚é</summary>
    public void Initialize(GameObject Sound)
    {
        // ƒQ[ƒ€ƒIƒuƒWƒFƒNƒg‚ÆƒRƒ“ƒ|[ƒlƒ“ƒg‚Ìæ“¾
        OK = transform.Find("OK").gameObject;  // ‚n‚j‰æ‘œ
        NG = transform.Find("NG").gameObject;  // ‚m‚f‰æ‘œ
        OKTD = OK.GetComponent<TouchDetect>();  // ‚n‚jTouchDetect
        NGTD = NG.GetComponent<TouchDetect>();  // ‚m‚fTouchDetect

        this.Sound = Sound;
    }

    /// <summary>‚n‚j‚ªƒ^ƒbƒ`‚³‚ê‚½‚ç"OK"A‚m‚f‚ªƒ^ƒbƒ`‚³‚ê‚½‚ç"NG"‚Æ•Ô‚·</summary>
    /// <returns>"OK"E"NG"E""‚Ì‚¢‚¸‚ê‚©</returns>
    public string CheckTouch()
    {
        if (OKTD.DetectTouch())
        {
            // ‚n‚j‰¹‚ğ–Â‚ç‚µ‚Ä‚n‚j‚Æ•Ô‚·
            Sound.GetComponent<SolveQuestion_Sound>().PlaySound(SolveQuestion_Sound.SOUND.OK);
            return "OK";
        }
        if (NGTD.DetectTouch())
        {
            // ‚m‚f‰¹‚ğ–Â‚ç‚µ‚Ä‚m‚f‚Æ•Ô‚·
            Sound.GetComponent<SolveQuestion_Sound>().PlaySound(SolveQuestion_Sound.SOUND.NG);
            return "NG";
        }
        return "";
    }
}
