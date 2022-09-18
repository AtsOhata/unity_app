using System.Collections.Generic;

/// <summary>
/// ホーム画面のパーツ情報が記載されている
/// </summary>lkmn 
[System.Serializable]
public class Home_DTO
{
    /// <summary>現在の画面名</summary>
    public string CurrentScreenName;

    /// <summary>画面設定リスト</summary>
    public List<ScreenConfig> ScreenConfigs = new List<ScreenConfig>();

    /// <summary>画面変遷履歴<br></br>次にホーム画面に来るときに元の配置に戻す用</summary>
    public List<string> PreviousScreenNames = new List<string>();

    /// <summary>パーツリスト</summary>
    public List<Part> Parts = new List<Part>();

    public class ScreenConfig
    {
        /// <summary>
        /// 出現する画面名<br></br>
        /// 其々の画面に名称が付けられており、対応した画面に於いてパーツが出現する<br></br>
        /// 最初の画面の画面名は""<br></br>
        /// </summary>
        public string ScreenName;
        /// <summary>
        /// 画面タイプ 1-切り替え 2-スクロール
        /// </summary>
        public int ScreenType = 2;

        /// <summary>
        /// スクロール値
        /// </summary>
        public float ScreenValue = 1f;
    }

    /// <summary>パーツ</summary>
    public class Part
    {
        /// <summary>
        /// 出現する画面名<br></br>
        /// ScreenConfig.ScreenNameと一致する
        /// </summary>
        public string ScreenName;

        /// <summary>
        /// パーツタイプ<br></br>
        /// 使用するプレハブを指定する<br></br>
        /// </summary>
        public string PartType;

        /// <summary>
        /// パーツが使用する値<br></br>
        /// パーツ細部の仕様確定や情報伝達に使用する<br></br>
        /// </summary>
        public List<string> Values;
    }

    /// <summary>データを読み込む</summary>
    /// <returns>Home_DTO.xmlの情報をHome_DTOクラスに変換したもの</returns>
    static public Home_DTO Load()
    {
        return FileUtility.LoadXml<Home_DTO>("Home_DTO", true);
    }
}






