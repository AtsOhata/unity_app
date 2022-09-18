using UnityEngine;

/// <summary>
/// 固定値をここに書く ★将来的に外部ファイルから読み込めるようにする？
/// </summary>
public class GameOption : MonoBehaviour
{
    // デバッグモード　ONにすると個別シーンの起動等ができるようになったりする
    static public readonly bool IsDebugMode = false;

    // アプリの題名


    // *****
    // 共通
    // *****
    /// <summary>１つのセルに複数の情報が入っている時等に使用する区切り文字</summary>
    static public readonly char SEPARATOR = '#';

    // --------------------
    // Resources配下のパス
    // --------------------
    // QBInfo
    /// <summary>QBInfo.csvのパス</summary>
    static public readonly string QB_INFO_PATH = "QBInfo";
    /// <summary>問題帳</summary>
    static public readonly string QB_INFO_COLUMN_PB = "問題帳";
    /// <summary>問題帳副題</summary>
    static public readonly string QB_INFO_COLUMN_SUBTITLE = "問題帳副題";
    /// <summary>画像ファイル名</summary>
    static public readonly string QB_INFO_COLUMN_IMAGE_FILENAME = "画像ファイル名";
    /// <summary>問題リスト</summary>
    static public readonly string QB_INFO_COLUMN_QL = "問題リスト";
    /// <summary>問題リスト_ファイル名</summary>
    static public readonly string QB_INFO_COLUMN_QL_FILE_NAME = "問題リスト_ファイル名";

    // QLInfo
    /// <summary>QLInfo.csvのパス</summary>
    static public readonly string QL_INFO_PATH = "QLInfo";
    /// <summary>問題リスト</summary>
    static public readonly string QL_INFO_COLUMN_PL = "問題リスト";
    /// <summary>問題リスト_ファイル名</summary>
    static public readonly string QL_INFO_COLUMN_PL_FILENAME = "問題リスト_ファイル名";

    // 問題リスト
    /// <summary>問題リストフォルダーのパス</summary>
    static public readonly string QL_FOLDER_PATH = "QL";
    /// <summary>問題</summary>
    static public readonly string QL_COLUMN_PROBLEM = "問題";
    /// <summary>正答プール</summary>
    static public readonly string QL_COLUMN_CORRECT_ANSWER_POOL = "正答プール";
    /// <summary>間違いプール</summary>
    static public readonly string QL_COLUMN_WRONG_ANSWER_POOL = "間違いプール";
    /// <summary>正誤比率</summary>
    static public readonly string QL_COLUMN_CORRECT_WRONG_RATIO = "正誤比率";
    // 問題頻度リスト
    /// <summary>問題頻度リスト.csvのパス</summary>
    static public readonly string QUESTION_PROBABILITY_LIST = "QuestionProbabilityList";
    /// <summary>問題</summary>
    static public readonly string QUESTIONFREQUENCYLIST_COLUMN_PROBLEM = "問題";
    /// <summary>出題確率</summary>
    static public readonly string QUESTIONFREQUENCYLIST_COLUMN_PROBLEM_FREQUENCY = "出題確率";


    // --------------------
    // ゲームの設定
    // --------------------
    /// <summary>出題数</summary>
    static public readonly int QUESTION_QUANTITY = 10;
    /// <summary>デフォルト正誤比率</summary>
    static public readonly string DEFAULT_CORRECT_WRONG_RATIO = "1:3";
}
