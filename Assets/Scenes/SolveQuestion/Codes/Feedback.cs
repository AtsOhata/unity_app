using System.Collections;
using UnityEngine;

public class Feedback : MonoBehaviour
{
    // ＯＫ／ＮＧ
    GameObject OK, NG;

    // サウンド
    GameObject Sound;

    // コルーチン
    Coroutine c_1;

    /// <summary>初期化する</summary>
    public void Initialize(GameObject Sound)
    {
        // ゲームオブジェクトとコンポーネントの取得
        OK = transform.Find("OK").gameObject;  // ＯＫ画像
        NG = transform.Find("NG").gameObject;  // ＮＧ画像

        this.Sound = Sound;
        Set();
    }

    /// <summary>フィードバック</summary>
    /// <param name="IsCorrect">正解なら真、間違いなら偽</param>
    /// <param name="Time">正解間違い画像の提示時間</param>
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
        // ＯＫ音を鳴らす
        Sound.GetComponent<SolveQuestion_Sound>().PlaySound(SolveQuestion_Sound.SOUND.OK);
        // ShowTimeを過ぎるまで画像表示
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
        // ＮＧ音を鳴らす
        Sound.GetComponent<SolveQuestion_Sound>().PlaySound(SolveQuestion_Sound.SOUND.NG);
        // ShowTimeを過ぎるまで画像表示
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
