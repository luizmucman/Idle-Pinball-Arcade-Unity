using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DoubleFormatter
{
    public static string Format(double num)
    {
        return FormatNumberString(num);
    }

    private static string FormatNumberString(double number)
    {
        if(number.ToString().Contains("E"))
        {
            return number.ToString("0.00E+0");
        }

        if (number.ToString().Split('.')[0].Length < 5)
        {
            return number.ToString().Split('.')[0];
        }

        if (number.ToString().Split('.')[0].Length < 7)
        {
            return FormatThousands(number.ToString().Split('.')[0]);
        }

        return number.ToString("0.00E+0");
    }

    private static string FormatThousands(string number)
    {
        string leadingNumbers = number.Substring(0, number.Length - 3);
        string decimals = number.Substring(number.Length - 3, 3);

        return CreateNumericalFormat(leadingNumbers, decimals, "K");
    }

    private static string CreateNumericalFormat(string leadingNumbers, string decimals, string suffix)
    {
        return String.Format("{0}.{1}{2}", leadingNumbers, decimals, suffix);
    }
}
