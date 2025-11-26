using System;
using UnityEngine;
using YandexMobileAds;
using YandexMobileAds.Base;

public class AdsManagerYandex : MonoBehaviour
{
    public static AdsManagerYandex Instance;

    [Header("Ad IDs")]
    [SerializeField] private string _appOpenAdId;
    [SerializeField] private string _bannerAdId;
    [SerializeField] private string _interstitialAdId;
    [SerializeField] private string _rewardedAdId;

    [Header("App Open Ad")]
    private AppOpenAdLoader appOpenAdLoader;
    private AppOpenAd appOpenAd;

    [Header("Banner Ad")]
    private Banner banner;

    [Header("Interstitial Ad")]
    private InterstitialAdLoader interstitialAdLoader;
    private Interstitial interstitial;

    [Header("Rewarded Ad")]
    private RewardedAdLoader rewardedAdLoader;
    private RewardedAd rewardedAd;

    public void Awake()
    {
        DontDestroyOnLoad(this);

        this.appOpenAdLoader = new AppOpenAdLoader();
        this.appOpenAdLoader.OnAdLoaded += this.HandleAdLoaded;
        this.appOpenAdLoader.OnAdFailedToLoad += this.HandleAdFailedToLoad;

        // Use the AppStateObserver to listen to application open/close events.
        AppStateObserver.OnAppStateChanged += HandleAppStateChanged;

        this.interstitialAdLoader = new InterstitialAdLoader();
        this.interstitialAdLoader.OnAdLoaded += this.HandleInterstitialAdLoaded;
        this.interstitialAdLoader.OnAdFailedToLoad += this.HandleInterstitialAdFailedToLoad;

        this.rewardedAdLoader = new RewardedAdLoader();
        this.rewardedAdLoader.OnAdLoaded += this.HandleRewardAdLoaded;
        this.rewardedAdLoader.OnAdFailedToLoad += this.HandleRewardAdFailedToLoad;
    }

    private void Start()
    {
        RequestBanner();
        RequestInterstitial();
        RequestRewardedAd();
    }

    public void OnDestroy()
    {
        // Unsubscribe from the event to avoid memory leaks.
        AppStateObserver.OnAppStateChanged -= HandleAppStateChanged;
    }

    #region App Open Ad
    private void RequestAppOpenAd()
    {
        //Sets COPPA restriction for user age under 13
        MobileAds.SetAgeRestrictedUser(true);

        // Replace demo Unit ID 'demo-appOpenAd-yandex' with actual Ad Unit ID
        string adUnitId = _appOpenAdId;

        if (this.appOpenAd != null)
        {
            this.appOpenAd.Destroy();
        }

        this.appOpenAdLoader.LoadAd(this.CreateAdRequestConfiguration(adUnitId));
    }

    private void ShowAppOpenAd()
    {
        if (this.appOpenAd == null) return;

        this.appOpenAd.OnAdClicked += this.HandleAppOpenAdClicked;
        this.appOpenAd.OnAdShown += this.HandleAdShown;
        this.appOpenAd.OnAdFailedToShow += this.HandleAdFailedToShow;
        this.appOpenAd.OnAdImpression += this.HandleAppOpenImpression;
        this.appOpenAd.OnAdDismissed += this.HandleAdDismissed;

        this.appOpenAd.Show();
    }
    #endregion

    #region Banner Ad
    public void RequestBanner()
    {

        //Sets COPPA restriction for user age under 13
        MobileAds.SetAgeRestrictedUser(true);

        // Replace demo Unit ID 'demo-banner-yandex' with actual Ad Unit ID
        string adUnitId = _bannerAdId;

        if (this.banner != null)
        {
            this.banner.Destroy();
        }
        // Set sticky banner width
        BannerAdSize bannerSize = BannerAdSize.StickySize(GetScreenWidthDp());
        // Or set inline banner maximum width and height
        // BannerAdSize bannerSize = BannerAdSize.InlineSize(GetScreenWidthDp(), 300);
        this.banner = new Banner(adUnitId, bannerSize, AdPosition.BottomCenter);

        this.banner.OnAdLoaded += this.HandleAdLoaded;
        this.banner.OnAdFailedToLoad += this.HandleAdFailedToLoad;
        this.banner.OnReturnedToApplication += this.HandleReturnedToApplication;
        this.banner.OnLeftApplication += this.HandleLeftApplication;
        this.banner.OnAdClicked += this.HandleBannerAdClicked;
        this.banner.OnImpression += this.HandleBannerImpression;

        this.banner.LoadAd(this.CreateAdRequest());
    }

    // Example how to get screen width for request
    private int GetScreenWidthDp()
    {
        int screenWidth = (int)Screen.safeArea.width;
        return ScreenUtils.ConvertPixelsToDp(screenWidth);
    }

    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder().Build();
    }
    #endregion

    #region Interstitial Ad
    private void RequestInterstitial()
    {
        //Sets COPPA restriction for user age under 13
        MobileAds.SetAgeRestrictedUser(true);

        // Replace demo Unit ID 'demo-interstitial-yandex' with actual Ad Unit ID
        string adUnitId = _interstitialAdId;

        if (this.interstitial != null)
        {
            this.interstitial.Destroy();
        }

        this.interstitialAdLoader.LoadAd(this.CreateAdRequest(adUnitId));
    }

    public void ShowInterstitial()
    {
        if (this.interstitial == null)
            return;

        this.interstitial.OnAdClicked += this.HandleInterstitialAdClicked;
        this.interstitial.OnAdShown += this.HandleInterstitialAdShown;
        this.interstitial.OnAdFailedToShow += this.HandleInterstitialAdFailedToShow;
        this.interstitial.OnAdImpression += this.HandleInterstitialImpression;
        this.interstitial.OnAdDismissed += this.HandleInterstitialAdDismissed;

        this.interstitial.Show();
    }
    #endregion

    #region Rewarded Ad
    private void RequestRewardedAd()
    {
        //Sets COPPA restriction for user age under 13
        MobileAds.SetAgeRestrictedUser(true);

        if (this.rewardedAd != null)
        {
            this.rewardedAd.Destroy();
        }

        // Replace demo Unit ID 'demo-rewarded-yandex' with actual Ad Unit ID
        string adUnitId = _rewardedAdId;

        this.rewardedAdLoader.LoadAd(this.CreateAdRequest(adUnitId));
    }

    public void ShowRewardedAd()
    {
        if (this.rewardedAd == null)
        {
            return;
        }

        this.rewardedAd.OnAdClicked += this.HandleRewardAdClicked;
        this.rewardedAd.OnAdShown += this.HandleRewardAdShown;
        this.rewardedAd.OnAdFailedToShow += this.HandleRewardAdFailedToShow;
        this.rewardedAd.OnAdImpression += this.HandleRewardImpression;
        this.rewardedAd.OnAdDismissed += this.HandleRewardAdDismissed;
        this.rewardedAd.OnRewarded += this.HandleRewardRewarded;

        this.rewardedAd.Show();
    }
    #endregion

    private AdRequestConfiguration CreateAdRequestConfiguration(string adUnitId)
    {
        return new AdRequestConfiguration.Builder(adUnitId).Build();
    }

    private AdRequestConfiguration CreateAdRequest(string adUnitId)
    {
        return new AdRequestConfiguration.Builder(adUnitId).Build();
    }

    #region AppOpenAd callback handlers

    public void HandleAppStateChanged(object sender, AppStateChangedEventArgs args)
    {
        if (this.appOpenAd != null && args.IsInBackground == false)
        {
            ShowAppOpenAd();
        }
    }

    public void HandleAdLoaded(object sender, AppOpenAdLoadedEventArgs args)
    {
        this.appOpenAd = args.AppOpenAd;
    }

    public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        
    }

    public void HandleAppOpenAdClicked(object sender, EventArgs args)
    {
        
    }

    public void HandleAdShown(object sender, EventArgs args)
    {
        
    }

    public void HandleAdDismissed(object sender, EventArgs args)
    {
        this.appOpenAd.Destroy();
        this.appOpenAd = null;
    }

    public void HandleAppOpenImpression(object sender, ImpressionData impressionData)
    {
        var data = impressionData == null ? "null" : impressionData.rawData;
    }

    public void HandleAdFailedToShow(object sender, AdFailureEventArgs args)
    {
        
    }

    #endregion

    #region Banner callback handlers

    public void HandleAdLoaded(object sender, EventArgs args)
    {
        this.banner.Show();
    }

    public void HandleAdFailedToLoad(object sender, AdFailureEventArgs args)
    {
        
    }

    public void HandleLeftApplication(object sender, EventArgs args)
    {
        
    }

    public void HandleReturnedToApplication(object sender, EventArgs args)
    {
        
    }

    public void HandleAdLeftApplication(object sender, EventArgs args)
    {
        
    }

    public void HandleBannerAdClicked(object sender, EventArgs args)
    {
        
    }

    public void HandleBannerImpression(object sender, ImpressionData impressionData)
    {
        var data = impressionData == null ? "null" : impressionData.rawData;
    }

    #endregion

    #region Interstitial callback handlers

public void HandleInterstitialAdLoaded(object sender, InterstitialAdLoadedEventArgs args)
    {
        this.interstitial = args.Interstitial;
    }

    public void HandleInterstitialAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        
    }
    public void HandleInterstitialAdClicked(object sender, EventArgs args)
    {
        
    }

    public void HandleInterstitialAdShown(object sender, EventArgs args)
    {
        
    }

    public void HandleInterstitialAdDismissed(object sender, EventArgs args)
    {
        this.interstitial.Destroy();
        this.interstitial = null;
    }

    public void HandleInterstitialImpression(object sender, ImpressionData impressionData)
    {
        var data = impressionData == null ? "null" : impressionData.rawData;
    }

    public void HandleInterstitialAdFailedToShow(object sender, AdFailureEventArgs args)
    {
        
    }
    #endregion

    #region Rewarded Ad callback handlers

    public void HandleRewardAdLoaded(object sender, RewardedAdLoadedEventArgs args)
    {
        this.rewardedAd = args.RewardedAd;
    }

    public void HandleRewardAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        
    }

    public void HandleRewardAdClicked(object sender, EventArgs args)
    {
        
    }

    public void HandleRewardAdShown(object sender, EventArgs args)
    {
        
    }

    public void HandleRewardAdDismissed(object sender, EventArgs args)
    {
        this.rewardedAd.Destroy();
        this.rewardedAd = null;
    }

    public void HandleRewardImpression(object sender, ImpressionData impressionData)
    {
        var data = impressionData == null ? "null" : impressionData.rawData;
    }

    public void HandleRewardRewarded(object sender, Reward args)
    {

    }

    public void HandleRewardAdFailedToShow(object sender, AdFailureEventArgs args)
    {
        
    }

    #endregion
}