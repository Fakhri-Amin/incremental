using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class ExtensionMethod
{
    public static void ClearChild(this Transform t)
    {
        for (int i = 0; i < t.childCount; i++)
        {
            MonoBehaviour.Destroy(t.GetChild(i).gameObject);
        }
    }

    public static string ToCurrency(this int number)
    {
        if (number >= 1_000_000)
        {
            // Over 1 million: show up to 1 decimal place
            return (number / 1_000_000f).ToString("0.#") + "M";
        }
        else if (number >= 100_000)
        {
            // Over 100K: show no decimals
            return (number / 1_000f).ToString("0") + "K";
        }
        else if (number >= 10_000)
        {
            // Between 10K and 100K: show 1 decimal
            return (number / 1_000f).ToString("0.#") + "K";
        }
        else if (number >= 1_000)
        {
            // Below 10K: show 2 decimals
            return (number / 1_000f).ToString("0.##") + "K";
        }
        else
        {
            return number.ToString();
        }
    }


    public static string ToStringSpriteAsset(this int text)
    {
        return "<sprite index=0>" + text;
    }

    public static string ToStringSpriteAsset(this string text)
    {
        return "<sprite index=0>" + text;
    }
}