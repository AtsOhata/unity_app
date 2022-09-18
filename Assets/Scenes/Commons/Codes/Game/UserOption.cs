/// <summary>ユーザー設定</summary>
[System.Serializable]
public class UserOption
{
    /// <summary>ユーザー設定が入っているインスタンス</summary>
    public static UserOption Instance;

    /// <summary>ユーザー設定ファイル名</summary>
    readonly static string OptionFileName = "UserOption.xml";

    /// <summary>１度に出す問題量</summary>
    public int QLQuestionAmount;

    /// <summary>音量</summary>
    public float SoundValue;

    /// <summary>回答時秒数表示フラグ</summary>
    public bool SecondShowFlag;

    public UserOption()
    {
        QLQuestionAmount = 10;
        SoundValue = 1.0f;
        SecondShowFlag = true;
    }

    /// <summary>設定を読み込む</summary>
    /// <returns>設定の情報をOptionクラスに変換したもの</returns>
    static public UserOption Load()
    {
        UserOption data = FileUtility.LoadXml<UserOption>(OptionFileName, false);
        // 設定データが無ければ作成する
        if (data == null)
        {
            data = new UserOption();
            FileUtility.SaveXml(OptionFileName, data);
        }
        return data;
    }

    /// <summary>セーブする</summary>
    /// <param name="optionXml">セーブするOptionクラス</param>
    static public void Save()
    {
        FileUtility.SaveXml(OptionFileName, Instance);
    }

}
