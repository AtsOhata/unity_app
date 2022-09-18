using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdMob : MonoBehaviour
{
    // 広告ユニットID
#if UNITY_ANDROID
    //本番用const string adUnitId = "ca-app-pub-7581824672125490~7919651806";
    const string adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
    //本番用const string adUnitId = "ca-app-pub-7581824672125490/6742301504";
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
        // 前のBannerViewのインスタンスが残っていたら破棄する
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


    /// <summary>広告が読み込まれたら実行</summary>
    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        // リワード広告ではないので下記の文はいらない (バナーなら自動的に表示される)
        // bannerView.Show();
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        // リワード広告ではないのでたぶん要らない
        // RequestBanner();
    }

}