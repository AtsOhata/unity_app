using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class OO_Answer : MonoBehaviour
{
    public void Initialize(string answer)
    {
        // �Q�[���I�u�W�F�N�g�ƃR���|�[�l���g�̎擾
        GameObject Answer = transform.Find("Text").gameObject;  // �����e�L�X�g (�{�^�����������܂Ŕ�\��)
        GameObject Button = transform.Find("Button").gameObject;  // �����{�^��
        TouchDetect ButtonTD = Button.GetComponent<TouchDetect>();  // �����{�^��TouchDetect

        // �����e�L�X�g�̃e�L�X�g���f
        Answer.GetComponent<Text>().text = answer;

        // �����e�L�X�g�̔�\��
        Answer.SetActive(false);

        // �����{�^���R���[�`���̎n��
        StartCoroutine(ShowAnswerButton());

        /// <summary>�����{�^���R���[�`��</summary>
        IEnumerator ShowAnswerButton()
        {
            while (true)
            {
                // �����{�^�����^�b�`���ꂽ��
                if (ButtonTD.DetectTouch())
                {
                    // �����e�L�X�g�̕\��
                    Answer.SetActive(true);

                    // �����{�^���̔�\��
                    Button.SetActive(false);
                }
                yield return null;
            }
        }
    }
}
