using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;
 
 
public class UnityAd : MonoBehaviour
{
    public string gameId; //"GameID"�����
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
            yield return new WaitForSeconds(0.3f); // 0.3�b��ɍL���\��
        }
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER); //�o�i�[���Z�b�g
        Advertisement.Banner.Show(bannerId);
    }
}