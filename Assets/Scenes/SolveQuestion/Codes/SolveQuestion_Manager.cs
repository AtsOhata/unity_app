using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Collections;

public class SolveQuestion_Manager : MonoBehaviour
{
    public GameObject UI;  // �t�h

    // ----
    // �t�h
    // ----
    GameObject UI_QuestionCounter;  // �o��Z��
    GameObject UI_Quit;  // ��߂�
    GameObject UI_Feedback;  // ����
    GameObject UI_StartButton;  // �J�n�{�^��
    public GameObject Sound;  // ��
    UI_QuestionCounter QuestionCounter;  // �o��Z��X�N���v�g
    UI_StartButton StartButton;  // �X�^�[�g�{�^���X�N���v�g

    /// <summary>��</summary>
    List<string> ChosenAnswers = new List<string>();

    /// <summary>���݂̖��</summary>
    int CurrentQuestionNumber = 0;

    /// <summary>���݂̖��̐����̐�</summary>
    int CurrentQuestionCorrectAnswerQuantity = 1;

    // ----------
    // �o��p�[�c
    // ----------
    /// <summary>�X�N���[���o�[</summary>
    public GameObject ScrollView_ScrollBarVertical;
    /// <summary>�����Ƀp�[�c(���i)��u���Ă������Ƃŏo���ʂ����</summary>
    public GameObject ScrollView_Content;

    /// <summary>�摜�p�[�c</summary>
    public GameObject PicturePrefab;
    /// <summary>�ǉ������p�[�c</summary>
    public GameObject ReadingMaterialPrefab;
    /// <summary>���ӏ����p�[�c</summary>
    public GameObject CautionPrefab;
    /// <summary>���p�[�c</summary>
    public GameObject QuestionPrefab;
    /// <summary>���ꓚ�����p�[�c</summary>
    public GameObject OO_AnswerPrefab;
    /// <summary>�n�j�^�m�f�p�[�c</summary>
    public GameObject OO_OKNGPrefab;
    /// <summary>�I�����p�[�c</summary>
    public GameObject MultipleChoicesPrefab;
    /// <summary>�L�q�p�[�c</summary>
    public GameObject DescriptionPrefab;
    /// <summary>���בւ��p�[�c</summary>
    public GameObject ChooseOrderChoicesPrefab;
    /// <summary>�����p�[�c</summary>
    public GameObject AnswerCheckButtonPrefab;

    // ------------
    // �I�����p�[�c
    // ------------
    public GameObject ChoiceWindow;

    // ��L���i�̓��AUpdate()�ŊǗ�����K�v��������̂��i�[����
    OO_OKNG oKNG;
    MultipleChoices multipleChoices;  // Choice���i
    InputField description;  // �L�q�p�[�c��InputField���i
    ChooseOrderChoices chooseOrderChoices;  // ���ёւ��{�^��
    GoToNextQuestionButton GoToNextQuestionButton;  // �����{�^��

    /// <summary>�񓚎���</summary>
    float TimeForAnswer;

    /// <summary>�t�B�[�h�o�b�N���邩�ǂ���</summary>
    bool FeedbackFlag = true;

    /// <summary>�t�B�[�h�o�b�N���ł���t���O</summary>
    bool IsFeedbacking = false;

    /// <summary>�t�B�[�h�o�b�N�J�E���^�[</summary>
    float FeedbackCount = 0f;

    /// <summary>�t�B�[�h�o�b�N�b��</summary>
    readonly float FeedbackTime = 0.6f;
    
    /// <summary>�����{�^���t���O</summary>
    bool GoToNextQuestionButtonFlag = false;
    
    /// <summary>�����{�^�������t���O</summary>
    bool AnswerCheckButtonFlag = false;
    void Start()
    {
        // �t�h�̃Q�[���I�u�W�F�N�g�ǂݍ���
        UI_QuestionCounter = UI.transform.Find("QuestionCounter").gameObject;
        UI_Quit = UI.transform.Find("Quit/Image").gameObject;
        UI_Feedback = UI.transform.Find("Feedback").gameObject;
        UI_StartButton = UI.transform.Find("StartButton").gameObject;
        // �t�h�̏�����
        UI_Feedback.GetComponent<Feedback>().Initialize(Sound);
        QuestionCounter = UI_QuestionCounter.GetComponent<UI_QuestionCounter>();
        QuestionCounter.Initialize(GlobalVariables.SolveQuestion_DTO.QSs.Count);
        StartButton = UI_StartButton.GetComponent<UI_StartButton>();
        StartButton.Initialize();
        StartCoroutine(QuitButton());
    }

    void Update()
    {
        // ==========
        // �J�n���̂�
        // ==========
        // �X�^�[�g�{�^���^�b�`
        if (UI_StartButton != null && StartButton.CheckTouch(Sound))
        {
            SetChoices(CurrentQuestionNumber);  // �ŏ��̖����Z�b�g����
            TimeForAnswer = 0;  // ���Ԍv���J�n
        }

        // ========
        // �񓚒i�K
        // ========
        // ���񎦌�̂O�D�P�b�@�Ɓ@�t�B�[�h�o�b�N����
        // �I���������I�ׂȂ��i����:����E�e�P�̃t���C���O���O�D�P�b�j
        if (TimeForAnswer > 0.1f && !IsFeedbacking)
        {
            // �n�j�^�m�f�^�b�`
            if (oKNG != null) oKNG.CheckTouch();

            // �I�����^�b�`
            if (multipleChoices != null)
                ChosenAnswers = multipleChoices.CheckTouch();

            // ���ёւ��I�����^�b�`
            if (chooseOrderChoices != null)
                ChosenAnswers = chooseOrderChoices.CheckTouch();
        }

        // �����t���O�Ǘ�
        if (GoToNextQuestionButtonFlag && GoToNextQuestionButton.CheckTouch())
            AnswerCheckButtonFlag = true;

        // ****************************
        // �������킹���t�B�[�h�o�b�N��
        // ****************************
        // �I������I�т������瓚�����킹����
        // ((�����{�^���t���OOFF && �񓚃��X�g�̒��g�̐������␳�����ƈ�v)
        // || (�����{�^�������t���OON))
        // && �t�B�[�h�o�b�N���ł͂Ȃ��@�ꍇ
        if (((!GoToNextQuestionButtonFlag && ChosenAnswers.Count >= CurrentQuestionCorrectAnswerQuantity)
            || AnswerCheckButtonFlag)
            && !IsFeedbacking)
        {
            // �o��Z�b�g�擾
            SolveQuestion_DTO.QS qS = GlobalVariables.SolveQuestion_DTO.QSs[CurrentQuestionNumber];

            // ���̃V�[����DTO
            ShowAnswer_DTO.QuestionResult qR = new ShowAnswer_DTO.QuestionResult();

            //===========
            // �������킹
            //===========
            qR.IsCorrect = true;
            // �����I��
            ChosenAnswers.ForEach(x =>
            { 
                if (!qS.Answers.Contains(x))
                    qR.IsCorrect = false; 
            });
            // �L�q
            if (description != null)
            {
                if (string.IsNullOrEmpty(description.text))
                    qR.IsCorrect = false;
                else
                {
                    List<string> playerAnswers = description.text.Split(',').ToList();
                    playerAnswers = playerAnswers.Distinct().ToList();
                    int count = 0;
                    playerAnswers.ForEach(x =>
                    {
                        if (qS.Answers.Contains(x))
                            count++;
                        else
                            qR.IsCorrect = false;
                    });
                    if (count != qS.PlayerChooseQuantity)
                        qR.IsCorrect = false;
                }
            }
            // ���בւ�
            if (chooseOrderChoices != null)
            {
                if (!(string.Join("", ChosenAnswers) == string.Join("", qS.Answers)))
                    qR.IsCorrect = false;
            }

            // �t�B�[�h�o�b�N
            if (FeedbackFlag)
            {
                // �Z�~�̃A�j���[�V����
                UI_Feedback.GetComponent<Feedback>().GiveFeedback(qR.IsCorrect, FeedbackTime);
                // ����I�����̐F��ς���
                if (multipleChoices != null)
                    multipleChoices.Feedback(qS.Answers);
                if (chooseOrderChoices != null)
                    chooseOrderChoices.Feedback(qS.Answers);
            };

            // ���V�[����DTO�ɋl�߂�
            GlobalVariables.ShowAnswer_DTO.GrossAnswerQuantity++;  // �r���ŉ񓚂����߂��ꍇ�̎����l���A�񓚖��ɋL�^
            qR.Question = qS.Question;  // ���L�^
            qR.QuestionAlignment = qS.QuestionAlignment;  // ��蕶������
            qR.Picture = qS.Image;  // �摜��
            // �v���C���[�񓚋L�^
            if (qS.QT == SolveQuestion_DTO.QT.DESCRIPTION)  // �L�q
                qR.PlayerAnswers = description.text.Split(',').ToList();
            else
                qR.PlayerAnswers = new List<string>(ChosenAnswers);  // �v���C���[�񓚋L�^
            if (qS.QT == SolveQuestion_DTO.QT.CHOOSE_ORDER)  // ���בւ��̎��͏���������t����
                for (int i = 0; i < qR.PlayerAnswers.Count; i++)
                    qR.PlayerAnswers[i] = (i + 1) + "." + qR.PlayerAnswers[i];
            qR.CorrectAnswers = qS.Answers;  // �����L�^
            if (qS.QT == SolveQuestion_DTO.QT.CHOOSE_ORDER)  // ���בւ��̎��͏���������t����
                for (int i = 0; i < qR.CorrectAnswers.Count; i++)
                    qR.CorrectAnswers[i] = (i + 1) + "." + qR.CorrectAnswers[i];
            qR.Choices = qS.Choices;  // �I�����L�^
            qR.TimeForAnswer = TimeForAnswer;  // �񓚎���
            GlobalVariables.ShowAnswer_DTO.QR.Add(qR);

            // ����P��̏���Savedata����擾
            Savedata.LearnedWord savedWord = Savedata.Instance.LearnedWords.Find(x => x.Alias == GlobalVariables.SolveQuestion_DTO.QSs[CurrentQuestionNumber].Alias);
            if (savedWord == null)  // ���߂Ă̒P��̎�
            {
                savedWord = new Savedata.LearnedWord();  // �V�P��o�^
                savedWord.Alias = qS.Alias;  // �ʂ薼
                savedWord.Results.Add(qR.IsCorrect);  // �������킹����
                savedWord.Time.Add(TimeForAnswer);  // �񓚎���
                savedWord.FixedPercentage = savedWord.CalcFixedPercentage(savedWord.Time, savedWord.Results);  // �蒅��
                Savedata.Instance.LearnedWords.Add(savedWord);
            }
            else  // ���Ƀf�[�^�����鎞
            {
                savedWord.Results.Add(qR.IsCorrect);  // �������킹����
                savedWord.Time.Add(TimeForAnswer);  // �񓚎���
                                                    //�������킹���ʂ� �񓚎��Ԃ͒��߂R�񕪂̂ݕۑ�����
                while (savedWord.Results.Count > 3)
                    savedWord.Results.RemoveAt(0);
                while (savedWord.Time.Count > 3)
                    savedWord.Time.RemoveAt(0);
                savedWord.FixedPercentage = savedWord.CalcFixedPercentage(savedWord.Time, savedWord.Results);  // �蒅��
                int a = Savedata.Instance.LearnedWords.FindIndex(x => x.Alias == qS.Alias);
                Savedata.Instance.LearnedWords[a] = savedWord;
            }

            ChosenAnswers.Clear();

            IsFeedbacking = true;

        }

        // *****************************************************
        // �t�B�[�h�o�b�N�I����@���̖���
        // *****************************************************
        if (!FeedbackFlag || FeedbackCount > FeedbackTime)
        {
            // �t�h�X�V
            UI_QuestionCounter.GetComponent<UI_QuestionCounter>().AddOne();

            // �񓚎��ԃ��Z�b�g
            TimeForAnswer = 0;

            if (CurrentQuestionNumber >= QuestionCounter.QuestionQuantity - 1)  // ���̖�肪�Ō�̖��ł��鎞
            {
                ToNextGameScene();
                return;
            }
            else  // ���̖��ֈڂ�
            {
                CurrentQuestionNumber++;
                SetChoices(CurrentQuestionNumber);
            }
            IsFeedbacking = false;
            FeedbackCount = 0;
        }

        // ���ԉ��Z
        TimeForAnswer += Time.deltaTime;

        // �t�B�[�h�o�b�N���̂݉��Z
        if (IsFeedbacking)
            FeedbackCount += Time.deltaTime;
    }

    void ToNextGameScene()
    {
        // ���V�[����DTO�Ƀf�[�^������
        GlobalVariables.ShowAnswer_DTO.QLTitle = GlobalVariables.SolveQuestion_DTO.QLTitle;
        // �g�p�ς�DTO����
        GlobalVariables.SolveQuestion_DTO.QSs = new List<SolveQuestion_DTO.QS>();
        // ���ђB�����̎Z�o
        Savedata.Instance.AchievementDatas.ForEach(x => x.CalcPercent(Savedata.Instance.LearnedWords));
        // �f�[�^�Z�[�u
        Savedata.Save(Savedata.Instance);
        // ���̃Q�[���V�[���ֈڂ�
        SceneManager.LoadScene("ShowAnswer_Scene");
    }

    /// <summary>���̃Z�b�g</summary>
    /// <param name="number">���ԍ�</param>
    void SetChoices(int number)
    {
        // �O��̏o��Z�b�g�X�N���[���������E�֘A�R���|�[�l���g������
        foreach (Transform t in ScrollView_Content.transform) Destroy(t.gameObject);  // ���ׂĂ̎q�I�u�W�F�N�g�폜
        foreach (Transform t in ChoiceWindow.transform) Destroy(t.gameObject);  // ���ׂĂ̎q�I�u�W�F�N�g�폜
        ScrollView_ScrollBarVertical.GetComponent<Scrollbar>().value = 1;

        // �o��Z�b�g�擾
        SolveQuestion_DTO.QS QS = GlobalVariables.SolveQuestion_DTO.QSs[number];

        // �摜������ꍇ
        if (!string.IsNullOrEmpty(QS.Image))
        {
            // �o��Z�b�g�ɉ摜�p�[�c��t������
            GameObject picture = Instantiate(PicturePrefab, ScrollView_Content.transform);  // �摜�p�[�c�̍쐬
            picture.transform.localScale = Vector3.Scale(ReadingMaterialPrefab.transform.localScale, new Vector3(1.0f, 1.0f, 1.0f));  // ���@�ŃT�C�Y������ɕύX����Ă��܂��΍�
            Image i = picture.GetComponent<Image>();
            i.sprite = FileUtility.ReadSprite("/QL/Pics/" + QS.Image);
            i.type = Image.Type.Filled;  // �摜�^�C�v(�ڂ�����Image��Sprite�ɓK���ȉ摜����荞�ނƕ�����) �A�X�y��ۑ��ɕK�v
            i.preserveAspect = true;
        }
        // �ǉ�����������ꍇ
        if (!string.IsNullOrEmpty(QS.ReadingMaterial))
        {
            // �o��Z�b�g�ɓǉ������p�[�c��t������
            GameObject readingMaterial = Instantiate(ReadingMaterialPrefab, ScrollView_Content.transform);  // �ǉ������p�[�c�̍쐬
            readingMaterial.transform.localScale = Vector3.Scale(ReadingMaterialPrefab.transform.localScale, new Vector3(1.0f, 1.0f, 1.0f));  // ���@�ŃT�C�Y������ɕύX����Ă��܂��΍�
            readingMaterial.GetComponent<ReadingMaterial>().Initialize(QS.ReadingMaterial);  // ����������
        }
        // ��肪����ꍇ
        if (!string.IsNullOrEmpty(QS.Question))
        {
            // �o��Z�b�g�ɖ��p�[�c��t������
            GameObject question = Instantiate(QuestionPrefab, ScrollView_Content.transform);  // ���p�[�c�̍쐬
            question.transform.localScale = Vector3.Scale(QuestionPrefab.transform.localScale, new Vector3(1.0f, 1.0f, 1.0f));  // ���@�ŃT�C�Y������ɕύX����Ă��܂��΍�
            question.GetComponent<Question>().Initialize(QS.Question);  // ����������
            question.transform.Find("BodyText").GetComponent<Text>().alignment = QS.QuestionAlignment;
        }
        // ���ӏ���������ꍇ
        if (!string.IsNullOrEmpty(QS.Caution))
        {
            // �o��Z�b�g�ɒ��ӏ����p�[�c��t������
            GameObject g = Instantiate(CautionPrefab, ScrollView_Content.transform);  // ���ӏ����p�[�c�̍쐬
            g.transform.localScale = Vector3.Scale(CautionPrefab.transform.localScale, new Vector3(1.0f, 1.0f, 1.0f));  // ���@�ŃT�C�Y������ɕύX����Ă��܂��΍�
            g.GetComponent<Text>().text = QS.Caution;
        }

        // �I�����ݒ�
        // �p�s�^�C�v�ʂɏ������������
        if (QS.QT == SolveQuestion_DTO.QT.ONE_QUESTION_ONE_ANSWER)  // ���ꓚ
        {
            GameObject oOAnswer = Instantiate(OO_AnswerPrefab, ScrollView_Content.transform);  // ���ꓚ�����p�[�c�̍쐬
            oOAnswer.transform.localScale = Vector3.Scale(OO_AnswerPrefab.transform.localScale, new Vector3(1.0f, 1.0f, 1.0f));  // ���@�ŃT�C�Y������ɕύX����Ă��܂��΍�
            oOAnswer.GetComponent<OO_Answer>().Initialize(QS.Answers[0]);  // ����������
            GameObject oKNG = Instantiate(OO_OKNGPrefab, ScrollView_Content.transform);  // ���ꓚ�n�j�^�m�f�p�[�c�̍쐬
            oKNG.transform.localScale = Vector3.Scale(OO_OKNGPrefab.transform.localScale, new Vector3(1.0f, 1.0f, 1.0f));  // ���@�ŃT�C�Y������ɕύX����Ă��܂��΍�
            oKNG.GetComponent<OO_OKNG>().Initialize(Sound);  // ����������
            this.oKNG = oKNG.GetComponent<OO_OKNG>();
        }
        else if (QS.QT == SolveQuestion_DTO.QT.MULTIPLE_CHOICE)  // �����I��
        {
            GameObject choice = Instantiate(MultipleChoicesPrefab, ChoiceWindow.transform);  // �I�����p�[�c�̍쐬
            choice.transform.localScale = Vector3.Scale(MultipleChoicesPrefab.transform.localScale, new Vector3(1.0f, 1.0f, 1.0f));  // ���@�ŃT�C�Y������ɕύX����Ă��܂��΍�
            RectTransform rt = choice.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector3(0, 0, 0);
            rt.anchorMin = rt.anchorMax = new Vector2(0.5f, 0.5f);
            multipleChoices = choice.GetComponent<MultipleChoices>();  // �X�N���v�g�擾
            choice.GetComponent<MultipleChoices>().Initialize(QS.Choices);  // ����������
            CurrentQuestionCorrectAnswerQuantity = QS.PlayerChooseQuantity;  // ������ׂ����̎擾
        }
        else if (QS.QT == SolveQuestion_DTO.QT.DESCRIPTION)  // �L�q
        {
            GameObject g = Instantiate(DescriptionPrefab, ChoiceWindow.transform);  // �L�q�p�[�c�̍쐬
            g.transform.localScale = Vector3.Scale(DescriptionPrefab.transform.localScale, new Vector3(1.0f, 1.0f, 1.0f));  // ���@�ŃT�C�Y������ɕύX����Ă��܂��΍�
            description = g.GetComponent<InputField>();  // �X�N���v�g�擾
        }
        else if (QS.QT == SolveQuestion_DTO.QT.CHOOSE_ORDER)  // ���ёւ�
        {
            GameObject g = Instantiate(ChooseOrderChoicesPrefab, ChoiceWindow.transform);  // ���בւ��p�[�c�̍쐬
            g.transform.localScale = Vector3.Scale(ChooseOrderChoicesPrefab.transform.localScale, new Vector3(1.0f, 1.0f, 1.0f));  // ���@�ŃT�C�Y������ɕύX����Ă��܂��΍�
            RectTransform rt = g.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector3(0, 0, 0);
            rt.anchorMin = rt.anchorMax = new Vector2(0.5f, 0.5f);
            chooseOrderChoices = g.transform.GetComponent<ChooseOrderChoices>();  // �X�N���v�g�擾
            chooseOrderChoices.Initialize(QS.Choices);
            CurrentQuestionCorrectAnswerQuantity = QS.PlayerChooseQuantity;  // ������ׂ����̎擾
        }

        // �u�����J�ڃ{�^���v�p�[�c
        if (QS.GoToNextQuestionButtonType != 0)
        {
            // �o��Z�b�g�Ɏ����J�ڃ{�^���p�[�c��t������
            GameObject g = Instantiate(AnswerCheckButtonPrefab, ScrollView_Content.transform);  // ���J�ڃ{�^���p�[�c�̍쐬
            g.transform.localScale = Vector3.Scale(AnswerCheckButtonPrefab.transform.localScale, new Vector3(1.0f, 1.0f, 1.0f));  // ���@�ŃT�C�Y������ɕύX����Ă��܂��΍�
            GoToNextQuestionButton = g.GetComponent<GoToNextQuestionButton>();
            GoToNextQuestionButton.Initialize(QS.GoToNextQuestionButtonType);
            if (QS.GoToNextQuestionButtonType == 2)
                FeedbackFlag = false;
            GoToNextQuestionButtonFlag = true;  // �{�^���������Ȃ��Ǝ���ʂɑJ�ڂ��Ȃ��Ȃ�
        }
        else
            GoToNextQuestionButtonFlag = false;  // �����𖞂����Ύ���ʂɎ����J��

        AnswerCheckButtonFlag = false;
    }


    /// <summary>��߂�{�^���̓���</summary>
    IEnumerator QuitButton()
    {
        // ��߂�TouchDetect
        TouchDetect UI_QuitTD = UI_Quit.GetComponent<TouchDetect>();
        while (true)
        {
            if (UI_QuitTD.DetectTouch())
            {
                ToNextGameScene();
                yield break;
            }
            yield return null;
        }
    }
}

