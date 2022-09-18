using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class OO_Answer : MonoBehaviour
{
    public void Initialize(string answer)
    {
        // ゲームオブジェクトとコンポーネントの取得
        GameObject Answer = transform.Find("Text").gameObject;  // 答えテキスト (ボタンが押されるまで非表示)
        GameObject Button = transform.Find("Button").gameObject;  // 答えボタン
        TouchDetect ButtonTD = Button.GetComponent<TouchDetect>();  // 答えボタンTouchDetect

        // 答えテキストのテキスト反映
        Answer.GetComponent<Text>().text = answer;

        // 答えテキストの非表示
        Answer.SetActive(false);

        // 答えボタンコルーチンの始動
        StartCoroutine(ShowAnswerButton());

        /// <summary>答えボタンコルーチン</summary>
        IEnumerator ShowAnswerButton()
        {
            while (true)
            {
                // 答えボタンがタッチされたら
                if (ButtonTD.DetectTouch())
                {
                    // 答えテキストの表示
                    Answer.SetActive(true);

                    // 答えボタンの非表示
                    Button.SetActive(false);
                }
                yield return null;
            }
        }
    }
}
