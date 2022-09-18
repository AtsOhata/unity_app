using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGame_Manager : MonoBehaviour
{
    public Slider slider;  // �X���C�_�[
    public GameObject Background;  // �w�i

    float Speed = 2f;  // �X���C�_�[�̑��x
    float ProgressPercent;  // �i�s��

    void Start()
    {
        // �A�v�����\�[�X�t�H���_�̃p�X�擾
        FileUtility.Initialization();

        Background.GetComponent<Image>().sprite = FileUtility.ReadSprite("/Pics/LoadGame_Background");
        slider.transform.Find("Handle Slide Area/Handle").GetComponent<Image>().sprite = FileUtility.ReadSprite("/Pics/LoadGame_Handle");
        StartCoroutine("LoadQB");
    }

    void Update()
    {
        // �X���C�_�[�X�V
        slider.value = Mathf.Lerp(slider.value, ProgressPercent, Time.deltaTime * Speed);
        // �ǂݍ��ݗ�100%�ɂȂ�����Scene_Home�ɑJ�ڂ���
        if (slider.value >= 1)
        {
            SceneManager.LoadScene("Home_Scene");
        }
    }

    IEnumerator LoadQB()
    {

        // �A�v���ݒ�̓ǂݍ���
        List<string> list = FileUtility.ReadText("AppConstants");
        list.ForEach(x =>
        {
            // AppConstants�̓��e��=�ŋ�؂��Ď����Ɋi�[����
            List<string> list2 = new List<string>(x.Split('='));
            if (list2.Count > 1)
                GlobalVariables.AppConstants.Add(list2[0], list2[1]);
        });
        // HomeDTO.xml�̓ǂݍ���
        GlobalVariables.Home_DTO = Home_DTO.Load();
        ProgressPercent = 0.4f;
        yield return new WaitForSeconds(0.5f);  // 0.5�b��ɏ����ĊJ

        // Savedata.xml�̓ǂݍ���
        Savedata.Instance = Savedata.Load();
        // ���[�U�[�ݒ�̓ǂݍ���
        UserOption.Instance = UserOption.Load();

        ProgressPercent = 0.5f;
        yield return new WaitForSeconds(0.5f);  // 0.5�b��ɏ����ĊJ

        ProgressPercent = 0.9f;
        yield return new WaitForSeconds(0.5f);  // 0.5�b��ɏ����ĊJ

        ProgressPercent = 1.01f;
    }
}
