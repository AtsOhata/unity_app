using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Home_Manager : MonoBehaviour
{
    // =====
    // ＵＩ
    // =====
    /// <summary>戻るボタン</summary>
    public GameObject ReturnButton;
    /// <summary>実績表示ボタン</summary>
    public GameObject ShowAchievementsButton;
    /// <summary>設定ボタン</summary>
    public GameObject OptionButton;
    /// <summary>設定</summary>
    public GameObject Option;
    /// <summary>スクローラー</summary>
    public GameObject Scroller_GameObject;
    /// <summary>メッセージボックス</summary>
    public GameObject MsgBox;
    // 上記ＵＩの内、Update()で管理する必要があるものを格納する
    ReturnButton returnButton;
    ShowAchievementsButton AchievementButton;
    OptionButton optionButton;
    Text NewsBoxText1;
    /// <summary>設定パーツ</summary>
    List<OptionInterface> OptionAbstracts = new List<OptionInterface>();

    // ========
    // 各パーツ
    // ========
    /// <summary>ＱＢパーツ</summary>
    public GameObject QBPrefab;
    /// <summary>ＱＬパーツ</summary>
    public GameObject QLPrefab;
    /// <summary>プレイモード(Word)パーツ</summary>
    public GameObject PlayModeWordsPrefab;
    /// <summary>プレイモード(Story)パーツ</summary>
    public GameObject PlayModeStoryPrefab;
    /// <summary>実績パーツ</summary>
    public GameObject AchievementPrefab;
    // 上記部品の内、Update()で管理する必要があるものを格納する
    List<QB> QBs = new List<QB>();
    List<QL> QLs = new List<QL>();
    List<Achievement> Achievements = new List<Achievement>();

    // ======
    // その他
    // ======
    /// <summary>次シーンのDTO</summary>
    LoadQuestion_DTO loadQuestion_DTO = new LoadQuestion_DTO();

    void Start()
    {
        Initialization();

        WipeScreenContents();
        Home_DTO.ScreenConfig sc = GlobalVariables.Home_DTO.ScreenConfigs.Find(x => x.ScreenName == GlobalVariables.Home_DTO.CurrentScreenName);
        SetScrollContents(sc.ScreenName, sc.ScreenValue);
    }


    void Update()
    {
        UIAction();

        QBAction();

        QLAction();

        // UIの動作
        void UIAction()
        {
            // 戻るボタンがタッチされたら階層を１つあげる
            if (returnButton.CheckTouch())
            {
                WipeScreenContents();
                Option.SetActive(false);
                string previousScreenName = returnButton.ReturnScreen();
                SetScrollContents(previousScreenName);
                GlobalVariables.Home_DTO.CurrentScreenName = previousScreenName;
            }
            // 実績表示ボタンがタッチされたら実績画面に移る
            if (AchievementButton.CheckTouch())
            {
                // 設定画面に移る
                if (GlobalVariables.Home_DTO.CurrentScreenName != AchievementButton.ScreenName)
                {
                    WipeScreenContents();
                    Option.SetActive(false);
                    SetScrollContents(AchievementButton.ScreenName);
                    // 設定画面からの遷移以外なら次の画面で戻るボタンを押した時に戻れるようにする
                    if (GlobalVariables.Home_DTO.CurrentScreenName != optionButton.ScreenName)
                        returnButton.AddPreviouScreenName(GlobalVariables.Home_DTO.CurrentScreenName);
                    // 現在の画面名更新
                    GlobalVariables.Home_DTO.CurrentScreenName = AchievementButton.ScreenName;
                }
                else
                {
                    WipeScreenContents();
                    Option.SetActive(false);
                    string previousScreenName = returnButton.ReturnScreen();
                    SetScrollContents(previousScreenName);
                    GlobalVariables.Home_DTO.CurrentScreenName = previousScreenName;
                }
            }
            // 設定ボタンがタッチされたら設定画面に移る
            if (optionButton.CheckTouch())
            {
                // 設定画面に移る
                if (GlobalVariables.Home_DTO.CurrentScreenName != optionButton.ScreenName)
                {
                    WipeScreenContents();
                    Option.SetActive(true);
                    // 実績画面からの遷移以外なら次の画面で戻るボタンを押した時に戻れるようにする
                    if(GlobalVariables.Home_DTO.CurrentScreenName != AchievementButton.ScreenName)
                        returnButton.AddPreviouScreenName(GlobalVariables.Home_DTO.CurrentScreenName);
                    // 現在の画面名更新
                    GlobalVariables.Home_DTO.CurrentScreenName = optionButton.ScreenName;
                }
                // 現在設定画面なら前の画面に戻る
                else
                {
                    WipeScreenContents();
                    Option.SetActive(false);
                    string previousScreenName = returnButton.ReturnScreen();
                    SetScrollContents(previousScreenName);
                    GlobalVariables.Home_DTO.CurrentScreenName = previousScreenName;
                }
            }

            // 実績の動作
            Achievements.ForEach(x => x.CheckTouch());
            // 設定の動作
            if (Option.activeSelf)
            {
                OptionAbstracts.ForEach(x => x.Act());
            }

        }

        // QBの動作
        void QBAction()
        {
            // QBがタッチされたらQB名を保存して次の画面へ行く
            for (int i = 0; i < QBs.Count; i++)
            {
                // 実機の画面サイズに因りなぜかScrollView配下のContentのz座標がマイナスになることがあるのでその対策
                if (QBs[i].GetComponent<RectTransform>().position.z < 0)
                    QBs[i].GetComponent<RectTransform>().position = new Vector3(QBs[i].transform.position.x, QBs[i].transform.position.y, 1f);

                if (QBs[i].CheckTouch())
                {
                    loadQuestion_DTO.QBTitle = QBs[i].transform.Find("Text").GetComponent<Text>().text;
                    string str = QBs[i].NextScreenName;
                    WipeScreenContents();
                    SetScrollContents(str);
                    // 次の画面で戻るボタンを押した時に戻れるようにする
                    returnButton.AddPreviouScreenName(GlobalVariables.Home_DTO.CurrentScreenName);
                    // 現在の画面名保存
                    GlobalVariables.Home_DTO.CurrentScreenName = str;
                    break;
                }
            }
        }
        // QLの動作
        void QLAction()
        {
            string nextScreenName = "";
            // QLがタッチされたらQL名を保存して次の画面へ行く
            QLs.ForEach(x =>
            {
                // 実機の画面サイズに因りなぜかScrollView配下のContentのz座標がマイナスになることがあるのでその対策
                if (x.GetComponent<RectTransform>().position.z < 0)
                    x.GetComponent<RectTransform>().position = new Vector3(x.transform.position.x, x.transform.position.y, 1f);

                nextScreenName = x.CheckTouch();
                loadQuestion_DTO.QLTitle = x.transform.Find("Text").GetComponent<Text>().text;
                if (!string.IsNullOrEmpty(nextScreenName))
                {
                    ToSceneLoadQuestion(x);
                }
            });
        }
    }

    /// <summary>画面名に応じたパーツを画面に反映させる</summary>
    /// <param name="ScreenName">画面名</param>
    void SetScrollContents(string ScreenName, float ScrollValue = 1.0f)
    {
        List<Transform> screenParts = new List<Transform>();

        // 遷移前画面状態の保存
        if (GlobalVariables.Home_DTO.CurrentScreenName != ScreenName)
        {
            Home_DTO.ScreenConfig previousSc = GlobalVariables.Home_DTO.ScreenConfigs.Find(x => x.ScreenName == GlobalVariables.Home_DTO.CurrentScreenName);
            // スクロール状態の保存
            if (Scroller_GameObject.GetComponentInChildren<Scrollbar>() != null)
                previousSc.ScreenValue = Scroller_GameObject.GetComponentInChildren<Scrollbar>().value;
        }

        // 遷移先画面コンフィグの取得
        Home_DTO.ScreenConfig sc = GlobalVariables.Home_DTO.ScreenConfigs.Find(x => x.ScreenName == ScreenName);

        // 画面　切り替え・スクロール
        int screenType = sc.ScreenType;

        // パーツの設定
        Transform st;
        if (screenType == 2)  // スクロール
        {
            st = Scroller_GameObject.transform.Find("Viewport/Content");
        }
        else  // 切り替え
        {
            st = Scroller_GameObject.transform.Find("Viewport/Content");
        }
        GlobalVariables.Home_DTO.Parts.ForEach(Part =>
        {
            if (Part.ScreenName == ScreenName)
            {
                if (Part.PartType == "QB")
                {
                    GameObject part = Instantiate(QBPrefab, st);
                    QB qB = part.GetComponent<QB>();
                    qB.Initialize(Part.Values);
                    part.transform.localScale = Vector3.Scale(QBPrefab.transform.localScale, new Vector3(1.0f, 1.0f, 1.0f));  // 実機でサイズが勝手に変更されてしまう対策
                    QBs.Add(qB);
                    screenParts.Add(part.transform);
                }
                else if (Part.PartType == "QL")
                {
                    GameObject part = Instantiate(QLPrefab, st);
                    QL qL = part.GetComponent<QL>();
                    qL.Initialize(Part.Values);
                    part.transform.localScale = Vector3.Scale(QLPrefab.transform.localScale, new Vector3(1.0f, 1.0f, 1.0f));  // 実機でサイズが勝手に変更されてしまう対策
                    QLs.Add(qL);
                    screenParts.Add(part.transform);
                    part.transform.SetParent(st);
                }
                else if (Part.PartType == "Achievement")
                {
                    GameObject part = Instantiate(AchievementPrefab, st);
                    Achievement a = part.GetComponent<Achievement>();
                    Achievements.Add(a);
                    part.transform.localScale = Vector3.Scale(AchievementPrefab.transform.localScale, new Vector3(1.0f, 1.0f, 1.0f));  // 実機でサイズが勝手に変更されてしまう対策
                    Text MsgBoxText1 = MsgBox.transform.Find("Text1").GetComponent<Text>();
                    Text MsgBoxText2 = MsgBox.transform.Find("Text2").GetComponent<Text>();
                    a.Initialize(Part.Values, MsgBoxText1, MsgBoxText2);
                    screenParts.Add(part.transform);
                    part.transform.SetParent(st);
                }
            }
        });

            if (Scroller_GameObject.GetComponentInChildren<Scrollbar>() != null)
            Scroller_GameObject.GetComponentInChildren<Scrollbar>().value = sc.ScreenValue;
    }

    /// <summary> LoadQuestionへ行く</summary>
    /// <param name="chosenQL">選ばれたQL</param>
    void ToSceneLoadQuestion(QL chosenQL)
    {
        // 次回Home画面に戻る時のHome_DTOの設定 (Home_DTOは削除せず残る)
        GlobalVariables.Home_DTO.PreviousScreenNames = returnButton.PreviousScreenNames;
        Home_DTO.ScreenConfig sc= GlobalVariables.Home_DTO.ScreenConfigs.Find(x => x.ScreenName == GlobalVariables.Home_DTO.CurrentScreenName);
        sc.ScreenValue = Scroller_GameObject.GetComponentInChildren<Scrollbar>().value;

        // 次シーンのDTOに値を詰める
        LoadQuestion_DTO loadQuestionDTO = new LoadQuestion_DTO();
        loadQuestionDTO.QLTitle = chosenQL.QLTitle;  // QL名称
        loadQuestionDTO.ChosenQLPath = chosenQL.QLPath;  // 選ばれたQLのパス
        loadQuestionDTO.QuestionQuantity = 10;  // UserOption.Instance.QLQuestionAmount;  // 問題数
        GlobalVariables.LoadQuestion_DTO = loadQuestionDTO;

        // 次シーンに遷移する
        SceneManager.LoadScene("LoadQuestion_Scene");
    }


    /// <summary>スクロールオブジェクトの更新</summary>
    void WipeScreenContents()
    {
        QBs.Clear();
        QLs.Clear();
        Achievements.Clear();
        foreach (Transform t in Scroller_GameObject.transform.Find("Viewport/Content"))
            Destroy(t.gameObject);
    }

    /// <summary>初期化</summary>
    void Initialization()
    {
        // UIの初期化
        returnButton = ReturnButton.GetComponent<ReturnButton>();
        returnButton.Initialize();
        AchievementButton = ShowAchievementsButton.GetComponent<ShowAchievementsButton>();
        AchievementButton.Initialize("実績");
        optionButton = OptionButton.GetComponent<OptionButton>();
        optionButton.Initialize("設定");
        Option.SetActive(false);  // 設定画面は最初非表示
        OptionAbstracts.AddRange(Option.transform.GetComponentsInChildren<OptionInterface>());  // 設定項目取得
        OptionAbstracts.ForEach(x => x.Initialize());  // 設定項目初期化

        // 一旦メッセージボックスを省略
        MsgBox.SetActive(false);
    }
}
