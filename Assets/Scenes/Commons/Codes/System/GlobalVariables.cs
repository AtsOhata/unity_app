using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// シーン間で保持しておきたい変数を格納するクラス
/// </summary>
public class GlobalVariables : MonoBehaviour
{
    /// <summary>アプリの設定(Resourcesより取得)</summary>
    static public Dictionary<string, string> AppConstants = new Dictionary<string, string>();

    /// <summary>画面遷移に必要な情報</summary>
    static public Home_DTO Home_DTO = new Home_DTO();
    /// <summary>画面遷移に必要な情報</summary>
    static public LoadQuestion_DTO LoadQuestion_DTO = new LoadQuestion_DTO();
    /// <summary>画面遷移に必要な情報</summary>
    static public SolveQuestion_DTO SolveQuestion_DTO = new SolveQuestion_DTO();
    /// <summary>画面遷移に必要な情報</summary>
    static public ShowAnswer_DTO ShowAnswer_DTO = new ShowAnswer_DTO();

    void Start()
    {
        // シーン変更しても破棄されない
        DontDestroyOnLoad(this);
    }

}
