using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Collections;

public class SolveQuestion_Manager : MonoBehaviour
{
    public GameObject UI;  // ＵＩ

    // ----
    // ＵＩ
    // ----
    GameObject UI_QuestionCounter;  // 出題算器
    GameObject UI_Quit;  // やめる
    GameObject UI_Feedback;  // 解応
    GameObject UI_StartButton;  // 開始ボタン
    public GameObject Sound;  // 音
    UI_QuestionCounter QuestionCounter;  // 出題算器スクリプト
    UI_StartButton StartButton;  // スタートボタンスクリプト

    /// <summary>回答</summary>
    List<string> ChosenAnswers = new List<string>();

    /// <summary>現在の問題</summary>
    int CurrentQuestionNumber = 0;

    /// <summary>現在の問題の正答の数</summary>
    int CurrentQuestionCorrectAnswerQuantity = 1;

    // ----------
    // 出題パーツ
    // ----------
    /// <summary>スクロールバー</summary>
    public GameObject ScrollView_ScrollBarVertical;
    /// <summary>ここにパーツ(部品)を置いていくことで出題画面を作る</summary>
    public GameObject ScrollView_Content;

    /// <summary>画像パーツ</summary>
    public GameObject PicturePrefab;
    /// <summary>読解資料パーツ</summary>
    public GameObject ReadingMaterialPrefab;
    /// <summary>注意書きパーツ</summary>
    public GameObject CautionPrefab;
    /// <summary>問題パーツ</summary>
    public GameObject QuestionPrefab;
    /// <summary>一問一答答えパーツ</summary>
    public GameObject OO_AnswerPrefab;
    /// <summary>ＯＫ／ＮＧパーツ</summary>
    public GameObject OO_OKNGPrefab;
    /// <summary>選択肢パーツ</summary>
    public GameObject MultipleChoicesPrefab;
    /// <summary>記述パーツ</summary>
    public GameObject DescriptionPrefab;
    /// <summary>並べ替えパーツ</summary>
    public GameObject ChooseOrderChoicesPrefab;
    /// <summary>答合パーツ</summary>
    public GameObject AnswerCheckButtonPrefab;

    // ------------
    // 選択肢パーツ
    // ------------
    public GameObject ChoiceWindow;

    // 上記部品の内、Update()で管理する必要があるものを格納する
    OO_OKNG oKNG;
    MultipleChoices multipleChoices;  // Choice部品
    InputField description;  // 記述パーツのInputField部品
    ChooseOrderChoices chooseOrderChoices;  // 並び替えボタン
    GoToNextQuestionButton GoToNextQuestionButton;  // 答合ボタン

    /// <summary>回答時間</summary>
    float TimeForAnswer;

    /// <summary>フィードバックするかどうか</summary>
    bool FeedbackFlag = true;

    /// <summary>フィードバック中であるフラグ</summary>
    bool IsFeedbacking = false;

    /// <summary>フィードバックカウンター</summary>
    float FeedbackCount = 0f;

    /// <summary>フィードバック秒数</summary>
    readonly float FeedbackTime = 0.6f;
    
    /// <summary>答合ボタンフラグ</summary>
    bool GoToNextQuestionButtonFlag = false;
    
    /// <summary>答合ボタン押下フラグ</summary>
    bool AnswerCheckButtonFlag = false;
    void Start()
    {
        // ＵＩのゲームオブジェクト読み込み
        UI_QuestionCounter = UI.transform.Find("QuestionCounter").gameObject;
        UI_Quit = UI.transform.Find("Quit/Image").gameObject;
        UI_Feedback = UI.transform.Find("Feedback").gameObject;
        UI_StartButton = UI.transform.Find("StartButton").gameObject;
        // ＵＩの初期化
        UI_Feedback.GetComponent<Feedback>().Initialize(Sound);
        QuestionCounter = UI_QuestionCounter.GetComponent<UI_QuestionCounter>();
        QuestionCounter.Initialize(GlobalVariables.SolveQuestion_DTO.QSs.Count);
        StartButton = UI_StartButton.GetComponent<UI_StartButton>();
        StartButton.Initialize();
        StartCoroutine(QuitButton());
    }

    void Update()
    {
        // ==========
        // 開始時のみ
        // ==========
        // スタートボタンタッチ
        if (UI_StartButton != null && StartButton.CheckTouch(Sound))
        {
            SetChoices(CurrentQuestionNumber);  // 最初の問題をセットする
            TimeForAnswer = 0;  // 時間計測開始
        }

        // ========
        // 回答段階
        // ========
        // 問題提示後の０．１秒　と　フィードバック中は
        // 選択肢等が選べない（根拠:陸上・Ｆ１のフライングが０．１秒）
        if (TimeForAnswer > 0.1f && !IsFeedbacking)
        {
            // ＯＫ／ＮＧタッチ
            if (oKNG != null) oKNG.CheckTouch();

            // 選択肢タッチ
            if (multipleChoices != null)
                ChosenAnswers = multipleChoices.CheckTouch();

            // 並び替え選択肢タッチ
            if (chooseOrderChoices != null)
                ChosenAnswers = chooseOrderChoices.CheckTouch();
        }

        // 答合フラグ管理
        if (GoToNextQuestionButtonFlag && GoToNextQuestionButton.CheckTouch())
            AnswerCheckButtonFlag = true;

        // ****************************
        // 答え合わせしフィードバックへ
        // ****************************
        // 選択肢を選びきったら答え合わせする
        // ((答合ボタンフラグOFF && 回答リストの中身の数が現問正答数と一致)
        // || (答合ボタン押下フラグON))
        // && フィードバック中ではない　場合
        if (((!GoToNextQuestionButtonFlag && ChosenAnswers.Count >= CurrentQuestionCorrectAnswerQuantity)
            || AnswerCheckButtonFlag)
            && !IsFeedbacking)
        {
            // 出題セット取得
            SolveQuestion_DTO.QS qS = GlobalVariables.SolveQuestion_DTO.QSs[CurrentQuestionNumber];

            // 次のシーンのDTO
            ShowAnswer_DTO.QuestionResult qR = new ShowAnswer_DTO.QuestionResult();

            //===========
            // 答え合わせ
            //===========
            qR.IsCorrect = true;
            // 多肢選択
            ChosenAnswers.ForEach(x =>
            { 
                if (!qS.Answers.Contains(x))
                    qR.IsCorrect = false; 
            });
            // 記述
            if (description != null)
            {
                if (string.IsNullOrEmpty(description.text))
                    qR.IsCorrect = false;
                else
                {
                    List<string> playerAnswers = description.text.Split(',').ToList();
                    playerAnswers = playerAnswers.Distinct().ToList();
                    int count = 0;
                    playerAnswers.ForEach(x =>
                    {
                        if (qS.Answers.Contains(x))
                            count++;
                        else
                            qR.IsCorrect = false;
                    });
                    if (count != qS.PlayerChooseQuantity)
                        qR.IsCorrect = false;
                }
            }
            // 並べ替え
            if (chooseOrderChoices != null)
            {
                if (!(string.Join("", ChosenAnswers) == string.Join("", qS.Answers)))
                    qR.IsCorrect = false;
            }

            // フィードバック
            if (FeedbackFlag)
            {
                // 〇×のアニメーション
                UI_Feedback.GetComponent<Feedback>().GiveFeedback(qR.IsCorrect, FeedbackTime);
                // 正解選択肢の色を変える
                if (multipleChoices != null)
                    multipleChoices.Feedback(qS.Answers);
                if (chooseOrderChoices != null)
                    chooseOrderChoices.Feedback(qS.Answers);
            };

            // 次シーンのDTOに詰める
            GlobalVariables.ShowAnswer_DTO.GrossAnswerQuantity++;  // 途中で回答を辞めた場合の事も考え、回答毎に記録
            qR.Question = qS.Question;  // 問題記録
            qR.QuestionAlignment = qS.QuestionAlignment;  // 問題文字揃え
            qR.Picture = qS.Image;  // 画像名
            // プレイヤー回答記録
            if (qS.QT == SolveQuestion_DTO.QT.DESCRIPTION)  // 記述
                qR.PlayerAnswers = description.text.Split(',').ToList();
            else
                qR.PlayerAnswers = new List<string>(ChosenAnswers);  // プレイヤー回答記録
            if (qS.QT == SolveQuestion_DTO.QT.CHOOSE_ORDER)  // 並べ替えの時は順序数字を付ける
                for (int i = 0; i < qR.PlayerAnswers.Count; i++)
                    qR.PlayerAnswers[i] = (i + 1) + "." + qR.PlayerAnswers[i];
            qR.CorrectAnswers = qS.Answers;  // 正解記録
            if (qS.QT == SolveQuestion_DTO.QT.CHOOSE_ORDER)  // 並べ替えの時は順序数字を付ける
                for (int i = 0; i < qR.CorrectAnswers.Count; i++)
                    qR.CorrectAnswers[i] = (i + 1) + "." + qR.CorrectAnswers[i];
            qR.Choices = qS.Choices;  // 選択肢記録
            qR.TimeForAnswer = TimeForAnswer;  // 回答時間
            GlobalVariables.ShowAnswer_DTO.QR.Add(qR);

            // 正解単語の情報をSavedataから取得
            Savedata.LearnedWord savedWord = Savedata.Instance.LearnedWords.Find(x => x.Alias == GlobalVariables.SolveQuestion_DTO.QSs[CurrentQuestionNumber].Alias);
            if (savedWord == null)  // 初めての単語の時
            {
                savedWord = new Savedata.LearnedWord();  // 新単語登録
                savedWord.Alias = qS.Alias;  // 通り名
                savedWord.Results.Add(qR.IsCorrect);  // 答え合わせ結果
                savedWord.Time.Add(TimeForAnswer);  // 回答時間
                savedWord.FixedPercentage = savedWord.CalcFixedPercentage(savedWord.Time, savedWord.Results);  // 定着率
                Savedata.Instance.LearnedWords.Add(savedWord);
            }
            else  // 既にデータがある時
            {
                savedWord.Results.Add(qR.IsCorrect);  // 答え合わせ結果
                savedWord.Time.Add(TimeForAnswer);  // 回答時間
                                                    //答え合わせ結果と 回答時間は直近３回分のみ保存する
                while (savedWord.Results.Count > 3)
                    savedWord.Results.RemoveAt(0);
                while (savedWord.Time.Count > 3)
                    savedWord.Time.RemoveAt(0);
                savedWord.FixedPercentage = savedWord.CalcFixedPercentage(savedWord.Time, savedWord.Results);  // 定着率
                int a = Savedata.Instance.LearnedWords.FindIndex(x => x.Alias == qS.Alias);
                Savedata.Instance.LearnedWords[a] = savedWord;
            }

            ChosenAnswers.Clear();

            IsFeedbacking = true;

        }

        // *****************************************************
        // フィードバック終了後　次の問題へ
        // *****************************************************
        if (!FeedbackFlag || FeedbackCount > FeedbackTime)
        {
            // ＵＩ更新
            UI_QuestionCounter.GetComponent<UI_QuestionCounter>().AddOne();

            // 回答時間リセット
            TimeForAnswer = 0;

            if (CurrentQuestionNumber >= QuestionCounter.QuestionQuantity - 1)  // 今の問題が最後の問題である時
            {
                ToNextGameScene();
                return;
            }
            else  // 次の問題へ移る
            {
                CurrentQuestionNumber++;
                SetChoices(CurrentQuestionNumber);
            }
            IsFeedbacking = false;
            FeedbackCount = 0;
        }

        // 時間加算
        TimeForAnswer += Time.deltaTime;

        // フィードバック中のみ加算
        if (IsFeedbacking)
            FeedbackCount += Time.deltaTime;
    }

    void ToNextGameScene()
    {
        // 次シーンのDTOにデータを入れる
        GlobalVariables.ShowAnswer_DTO.QLTitle = GlobalVariables.SolveQuestion_DTO.QLTitle;
        // 使用済みDTO消去
        GlobalVariables.SolveQuestion_DTO.QSs = new List<SolveQuestion_DTO.QS>();
        // 実績達成率の算出
        Savedata.Instance.AchievementDatas.ForEach(x => x.CalcPercent(Savedata.Instance.LearnedWords));
        // データセーブ
        Savedata.Save(Savedata.Instance);
        // 次のゲームシーンへ移る
        SceneManager.LoadScene("ShowAnswer_Scene");
    }

    /// <summary>問題のセット</summary>
    /// <param name="number">問題番号</param>
    void SetChoices(int number)
    {
        // 前回の出題セットスクロール白紙化・関連コンポーネント白紙化
        foreach (Transform t in ScrollView_Content.transform) Destroy(t.gameObject);  // すべての子オブジェクト削除
        foreach (Transform t in ChoiceWindow.transform) Destroy(t.gameObject);  // すべての子オブジェクト削除
        ScrollView_ScrollBarVertical.GetComponent<Scrollbar>().value = 1;

        // 出題セット取得
        SolveQuestion_DTO.QS QS = GlobalVariables.SolveQuestion_DTO.QSs[number];

        // 画像がある場合
        if (!string.IsNullOrEmpty(QS.Image))
        {
            // 出題セットに画像パーツを付け足す
            GameObject picture = Instantiate(PicturePrefab, ScrollView_Content.transform);  // 画像パーツの作成
            picture.transform.localScale = Vector3.Scale(ReadingMaterialPrefab.transform.localScale, new Vector3(1.0f, 1.0f, 1.0f));  // 実機でサイズが勝手に変更されてしまう対策
            Image i = picture.GetComponent<Image>();
            i.sprite = FileUtility.ReadSprite("/QL/Pics/" + QS.Image);
            i.type = Image.Type.Filled;  // 画像タイプ(詳しくはImageでSpriteに適当な画像を放り込むと分かる) アスペ比保存に必要
            i.preserveAspect = true;
        }
        // 読解資料がある場合
        if (!string.IsNullOrEmpty(QS.ReadingMaterial))
        {
            // 出題セットに読解資料パーツを付け足す
            GameObject readingMaterial = Instantiate(ReadingMaterialPrefab, ScrollView_Content.transform);  // 読解資料パーツの作成
            readingMaterial.transform.localScale = Vector3.Scale(ReadingMaterialPrefab.transform.localScale, new Vector3(1.0f, 1.0f, 1.0f));  // 実機でサイズが勝手に変更されてしまう対策
            readingMaterial.GetComponent<ReadingMaterial>().Initialize(QS.ReadingMaterial);  // 初期化する
        }
        // 問題がある場合
        if (!string.IsNullOrEmpty(QS.Question))
        {
            // 出題セットに問題パーツを付け足す
            GameObject question = Instantiate(QuestionPrefab, ScrollView_Content.transform);  // 問題パーツの作成
            question.transform.localScale = Vector3.Scale(QuestionPrefab.transform.localScale, new Vector3(1.0f, 1.0f, 1.0f));  // 実機でサイズが勝手に変更されてしまう対策
            question.GetComponent<Question>().Initialize(QS.Question);  // 初期化する
            question.transform.Find("BodyText").GetComponent<Text>().alignment = QS.QuestionAlignment;
        }
        // 注意書きがある場合
        if (!string.IsNullOrEmpty(QS.Caution))
        {
            // 出題セットに注意書きパーツを付け足す
            GameObject g = Instantiate(CautionPrefab, ScrollView_Content.transform);  // 注意書きパーツの作成
            g.transform.localScale = Vector3.Scale(CautionPrefab.transform.localScale, new Vector3(1.0f, 1.0f, 1.0f));  // 実機でサイズが勝手に変更されてしまう対策
            g.GetComponent<Text>().text = QS.Caution;
        }

        // 選択肢設定
        // ＱＴタイプ別に処理が分かれる
        if (QS.QT == SolveQuestion_DTO.QT.ONE_QUESTION_ONE_ANSWER)  // 一問一答
        {
            GameObject oOAnswer = Instantiate(OO_AnswerPrefab, ScrollView_Content.transform);  // 一問一答答えパーツの作成
            oOAnswer.transform.localScale = Vector3.Scale(OO_AnswerPrefab.transform.localScale, new Vector3(1.0f, 1.0f, 1.0f));  // 実機でサイズが勝手に変更されてしまう対策
            oOAnswer.GetComponent<OO_Answer>().Initialize(QS.Answers[0]);  // 初期化する
            GameObject oKNG = Instantiate(OO_OKNGPrefab, ScrollView_Content.transform);  // 一問一答ＯＫ／ＮＧパーツの作成
            oKNG.transform.localScale = Vector3.Scale(OO_OKNGPrefab.transform.localScale, new Vector3(1.0f, 1.0f, 1.0f));  // 実機でサイズが勝手に変更されてしまう対策
            oKNG.GetComponent<OO_OKNG>().Initialize(Sound);  // 初期化する
            this.oKNG = oKNG.GetComponent<OO_OKNG>();
        }
        else if (QS.QT == SolveQuestion_DTO.QT.MULTIPLE_CHOICE)  // 多肢選択
        {
            GameObject choice = Instantiate(MultipleChoicesPrefab, ChoiceWindow.transform);  // 選択肢パーツの作成
            choice.transform.localScale = Vector3.Scale(MultipleChoicesPrefab.transform.localScale, new Vector3(1.0f, 1.0f, 1.0f));  // 実機でサイズが勝手に変更されてしまう対策
            RectTransform rt = choice.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector3(0, 0, 0);
            rt.anchorMin = rt.anchorMax = new Vector2(0.5f, 0.5f);
            multipleChoices = choice.GetComponent<MultipleChoices>();  // スクリプト取得
            choice.GetComponent<MultipleChoices>().Initialize(QS.Choices);  // 初期化する
            CurrentQuestionCorrectAnswerQuantity = QS.PlayerChooseQuantity;  // 答えるべき数の取得
        }
        else if (QS.QT == SolveQuestion_DTO.QT.DESCRIPTION)  // 記述
        {
            GameObject g = Instantiate(DescriptionPrefab, ChoiceWindow.transform);  // 記述パーツの作成
            g.transform.localScale = Vector3.Scale(DescriptionPrefab.transform.localScale, new Vector3(1.0f, 1.0f, 1.0f));  // 実機でサイズが勝手に変更されてしまう対策
            description = g.GetComponent<InputField>();  // スクリプト取得
        }
        else if (QS.QT == SolveQuestion_DTO.QT.CHOOSE_ORDER)  // 並び替え
        {
            GameObject g = Instantiate(ChooseOrderChoicesPrefab, ChoiceWindow.transform);  // 並べ替えパーツの作成
            g.transform.localScale = Vector3.Scale(ChooseOrderChoicesPrefab.transform.localScale, new Vector3(1.0f, 1.0f, 1.0f));  // 実機でサイズが勝手に変更されてしまう対策
            RectTransform rt = g.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector3(0, 0, 0);
            rt.anchorMin = rt.anchorMax = new Vector2(0.5f, 0.5f);
            chooseOrderChoices = g.transform.GetComponent<ChooseOrderChoices>();  // スクリプト取得
            chooseOrderChoices.Initialize(QS.Choices);
            CurrentQuestionCorrectAnswerQuantity = QS.PlayerChooseQuantity;  // 答えるべき数の取得
        }

        // 「次問題遷移ボタン」パーツ
        if (QS.GoToNextQuestionButtonType != 0)
        {
            // 出題セットに次問題遷移ボタンパーツを付け足す
            GameObject g = Instantiate(AnswerCheckButtonPrefab, ScrollView_Content.transform);  // 問題遷移ボタンパーツの作成
            g.transform.localScale = Vector3.Scale(AnswerCheckButtonPrefab.transform.localScale, new Vector3(1.0f, 1.0f, 1.0f));  // 実機でサイズが勝手に変更されてしまう対策
            GoToNextQuestionButton = g.GetComponent<GoToNextQuestionButton>();
            GoToNextQuestionButton.Initialize(QS.GoToNextQuestionButtonType);
            if (QS.GoToNextQuestionButtonType == 2)
                FeedbackFlag = false;
            GoToNextQuestionButtonFlag = true;  // ボタンを押さないと次画面に遷移しなくなる
        }
        else
            GoToNextQuestionButtonFlag = false;  // 条件を満たせば次画面に自動遷移

        AnswerCheckButtonFlag = false;
    }


    /// <summary>やめるボタンの動作</summary>
    IEnumerator QuitButton()
    {
        // やめるTouchDetect
        TouchDetect UI_QuitTD = UI_Quit.GetComponent<TouchDetect>();
        while (true)
        {
            if (UI_QuitTD.DetectTouch())
            {
                ToNextGameScene();
                yield break;
            }
            yield return null;
        }
    }
}

