using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YandexMobileAds;
using YandexMobileAds.Base;

public class YandexAdsManager : MonoBehaviour
{

    public static YandexAdManager Instance;

    #region Ad Blocks
    private RewardedAd rewardedAd;
    private Interstitial interstitialAd;
    private YandexMobileAds.Banner bannerAd;
    #endregion

    #region
    [SerializeField] private string rewardedAdId;
    [SerializeField] private string interstitialAdId;
    [SerializeField] private string bannerAdId;
    #endregion

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    private void Start()
    {
        RequestRewardedAd();
        RequestInterstitial();
        RequestBanner();
    }

    #region BannerAd
    private void RequestBanner()
    {
        bannerAd = new YandexMobileAds.Banner(bannerAdId, AdSize.BANNER_320x50, AdPosition.BottomCenter);
        //óêàçûâàåì id ìåñòà, ðàçìåð áàííåðà è åãî ïîçèöèþ.
    }

    public void ShowBanner()
    {
        AdRequest request = new AdRequest.Builder().Build(); //ñîçäà¸ì çàïðîñ íà âûçîâ áàííåðà
        bannerAd.LoadAd(request); //îòïðàâëÿåì çàïðîñ íà âûçîâ áàííåðà
        RequestBanner();
    }
    #endregion


    #region RewardedAd
    private void RequestRewardedAd()
    {
        rewardedAd = new RewardedAd(rewardedAdId); //çàïîëíÿåì ïåðåìåííóþ ñ ðåêëàìîé
        AdRequest request = new AdRequest.Builder().Build(); //ñîçäà¸ì çàïðîñ íà ðåêëàìó
        rewardedAd.LoadAd(request); // îòñûëàåì çàïðîñ íà ðåêëàìó
        //íà÷èíàåì îòñëåæèâàòü ñîáûòèÿ ðåêëàìû
        rewardedAd.OnRewardedAdLoaded += this.HandleRewardedAdLoaded;
        rewardedAd.OnRewardedAdFailedToLoad += this.HandleRewardedAdFailedToLoad;
        rewardedAd.OnReturnedToApplication += this.HandleReturnedToApplication;
        rewardedAd.OnLeftApplication += this.HandleLeftApplication;
        rewardedAd.OnRewardedAdShown += this.HandleRewardedAdShown;
        rewardedAd.OnRewardedAdDismissed += this.HandleRewardedAdDismissed;
        rewardedAd.OnImpression += this.HandleImpression;
        rewardedAd.OnRewarded += this.HandleRewarded;
    }
    //ìåòîä äëÿ âûçîâà ðåêëàìû
    public void ShowRewardedAd()
    {
        //åñëè ðåêëàìà çàãðóæåíà — ïîêàçûâàåì å¸
        if (this.rewardedAd.IsLoaded())
        {
            rewardedAd.Show();
        }
        RequestRewardedAd();
    }
    //êàæäûé ìåòîä áóäåò âûçâàí ïðè îïðåäåëåííîì äåéñòâèè ñâÿçàííîé ñ ðåêëàìîé (å¸ çàãðóçêîé è òä)
    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        //ShowRewardedAd(); //ðåêëàìà çàãðóæåíà — ïîêàçûâàåì å¸
    }
    public void HandleRewardedAdFailedToLoad(object sender, AdFailureEventArgs args)
    {
        Debug.Log("HandleRewardedAdFailedToLoad event received with message: " + args.Message);
    }
    public void HandleRewardedAdShown(object sender, EventArgs args)
    {
        Debug.Log("HandleRewardedAdShown event received");
    }
    public void HandleRewardedAdDismissed(object sender, EventArgs args)
    {
        Debug.Log("HandleRewardedAdDismissed event received");
    }
    public void HandleRewarded(object sender, Reward args)
    {
        Debug.Log("HandleRewarded event received: amout = " +args.amount + ", type = " +args.type);
    }
    #endregion



    #region InterstitialAd
    private void RequestInterstitial()
    {
        interstitialAd = new Interstitial(interstitialAdId); //çàïîëíÿåì ïåðåìåííóþ ñ ðåêëàìîé
        AdRequest request = new AdRequest.Builder().Build(); //ñîçäà¸ì çàïðîñ íà ïîêàç ðåêëàìû
        interstitialAd.LoadAd(request); //îòïðàâëÿåì çàïðîñ
                                      //êàæäûé ìåòîä áóäåò âûçâàí ïðè îïðåäåëåííîì äåéñòâèè ñâÿçàííîé ñ ðåêëàìîé (å¸ çàãðóçêîé è òä)
        interstitialAd.OnInterstitialLoaded += this.HandleInterstitialLoaded;
        interstitialAd.OnInterstitialFailedToLoad += this.HandleInterstitialFailedToLoad;
        interstitialAd.OnReturnedToApplication += this.HandleReturnedToApplication;
        interstitialAd.OnLeftApplication += this.HandleLeftApplication;
        interstitialAd.OnInterstitialShown += this.HandleInterstitialShown;
        interstitialAd.OnInterstitialDismissed += this.HandleInterstitialDismissed;
        interstitialAd.OnImpression += this.HandleImpression;
    }
    //ìåòîä äëÿ ïîêàçà ðåêëàìû
    public void ShowInterstitial()
    {
        if (this.interstitialAd.IsLoaded())
        {
            interstitialAd.Show();
        }
        RequestInterstitial();
    }
    public void HandleInterstitialLoaded(object sender, EventArgs args)
    {
        //ShowInterstitial(); //ïðè çàãðóçêè ðåêëàìû — ïîêàçûâàåì å¸
    }
    public void HandleInterstitialFailedToLoad(object sender, AdFailureEventArgs args)
    {
        Debug.Log("HandleInterstitialFailedToLoad event received with message: " + args.Message);
    }
    public void HandleInterstitialShown(object sender, EventArgs args)
    {
        Debug.Log("HandleInterstitialShown event received");
    }
    public void HandleInterstitialDismissed(object sender, EventArgs args)
    {
        Debug.Log("HandleInterstitialDismissed event received");
    }
    #endregion



    #region Others Callbacks
    public void HandleReturnedToApplication(object sender, EventArgs args)
    {
        Debug.Log("HandleReturnedToApplication event received");
    }
    public void HandleLeftApplication(object sender, EventArgs args)
    {
        Debug.Log("HandleLeftApplication event received");
    }
    public void HandleImpression(object sender, ImpressionData impressionData)
    {
        Debug.Log("HandleImpression event received with data: " + impressionData);
    }
    #endregion
}
