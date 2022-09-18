using UnityEngine;

public class SolveQuestion_Sound : MonoBehaviour
{

    static public SolveQuestion_Sound instance;

    public enum SOUND
    {
        /// <summary>‚n‚j‰¹</summary>
        OK,
        /// <summary>‚m‚f‰¹</summary>
        NG,
        /// <summary>ŠJŽnƒ{ƒ^ƒ“‰¹</summary>
        START_BUTTON
    }

    AudioClip OK;  // ‚n‚j‰¹
    AudioClip NG;  // ‚m‚f‰¹
    AudioClip StartButton;  // ŠJŽnƒ{ƒ^ƒ“‰¹

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
        if (sound == SOUND.OK) audioSource.PlayOneShot(OK, UserOption.Instance.SoundValue);  // ‚n‚j‰¹
        if (sound == SOUND.NG) audioSource.PlayOneShot(NG, UserOption.Instance.SoundValue);  // ‚m‚f‰¹
        if (sound == SOUND.START_BUTTON) audioSource.PlayOneShot(StartButton, UserOption.Instance.SoundValue);  // ŠJŽnƒ{ƒ^ƒ“‰¹
    }
}
