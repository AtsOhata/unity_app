using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Question : MonoBehaviour
{
    /// <summary>�P�s�̍ő�o�C�g�� 32�o�C�g(�S�p16����)</summary>
    int MaxBite = 32;

    /// <summary> �ő�s��</summary>
    int MaxLength = 6;

    public void Initialize(string question)
    {
        // ��蕶�𒷂������Ȃ�Ȃ��悤�Ɏw�蕶�����ŋ�؂��Ċi�[���A��蕶�̑����̕����ɂ̓{�^���ōs�����ł���悤�ɂ���
        List<string> QuestionSeparated = StringUtility.CutTextB(question, MaxBite, MaxBite * MaxLength);  // ��蕶���ő�o�C�g���Ő؂蕪���Ď擾
        Text bodyText = transform.Find("BodyText").GetComponent<Text>();  // �{��
        bodyText.text = QuestionSeparated[0];  // ��蕶�̍ŏ��̕������e�L�X�g���f
        GameObject continueButton = transform.Find("Continue").gameObject;  // �����{�^��
        TouchDetect continueButtonTD = continueButton.GetComponent<TouchDetect>();  // �����{�^��TouchDetect
        GameObject returnButton = transform.Find("Return").gameObject;  // �߂�{�^��
        TouchDetect returnButtonTD = returnButton.GetComponent<TouchDetect>();  // �߂�{�^��TouchDetect

        // �����^�߂�{�^���^�b�`�R���[�`���̎n��
        StartCoroutine(QuestionContinueReturnButton());

        /// <summary>�ǉ��̑����^�߂�{�^���^�b�`�R���[�`��</summary>
        IEnumerator QuestionContinueReturnButton()
        {
            // �؂蕪�����e�L�X�g����̎��͑����^�߂�{�^�����v��Ȃ����߃{�^�����\���ɂ��ăR���[�`���X�g�b�v
            if (QuestionSeparated.Count == 1)
            {
                continueButton.SetActive(false);// �����{�^����\��
                returnButton.SetActive(false);// �߂�{�^����\��
                yield break;
            }

            // �{�^���̕\����������Ԃɂ���
            continueButton.SetActive(true);  // �����{�^���\��
            returnButton.SetActive(false);// �߂�{�^����\��

            // �Q�[���I�u�W�F�N�g�ƃR���|�[�l���g�̎擾
            int QuestionCurrentTextIndex = 0;  // ���ݕ\�����̖�蕶�����̔ԍ�
            while (true)
            {
                // �����{�^�����\�����Ń^�b�`���ꂽ�玟�̕����̖�蕶��\������
                if (continueButton.activeSelf && continueButtonTD.DetectTouch())
                {
                    QuestionCurrentTextIndex++;  // ���ݕ\�����̖�蕶�����̔ԍ�
                    bodyText.text = QuestionSeparated[QuestionCurrentTextIndex];  // ��蕶���e�L�X�g���f
                    returnButton.SetActive(true);  // �߂�{�^���\��
                    // ���̍Ō�̕�����\�����Ă���̂ł���Α����{�^�����\���ɂ���
                    if (QuestionCurrentTextIndex == QuestionSeparated.Count - 1)
                    {
                        continueButton.SetActive(false);
                    }
                }

                // �߂�{�^�����\�����Ń^�b�`���ꂽ��O�̕����̖���\������
                if (returnButton.activeSelf && returnButtonTD.DetectTouch())
                {
                    QuestionCurrentTextIndex--;  // ���ݕ\�����̖�蕔���̔ԍ�
                    bodyText.text = QuestionSeparated[QuestionCurrentTextIndex];  // �����e�L�X�g���f
                    continueButton.SetActive(true);  // �����{�^���\��
                    // ���̍ŏ��̕�����\�����Ă���̂ł���Ζ߂�{�^�����\���ɂ���
                    if (QuestionCurrentTextIndex == 0)
                    {
                        returnButton.SetActive(false);
                    }
                }


                yield return null;
            }
        }
    }
}
