using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 読解資料
/// </summary>
public class ReadingMaterial : MonoBehaviour
{

    /// <summary>１行の最大バイト数</summary>
    int MaxBite = 32;

    /// <summary>１頁あたりの最大行数</summary>
    int MaxLength = 20;

    public void Initialize(string readingMaterial)
    {

        // 読解資料を長すぎくならないように指定文字数で区切って格納し、読解資料の続きの部分にはボタンで行き来できるようにする
        List<string> ReadingMaterialSeparated = StringUtility.CutTextB(readingMaterial, MaxBite, MaxBite * MaxLength);  // 読解資料を１頁あたりの最大バイト数で切り分けて取得
        Text bodyText = transform.Find("BodyText").GetComponent<Text>();  // 本文
        GameObject continueButton = transform.Find("Continue").gameObject;  // 続きボタン
        TouchDetect continueButtonTD = continueButton.GetComponent<TouchDetect>();  // 続きボタンTouchDetect
        GameObject returnButton = transform.Find("Return").gameObject;  // 戻るボタン
        TouchDetect returnButtonTD = returnButton.GetComponent<TouchDetect>();  // 戻るボタンTouchDetect
        bodyText.text = ReadingMaterialSeparated[0];  // 読解資料の最初の部分をテキスト反映

        // 続き／戻るボタンタッチコルーチンの始動
        StartCoroutine(ReadingMaterialContinueReturnButton());

        /// <summary>読解の続き／戻るボタンタッチコルーチン</summary>
        IEnumerator ReadingMaterialContinueReturnButton()
        {
            // 切り分けたテキストが一つの時は続き／戻るボタンが要らないためコルーチンストップ
            if (ReadingMaterialSeparated.Count == 1)
            {
                continueButton.SetActive(false);// 続きボタン非表示
                returnButton.SetActive(false);// 戻るボタン非表示
                yield break;
            }

            // ボタンの表示を初期状態にする
            continueButton.SetActive(true);  // 続きボタン表示
            returnButton.SetActive(false);// 戻るボタン非表示

            // ゲームオブジェクトとコンポーネントの取得
            int ReadingMaterialCurrentTextIndex = 0;  // 現在表示中の読解資料部分の番号
            while (true)
            {
                // 続きボタンが表示中でタッチされたら次の部分の読解資料を表示する
                if (continueButton.activeSelf && continueButtonTD.DetectTouch())
                {
                    ReadingMaterialCurrentTextIndex++;  // 現在表示中の読解資料部分の番号
                    bodyText.text = ReadingMaterialSeparated[ReadingMaterialCurrentTextIndex];  // 読解資料をテキスト反映
                    returnButton.SetActive(true);  // 戻るボタン表示
                    // 読解資料の最後の部分を表示しているのであれば続きボタンを非表示にする
                    if (ReadingMaterialCurrentTextIndex == ReadingMaterialSeparated.Count - 1)
                    {
                        continueButton.SetActive(false);
                    }
                }

                // 戻るボタンが表示中でタッチされたら前の部分の読解資料を表示する
                if (returnButton.activeSelf && returnButtonTD.DetectTouch())
                {
                    ReadingMaterialCurrentTextIndex--;  // 現在表示中の読解資料部分の番号
                    bodyText.text = ReadingMaterialSeparated[ReadingMaterialCurrentTextIndex];  // 読解資料をテキスト反映
                    continueButton.SetActive(true);  // 続きボタン表示
                    // 読解資料の最初の部分を表示しているのであれば戻るボタンを非表示にする
                    if (ReadingMaterialCurrentTextIndex == 0)
                    {
                        returnButton.SetActive(false);
                    }
                }

                yield return null;
            }
        }
    }
}
