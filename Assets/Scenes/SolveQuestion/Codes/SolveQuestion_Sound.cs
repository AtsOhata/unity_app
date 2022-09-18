using UnityEngine;

public class SolveQuestion_Sound : MonoBehaviour
{

    static public SolveQuestion_Sound instance;

    public enum SOUND
    {
        /// <summary>�n�j��</summary>
        OK,
        /// <summary>�m�f��</summary>
        NG,
        /// <summary>�J�n�{�^����</summary>
        START_BUTTON
    }

    AudioClip OK;  // �n�j��
    AudioClip NG;  // �m�f��
    AudioClip StartButton;  // �J�n�{�^����

    void Awake()
    {
        if (instance == null)
        {

            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        OK = FileUtility.LoadAudioClip("OK");
        NG = FileUtility.LoadAudioClip("NG");
        StartButton = FileUtility.LoadAudioClip("StartButton");
    }

    public void PlaySound(SOUND sound)
    {
        if (sound == SOUND.OK) audioSource.PlayOneShot(OK, UserOption.Instance.SoundValue);  // �n�j��
        if (sound == SOUND.NG) audioSource.PlayOneShot(NG, UserOption.Instance.SoundValue);  // �m�f��
        if (sound == SOUND.START_BUTTON) audioSource.PlayOneShot(StartButton, UserOption.Instance.SoundValue);  // �J�n�{�^����
    }
}
