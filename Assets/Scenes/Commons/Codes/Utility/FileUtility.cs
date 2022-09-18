using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class FileUtility : MonoBehaviour
{

    static string ResourceFolderName;

    /// <summary>全てのアプリ情報が入っているリソースフォルダ名を読み込む</summary>
    public static void Initialization()
    {
        ResourceFolderName = ReadText("CurrentProjectName")[0];
    }

    /// <summary>Spriteの取得 (jpg/pngなど)</summary>
    /// <param name="FileName">画像の名前(拡張子を除く)</param>
    /// <returns>Sprite</returns>
    public static Sprite ReadSprite(string FileName)
    {
        Debug.Log(ResourceFolderName + FileName);
        return Resources.Load<Sprite>(ResourceFolderName + FileName);
    }

    public static AudioClip LoadAudioClip(string FileName)
    {
        return Resources.Load<AudioClip>(ResourceFolderName + "/SE/" + FileName);
    }

    /// <summary>txt/csvファイルを読み込む関数</summary>
    /// <param name="FilePath">Resources配下のパス(拡張子を除く)</param>
    /// <returns>読み込んだデータの配列</returns>
    static public List<string> ReadText(string FilePath)
    {
        if (!string.IsNullOrEmpty(ResourceFolderName))
            FilePath = ResourceFolderName + "/" + FilePath;

        // データを入れるリスト 
        List<string> textData = new List<string>();
        // ファイルの読み込み
        TextAsset textFile = Resources.Load(FilePath) as TextAsset; // Resources下のCSV読み込み
        StringReader reader = new StringReader(textFile.text);
        // 一行ずつ読み込み
        // リストに追加していく
        while (reader.Peek() != -1) // reader.Peekが-1になるまで
        {
            textData.Add(reader.ReadLine()); // リストに行を追加
        }
        return textData;
    }

    /// <summary>指定したxmlファイルからデータを読み込み指定した型に変換する</summary>
    /// <typeparam name="T">変換先型</typeparam>
    /// <param name="XmlPath">Xmlのパス</param>
    /// <param name="IsResource">true = Resourceより取得<br></br>false = PersistentDataより取得</param>
    /// <returns>読み込んだxmlファイルを指定型に変換したもの</returns>
    static public T LoadXml<T>(string XmlPath, bool IsResource = true) where T : class, new()
    {
        // Resourcesより取得
        if (IsResource)
        {
            if (!string.IsNullOrEmpty(ResourceFolderName))
                XmlPath = ResourceFolderName + "/" + XmlPath;

            TextAsset textFile = Resources.Load(XmlPath) as TextAsset; // Resources下のCSV読み込み
            using (var reader = new StringReader(textFile.text))
            {
                var serializer = new XmlSerializer(typeof(T));
                return serializer.Deserialize(reader) as T;
            }
        }

        // PersistentDataPathより取得 (ビルド時には含まれない為見つからない場合を考慮する)
        else
        {
            string filePath = Path.Combine(Application.persistentDataPath, XmlPath);
            // Xmlが見つからない場合はnullを返す
            if (!File.Exists(filePath))
            {
                return null;
            }
            XmlSerializer xmls = new XmlSerializer(typeof(T));
            return DeserializeT(filePath);
        }

        // xmlをデシリアライズ
        T DeserializeT(string path)
        {
            XmlSerializer xmls = new XmlSerializer(typeof(T));
            using (FileStream fs = File.OpenRead(path))
            {
                return xmls.Deserialize(fs) as T;  // デシリアライズ = xml形式からクラス形式にする
            }
        }

    }

    static public void SaveXml<T>(string XmlPath, T t) where T : class, new()
    {
        string filePath = Path.Combine(Application.persistentDataPath, XmlPath);
        using (StreamWriter sw = new StreamWriter(filePath, false)) //true=追記 false=上書き
        {
            XmlSerializer xmls = new XmlSerializer(typeof(T));
            xmls.Serialize(sw, t);  // シリアライズ = クラス形式からxml形式にする
        }
    }

}
