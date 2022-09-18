using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class ShowAnswer_Manager : MonoBehaviour
{

    // タブ
    enum TAB
    {
        GROSS_RESULT,  // 総合結果タブ
        EACH_RESULT  // 個別結果タブ
    }
    TAB CurrentTab = TAB.GROSS_RESULT;

    // ------
    // その他
    // ------
    /// <summary>トップへ</summary>
    public GameObject ToTop;

    // --------
    // 総合結果
    // --------
    /// <summary>総合結果タブ</summary>
    public GameObject Tab1;
    /// <summary>総合結果タブ</summary>
    public GameObject Tab1_Tab;

    // --------
    // 個別結果
    // --------
    /// <summary>個別結果タブ</summary>
    public GameObject Tab2;
    /// <summary>〇</summary>
    public Sprite Tab2_Round;
    /// <summary>×</summary>
    public Sprite Tab2_Cross;
    /// <summary>個別結果タブ</summary>
    public GameObject Tab2_Tab;

    public GameObject ScrollViewQR;  // 問題回答ScrollView
    GameObject QRBase;  // 問題回答のGameObjectPrefab
    List<GameObject> QRs = new List<GameObject>();  // 問題回答のGameObject

    void Start()
    {
        // QL名称　のテキスト反映
        Tab1.transform.Find("QL名称").GetComponent<Text>().text = GlobalVariables.ShowAnswer_DTO.QLTitle;

        // 正解数　のテキスト反映
        Tab1.transform.Find("正解数").GetComponent<Text>().text = GlobalVariables.ShowAnswer_DTO.QR.Count(x => x.IsCorrect == true) + "/" + GlobalVariables.ShowAnswer_DTO.GrossAnswerQuantity;

        // 割合　のテキスト反映
        Tab1.transform.Find("割合").GetComponent<Text>().text = (((float)GlobalVariables.ShowAnswer_DTO.QR.Count(x => x.IsCorrect == true) / GlobalVariables.ShowAnswer_DTO.GrossAnswerQuantity) * 100).ToString("F3") + "%";

        // ShowAnswerDTOの分の問題を生成する
        if (QRBase == null) QRBase = ScrollViewQR.transform.Find("ViewportQR/ContentQR/QR").gameObject;  //複製するプレハブの取得
        QRBase.SetActive(true);
        Vector3 baseSize = QRBase.transform.localScale;  // プレハブのサイズ
        int index = 1;  // 通し番号
        GlobalVariables.ShowAnswer_DTO.QR.ForEach(x =>
        {
            GameObject g = Instantiate(QRBase);
            g.transform.SetParent(ScrollViewQR.transform.Find("ViewportQR/ContentQR"));  // 親にContentを指定
            g.transform.Find("Text_Problem").GetComponent<Text>().text = x.Question;  // 問題
            // 問題がnullなら問題画像の位置を変える
            if (string.IsNullOrEmpty(x.Question))
                g.transform.Find("Image_QuestionImage").GetComponent<RectTransform>().position = new Vector3(-50.0f, g.transform.Find("Image_QuestionImage").GetComponent<RectTransform>().position.y, g.transform.Find("Image_QuestionImage").GetComponent<RectTransform>().position.z);
            if (!string.IsNullOrEmpty(x.Picture))  // 問題画像
                g.transform.Find("Image_QuestionImage").GetComponent<Image>().sprite = FileUtility.ReadSprite("/QL/Pics/" + x.Picture);
            else
                g.transform.Find("Image_QuestionImage").gameObject.SetActive(false);
            if (x.IsCorrect)
                g.transform.Find("Image_Background").GetComponent<Image>().color = new Color(0.85f, 1f, 0.82f, 1f);
            else
                g.transform.Find("Image_Background").GetComponent<Image>().color = new Color(1f, 0.85f, 0.82f, 1f);
            g.transform.Find("Text_PlayerAnswer").GetComponent<Text>().text = string.Join(",", x.PlayerAnswers);  // プレイヤーの回答
            g.transform.Find("Text_CorrectAnswer").GetComponent<Text>().text = string.Join(",", x.CorrectAnswers);  // 正解
            if(UserOption.Instance.SecondShowFlag)
                g.transform.Find("Text_TimeForAnswer").GetComponent<Text>().text = x.TimeForAnswer.ToString("F3") + " s";  // 回答時間
            g.transform.localScale = Vector3.Scale(baseSize, new Vector3(0.95f, 0.95f, 1f));  // なぜか勝手にサイズが変更されてしまうので防ぐ
            QRs.Add(g);
            index++;
        });
        QRBase.SetActive(false);  // プレハブ非アクティブ

        // 総合結果タブを表示する
        ChangeTab(TAB.GROSS_RESULT);
    }

    void Update()
    {
        QRs.ForEach(x =>
        {
            // 実機の画面サイズに因りなぜかScrollView配下のContentのz座標がマイナスになることがあるのでその対策
            if (x.GetComponent<RectTransform>().position.z < 0)
                x.GetComponent<RectTransform>().position = new Vector3(x.transform.position.x, x.transform.position.y, 1f);
        });

        // ------------
        // 総合結果タブ
        // ------------
        if (Tab1_Tab.GetComponent<TouchDetect>().DetectTouch()) ChangeTab(TAB.GROSS_RESULT);

        // ------------
        // 個別結果タブ
        // ------------
        if (Tab2_Tab.GetComponent<TouchDetect>().DetectTouch()) ChangeTab(TAB.EACH_RESULT);

        // トップへが押されたら
        if (ToTop.GetComponent<TouchDetect>().DetectTouch())
        {
            Finalizes();
            // Scene_Homeに移る
            SceneManager.LoadScene("Home_Scene");
        }
    }

    void ChangeTab(TAB tabTo)
    {
        // すべてのタブの内容を非表示する
        Tab1.SetActive(false);
        Tab2.SetActive(false);
        // 目的のタブの内容を表示する
        if (tabTo == TAB.GROSS_RESULT) Tab1.SetActive(true);
        else if (tabTo == TAB.EACH_RESULT) Tab2.SetActive(true);

        CurrentTab = tabTo;
    }

    void Finalizes()
    {
        // 使用済みDTO初期化
        GlobalVariables.ShowAnswer_DTO = new ShowAnswer_DTO();
        // SolveQuestionシーンから受け継いだSoundオブジェクトを消す(消さないと２回目のSolveQuestionでバグる)
        Destroy(GameObject.Find("Sound"));
    }

}
