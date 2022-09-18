using System.Collections.Generic;
using System.Text;

static public class StringUtility
{
    readonly static int ConvertionConstant = 65248;

    /// <summary>
    /// <para>��������w��o�C�g���ŋ�؂��ă��X�g������</para>
    /// <para>�p�ꓙ�A�P��Ԃ𔼊p�X�y�[�X�ŋ�؂��Ă���ꍇ�Ō�̒P�ꂪ�r�؂�Ȃ��悤�ɍŌ�̔��p�X�y�[�X��T�o���Ă����ŋ�؂�</para>
    /// <para>�p�r: ���p(1�o�C�g)�S�p(2�o�C�g)���l����������̕\���������킹�鎖���ł���</para>
    /// </summary>
    /// <param name="Text">������</param>
    /// <param name="LineLength">��s�������</param>
    /// <param name="ByteLength">�w��o�C�g��</param>
    /// <returns>����������X�g����������</returns>
    static public List<string> CutTextB(string Text, int LineLength, int ByteLength)
    {
        // \n(���s)������ꍇ��LineLength * ���p�X�y�[�X�𑫂�
        

        var strList = new List<string>();
        Encoding e = Encoding.GetEncoding("Shift-JIS");

        while (e.GetByteCount(Text) > ByteLength)
        {
            string cutText = Text.LeftB(ByteLength);  // �w��o�C�g���ŋ�؂�
            int a = cutText.LastIndexOf(' ');  // �Ō�̔��p�X�y�[�X�̈ʒu�𒲂ׂ�
            if (a == -1) a = cutText.Length;  // ���p�X�y�[�X���Ȃ��ꍇCutLength�ƂȂ�
            strList.Add(cutText.Substring(0, a));  // �ŏ��̎w��o�C�g��(���m�ɂ͒P���؂�̂��ߏ�������)�̐؂�o��
            Text = Text.Substring(a);  // �c��̃e�L�X�g
        }
        strList.Add(Text);  // �Ō�̕���
        return strList;
    }


    /// <summary>
    /// SubString�̃o�C�g����
    /// </summary>
    /// <param name="text">������</param>
    /// <param name="maxByteCount">�؂�o���o�C�g��</param>
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
    /// �e�L�X�g�����b�`�e�L�X�g�����Ė��Ƃ���<br></br>
    /// �Ǝ��̃��b�`�e�L�X�g���^�O���g�p����<br></br>
    /// (���s)�^�O�@-�@���s����<br></br>
    /// (����)(/����)�^�O�@-�@�����ɂ���<br></br>
    /// (�Α�)(/�Α�)�^�O�@-�@�Α̂ɂ���<br></br>
    /// (��)(/��)�^�O�@-�@�Ԃɂ���<br></br>
    /// (�n�C����)(/�n�C����)�^�O�@-�@�n�C�p�[�����N��ɂ���<br></br>
    /// </summary>
    public static string MakeRichText(this string text)
    {
        string Result = text.Replace("(����)", "<b>");
        Result = Result.Replace("(/����)", "</b>");
        Result = Result.Replace("(�Α�)", "<i>");
        Result = Result.Replace("(/�Α�)", "</i>");
        Result = Result.Replace("(���s)", "\n");
        Result = Result.Replace("(��)", "<color=red>");
        Result = Result.Replace("(/��)", "</color>");
        return Result;
    }

    /// <summary>
    /// ���p��S�p�ɂ���
    /// </summary>
    /// <param name="halfWidthStr">���p������</param>
    /// <returns>�S�p�ɕϊ�����������</returns>
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
    /// �S�p�𔼊p�ɂ���
    /// </summary>
    /// <param name="fullWidthStr">�S�p������</param>
    /// <returns>���p�ɕϊ�����������</returns>
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
