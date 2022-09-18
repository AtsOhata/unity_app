using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Scene_LoadQuestionのManagerクラス
/// </summary>
public class LoadQuestion_Manager : MonoBehaviour
{
    void Start()
    {
        // ************************************************************************
        // 全体の流れ
        // リソースから情報取得→問題を選ぶ→選ばれた問題を次のシーンのDTOに設定する
        // ************************************************************************
        QuestionSet QS = QuestionSet.Load("QL/" + GlobalVariables.LoadQuestion_DTO.ChosenQLPath);  // 問題セットの取得
        if(QS.RandomFlag)
        {
            UnityEngine.Random.InitState(DateTime.Now.Millisecond);  // 現在の日時をシード値に設定
            QS.QuestionPacks = Utility.Randomize(QS.QuestionPacks);  // 問題リストの不規則化
        }
        if (QS.UpperLimitFlag)
        {
            // 出題数分の問題を選ぶ
            if (QS.QuestionPacks.Count > GlobalVariables.LoadQuestion_DTO.QuestionQuantity)  // 問題リストの総問題数が決められた出題数より多い時のみ選定の要あり
            {
                QS.QuestionPacks = ChooseQuestionPacks(GlobalVariables.LoadQuestion_DTO.QuestionQuantity);

                List<QuestionSet.QuestionPack> ChooseQuestionPacks(int Quantity)
                {
                    List<QuestionSet.QuestionPack> qPs = new();
                    int count = 0;
                    List<int> chosenIndices = new();
                    while (qPs.Count < Quantity)
                    {
                        // 出題確率
                        float probability = 1;
                        // ランダムに揀ぶ
                        // ① 問題リストから無作為に選んでいない問題を選び、その問題の通し名を取得する
                        int x;
                        do
                        {
                            x = UnityEngine.Random.Range(0, QS.QuestionPacks.Count);
                        } while (chosenIndices.Contains(x));
                        string chosen = QS.QuestionPacks[x].Alias;
                        // ② ループが1000回以内で、問題リストの通し名と一致するSavedataがあれば、そのSavedataの定着率＋１の逆数が出題確率
                        Savedata.LearnedWord w = Savedata.Instance.LearnedWords.Find(x => x.Alias == chosen);
                        if (count < 1000 && w != null)
                            probability = 1 / (w.FixedPercentage + 1f);
                        // ③ 乱数を回して出題確率よりも低い数値であれば出題リストに加える
                        if (UnityEngine.Random.value < probability)
                        {
                            qPs.Add(QS.QuestionPacks[x]);
                            chosenIndices.Add(x);
                        }
                        else
                            count++;
                    }
                    return qPs;
                }
            }
        }

        // 次シーンのDTO組み立て
        List<SolveQuestion_DTO.QS> QSs = new List<SolveQuestion_DTO.QS>();
        QS.QuestionPacks.ForEach(x =>
        {
            SolveQuestion_DTO.QS SQ = new();

            // 通し名
            SQ.Alias = x.Alias;
            // 画像
            if (!string.IsNullOrEmpty(x.Image))
                SQ.Image = x.Image.Trim();
            // 読解資料 (リッチテキスト化)
            if (!string.IsNullOrEmpty(x.ReadingMaterial))
                SQ.ReadingMaterial = x.ReadingMaterial.Trim().MakeRichText();

            // 問題の読み込み
            x.QuestionBases.ForEach(y =>
            {
                SQ.QuestionAlignment = y.QuestionAlignment;

                // 注意書き
                if (!string.IsNullOrEmpty(y.Caution))
                    SQ.Caution = y.Caution;

                if (y is OneQuestionOneAnswer)  // 一問一答
                {
                    OneQuestionOneAnswer o = (OneQuestionOneAnswer)y;
                    SQ.QT = SolveQuestion_DTO.QT.ONE_QUESTION_ONE_ANSWER;  // 問題タイプ
                    SQ.Question = o.Question.MakeRichText();  // 問題（リッチテキスト化する ）
                    SQ.Answers.Add(o.Answer);  // 答え
                }
                else if (y is MultipleChoice)  // 多肢選択問題
                {
                    MultipleChoice o = (MultipleChoice)y;
                    o.Initialize();  // 初期化
                    SQ.QT = SolveQuestion_DTO.QT.MULTIPLE_CHOICE;  // 問題タイプ
                    if(o.Question != null)
                        SQ.Question = o.Question.MakeRichText();  // 問題（リッチテキスト化する ）
                    SQ.Answers = o.CorrectChoicePool;  // 正答
                    SQ.Choices = o.Choices;  // 選択肢
                    SQ.PlayerChooseQuantity = o.PlayerChooseQuantity;  // プレイヤー選択量
                }
                else if (y is Description)  // 記述問題
                {
                    Description o = (Description)y;
                    o.Initialize();  // 初期化
                    SQ.QT = SolveQuestion_DTO.QT.DESCRIPTION;  // 問題タイプ
                    SQ.Question = o.Question.MakeRichText();  // 問題（リッチテキスト化する ）
                    SQ.Answers = o.CorrectChoicePool;  // 正答
                }
                else if (y is ChooseOrder)  // 並び替え問題
                {
                    ChooseOrder o = (ChooseOrder)y;
                    o.Initialize();  // 初期化
                    SQ.QT = SolveQuestion_DTO.QT.CHOOSE_ORDER;  // 問題タイプ
                    SQ.Question = o.Question.MakeRichText();  // 問題（リッチテキスト化する ）
                    SQ.Answers = o.CorrectOrder.Split(',').ToList();  // 正答
                    SQ.Choices = o.Choices;  // 選択肢
                    SQ.PlayerChooseQuantity = o.PlayerChooseQuantity;  // プレイヤー選択量
                }
            });

            // ================
            // 次問題遷移ボタン
            // ================
            if (x.GoToNextQuestionButtonType == 0)
            {
                // 条件を満たした際に「答え合わせ」ボタン

                // 問題が2問以上ある || 記述問題がある　時要ることにしておく
                if (x.QuestionBases.Count > 1
                    || x.QuestionBases.Where(y => y is Description).ToList().Count > 0)
                    SQ.GoToNextQuestionButtonType = 1;
                else
                    SQ.GoToNextQuestionButtonType = 0;
            }
            else if(x.GoToNextQuestionButtonType == 2)
            {
                // 「次へ」ボタン
                SQ.GoToNextQuestionButtonType = 2;
            }

            QSs.Add(SQ);
        });
        GlobalVariables.SolveQuestion_DTO.QLTitle = GlobalVariables.LoadQuestion_DTO.QLTitle;
        GlobalVariables.SolveQuestion_DTO.QSs = QSs;
        
        // 次のゲームシーンに移る
        SceneManager.LoadScene("SolveQuestion_Scene");
    }

}
