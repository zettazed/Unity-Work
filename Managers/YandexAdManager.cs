using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YandexMobileAds;
using YandexMobileAds.Base;

public class YandexAdManager : MonoBehaviour
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
        //указываем id места, размер баннера и его позицию.
    }

    public void ShowBanner()
    {
        AdRequest request = new AdRequest.Builder().Build(); //создаЄм запрос на вызов баннера
        bannerAd.LoadAd(request); //отправл€ем запрос на вызов баннера
        RequestBanner();
    }
    #endregion


    #region RewardedAd
    private void RequestRewardedAd()
    {
        rewardedAd = new RewardedAd(rewardedAdId); //заполн€ем переменную с рекламой
        AdRequest request = new AdRequest.Builder().Build(); //создаЄм запрос на рекламу
        rewardedAd.LoadAd(request); // отсылаем запрос на рекламу
        //начинаем отслеживать событи€ рекламы
        rewardedAd.OnRewardedAdLoaded += this.HandleRewardedAdLoaded;
        rewardedAd.OnRewardedAdFailedToLoad += this.HandleRewardedAdFailedToLoad;
        rewardedAd.OnReturnedToApplication += this.HandleReturnedToApplication;
        rewardedAd.OnLeftApplication += this.HandleLeftApplication;
        rewardedAd.OnRewardedAdShown += this.HandleRewardedAdShown;
        rewardedAd.OnRewardedAdDismissed += this.HandleRewardedAdDismissed;
        rewardedAd.OnImpression += this.HandleImpression;
        rewardedAd.OnRewarded += this.HandleRewarded;
    }
    //метод дл€ вызова рекламы
    public void ShowRewardedAd()
    {
        //если реклама загружена Ч показываем еЄ
        if (this.rewardedAd.IsLoaded())
        {
            rewardedAd.Show();
        }
        RequestRewardedAd();
    }
    //каждый метод будет вызван при определенном действии св€занной с рекламой (еЄ загрузкой и тд)
    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        //ShowRewardedAd(); //реклама загружена Ч показываем еЄ
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
        interstitialAd = new Interstitial(interstitialAdId); //заполн€ем переменную с рекламой
        AdRequest request = new AdRequest.Builder().Build(); //создаЄм запрос на показ рекламы
        interstitialAd.LoadAd(request); //отправл€ем запрос
                                      //каждый метод будет вызван при определенном действии св€занной с рекламой (еЄ загрузкой и тд)
        interstitialAd.OnInterstitialLoaded += this.HandleInterstitialLoaded;
        interstitialAd.OnInterstitialFailedToLoad += this.HandleInterstitialFailedToLoad;
        interstitialAd.OnReturnedToApplication += this.HandleReturnedToApplication;
        interstitialAd.OnLeftApplication += this.HandleLeftApplication;
        interstitialAd.OnInterstitialShown += this.HandleInterstitialShown;
        interstitialAd.OnInterstitialDismissed += this.HandleInterstitialDismissed;
        interstitialAd.OnImpression += this.HandleImpression;
    }
    //метод дл€ показа рекламы
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
        //ShowInterstitial(); //при загрузки рекламы Ч показываем еЄ
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