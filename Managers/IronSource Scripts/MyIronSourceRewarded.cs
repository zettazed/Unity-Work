using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyIronSourceRewarded : MonoBehaviour
{
    public static MyIronSourceRewarded Instance;
    public string appkey;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        IronSource.Agent.shouldTrackNetworkState(true);
        IronSourceEvents.onRewardedVideoAvailabilityChangedEvent += RewardedVideoAvailabilityChangedEvent;
        IronSourceEvents.onRewardedVideoAdClosedEvent += RewardedVideoAdClosedEvent;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void rewarded()
    {

        IronSource.Agent.showRewardedVideo();
    }

    void RewardedVideoAdClosedEvent()
    {
        IronSource.Agent.init(appkey, IronSourceAdUnits.REWARDED_VIDEO);
        IronSource.Agent.shouldTrackNetworkState(true);
        AdsManager.Instance.IronSourceReward();
    }

    void RewardedVideoAvailabilityChangedEvent(bool available)
    {
        //Change the in-app 'Traffic Driver' state according to availability.
        bool rewardedVideoAvailability = available;

    }
}