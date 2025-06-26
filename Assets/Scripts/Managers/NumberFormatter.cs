using System;
using UnityEngine;

public static class NumberFormatter
{
    private static readonly string[] suffixes = { "", "k", "M", "B", "T", "Qa", "Qi", "Sx", "Sp", "Oc", "No" };

    public static string Format(double value, int decimalPlaces = 1)
    {
        if (value < 1000)
            return value.ToString("F0");

        int magnitude = (int)Math.Floor(Math.Log10(value) / 3);
        magnitude = Mathf.Min(magnitude, suffixes.Length - 1);

        double scaled = value / Math.Pow(1000, magnitude);
        string format = "F" + decimalPlaces;

        return scaled.ToString(format) + suffixes[magnitude];
    }
}
