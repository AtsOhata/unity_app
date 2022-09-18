using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Home_Manager : MonoBehaviour
{
    // =====
    // �t�h
    // =====
    /// <summary>�߂�{�^��</summary>
    public GameObject ReturnButton;
    /// <summary>���ѕ\���{�^��</summary>
    public GameObject ShowAchievementsButton;
    /// <summary>�ݒ�{�^��</summary>
    public GameObject OptionButton;
    /// <summary>�ݒ�</summary>
    public GameObject Option;
    /// <summary>�X�N���[���[</summary>
    public GameObject Scroller_GameObject;
    /// <summary>���b�Z�[�W�{�b�N�X</summary>
    public GameObject MsgBox;
    // ��L�t�h�̓��AUpdate()�ŊǗ�����K�v��������̂��i�[����
    ReturnButton returnButton;
    ShowAchievementsButton AchievementButton;
    OptionButton optionButton;
    Text NewsBoxText1;
    /// <summary>�ݒ�p�[�c</summary>
    List<OptionInterface> OptionAbstracts = new List<OptionInterface>();

    // ========
    // �e�p�[�c
    // ========
    /// <summary>�p�a�p�[�c</summary>
    public GameObject QBPrefab;
    /// <summary>�p�k�p�[�c</summary>
    public GameObject QLPrefab;
    /// <summary>�v���C���[�h(Word)�p�[�c</summary>
    public GameObject PlayModeWordsPrefab;
    /// <summary>�v���C���[�h(Story)�p�[�c</summary>
    public GameObject PlayModeStoryPrefab;
    /// <summary>���уp�[�c</summary>
    public GameObject AchievementPrefab;
    // ��L���i�̓��AUpdate()�ŊǗ�����K�v��������̂��i�[����
    List<QB> QBs = new List<QB>();
    List<QL> QLs = new List<QL>();
    List<Achievement> Achievements = new List<Achievement>();

    // ======
    // ���̑�
    // ======
    /// <summary>���V�[����DTO</summary>
    LoadQuestion_DTO loadQuestion_DTO = new LoadQuestion_DTO();

    void Start()
    {
        Initialization();

        WipeScreenContents();
        Home_DTO.ScreenConfig sc = GlobalVariables.Home_DTO.ScreenConfigs.Find(x => x.ScreenName == GlobalVariables.Home_DTO.CurrentScreenName);
        SetScrollContents(sc.ScreenName, sc.ScreenValue);
    }


    void Update()
    {
        UIAction();

        QBAction();

        QLAction();

        // UI�̓���
        void UIAction()
        {
            // �߂�{�^�����^�b�`���ꂽ��K�w���P������
            if (returnButton.CheckTouch())
            {
                WipeScreenContents();
                Option.SetActive(false);
                string previousScreenName = returnButton.ReturnScreen();
                SetScrollContents(previousScreenName);
                GlobalVariables.Home_DTO.CurrentScreenName = previousScreenName;
            }
            // ���ѕ\���{�^�����^�b�`���ꂽ����щ�ʂɈڂ�
            if (AchievementButton.CheckTouch())
            {
                // �ݒ��ʂɈڂ�
                if (GlobalVariables.Home_DTO.CurrentScreenName != AchievementButton.ScreenName)
                {
                    WipeScreenContents();
                    Option.SetActive(false);
                    SetScrollContents(AchievementButton.ScreenName);
                    // �ݒ��ʂ���̑J�ڈȊO�Ȃ玟�̉�ʂŖ߂�{�^�������������ɖ߂��悤�ɂ���
                    if (GlobalVariables.Home_DTO.CurrentScreenName != optionButton.ScreenName)
                        returnButton.AddPreviouScreenName(GlobalVariables.Home_DTO.CurrentScreenName);
                    // ���݂̉�ʖ��X�V
                    GlobalVariables.Home_DTO.CurrentScreenName = AchievementButton.ScreenName;
                }
                else
                {
                    WipeScreenContents();
                    Option.SetActive(false);
                    string previousScreenName = returnButton.ReturnScreen();
                    SetScrollContents(previousScreenName);
                    GlobalVariables.Home_DTO.CurrentScreenName = previousScreenName;
                }
            }
            // �ݒ�{�^�����^�b�`���ꂽ��ݒ��ʂɈڂ�
            if (optionButton.CheckTouch())
            {
                // �ݒ��ʂɈڂ�
                if (GlobalVariables.Home_DTO.CurrentScreenName != optionButton.ScreenName)
                {
                    WipeScreenContents();
                    Option.SetActive(true);
                    // ���щ�ʂ���̑J�ڈȊO�Ȃ玟�̉�ʂŖ߂�{�^�������������ɖ߂��悤�ɂ���
                    if(GlobalVariables.Home_DTO.CurrentScreenName != AchievementButton.ScreenName)
                        returnButton.AddPreviouScreenName(GlobalVariables.Home_DTO.CurrentScreenName);
                    // ���݂̉�ʖ��X�V
                    GlobalVariables.Home_DTO.CurrentScreenName = optionButton.ScreenName;
                }
                // ���ݐݒ��ʂȂ�O�̉�ʂɖ߂�
                else
                {
                    WipeScreenContents();
                    Option.SetActive(false);
                    string previousScreenName = returnButton.ReturnScreen();
                    SetScrollContents(previousScreenName);
                    GlobalVariables.Home_DTO.CurrentScreenName = previousScreenName;
                }
            }

            // ���т̓���
            Achievements.ForEach(x => x.CheckTouch());
            // �ݒ�̓���
            if (Option.activeSelf)
            {
                OptionAbstracts.ForEach(x => x.Act());
            }

        }

        // QB�̓���
        void QBAction()
        {
            // QB���^�b�`���ꂽ��QB����ۑ����Ď��̉�ʂ֍s��
            for (int i = 0; i < QBs.Count; i++)
            {
                // ���@�̉�ʃT�C�Y�Ɉ���Ȃ���ScrollView�z����Content��z���W���}�C�i�X�ɂȂ邱�Ƃ�����̂ł��̑΍�
                if (QBs[i].GetComponent<RectTransform>().position.z < 0)
                    QBs[i].GetComponent<RectTransform>().position = new Vector3(QBs[i].transform.position.x, QBs[i].transform.position.y, 1f);

                if (QBs[i].CheckTouch())
                {
                    loadQuestion_DTO.QBTitle = QBs[i].transform.Find("Text").GetComponent<Text>().text;
                    string str = QBs[i].NextScreenName;
                    WipeScreenContents();
                    SetScrollContents(str);
                    // ���̉�ʂŖ߂�{�^�������������ɖ߂��悤�ɂ���
                    returnButton.AddPreviouScreenName(GlobalVariables.Home_DTO.CurrentScreenName);
                    // ���݂̉�ʖ��ۑ�
                    GlobalVariables.Home_DTO.CurrentScreenName = str;
                    break;
                }
            }
        }
        // QL�̓���
        void QLAction()
        {
            string nextScreenName = "";
            // QL���^�b�`���ꂽ��QL����ۑ����Ď��̉�ʂ֍s��
            QLs.ForEach(x =>
            {
                // ���@�̉�ʃT�C�Y�Ɉ���Ȃ���ScrollView�z����Content��z���W���}�C�i�X�ɂȂ邱�Ƃ�����̂ł��̑΍�
                if (x.GetComponent<RectTransform>().position.z < 0)
                    x.GetComponent<RectTransform>().position = new Vector3(x.transform.position.x, x.transform.position.y, 1f);

                nextScreenName = x.CheckTouch();
                loadQuestion_DTO.QLTitle = x.transform.Find("Text").GetComponent<Text>().text;
                if (!string.IsNullOrEmpty(nextScreenName))
                {
                    ToSceneLoadQuestion(x);
                }
            });
        }
    }

    /// <summary>��ʖ��ɉ������p�[�c����ʂɔ��f������</summary>
    /// <param name="ScreenName">��ʖ�</param>
    void SetScrollContents(string ScreenName, float ScrollValue = 1.0f)
    {
        List<Transform> screenParts = new List<Transform>();

        // �J�ڑO��ʏ�Ԃ̕ۑ�
        if (GlobalVariables.Home_DTO.CurrentScreenName != ScreenName)
        {
            Home_DTO.ScreenConfig previousSc = GlobalVariables.Home_DTO.ScreenConfigs.Find(x => x.ScreenName == GlobalVariables.Home_DTO.CurrentScreenName);
            // �X�N���[����Ԃ̕ۑ�
            if (Scroller_GameObject.GetComponentInChildren<Scrollbar>() != null)
                previousSc.ScreenValue = Scroller_GameObject.GetComponentInChildren<Scrollbar>().value;
        }

        // �J�ڐ��ʃR���t�B�O�̎擾
        Home_DTO.ScreenConfig sc = GlobalVariables.Home_DTO.ScreenConfigs.Find(x => x.ScreenName == ScreenName);

        // ��ʁ@�؂�ւ��E�X�N���[��
        int screenType = sc.ScreenType;

        // �p�[�c�̐ݒ�
        Transform st;
        if (screenType == 2)  // �X�N���[��
        {
            st = Scroller_GameObject.transform.Find("Viewport/Content");
        }
        else  // �؂�ւ�
        {
            st = Scroller_GameObject.transform.Find("Viewport/Content");
        }
        GlobalVariables.Home_DTO.Parts.ForEach(Part =>
        {
            if (Part.ScreenName == ScreenName)
            {
                if (Part.PartType == "QB")
                {
                    GameObject part = Instantiate(QBPrefab, st);
                    QB qB = part.GetComponent<QB>();
                    qB.Initialize(Part.Values);
                    part.transform.localScale = Vector3.Scale(QBPrefab.transform.localScale, new Vector3(1.0f, 1.0f, 1.0f));  // ���@�ŃT�C�Y������ɕύX����Ă��܂��΍�
                    QBs.Add(qB);
                    screenParts.Add(part.transform);
                }
                else if (Part.PartType == "QL")
                {
                    GameObject part = Instantiate(QLPrefab, st);
                    QL qL = part.GetComponent<QL>();
                    qL.Initialize(Part.Values);
                    part.transform.localScale = Vector3.Scale(QLPrefab.transform.localScale, new Vector3(1.0f, 1.0f, 1.0f));  // ���@�ŃT�C�Y������ɕύX����Ă��܂��΍�
                    QLs.Add(qL);
                    screenParts.Add(part.transform);
                    part.transform.SetParent(st);
                }
                else if (Part.PartType == "Achievement")
                {
                    GameObject part = Instantiate(AchievementPrefab, st);
                    Achievement a = part.GetComponent<Achievement>();
                    Achievements.Add(a);
                    part.transform.localScale = Vector3.Scale(AchievementPrefab.transform.localScale, new Vector3(1.0f, 1.0f, 1.0f));  // ���@�ŃT�C�Y������ɕύX����Ă��܂��΍�
                    Text MsgBoxText1 = MsgBox.transform.Find("Text1").GetComponent<Text>();
                    Text MsgBoxText2 = MsgBox.transform.Find("Text2").GetComponent<Text>();
                    a.Initialize(Part.Values, MsgBoxText1, MsgBoxText2);
                    screenParts.Add(part.transform);
                    part.transform.SetParent(st);
                }
            }
        });

            if (Scroller_GameObject.GetComponentInChildren<Scrollbar>() != null)
            Scroller_GameObject.GetComponentInChildren<Scrollbar>().value = sc.ScreenValue;
    }

    /// <summary> LoadQuestion�֍s��</summary>
    /// <param name="chosenQL">�I�΂ꂽQL</param>
    void ToSceneLoadQuestion(QL chosenQL)
    {
        // ����Home��ʂɖ߂鎞��Home_DTO�̐ݒ� (Home_DTO�͍폜�����c��)
        GlobalVariables.Home_DTO.PreviousScreenNames = returnButton.PreviousScreenNames;
        Home_DTO.ScreenConfig sc= GlobalVariables.Home_DTO.ScreenConfigs.Find(x => x.ScreenName == GlobalVariables.Home_DTO.CurrentScreenName);
        sc.ScreenValue = Scroller_GameObject.GetComponentInChildren<Scrollbar>().value;

        // ���V�[����DTO�ɒl���l�߂�
        LoadQuestion_DTO loadQuestionDTO = new LoadQuestion_DTO();
        loadQuestionDTO.QLTitle = chosenQL.QLTitle;  // QL����
        loadQuestionDTO.ChosenQLPath = chosenQL.QLPath;  // �I�΂ꂽQL�̃p�X
        loadQuestionDTO.QuestionQuantity = 10;  // UserOption.Instance.QLQuestionAmount;  // ��萔
        GlobalVariables.LoadQuestion_DTO = loadQuestionDTO;

        // ���V�[���ɑJ�ڂ���
        SceneManager.LoadScene("LoadQuestion_Scene");
    }


    /// <summary>�X�N���[���I�u�W�F�N�g�̍X�V</summary>
    void WipeScreenContents()
    {
        QBs.Clear();
        QLs.Clear();
        Achievements.Clear();
        foreach (Transform t in Scroller_GameObject.transform.Find("Viewport/Content"))
            Destroy(t.gameObject);
    }

    /// <summary>������</summary>
    void Initialization()
    {
        // UI�̏�����
        returnButton = ReturnButton.GetComponent<ReturnButton>();
        returnButton.Initialize();
        AchievementButton = ShowAchievementsButton.GetComponent<ShowAchievementsButton>();
        AchievementButton.Initialize("����");
        optionButton = OptionButton.GetComponent<OptionButton>();
        optionButton.Initialize("�ݒ�");
        Option.SetActive(false);  // �ݒ��ʂ͍ŏ���\��
        OptionAbstracts.AddRange(Option.transform.GetComponentsInChildren<OptionInterface>());  // �ݒ荀�ڎ擾
        OptionAbstracts.ForEach(x => x.Initialize());  // �ݒ荀�ڏ�����

        // ��U���b�Z�[�W�{�b�N�X���ȗ�
        MsgBox.SetActive(false);
    }
}
