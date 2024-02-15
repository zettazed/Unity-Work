using System;
using System.Linq;
using System.Numerics;

public static class Parser
{
    public static int StringToInt(string text)
    {
        int parsingInt = 0;
        int.TryParse(string.Join("", text.Where(c => char.IsDigit(c))), out parsingInt);
        return parsingInt;
    }

    public static BigInteger StringToBigInteger(string text)
    {
        BigInteger parsingInt = 0;
        BigInteger.TryParse(string.Join("", text.Where(c => char.IsDigit(c))), out parsingInt);
        return parsingInt;
    }

    public static DateTime StringToDateTime(string text)
    {
        DateTime time;
        time = DateTime.Parse(text);

        return time;
    }
    
    public static string ParseString(string text)
    {
        string[] textSplit = text.Split('_');
        return textSplit[0];
    }
}
