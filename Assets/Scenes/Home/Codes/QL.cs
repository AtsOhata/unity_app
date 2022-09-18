using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QL : MonoBehaviour
{
    TouchDetect TD;

    public string QLTitle;
    public string QLPath;

    /// <summary>QL������</summary>
    /// <param name="values">
    ///   values[0] = QLTitle<br></br>
    ///   values[1] = QLPath<br></br>
    /// </param>
    public void Initialize(List<string> values)
    {
        TD = GetComponent<TouchDetect>();

        // �e�L�X�g���f
        transform.Find("Text").GetComponent<Text>().text = values[0];

        // ���擾
        QLTitle = values[0];
        QLPath = values[1];

        // �Y������QLData��Savedata�ɂ��邩�ǂ��������ɍs���A�Ȃ����Savedata�ɑ���
        if (!Savedata.Instance.QLDatas.Exists(x => x.QLTitle == QLTitle))
        {
            Savedata.Instance.QLDatas.Add(new Savedata.QLData(QLTitle, 0));
            Savedata.Save(Savedata.Instance);
        }
        Savedata.QLData qldata = Savedata.Instance.QLDatas.Find(x => x.QLTitle == QLTitle);
        // �B�����̎Z�o
        qldata.CalcPercent(Savedata.Instance.LearnedWords);

        // �B�������X���C�_�[�ɔ��f����
        // �B�������O�Ȃ�X���C�_�[�̐F�𕴂�킵�����߉B��
        if (qldata.Percent <= 0f)
            transform.Find("Slider/Fill Area/Fill").gameObject.SetActive(false);
        else
            transform.Find("Slider").GetComponent<Slider>().value = qldata.Percent;

        // �B������80%�ȏ�ł���΃g���t�B�[�\��
        if (qldata.Percent >= 0.8)
            transform.Find("Image").gameObject.SetActive(true);
        else
            transform.Find("Image").gameObject.SetActive(false);
    }

    /// <summary>�^�b�`���ꂽ�玟�̉�ʖ���Ԃ�</summary>
    public string CheckTouch()
    {
        return TD.DetectTouch() ? QLPath : "";
    }
}
