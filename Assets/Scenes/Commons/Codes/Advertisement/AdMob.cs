using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdMob : MonoBehaviour
{
    // �L�����j�b�gID
#if UNITY_ANDROID
    //�{�ԗpconst string adUnitId = "ca-app-pub-7581824672125490~7919651806";
    const string adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
    //�{�ԗpconst string adUnitId = "ca-app-pub-7581824672125490/6742301504";
    const string adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
    const string adUnitId = "unknownDevice";
#endif

    BannerView bannerView;

    void Start()
    {
        MobileAds.Initialize(initStatus => { });

        RequestBanner();
    }

    void RequestBanner()
    {
        // �O��BannerView�̃C���X�^���X���c���Ă�����j������
        if (bannerView != null)
        {
            bannerView.Destroy();
            bannerView = null;
        }

        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
        // bannerView.OnAdLoaded += HandleRewardBasedVideoLoaded;
        // bannerView.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        bannerView.LoadAd(request);

        // Create a 320x50 banner at the top of the screen.
        //bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);
    }


    /// <summary>�L�����ǂݍ��܂ꂽ����s</summary>
    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        // �����[�h�L���ł͂Ȃ��̂ŉ��L�̕��͂���Ȃ� (�o�i�[�Ȃ玩���I�ɕ\�������)
        // bannerView.Show();
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        // �����[�h�L���ł͂Ȃ��̂ł��Ԃ�v��Ȃ�
        // RequestBanner();
    }

}