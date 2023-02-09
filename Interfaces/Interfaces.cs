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
}

public interface IAds
{
    public void InitInterstitialAd();
    public void InitRewardedAd();

    public void ShowInterstitialAd();
    public void ShowRewardedAd();
}
