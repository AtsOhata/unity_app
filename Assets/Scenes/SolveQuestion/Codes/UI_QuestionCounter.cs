using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_QuestionCounter : MonoBehaviour
{
    Text t1, t2;

    public int CurrentQuestion;
    public int QuestionQuantity;

    /// <summary>現在の問題数／問題数　のＵＩパーツ</summary>
    /// <param name="QuestionQuantity">全ての問題数</param>
    public void Initialize(int QuestionQuantity)
    {
        this.QuestionQuantity = QuestionQuantity;

        // Textコンポーネント取得
        t1 = transform.Find("CurrentQuestion").gameObject.GetComponent<Text>();
        t2 = transform.Find("QuestionQuantity").gameObject.GetComponent<Text>();

        // 現在の問題数を０に、問題数を決められた数にする
        t1.text = "0";
        t2.text = this.QuestionQuantity + "";
    }

    /// <summary>現在の問題数をテキスト反映する</summary>
    public void AddOne()
    {
        t1.text = ++CurrentQuestion + "";
    }

}
