using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;
 
 
public class UnityAd : MonoBehaviour
{
    public string gameId; //"GameID"を入力
    public string bannerId;
    bool testMode = false;

    void Start()
    {
        Advertisement.Initialize(gameId, testMode);
        StartCoroutine(showBanner());

    }
    IEnumerator showBanner()
    {
        while (!Advertisement.IsReady())
        {
            yield return new WaitForSeconds(0.3f); // 0.3秒後に広告表示
        }
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER); //バナーをセット
        Advertisement.Banner.Show(bannerId);
    }
}