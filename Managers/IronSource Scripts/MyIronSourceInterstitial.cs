using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyIronSourceInterstitial : MonoBehaviour
{
    public static MyIronSourceInterstitial Instance;
    public string appkey;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the fsirst frame update
    void Start()
    {
        IronSource.Agent.loadInterstitial();
        IronSourceEvents.onInterstitialAdClosedEvent += InterstitialAdClosedEvent;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Invoked when the interstitial ad closed and the user goes back to the application screen.
    void InterstitialAdClosedEvent()
    {
        IronSource.Agent.init(appkey, IronSourceAdUnits.INTERSTITIAL);
        IronSource.Agent.loadInterstitial();
        AdsManager.Instance.IronSourceInterstitialClosed();
    }

    public void interstitialplay()
    {
        IronSource.Agent.showInterstitial();
    }
}