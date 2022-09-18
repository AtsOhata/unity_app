using System.Collections.Generic;
using System.Text;

static public class StringUtility
{
    readonly static int ConvertionConstant = 65248;

    /// <summary>
    /// <para>文字列を指定バイト数で区切ってリスト化する</para>
    /// <para>英語等、単語間を半角スペースで区切っている場合最後の単語が途切れないように最後の半角スペースを探出してそこで区切る</para>
    /// <para>用途: 半角(1バイト)全角(2バイト)を考慮しつつ文字列の表示幅を合わせる事ができる</para>
    /// </summary>
    /// <param name="Text">文字列</param>
    /// <param name="LineLength">一行当たりの</param>
    /// <param name="ByteLength">指定バイト数</param>
    /// <returns>文字列をリスト化したもの</returns>
    static public List<string> CutTextB(string Text, int LineLength, int ByteLength)
    {
        // \n(改行)がある場合はLineLength * 半角スペースを足す
        

        var strList = new List<string>();
        Encoding e = Encoding.GetEncoding("Shift-JIS");

        while (e.GetByteCount(Text) > ByteLength)
        {
            string cutText = Text.LeftB(ByteLength);  // 指定バイト数で区切る
            int a = cutText.LastIndexOf(' ');  // 最後の半角スペースの位置を調べる
            if (a == -1) a = cutText.Length;  // 半角スペースがない場合CutLengthとなる
            strList.Add(cutText.Substring(0, a));  // 最初の指定バイト数(正確には単語区切りのため少し減る)の切り出し
            Text = Text.Substring(a);  // 残りのテキスト
        }
        strList.Add(Text);  // 最後の部分
        return strList;
    }


    /// <summary>
    /// SubStringのバイト数版
    /// </summary>
    /// <param name="text">文字列</param>
    /// <param name="maxByteCount">切り出しバイト数</param>
    /// <returns></returns>
    public static string LeftB(this string text, int maxByteCount)
    {
        Encoding e = Encoding.GetEncoding("Shift-JIS");
        byte[] bytes = e.GetBytes(text);
        if (bytes.Length <= maxByteCount) return text;

        var result = text.Substring(0, e.GetString(bytes, 0, maxByteCount).Length);

        while (e.GetByteCount(result) > maxByteCount)
        {
            result = result.Substring(0, result.Length - 1);
        }
        return result;
    }

    public static int CountBytes(this string Text)
    {
        Encoding e = Encoding.GetEncoding("Shift-JIS");
        return e.GetByteCount(Text);
    }

    public static byte[] GetBytes(this string Text)
    {
        Encoding e = Encoding.GetEncoding("Shift-JIS");
        return e.GetBytes(Text);
    }

    /// <summary>
    /// テキストをリッチテキスト化して問題とする<br></br>
    /// 独自のリッチテキスト化タグを使用する<br></br>
    /// (改行)タグ　-　改行する<br></br>
    /// (太字)(/太字)タグ　-　太字にする<br></br>
    /// (斜体)(/斜体)タグ　-　斜体にする<br></br>
    /// (赤)(/赤)タグ　-　赤にする<br></br>
    /// (ハイリン)(/ハイリン)タグ　-　ハイパーリンク状にする<br></br>
    /// </summary>
    public static string MakeRichText(this string text)
    {
        string Result = text.Replace("(太字)", "<b>");
        Result = Result.Replace("(/太字)", "</b>");
        Result = Result.Replace("(斜体)", "<i>");
        Result = Result.Replace("(/斜体)", "</i>");
        Result = Result.Replace("(改行)", "\n");
        Result = Result.Replace("(赤)", "<color=red>");
        Result = Result.Replace("(/赤)", "</color>");
        return Result;
    }

    /// <summary>
    /// 半角を全角にする
    /// </summary>
    /// <param name="halfWidthStr">半角文字列</param>
    /// <returns>全角に変換した文字列</returns>
    static public string ConvertToFullWidth(string halfWidthStr)
    {
        string fullWidthStr = null;

        for (int i = 0; i < halfWidthStr.Length; i++)
        {
            fullWidthStr += (char)(halfWidthStr[i] + ConvertionConstant);
        }

        return fullWidthStr;
    }

    /// <summary>
    /// 全角を半角にする
    /// </summary>
    /// <param name="fullWidthStr">全角文字列</param>
    /// <returns>半角に変換した文字列</returns>
    static public string ConvertToHalfWidth(string fullWidthStr)
    {
        string halfWidthStr = null;

        for (int i = 0; i < fullWidthStr.Length; i++)
        {
            halfWidthStr += (char)(fullWidthStr[i] - ConvertionConstant);
        }

        return halfWidthStr;
    }


}
