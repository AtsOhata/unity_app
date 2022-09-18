using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class ShowAnswer_Manager : MonoBehaviour
{

    // �^�u
    enum TAB
    {
        GROSS_RESULT,  // �������ʃ^�u
        EACH_RESULT  // �ʌ��ʃ^�u
    }
    TAB CurrentTab = TAB.GROSS_RESULT;

    // ------
    // ���̑�
    // ------
    /// <summary>�g�b�v��</summary>
    public GameObject ToTop;

    // --------
    // ��������
    // --------
    /// <summary>�������ʃ^�u</summary>
    public GameObject Tab1;
    /// <summary>�������ʃ^�u</summary>
    public GameObject Tab1_Tab;

    // --------
    // �ʌ���
    // --------
    /// <summary>�ʌ��ʃ^�u</summary>
    public GameObject Tab2;
    /// <summary>�Z</summary>
    public Sprite Tab2_Round;
    /// <summary>�~</summary>
    public Sprite Tab2_Cross;
    /// <summary>�ʌ��ʃ^�u</summary>
    public GameObject Tab2_Tab;

    public GameObject ScrollViewQR;  // ����ScrollView
    GameObject QRBase;  // ���񓚂�GameObjectPrefab
    List<GameObject> QRs = new List<GameObject>();  // ���񓚂�GameObject

    void Start()
    {
        // QL���́@�̃e�L�X�g���f
        Tab1.transform.Find("QL����").GetComponent<Text>().text = GlobalVariables.ShowAnswer_DTO.QLTitle;

        // ���𐔁@�̃e�L�X�g���f
        Tab1.transform.Find("����").GetComponent<Text>().text = GlobalVariables.ShowAnswer_DTO.QR.Count(x => x.IsCorrect == true) + "/" + GlobalVariables.ShowAnswer_DTO.GrossAnswerQuantity;

        // �����@�̃e�L�X�g���f
        Tab1.transform.Find("����").GetComponent<Text>().text = (((float)GlobalVariables.ShowAnswer_DTO.QR.Count(x => x.IsCorrect == true) / GlobalVariables.ShowAnswer_DTO.GrossAnswerQuantity) * 100).ToString("F3") + "%";

        // ShowAnswerDTO�̕��̖��𐶐�����
        if (QRBase == null) QRBase = ScrollViewQR.transform.Find("ViewportQR/ContentQR/QR").gameObject;  //��������v���n�u�̎擾
        QRBase.SetActive(true);
        Vector3 baseSize = QRBase.transform.localScale;  // �v���n�u�̃T�C�Y
        int index = 1;  // �ʂ��ԍ�
        GlobalVariables.ShowAnswer_DTO.QR.ForEach(x =>
        {
            GameObject g = Instantiate(QRBase);
            g.transform.SetParent(ScrollViewQR.transform.Find("ViewportQR/ContentQR"));  // �e��Content���w��
            g.transform.Find("Text_Problem").GetComponent<Text>().text = x.Question;  // ���
            // ��肪null�Ȃ���摜�̈ʒu��ς���
            if (string.IsNullOrEmpty(x.Question))
                g.transform.Find("Image_QuestionImage").GetComponent<RectTransform>().position = new Vector3(-50.0f, g.transform.Find("Image_QuestionImage").GetComponent<RectTransform>().position.y, g.transform.Find("Image_QuestionImage").GetComponent<RectTransform>().position.z);
            if (!string.IsNullOrEmpty(x.Picture))  // ���摜
                g.transform.Find("Image_QuestionImage").GetComponent<Image>().sprite = FileUtility.ReadSprite("/QL/Pics/" + x.Picture);
            else
                g.transform.Find("Image_QuestionImage").gameObject.SetActive(false);
            if (x.IsCorrect)
                g.transform.Find("Image_Background").GetComponent<Image>().color = new Color(0.85f, 1f, 0.82f, 1f);
            else
                g.transform.Find("Image_Background").GetComponent<Image>().color = new Color(1f, 0.85f, 0.82f, 1f);
            g.transform.Find("Text_PlayerAnswer").GetComponent<Text>().text = string.Join(",", x.PlayerAnswers);  // �v���C���[�̉�
            g.transform.Find("Text_CorrectAnswer").GetComponent<Text>().text = string.Join(",", x.CorrectAnswers);  // ����
            if(UserOption.Instance.SecondShowFlag)
                g.transform.Find("Text_TimeForAnswer").GetComponent<Text>().text = x.TimeForAnswer.ToString("F3") + " s";  // �񓚎���
            g.transform.localScale = Vector3.Scale(baseSize, new Vector3(0.95f, 0.95f, 1f));  // �Ȃ�������ɃT�C�Y���ύX����Ă��܂��̂Ŗh��
            QRs.Add(g);
            index++;
        });
        QRBase.SetActive(false);  // �v���n�u��A�N�e�B�u

        // �������ʃ^�u��\������
        ChangeTab(TAB.GROSS_RESULT);
    }

    void Update()
    {
        QRs.ForEach(x =>
        {
            // ���@�̉�ʃT�C�Y�Ɉ���Ȃ���ScrollView�z����Content��z���W���}�C�i�X�ɂȂ邱�Ƃ�����̂ł��̑΍�
            if (x.GetComponent<RectTransform>().position.z < 0)
                x.GetComponent<RectTransform>().position = new Vector3(x.transform.position.x, x.transform.position.y, 1f);
        });

        // ------------
        // �������ʃ^�u
        // ------------
        if (Tab1_Tab.GetComponent<TouchDetect>().DetectTouch()) ChangeTab(TAB.GROSS_RESULT);

        // ------------
        // �ʌ��ʃ^�u
        // ------------
        if (Tab2_Tab.GetComponent<TouchDetect>().DetectTouch()) ChangeTab(TAB.EACH_RESULT);

        // �g�b�v�ւ������ꂽ��
        if (ToTop.GetComponent<TouchDetect>().DetectTouch())
        {
            Finalizes();
            // Scene_Home�Ɉڂ�
            SceneManager.LoadScene("Home_Scene");
        }
    }

    void ChangeTab(TAB tabTo)
    {
        // ���ׂẴ^�u�̓��e���\������
        Tab1.SetActive(false);
        Tab2.SetActive(false);
        // �ړI�̃^�u�̓��e��\������
        if (tabTo == TAB.GROSS_RESULT) Tab1.SetActive(true);
        else if (tabTo == TAB.EACH_RESULT) Tab2.SetActive(true);

        CurrentTab = tabTo;
    }

    void Finalizes()
    {
        // �g�p�ς�DTO������
        GlobalVariables.ShowAnswer_DTO = new ShowAnswer_DTO();
        // SolveQuestion�V�[������󂯌p����Sound�I�u�W�F�N�g������(�����Ȃ��ƂQ��ڂ�SolveQuestion�Ńo�O��)
        Destroy(GameObject.Find("Sound"));
    }

}
