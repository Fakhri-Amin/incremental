using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class TimeMethodsServer : MonoBehaviour
{
#pragma warning disable UDR0001 // Domain Reload Analyzer
    private static DateTime _serverTime;
#pragma warning restore UDR0001 // Domain Reload Analyzer

    private const string API_URL = "https://timeapi.io/api/Time/current/zone?timeZone=Etc/UTC";

    public static bool IsTimeReady { get; private set; }
    public static DateTime Now => IsTimeReady ? _serverTime : DateTime.UtcNow;

    /// <summary>
    /// Call this at game start to fetch real-world time.
    /// </summary>
    public static IEnumerator FetchServerTime()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(API_URL))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed to fetch server time: " + request.error);
                _serverTime = DateTime.UtcNow;
            }
            else
            {
                try
                {
                    string json = request.downloadHandler.text;
                    WorldTimeResponse response = JsonUtility.FromJson<WorldTimeResponse>(json);
                    _serverTime = DateTime.Parse(response.dateTime, null, System.Globalization.DateTimeStyles.RoundtripKind);
                    IsTimeReady = true;
                    Debug.Log("✅ Server Time: " + _serverTime);
                }
                catch (Exception e)
                {
                    Debug.LogError("Failed to parse server time: " + e.Message);
                    _serverTime = DateTime.UtcNow;
                }
            }
        }
    }

    public static TimeSpan SubtractTime(DateTime oldTime)
    {
        return Now.Subtract(oldTime);
    }

    public static void SaveTime(string saveName)
    {
        PlayerPrefs.SetString(saveName, Now.ToBinary().ToString());
        PlayerPrefs.Save();
    }

    public static void SaveTime(string saveName, TimeSpan remainingTime)
    {
        PlayerPrefs.SetString(saveName, Now.Subtract(remainingTime).ToBinary().ToString());
        PlayerPrefs.Save();
    }

    public static DateTime LoadTime(string saveName)
    {
        if (!PlayerPrefs.HasKey(saveName))
        {
            SaveTime(saveName);
            return Now;
        }
        else
        {
            long temp = Convert.ToInt64(PlayerPrefs.GetString(saveName));
            return DateTime.FromBinary(temp);
        }
    }

    public static void ResetTime(string saveName)
    {
        if (PlayerPrefs.HasKey(saveName))
        {
            PlayerPrefs.DeleteKey(saveName);
        }
    }

    public static int LoadDay(string saveName)
    {
        if (!PlayerPrefs.HasKey(saveName))
        {
            SaveDay(saveName, 0);
            return 0;
        }
        else
        {
            return PlayerPrefs.GetInt(saveName);
        }
    }

    public static void ResetDay(string saveName)
    {
        SaveDay(saveName, 0);
    }

    public static void SaveDay(string saveName, int currentDay)
    {
        PlayerPrefs.SetInt(saveName, currentDay);
        PlayerPrefs.Save();
    }

    public static bool SaveExists(string saveName)
    {
        return PlayerPrefs.HasKey(saveName);
    }

    public static void MakeButtonAvailable(string saveName, TimeSpan openTime)
    {
        DateTime timeToSave = Now.Subtract(openTime);
        PlayerPrefs.SetString(saveName, timeToSave.ToBinary().ToString());
        PlayerPrefs.Save();
    }

    [Serializable]
    private class WorldTimeResponse
    {
        public string dateTime;
        public string timeZone;
    }
}

