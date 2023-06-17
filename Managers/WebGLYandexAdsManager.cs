using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using YandexSDK;

public class YandexAdsManager : MonoBehaviour
{
    public static YandexAdsManager Instance;

    public void ShowInterstitialAd()
    {
        //ShowInterstitialRequest();
        //if (YaSDK.instance.isInterstitialReady)
        //{
            //YaSDK.instance.ShowInterstitial();
        //}
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //YaSDK.onRewardedAdReward += Reward;
    }

    /*public void Reward(string placement)
    {
        AdsManager.Instance.Yandex_rewardedAd_OnUserEarnedReward();
    }*/

    public void Reward()
    {
        AdsManager.Instance.Yandex_rewardedAd_OnUserEarnedReward();
    }
}
