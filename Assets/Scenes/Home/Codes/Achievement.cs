using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���у{�^��<br></br>
/// �^�b�`���ꂽ����ъT�v��`�B���ɓ`����
/// </summary>
public class Achievement : MonoBehaviour
{
    /// <summary>���і��e�L�X�g�R���|�[�l���g</summary>
    public Text AchievementNameComponent;
    /// <summary>���ъT�v�e�L�X�g�R���|�[�l���g</summary>
    public Text AchievementSummaryComponent;

    TouchDetect TD;

    /// <summary>���і�</summary>
    string AchievementName;
    /// <summary>���ъT�v</summary>
    string AchievementSummary;

    /// <summary>���т̏�����</summary>
    /// <param name="values">[0] = ���і��@[1] = ���ъT�v</param>
    public void Initialize(List<string> values, Text achievementNameComponent, Text achievementSummaryComponent)
    {
        // �ŏ���Savedata�Ɏ��т����邩�ǂ��������ɍs���A�Ȃ����Savedata�ɑ���
        if (!Savedata.Instance.AchievementDatas.Exists(x => x.Name == values[2]))
        {
            Savedata.Instance.AchievementDatas.Add(new Savedata.AchievementData(values[2], 0));
            Savedata.Save(Savedata.Instance);
        }

        TD = GetComponent<TouchDetect>();

        Savedata.AchievementData achievement = Savedata.Instance.AchievementDatas.Find(x => x.Name == values[2]);
        // �B�����̎Z�o
        achievement.CalcPercent(Savedata.Instance.LearnedWords);

        AchievementName = values[0];
        AchievementSummary = values[1] + " ����" + achievement.Percent.ToString("F3") + "��";
        AchievementNameComponent = achievementNameComponent;
        AchievementSummaryComponent = achievementSummaryComponent;

        // ���ї� < ���ђB���ڕW�@�Ȃ瓧���x��������
        float a = achievement.Percent < achievement.TargetPercent ? 0.5f : 1f;
        transform.GetComponentInChildren<Image>().color = new Color(1.0f, 1.0f, 1.0f, a);
    }

    /// <summary>�^�b�`���ꂽ����ъT�v���Ǝ��ъT�v��`�B���ɓ`����</summary>
    public void CheckTouch()
    {
       if (TD.DetectTouch())
       {
            AchievementNameComponent.text = AchievementName;  // ���і�
            AchievementSummaryComponent.text = AchievementSummary;  // ���ъT�v
       }
    }
}
