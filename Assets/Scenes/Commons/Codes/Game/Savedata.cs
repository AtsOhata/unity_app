using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Savedata
{
    /// <summary>セーブデータが入っているインスタンス</summary>
    public static Savedata Instance;

    /// <summary>セーブデータファイル名</summary>
    readonly static public string SavedataFileName = "Savedata.xml";

    /// <summary>学習済み単語</summary>
    public List<LearnedWord> LearnedWords = new List<LearnedWord>();

    /// <summary>QLデータ</summary>
    public List<QLData> QLDatas = new List<QLData>();
    
    /// <summary>実績データ</summary>
    public List<AchievementData> AchievementDatas = new List<AchievementData>();

    /// <summary>学習済み単語</summary>
    public class LearnedWord
    {
        /// <summary>単語</summary>
        public string Alias;
        
        /// <summary>定着率(０(知らない)～１００％(知ってる))</summary>
        public float FixedPercentage;

        /// <summary>結果 (true = 正解　false = 不正解)</summary>
        public List<bool> Results = new List<bool>();

        /// <summary>回答時間</summary>
        public List<float> Time = new List<float>();

        /// <summary>
        /// 定着率計算メソッド<br></br>
        /// 最大で直近３回分の回答時間と結果から定着率を計算する<br></br>
        /// 最近１回目の結果が８０％に影響<br></br>
        /// 最近２回目の結果が７０％に影響<br></br>
        /// 最近３回目の結果が５０％に影響<br></br>
        /// 計算式 回答補正(正解で１/不正解で０) * 回数補正 * 速度補正(決められた秒以内で１・２秒超過で０．５)<br></br>
        /// </summary>
        public float CalcFixedPercentage(List<float> TimeForAnswer, List<bool> Results)
        {
            float fixedPercentage = 0;
            TimeForAnswer.Reverse();
            Results.Reverse();
            for (int i = 0; i < 3; i++)
            {
                // 回答時間と結果データが無かったら終わり
                if (TimeForAnswer.Count < i + 1 || Results.Count < i + 1)
                    break;
                int answer = Results[i] ? 1 : 0;  // 回答補正
                float speed = TimeForAnswer[i] < float.Parse(GlobalVariables.AppConstants["問題定着率計算時の速度補正秒数"]) ? 1 : 0.6f;  // 速度補正
                int trialTime = i == 0 ? 80 : i == 1 ? 70 : 50;  // 回数補正
                fixedPercentage += answer * speed * trialTime;
            }
            return fixedPercentage;
        }
    }

    /// <summary>
    /// QLの達成状態
    /// </summary>
    public class QLData
    {
        public QLData() { }
        public QLData(string QLTitle, float Percent)
        {
            this.QLTitle = QLTitle;
            this.Percent = Percent;
        }

        /// <summary>QLの名前</summary>
        public string QLTitle;
        
        /// <summary>達成率</summary>
        public float Percent;

        /// <summary>達成率を算出する</summary>
        /// <param name="LearnedWords">Savedata.xmlにあるLearnedWord</param>
        public void CalcPercent(List<LearnedWord> LearnedWords)
        {
            int LearnedQuestionsCount = 0;  // 身についた問題

            // QLDataフォルダ　から対象問題の通し名を抽出する
            List<string> targetWords;
            try
            {
                targetWords = FileUtility.ReadText("QLData/" + QLTitle);
            }
            catch (NullReferenceException)
            {
                // ファイルが見つからない場合に達成率を計算せずに飛ばす
                return;
            }
            // LearnedWordsに対象問題があり、その定着率が50%以上なら達成率に加算する
            LearnedWords.ForEach(x =>
            {
                if (targetWords.Contains(x.Alias) && x.FixedPercentage > 50)
                    LearnedQuestionsCount++;
            });

            // 達成率 = 身についた問題 / 対象問題数
            Percent = (float) LearnedQuestionsCount / targetWords.Count;
        }
    }

    /// <summary>実績の達成状態</summary>
    public class AchievementData
    {
        public AchievementData() { }
        public AchievementData(string Name, float Percent)
        {
            this.Name = Name;
            this.Percent = Percent;
            TargetPercent = int.Parse(GlobalVariables.AppConstants["実績のデフォルト目標％"]);
        }
        
        /// <summary>実績の名前</summary>
        public string Name;

        /// <summary>達成率</summary>
        public float Percent;

        /// <summary>達成目標％（対象問題リストの内何％達成していればＯＫ）</summary>
        public float TargetPercent;

        /// <summary>達成率を算出する</summary>
        /// <param name="LearnedWords">Savedata.xmlにあるLearnedWord</param>
        public void CalcPercent(List<LearnedWord> LearnedWords)
        {
            int LearnedQuestionsCount = 0;  // 身についた問題
            // AchievementDataフォルダ　から実績対象問題の通し名を抽出する
            List<string> targetWords;
            try
            {
                targetWords = FileUtility.ReadText("AchievementData/" + Name);
            } catch (NullReferenceException)
            {
                // ファイルが見つからない場合に達成率を計算せずに飛ばす
                return;
            }
            // LearnedWordsに実績対象問題があり、その定着率が一定以上なら達成率に加算する
            LearnedWords.ForEach(x =>
            {
                if (targetWords.Contains(x.Alias) && x.FixedPercentage >= int.Parse(GlobalVariables.AppConstants["実績" + Name + "の実績対象定着率％"]))
                    LearnedQuestionsCount++;
            });
            // 達成率 = 身についた問題 / 実績対象問題数 * 達成目標％の逆数
            Percent = (float) LearnedQuestionsCount / targetWords.Count * (100f / TargetPercent);
        }
    }

    /// <summary>セーブデータを読み込む</summary>
    /// <returns>Savedata.xmlの情報をSavedataクラスに変換したもの</returns>
    static public Savedata Load()
    {
        Savedata data = FileUtility.LoadXml<Savedata>(SavedataFileName, false);
        if(data == null)
        {
            // 初めてセーブデータ作成する際
            data = new Savedata();
            // AppSettingsからQLと実績を取得
            new List<string>(GlobalVariables.AppConstants["QL"].Split(',')).ForEach(x => data.QLDatas.Add(new QLData(x, 0)));
            new List<string>(GlobalVariables.AppConstants["実績"].Split(',')).ForEach(x => data.AchievementDatas.Add(new AchievementData(x, 0)));
            
            FileUtility.SaveXml(SavedataFileName, data);
        }
        return data;
    }

    /// <summary>セーブする</summary>
    /// <param name="savedataXml">セーブするデータが詰まったSavedataクラス</param>
    static public void Save(Savedata savedataXml)
    {
        FileUtility.SaveXml(SavedataFileName, savedataXml);
    }

}
