using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class VKManager : MonoBehaviour
{
    public static VKManager Instance;

    [DllImport("__Internal")]
    private static extern void ShowInterstitialRequest();

    [DllImport("__Internal")]
    private static extern void ShowRewardedRequest();

    [DllImport("__Internal")]
    private static extern void ShareRequest();

    [DllImport("__Internal")]
    private static extern void FriendsInviteRequest();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void ShowInterstitial()
    {
        ShowInterstitialRequest();
    }

    public void RewardVK()
    {
        AdsManager.Instance.Yandex_rewardedAd_OnUserEarnedReward();
    }

    public void ShowRewarded()
    {
        ShowRewardedRequest();
    }

    public void Share()
    {
        ShareRequest();
    }

    public void FriendsInvite()
    {
        FriendsInviteRequest();
    }

    public void GiveMoney1000()
    {
        //Game.Instance.gameData.AddMoney(1000);
    }

    public void GiveMoney5000()
    {
        //Game.Instance.gameData.AddMoney(5000);
    }

    public void AudioOff()
    {
        AudioListener.volume = 0;
    }

    public void AudioOn()
    {
        AudioListener.volume = 1;
    }
}
