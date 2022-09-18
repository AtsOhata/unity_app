using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class FileUtility : MonoBehaviour
{

    static string ResourceFolderName;

    /// <summary>�S�ẴA�v����񂪓����Ă��郊�\�[�X�t�H���_����ǂݍ���</summary>
    public static void Initialization()
    {
        ResourceFolderName = ReadText("CurrentProjectName")[0];
    }

    /// <summary>Sprite�̎擾 (jpg/png�Ȃ�)</summary>
    /// <param name="FileName">�摜�̖��O(�g���q������)</param>
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

    /// <summary>txt/csv�t�@�C����ǂݍ��ފ֐�</summary>
    /// <param name="FilePath">Resources�z���̃p�X(�g���q������)</param>
    /// <returns>�ǂݍ��񂾃f�[�^�̔z��</returns>
    static public List<string> ReadText(string FilePath)
    {
        if (!string.IsNullOrEmpty(ResourceFolderName))
            FilePath = ResourceFolderName + "/" + FilePath;

        // �f�[�^�����郊�X�g 
        List<string> textData = new List<string>();
        // �t�@�C���̓ǂݍ���
        TextAsset textFile = Resources.Load(FilePath) as TextAsset; // Resources����CSV�ǂݍ���
        StringReader reader = new StringReader(textFile.text);
        // ��s���ǂݍ���
        // ���X�g�ɒǉ����Ă���
        while (reader.Peek() != -1) // reader.Peek��-1�ɂȂ�܂�
        {
            textData.Add(reader.ReadLine()); // ���X�g�ɍs��ǉ�
        }
        return textData;
    }

    /// <summary>�w�肵��xml�t�@�C������f�[�^��ǂݍ��ݎw�肵���^�ɕϊ�����</summary>
    /// <typeparam name="T">�ϊ���^</typeparam>
    /// <param name="XmlPath">Xml�̃p�X</param>
    /// <param name="IsResource">true = Resource���擾<br></br>false = PersistentData���擾</param>
    /// <returns>�ǂݍ���xml�t�@�C�����w��^�ɕϊ���������</returns>
    static public T LoadXml<T>(string XmlPath, bool IsResource = true) where T : class, new()
    {
        // Resources���擾
        if (IsResource)
        {
            if (!string.IsNullOrEmpty(ResourceFolderName))
                XmlPath = ResourceFolderName + "/" + XmlPath;

            TextAsset textFile = Resources.Load(XmlPath) as TextAsset; // Resources����CSV�ǂݍ���
            using (var reader = new StringReader(textFile.text))
            {
                var serializer = new XmlSerializer(typeof(T));
                return serializer.Deserialize(reader) as T;
            }
        }

        // PersistentDataPath���擾 (�r���h���ɂ͊܂܂�Ȃ��׌�����Ȃ��ꍇ���l������)
        else
        {
            string filePath = Path.Combine(Application.persistentDataPath, XmlPath);
            // Xml��������Ȃ��ꍇ��null��Ԃ�
            if (!File.Exists(filePath))
            {
                return null;
            }
            XmlSerializer xmls = new XmlSerializer(typeof(T));
            return DeserializeT(filePath);
        }

        // xml���f�V���A���C�Y
        T DeserializeT(string path)
        {
            XmlSerializer xmls = new XmlSerializer(typeof(T));
            using (FileStream fs = File.OpenRead(path))
            {
                return xmls.Deserialize(fs) as T;  // �f�V���A���C�Y = xml�`������N���X�`���ɂ���
            }
        }

    }

    static public void SaveXml<T>(string XmlPath, T t) where T : class, new()
    {
        string filePath = Path.Combine(Application.persistentDataPath, XmlPath);
        using (StreamWriter sw = new StreamWriter(filePath, false)) //true=�ǋL false=�㏑��
        {
            XmlSerializer xmls = new XmlSerializer(typeof(T));
            xmls.Serialize(sw, t);  // �V���A���C�Y = �N���X�`������xml�`���ɂ���
        }
    }

}
