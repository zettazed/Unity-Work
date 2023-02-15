using System;

public static class DateManager
{
    public static int GetCurrentDay()
    {
        int currentDay;
        string date = DateTime.Today.ToString();
        string[] parserDate = date.Split('.');
        currentDay = Parser.StringToInt(parserDate[0]);

        return currentDay;
    }

    public static int GetCurrentHour()
    {
        int currentHour;
        string date = DateTime.UtcNow.ToLocalTime().ToString();
        string[] parserDate = date.Split(':');
        currentHour = Parser.StringToInt(parserDate[0]);

        return currentHour;
    }

    public static int GetCurrentMinute()
    {
        int currentMinute;
        string date = DateTime.UtcNow.ToLocalTime().ToString();
        string[] parserDate = date.Split(':');
        currentMinute = Parser.StringToInt(parserDate[1]);

        return currentMinute;
    }

    public static int GetCurrentSecond()
    {
        int currentSecond;
        string date = DateTime.UtcNow.ToLocalTime().ToString();
        string[] parserDate = date.Split(':');
        currentSecond = Parser.StringToInt(parserDate[2]);

        return currentSecond;
    }
}
