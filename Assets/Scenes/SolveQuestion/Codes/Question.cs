using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Question : MonoBehaviour
{
    /// <summary>１行の最大バイト数 32バイト(全角16文字)</summary>
    int MaxBite = 32;

    /// <summary> 最大行数</summary>
    int MaxLength = 6;

    public void Initialize(string question)
    {
        // 問題文を長すぎくならないように指定文字数で区切って格納し、問題文の続きの部分にはボタンで行き来できるようにする
        List<string> QuestionSeparated = StringUtility.CutTextB(question, MaxBite, MaxBite * MaxLength);  // 問題文を最大バイト数で切り分けて取得
        Text bodyText = transform.Find("BodyText").GetComponent<Text>();  // 本文
        bodyText.text = QuestionSeparated[0];  // 問題文の最初の部分をテキスト反映
        GameObject continueButton = transform.Find("Continue").gameObject;  // 続きボタン
        TouchDetect continueButtonTD = continueButton.GetComponent<TouchDetect>();  // 続きボタンTouchDetect
        GameObject returnButton = transform.Find("Return").gameObject;  // 戻るボタン
        TouchDetect returnButtonTD = returnButton.GetComponent<TouchDetect>();  // 戻るボタンTouchDetect

        // 続き／戻るボタンタッチコルーチンの始動
        StartCoroutine(QuestionContinueReturnButton());

        /// <summary>読解の続き／戻るボタンタッチコルーチン</summary>
        IEnumerator QuestionContinueReturnButton()
        {
            // 切り分けたテキストが一つの時は続き／戻るボタンが要らないためボタンを非表示にしてコルーチンストップ
            if (QuestionSeparated.Count == 1)
            {
                continueButton.SetActive(false);// 続きボタン非表示
                returnButton.SetActive(false);// 戻るボタン非表示
                yield break;
            }

            // ボタンの表示を初期状態にする
            continueButton.SetActive(true);  // 続きボタン表示
            returnButton.SetActive(false);// 戻るボタン非表示

            // ゲームオブジェクトとコンポーネントの取得
            int QuestionCurrentTextIndex = 0;  // 現在表示中の問題文部分の番号
            while (true)
            {
                // 続きボタンが表示中でタッチされたら次の部分の問題文を表示する
                if (continueButton.activeSelf && continueButtonTD.DetectTouch())
                {
                    QuestionCurrentTextIndex++;  // 現在表示中の問題文部分の番号
                    bodyText.text = QuestionSeparated[QuestionCurrentTextIndex];  // 問題文をテキスト反映
                    returnButton.SetActive(true);  // 戻るボタン表示
                    // 問題の最後の部分を表示しているのであれば続きボタンを非表示にする
                    if (QuestionCurrentTextIndex == QuestionSeparated.Count - 1)
                    {
                        continueButton.SetActive(false);
                    }
                }

                // 戻るボタンが表示中でタッチされたら前の部分の問題を表示する
                if (returnButton.activeSelf && returnButtonTD.DetectTouch())
                {
                    QuestionCurrentTextIndex--;  // 現在表示中の問題部分の番号
                    bodyText.text = QuestionSeparated[QuestionCurrentTextIndex];  // 問題をテキスト反映
                    continueButton.SetActive(true);  // 続きボタン表示
                    // 問題の最初の部分を表示しているのであれば戻るボタンを非表示にする
                    if (QuestionCurrentTextIndex == 0)
                    {
                        returnButton.SetActive(false);
                    }
                }


                yield return null;
            }
        }
    }
}
