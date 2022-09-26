using System;
using UnityEngine;
using UnityEngine.UI;
using GGMatch3;

public class DailyBonus : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    [SerializeField] private int _nowDay = 1;
    [SerializeField] private GameObject[] _gettedCases;
    [SerializeField] private Button[] _buttonsGetReward;
    [SerializeField] private Image[] _menuDays;
    [SerializeField] private Sprite _currentDaySprite;
    [SerializeField] private bool _canGetReward = false;

    private void Awake()
    {
        _nowDay = PlayerPrefs.GetInt("NowDayDailyLoginBonus", 1);
        DayCheck();
        if (_nowDay == 1)
        {
            _menuDays[0].sprite = _currentDaySprite;
            _canGetReward = true;
            foreach (Button _buttonGetReward in _buttonsGetReward)
                _buttonGetReward.interactable = true;
            OpenMenu();
        }
        else
        {
            for (int i = 0; i < _nowDay-1; i++)
            {
                _gettedCases[i].SetActive(true);
            }
        }
    }

    public void DayCheck()
    {
        string stringDate;
        if (PlayerPrefs.HasKey("PlayDate"))
            stringDate = PlayerPrefs.GetString("PlayDate");
        else
        {
            stringDate = DateTime.Now.ToString();
            PlayerPrefs.SetString("PlayDate", stringDate);
        }
            
        DateTime oldDate = Convert.ToDateTime(stringDate);
        DateTime newDate = System.DateTime.Now;

        TimeSpan difference = newDate.Subtract(oldDate);
        if (difference.Days == 1)
        {
            _canGetReward = true;
            _menuDays[_nowDay - 1].sprite = _currentDaySprite;
            foreach (Button _buttonGetReward in _buttonsGetReward)
                _buttonGetReward.interactable = true;
            string newStringDate = Convert.ToString(newDate);
            PlayerPrefs.SetString("PlayDate", newStringDate);
            OpenMenu();
        }
        else if(difference.Days > 1)
        {
            PlayerPrefs.DeleteKey("NowDayDailyLoginBonus");
            PlayerPrefs.DeleteKey("PlayDate");
            PlayerPrefs.DeleteKey("DailyBonus1Getted");
        }
        PlayerPrefs.Save();
    }

    public void OpenMenu()
    {
        _menu.SetActive(true);
    }

    public void CloseMenu()
    {
        _menu.SetActive(false); 
    }

    public void GetReward()
    {
        if (!_canGetReward)
            return;

        WalletManager walletManager = GGPlayerSettings.instance.walletManager;

        switch (_nowDay)
        {
            case 1:
                break;
        }
        if (_nowDay == 1)
            PlayerPrefs.SetInt("DailyBonus1Getted", 1);

        _gettedCases[_nowDay-1].SetActive(true);
        _nowDay++;
        PlayerPrefs.SetInt("NowDayDailyLoginBonus", _nowDay);
        PlayerPrefs.Save();
        foreach (Button _buttonGetReward in _buttonsGetReward)
            _buttonGetReward.interactable = false;
    }

    public void GetX2()
    {
        if (!_canGetReward)
            return;

        WalletManager walletManager = GGPlayerSettings.instance.walletManager;

        switch (_nowDay)
        {
            case 1:
                break;
        }
        if (_nowDay == 1)
            PlayerPrefs.SetInt("DailyBonus1Getted", 1);

        _gettedCases[_nowDay - 1].SetActive(true);
        _nowDay++;
        PlayerPrefs.SetInt("NowDayDailyLoginBonus", _nowDay);
        PlayerPrefs.Save();
        foreach (Button _buttonGetReward in _buttonsGetReward)
            _buttonGetReward.interactable = false;
    }

    public void GetReward_X2()
    {
        AdsManager adsManager = GameObject.Find("AdsManager").GetComponent<AdsManager>();
        adsManager.ShowRewardedAdDailyBonusMenu(8);
    }
}
