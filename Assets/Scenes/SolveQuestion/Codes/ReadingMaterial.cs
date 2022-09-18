using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �ǉ�����
/// </summary>
public class ReadingMaterial : MonoBehaviour
{

    /// <summary>�P�s�̍ő�o�C�g��</summary>
    int MaxBite = 32;

    /// <summary>�P�ł�����̍ő�s��</summary>
    int MaxLength = 20;

    public void Initialize(string readingMaterial)
    {

        // �ǉ������𒷂������Ȃ�Ȃ��悤�Ɏw�蕶�����ŋ�؂��Ċi�[���A�ǉ������̑����̕����ɂ̓{�^���ōs�����ł���悤�ɂ���
        List<string> ReadingMaterialSeparated = StringUtility.CutTextB(readingMaterial, MaxBite, MaxBite * MaxLength);  // �ǉ��������P�ł�����̍ő�o�C�g���Ő؂蕪���Ď擾
        Text bodyText = transform.Find("BodyText").GetComponent<Text>();  // �{��
        GameObject continueButton = transform.Find("Continue").gameObject;  // �����{�^��
        TouchDetect continueButtonTD = continueButton.GetComponent<TouchDetect>();  // �����{�^��TouchDetect
        GameObject returnButton = transform.Find("Return").gameObject;  // �߂�{�^��
        TouchDetect returnButtonTD = returnButton.GetComponent<TouchDetect>();  // �߂�{�^��TouchDetect
        bodyText.text = ReadingMaterialSeparated[0];  // �ǉ������̍ŏ��̕������e�L�X�g���f

        // �����^�߂�{�^���^�b�`�R���[�`���̎n��
        StartCoroutine(ReadingMaterialContinueReturnButton());

        /// <summary>�ǉ��̑����^�߂�{�^���^�b�`�R���[�`��</summary>
        IEnumerator ReadingMaterialContinueReturnButton()
        {
            // �؂蕪�����e�L�X�g����̎��͑����^�߂�{�^�����v��Ȃ����߃R���[�`���X�g�b�v
            if (ReadingMaterialSeparated.Count == 1)
            {
                continueButton.SetActive(false);// �����{�^����\��
                returnButton.SetActive(false);// �߂�{�^����\��
                yield break;
            }

            // �{�^���̕\����������Ԃɂ���
            continueButton.SetActive(true);  // �����{�^���\��
            returnButton.SetActive(false);// �߂�{�^����\��

            // �Q�[���I�u�W�F�N�g�ƃR���|�[�l���g�̎擾
            int ReadingMaterialCurrentTextIndex = 0;  // ���ݕ\�����̓ǉ����������̔ԍ�
            while (true)
            {
                // �����{�^�����\�����Ń^�b�`���ꂽ�玟�̕����̓ǉ�������\������
                if (continueButton.activeSelf && continueButtonTD.DetectTouch())
                {
                    ReadingMaterialCurrentTextIndex++;  // ���ݕ\�����̓ǉ����������̔ԍ�
                    bodyText.text = ReadingMaterialSeparated[ReadingMaterialCurrentTextIndex];  // �ǉ��������e�L�X�g���f
                    returnButton.SetActive(true);  // �߂�{�^���\��
                    // �ǉ������̍Ō�̕�����\�����Ă���̂ł���Α����{�^�����\���ɂ���
                    if (ReadingMaterialCurrentTextIndex == ReadingMaterialSeparated.Count - 1)
                    {
                        continueButton.SetActive(false);
                    }
                }

                // �߂�{�^�����\�����Ń^�b�`���ꂽ��O�̕����̓ǉ�������\������
                if (returnButton.activeSelf && returnButtonTD.DetectTouch())
                {
                    ReadingMaterialCurrentTextIndex--;  // ���ݕ\�����̓ǉ����������̔ԍ�
                    bodyText.text = ReadingMaterialSeparated[ReadingMaterialCurrentTextIndex];  // �ǉ��������e�L�X�g���f
                    continueButton.SetActive(true);  // �����{�^���\��
                    // �ǉ������̍ŏ��̕�����\�����Ă���̂ł���Ζ߂�{�^�����\���ɂ���
                    if (ReadingMaterialCurrentTextIndex == 0)
                    {
                        returnButton.SetActive(false);
                    }
                }

                yield return null;
            }
        }
    }
}
