using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Scene_LoadQuestion��Manager�N���X
/// </summary>
public class LoadQuestion_Manager : MonoBehaviour
{
    void Start()
    {
        // ************************************************************************
        // �S�̗̂���
        // ���\�[�X������擾������I�ԁ��I�΂ꂽ�������̃V�[����DTO�ɐݒ肷��
        // ************************************************************************
        QuestionSet QS = QuestionSet.Load("QL/" + GlobalVariables.LoadQuestion_DTO.ChosenQLPath);  // ���Z�b�g�̎擾
        if(QS.RandomFlag)
        {
            UnityEngine.Random.InitState(DateTime.Now.Millisecond);  // ���݂̓������V�[�h�l�ɐݒ�
            QS.QuestionPacks = Utility.Randomize(QS.QuestionPacks);  // ��胊�X�g�̕s�K����
        }
        if (QS.UpperLimitFlag)
        {
            // �o�萔���̖���I��
            if (QS.QuestionPacks.Count > GlobalVariables.LoadQuestion_DTO.QuestionQuantity)  // ��胊�X�g�̑���萔�����߂�ꂽ�o�萔��葽�����̂ݑI��̗v����
            {
                QS.QuestionPacks = ChooseQuestionPacks(GlobalVariables.LoadQuestion_DTO.QuestionQuantity);

                List<QuestionSet.QuestionPack> ChooseQuestionPacks(int Quantity)
                {
                    List<QuestionSet.QuestionPack> qPs = new();
                    int count = 0;
                    List<int> chosenIndices = new();
                    while (qPs.Count < Quantity)
                    {
                        // �o��m��
                        float probability = 1;
                        // �����_���ɝ���
                        // �@ ��胊�X�g���疳��ׂɑI��ł��Ȃ�����I�сA���̖��̒ʂ������擾����
                        int x;
                        do
                        {
                            x = UnityEngine.Random.Range(0, QS.QuestionPacks.Count);
                        } while (chosenIndices.Contains(x));
                        string chosen = QS.QuestionPacks[x].Alias;
                        // �A ���[�v��1000��ȓ��ŁA��胊�X�g�̒ʂ����ƈ�v����Savedata������΁A����Savedata�̒蒅���{�P�̋t�����o��m��
                        Savedata.LearnedWord w = Savedata.Instance.LearnedWords.Find(x => x.Alias == chosen);
                        if (count < 1000 && w != null)
                            probability = 1 / (w.FixedPercentage + 1f);
                        // �B �������񂵂ďo��m�������Ⴂ���l�ł���Ώo�胊�X�g�ɉ�����
                        if (UnityEngine.Random.value < probability)
                        {
                            qPs.Add(QS.QuestionPacks[x]);
                            chosenIndices.Add(x);
                        }
                        else
                            count++;
                    }
                    return qPs;
                }
            }
        }

        // ���V�[����DTO�g�ݗ���
        List<SolveQuestion_DTO.QS> QSs = new List<SolveQuestion_DTO.QS>();
        QS.QuestionPacks.ForEach(x =>
        {
            SolveQuestion_DTO.QS SQ = new();

            // �ʂ���
            SQ.Alias = x.Alias;
            // �摜
            if (!string.IsNullOrEmpty(x.Image))
                SQ.Image = x.Image.Trim();
            // �ǉ����� (���b�`�e�L�X�g��)
            if (!string.IsNullOrEmpty(x.ReadingMaterial))
                SQ.ReadingMaterial = x.ReadingMaterial.Trim().MakeRichText();

            // ���̓ǂݍ���
            x.QuestionBases.ForEach(y =>
            {
                SQ.QuestionAlignment = y.QuestionAlignment;

                // ���ӏ���
                if (!string.IsNullOrEmpty(y.Caution))
                    SQ.Caution = y.Caution;

                if (y is OneQuestionOneAnswer)  // ���ꓚ
                {
                    OneQuestionOneAnswer o = (OneQuestionOneAnswer)y;
                    SQ.QT = SolveQuestion_DTO.QT.ONE_QUESTION_ONE_ANSWER;  // ���^�C�v
                    SQ.Question = o.Question.MakeRichText();  // ���i���b�`�e�L�X�g������ �j
                    SQ.Answers.Add(o.Answer);  // ����
                }
                else if (y is MultipleChoice)  // �����I����
                {
                    MultipleChoice o = (MultipleChoice)y;
                    o.Initialize();  // ������
                    SQ.QT = SolveQuestion_DTO.QT.MULTIPLE_CHOICE;  // ���^�C�v
                    if(o.Question != null)
                        SQ.Question = o.Question.MakeRichText();  // ���i���b�`�e�L�X�g������ �j
                    SQ.Answers = o.CorrectChoicePool;  // ����
                    SQ.Choices = o.Choices;  // �I����
                    SQ.PlayerChooseQuantity = o.PlayerChooseQuantity;  // �v���C���[�I���
                }
                else if (y is Description)  // �L�q���
                {
                    Description o = (Description)y;
                    o.Initialize();  // ������
                    SQ.QT = SolveQuestion_DTO.QT.DESCRIPTION;  // ���^�C�v
                    SQ.Question = o.Question.MakeRichText();  // ���i���b�`�e�L�X�g������ �j
                    SQ.Answers = o.CorrectChoicePool;  // ����
                }
                else if (y is ChooseOrder)  // ���ёւ����
                {
                    ChooseOrder o = (ChooseOrder)y;
                    o.Initialize();  // ������
                    SQ.QT = SolveQuestion_DTO.QT.CHOOSE_ORDER;  // ���^�C�v
                    SQ.Question = o.Question.MakeRichText();  // ���i���b�`�e�L�X�g������ �j
                    SQ.Answers = o.CorrectOrder.Split(',').ToList();  // ����
                    SQ.Choices = o.Choices;  // �I����
                    SQ.PlayerChooseQuantity = o.PlayerChooseQuantity;  // �v���C���[�I���
                }
            });

            // ================
            // �����J�ڃ{�^��
            // ================
            if (x.GoToNextQuestionButtonType == 0)
            {
                // �����𖞂������ۂɁu�������킹�v�{�^��

                // ��肪2��ȏ゠�� || �L�q��肪����@���v�邱�Ƃɂ��Ă���
                if (x.QuestionBases.Count > 1
                    || x.QuestionBases.Where(y => y is Description).ToList().Count > 0)
                    SQ.GoToNextQuestionButtonType = 1;
                else
                    SQ.GoToNextQuestionButtonType = 0;
            }
            else if(x.GoToNextQuestionButtonType == 2)
            {
                // �u���ցv�{�^��
                SQ.GoToNextQuestionButtonType = 2;
            }

            QSs.Add(SQ);
        });
        GlobalVariables.SolveQuestion_DTO.QLTitle = GlobalVariables.LoadQuestion_DTO.QLTitle;
        GlobalVariables.SolveQuestion_DTO.QSs = QSs;
        
        // ���̃Q�[���V�[���Ɉڂ�
        SceneManager.LoadScene("SolveQuestion_Scene");
    }

}
