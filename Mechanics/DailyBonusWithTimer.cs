using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using GGMatch3;
using System.Threading;
using System.Runtime.InteropServices.ComTypes;
using Unity.VisualScripting;

public class DailyBonusWithTimer : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    [SerializeField] private int _nowDay = 1;
    [SerializeField] private GameObject[] _gettedCases;
    [SerializeField] private Button[] _buttonsGetReward;
    [SerializeField] private Image[] _menuDays;
    [SerializeField] private Sprite _currentDaySprite;
    [SerializeField] private Text _timerReset;
    [SerializeField] private bool _canGetReward = false;
    private DateTime oldDate;
    private DateTime newDate;
    private DateTime startDate;
    private DateTime endDate;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("StartDateDailyBonus"))
        {
            startDate = Convert.ToDateTime(DateTime.Now.ToString());
            PlayerPrefs.SetString("StartDateDailyBonus", startDate.ToString());
        }
        else
            startDate = Convert.ToDateTime(PlayerPrefs.GetString("StartDateDailyBonus"));

        endDate = startDate.AddDays(14);

        TimeSpan difference = endDate - DateTime.Now;
        if (difference.Seconds <= 0)
        {
            PlayerPrefs.DeleteKey("NowDayDailyLoginBonus");
            PlayerPrefs.DeleteKey("PlayDate");
            PlayerPrefs.DeleteKey("DailyBonus1Getted");
            PlayerPrefs.DeleteKey("StartDateDailyBonus");
            PlayerPrefs.DeleteKey("DailyBonusCompleted");
        }
        DayCheck();
        _nowDay = PlayerPrefs.GetInt("NowDayDailyLoginBonus", 1);
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

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(1f);
        TimeSpan difference = endDate-DateTime.Now;
        _timerReset.text = difference.Days.ToString() + "d " + difference.Hours.ToString() + "h " + difference.Minutes.ToString() + "m " + difference.Seconds.ToString() + "s ";
        
        StartCoroutine(Timer());
    }
        
    public void DayCheck()
    {
        _nowDay = PlayerPrefs.GetInt("NowDayDailyLoginBonus", 1);
        string stringDate;
        if (PlayerPrefs.HasKey("PlayDate"))
            stringDate = PlayerPrefs.GetString("PlayDate");
        else
        {
            stringDate = DateTime.Now.ToString();
            PlayerPrefs.SetString("PlayDate", stringDate);
        }
        
        oldDate = Convert.ToDateTime(stringDate);
        newDate = DateTime.Now;

        if (PlayerPrefs.GetInt("DailyBonusCompleted", 0) == 1)
            return;

        TimeSpan difference = newDate.Subtract(oldDate);
        if  (oldDate.Day == newDate.Day)
        {
            if (!_gettedCases[_nowDay - 1].activeInHierarchy)
            {
                _canGetReward = true;
                _menuDays[_nowDay - 1].sprite = _currentDaySprite;
                foreach (Button _buttonGetReward in _buttonsGetReward)
                    _buttonGetReward.interactable = true;
                string newStringDate = Convert.ToString(newDate);
                PlayerPrefs.SetString("PlayDate", newStringDate);
                OpenMenu();
            }
        }

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
            PlayerPrefs.DeleteKey("StartDateDailyBonus");
            PlayerPrefs.DeleteKey("DailyBonusCompleted");
        }
        StartCoroutine(Timer());
        PlayerPrefs.Save();
    }

    public void OpenMenu()
    {
        _menu.SetActive(true);
        TimeSpan difference = endDate - DateTime.Now;
        if (difference.Seconds <= 0)
            ResetDailyBonus();
    }

    private void ResetDailyBonus()
    {
        PlayerPrefs.DeleteKey("NowDayDailyLoginBonus");
        PlayerPrefs.DeleteKey("PlayDate");
        PlayerPrefs.DeleteKey("DailyBonus1Getted");
        PlayerPrefs.DeleteKey("StartDateDailyBonus");
        PlayerPrefs.DeleteKey("DailyBonusCompleted");

        _menuDays[0].sprite = _currentDaySprite;
        _canGetReward = true;
        foreach (Button _buttonGetReward in _buttonsGetReward)
            _buttonGetReward.interactable = true;

        for (int i = 0; i < _gettedCases.Length; i++)
        {
            _gettedCases[i].SetActive(false);
        }
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

        _gettedCases[_nowDay - 1].SetActive(true);
        _nowDay++;
        if (_nowDay >= 15)
        {
            PlayerPrefs.SetInt("DailyBonusCompleted", 1);
        }
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
        if (_nowDay >= 15)
        {
            PlayerPrefs.SetInt("DailyBonusCompleted", 1);
        }
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
