using System;

public static class DateManager
{
    public static int GetCurrentDay()
    {
        int currentDay;
        string date = DateTime.Today.ToString();
        string[] parserDate = date.Split('.');
        currentDay = Parser.IntParsing(parserDate[0]);

        return currentDay;
    }

    public static int GetCurrentHour()
    {
        int currentHour;
        string date = DateTime.UtcNow.ToLocalTime().ToString();
        string[] parserDate = date.Split(':');
        currentHour = Parser.IntParsing(parserDate[0]);

        return currentHour;
    }

    public static int GetCurrentMinute()
    {
        int currentMinute;
        string date = DateTime.UtcNow.ToLocalTime().ToString();
        string[] parserDate = date.Split(':');
        currentMinute = Parser.IntParsing(parserDate[1]);

        return currentMinute;
    }

    public static int GetCurrentSecond()
    {
        int currentSecond;
        string date = DateTime.UtcNow.ToLocalTime().ToString();
        string[] parserDate = date.Split(':');
        currentSecond = Parser.IntParsing(parserDate[2]);

        return currentSecond;
    }
}