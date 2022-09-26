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
    if (difference.Days >= 1)
    {
        _canGetReward = true;
        _buttonGetReward.interactable = true;
        string newStringDate = Convert.ToString(newDate);
        PlayerPrefs.SetString("PlayDate", newStringDate);
    }
    PlayerPrefs.Save();
}
