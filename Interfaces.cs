using UnityEngine;

public interface IAnyWindow
{
    public void CloseMenu();
    public void OpenMenu();
}

public interface IWindowsWithCloseOtherWindows : IAnyWindow
{
    public void OpenCloseOtherWindows(bool enabled);
}

public interface ITranslator
{
    public enum Language { };
    public static string GetTranslate(string text);
    public static void SetLanguage(string langCode);
}

public interface ICurrentDate
{
    public enum Language { };
    public static int GetCurrentDay();
    public static int GetCurrentHour();
    public static int GetCurrentMinute();
    public static int GetCurrentSecond();
}

public interface IFileWrite
{
    public static void WriteStringAtEnd(string path, string text);
}

public interface IFileRead
{
    public static string ReadFile(string path);
}

public interface IAds
{
    public void InitInterstitialAd();
    public void InitRewardedAd();

    public void ShowInterstitialAd();
    public void ShowRewardedAd();

    public virtual void _interstitialAd_OnAdClosed(object sender, System.EventArgs e);
    public virtual void _rewardedAd_OnAdClosed(object sender, System.EventArgs e);
    public virtual void _rewardedAd_OnUserEarnedReward(object sender, System.EventArgs e);
}
