using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class VKManager : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void ShareRequest();

    [DllImport("__Internal")]
    private static extern void FriendsInviteRequest();

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
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
        Game.Instance.gameData.AddMoney(1000);
    }

    public void GiveMoney5000()
    {
        Game.Instance.gameData.AddMoney(5000);
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