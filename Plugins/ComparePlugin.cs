using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ComparePlugin
{
    public static bool IntToBool(int value)
    {
        if (value == 1)
            return true;
        else
            return false;
    }

    public static int BoolToInt(bool value)
    {
        if (value)
            return 1;
        else
            return 0;
    }
}
