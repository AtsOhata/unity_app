using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGame_Manager : MonoBehaviour
{
    public Slider slider;  // スライダー
    public GameObject Background;  // 背景

    float Speed = 2f;  // スライダーの速度
    float ProgressPercent;  // 進行率

    void Start()
    {
        // アプリリソースフォルダのパス取得
        FileUtility.Initialization();

        Background.GetComponent<Image>().sprite = FileUtility.ReadSprite("/Pics/LoadGame_Background");
        slider.transform.Find("Handle Slide Area/Handle").GetComponent<Image>().sprite = FileUtility.ReadSprite("/Pics/LoadGame_Handle");
        StartCoroutine("LoadQB");
    }

    void Update()
    {
        // スライダー更新
        slider.value = Mathf.Lerp(slider.value, ProgressPercent, Time.deltaTime * Speed);
        // 読み込み率100%になったらScene_Homeに遷移する
        if (slider.value >= 1)
        {
            SceneManager.LoadScene("Home_Scene");
        }
    }

    IEnumerator LoadQB()
    {

        // アプリ設定の読み込み
        List<string> list = FileUtility.ReadText("AppConstants");
        list.ForEach(x =>
        {
            // AppConstantsの内容を=で区切って辞書に格納する
            List<string> list2 = new List<string>(x.Split('='));
            if (list2.Count > 1)
                GlobalVariables.AppConstants.Add(list2[0], list2[1]);
        });
        // HomeDTO.xmlの読み込み
        GlobalVariables.Home_DTO = Home_DTO.Load();
        ProgressPercent = 0.4f;
        yield return new WaitForSeconds(0.5f);  // 0.5秒後に処理再開

        // Savedata.xmlの読み込み
        Savedata.Instance = Savedata.Load();
        // ユーザー設定の読み込み
        UserOption.Instance = UserOption.Load();

        ProgressPercent = 0.5f;
        yield return new WaitForSeconds(0.5f);  // 0.5秒後に処理再開

        ProgressPercent = 0.9f;
        yield return new WaitForSeconds(0.5f);  // 0.5秒後に処理再開

        ProgressPercent = 1.01f;
    }
}
