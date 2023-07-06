using Firebase.Analytics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeAds : MonoBehaviour
{
    public static InitializeAds Instance;

    public string appKey;

    // Start is called before the first frame update

    private void Awake()
    {
        Instance = this;
        IronSource.Agent.init(appKey);
        IronSource.Agent.validateIntegration();
        IronSourceEvents.onImpressionDataReadyEvent += ImpressionSuccessEvent;
    }

    void Start()
    {
        //Loadbanner();
    }

    void OnApplicationPause(bool isPaused)
    {
        IronSource.Agent.onApplicationPause(isPaused);
    }

    public void Loadbanner()
    {
        IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, IronSourceBannerPosition.BOTTOM);
    }

    private void ImpressionSuccessEvent(IronSourceImpressionData impressionData)
    {
        if (impressionData != null)
        {
            Parameter[] AdParameters ={
                    new Parameter("ad_platform","ironSource"),
                    new Parameter("ad_source",impressionData.adNetwork),
                    new Parameter("ad_unit_name",impressionData.adUnit),
                    new Parameter("ad_format",impressionData.instanceName),
                    new Parameter("currency","USD"),
                    new Parameter("value",(double)impressionData.revenue)
            };
            FirebaseAnalytics.LogEvent("ad_impression", AdParameters);
        }
        if (impressionData != null)
        {
            Parameter[] AdParameters ={
                    new Parameter("ad_platform","ironSource"),
                    new Parameter("ad_source",impressionData.adNetwork),
                    new Parameter("ad_unit_name",impressionData.adUnit),
                    new Parameter("ad_format",impressionData.instanceName),
                    new Parameter("currency","USD"),
                    new Parameter("value",(double)impressionData.revenue)
            };
            FirebaseAnalytics.LogEvent("ad_iron_source", AdParameters);
        }
    }
}