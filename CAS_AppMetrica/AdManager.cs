using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using CAS.AdObject;

public class AdManager : MonoBehaviour
{
    public static AdManager Instance;

    public UnityEvent InterstitialAdShow;
    public UnityEvent RewardedAdShow;

    public Action OnInterstitialClosed;
    public Action OnRewardEvent;

    public bool secondFortune = false;


    private void Awake()
    {
        Instance = this;
    }

    public void ShowInterstitial()
    {
        InterstitialAdShow.Invoke();
    }

    public void ShowRewarded()
    {
        RewardedAdShow.Invoke();
    }

    public void OnReward()
    {
        OnRewardEvent.Invoke();
    }

    public void OnInterstitialClose()
    {
        OnInterstitialClosed.Invoke();
    }
}