using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Savedata
{
    /// <summary>�Z�[�u�f�[�^�������Ă���C���X�^���X</summary>
    public static Savedata Instance;

    /// <summary>�Z�[�u�f�[�^�t�@�C����</summary>
    readonly static public string SavedataFileName = "Savedata.xml";

    /// <summary>�w�K�ςݒP��</summary>
    public List<LearnedWord> LearnedWords = new List<LearnedWord>();

    /// <summary>QL�f�[�^</summary>
    public List<QLData> QLDatas = new List<QLData>();
    
    /// <summary>���уf�[�^</summary>
    public List<AchievementData> AchievementDatas = new List<AchievementData>();

    /// <summary>�w�K�ςݒP��</summary>
    public class LearnedWord
    {
        /// <summary>�P��</summary>
        public string Alias;
        
        /// <summary>�蒅��(�O(�m��Ȃ�)�`�P�O�O��(�m���Ă�))</summary>
        public float FixedPercentage;

        /// <summary>���� (true = �����@false = �s����)</summary>
        public List<bool> Results = new List<bool>();

        /// <summary>�񓚎���</summary>
        public List<float> Time = new List<float>();

        /// <summary>
        /// �蒅���v�Z���\�b�h<br></br>
        /// �ő�Œ��߂R�񕪂̉񓚎��Ԃƌ��ʂ���蒅�����v�Z����<br></br>
        /// �ŋ߂P��ڂ̌��ʂ��W�O���ɉe��<br></br>
        /// �ŋ߂Q��ڂ̌��ʂ��V�O���ɉe��<br></br>
        /// �ŋ߂R��ڂ̌��ʂ��T�O���ɉe��<br></br>
        /// �v�Z�� �񓚕␳(�����łP/�s�����łO) * �񐔕␳ * ���x�␳(���߂�ꂽ�b�ȓ��łP�E�Q�b���߂łO�D�T)<br></br>
        /// </summary>
        public float CalcFixedPercentage(List<float> TimeForAnswer, List<bool> Results)
        {
            float fixedPercentage = 0;
            TimeForAnswer.Reverse();
            Results.Reverse();
            for (int i = 0; i < 3; i++)
            {
                // �񓚎��Ԃƌ��ʃf�[�^������������I���
                if (TimeForAnswer.Count < i + 1 || Results.Count < i + 1)
                    break;
                int answer = Results[i] ? 1 : 0;  // �񓚕␳
                float speed = TimeForAnswer[i] < float.Parse(GlobalVariables.AppConstants["���蒅���v�Z���̑��x�␳�b��"]) ? 1 : 0.6f;  // ���x�␳
                int trialTime = i == 0 ? 80 : i == 1 ? 70 : 50;  // �񐔕␳
                fixedPercentage += answer * speed * trialTime;
            }
            return fixedPercentage;
        }
    }

    /// <summary>
    /// QL�̒B�����
    /// </summary>
    public class QLData
    {
        public QLData() { }
        public QLData(string QLTitle, float Percent)
        {
            this.QLTitle = QLTitle;
            this.Percent = Percent;
        }

        /// <summary>QL�̖��O</summary>
        public string QLTitle;
        
        /// <summary>�B����</summary>
        public float Percent;

        /// <summary>�B�������Z�o����</summary>
        /// <param name="LearnedWords">Savedata.xml�ɂ���LearnedWord</param>
        public void CalcPercent(List<LearnedWord> LearnedWords)
        {
            int LearnedQuestionsCount = 0;  // �g�ɂ������

            // QLData�t�H���_�@����Ώۖ��̒ʂ����𒊏o����
            List<string> targetWords;
            try
            {
                targetWords = FileUtility.ReadText("QLData/" + QLTitle);
            }
            catch (NullReferenceException)
            {
                // �t�@�C����������Ȃ��ꍇ�ɒB�������v�Z�����ɔ�΂�
                return;
            }
            // LearnedWords�ɑΏۖ�肪����A���̒蒅����50%�ȏ�Ȃ�B�����ɉ��Z����
            LearnedWords.ForEach(x =>
            {
                if (targetWords.Contains(x.Alias) && x.FixedPercentage > 50)
                    LearnedQuestionsCount++;
            });

            // �B���� = �g�ɂ������ / �Ώۖ�萔
            Percent = (float) LearnedQuestionsCount / targetWords.Count;
        }
    }

    /// <summary>���т̒B�����</summary>
    public class AchievementData
    {
        public AchievementData() { }
        public AchievementData(string Name, float Percent)
        {
            this.Name = Name;
            this.Percent = Percent;
            TargetPercent = int.Parse(GlobalVariables.AppConstants["���т̃f�t�H���g�ڕW��"]);
        }
        
        /// <summary>���т̖��O</summary>
        public string Name;

        /// <summary>�B����</summary>
        public float Percent;

        /// <summary>�B���ڕW���i�Ώۖ�胊�X�g�̓������B�����Ă���΂n�j�j</summary>
        public float TargetPercent;

        /// <summary>�B�������Z�o����</summary>
        /// <param name="LearnedWords">Savedata.xml�ɂ���LearnedWord</param>
        public void CalcPercent(List<LearnedWord> LearnedWords)
        {
            int LearnedQuestionsCount = 0;  // �g�ɂ������
            // AchievementData�t�H���_�@������ёΏۖ��̒ʂ����𒊏o����
            List<string> targetWords;
            try
            {
                targetWords = FileUtility.ReadText("AchievementData/" + Name);
            } catch (NullReferenceException)
            {
                // �t�@�C����������Ȃ��ꍇ�ɒB�������v�Z�����ɔ�΂�
                return;
            }
            // LearnedWords�Ɏ��ёΏۖ�肪����A���̒蒅�������ȏ�Ȃ�B�����ɉ��Z����
            LearnedWords.ForEach(x =>
            {
                if (targetWords.Contains(x.Alias) && x.FixedPercentage >= int.Parse(GlobalVariables.AppConstants["����" + Name + "�̎��ёΏے蒅����"]))
                    LearnedQuestionsCount++;
            });
            // �B���� = �g�ɂ������ / ���ёΏۖ�萔 * �B���ڕW���̋t��
            Percent = (float) LearnedQuestionsCount / targetWords.Count * (100f / TargetPercent);
        }
    }

    /// <summary>�Z�[�u�f�[�^��ǂݍ���</summary>
    /// <returns>Savedata.xml�̏���Savedata�N���X�ɕϊ���������</returns>
    static public Savedata Load()
    {
        Savedata data = FileUtility.LoadXml<Savedata>(SavedataFileName, false);
        if(data == null)
        {
            // ���߂ăZ�[�u�f�[�^�쐬�����
            data = new Savedata();
            // AppSettings����QL�Ǝ��т��擾
            new List<string>(GlobalVariables.AppConstants["QL"].Split(',')).ForEach(x => data.QLDatas.Add(new QLData(x, 0)));
            new List<string>(GlobalVariables.AppConstants["����"].Split(',')).ForEach(x => data.AchievementDatas.Add(new AchievementData(x, 0)));
            
            FileUtility.SaveXml(SavedataFileName, data);
        }
        return data;
    }

    /// <summary>�Z�[�u����</summary>
    /// <param name="savedataXml">�Z�[�u����f�[�^���l�܂���Savedata�N���X</param>
    static public void Save(Savedata savedataXml)
    {
        FileUtility.SaveXml(SavedataFileName, savedataXml);
    }

}
