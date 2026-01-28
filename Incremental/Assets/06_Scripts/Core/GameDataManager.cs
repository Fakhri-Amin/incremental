using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Eggtato.Utility;

[DefaultExecutionOrder(-999999)]
public class GameDataManager : PersistentSingleton<GameDataManager>
{
    // 🔹 Data Containers
    public GameData GameData => Data.Get<GameData>();
    public PlayerData PlayerData => Data.Get<PlayerData>();
    public SettingData SettingData => Data.Get<SettingData>();

    public new void Awake()
    {
        base.Awake();
    }

    // --------------------------------------------------------------------------
    #region 🔹 General Save / Clear
    public void SaveAll()
    {
        Data.Save();
    }

    public void SaveGame() => GameData.Save();
    public void SavePlayer() => PlayerData.Save();
    public void SaveSetting() => SettingData.Save();

    public void ClearAllData()
    {
        PlayerPrefs.DeleteAll();

        ResetData();

        SaveAll();
    }

    private void ResetData()
    {

    }

    #endregion

    // --------------------------------------------------------------------------

    #region 🔹 Coin
    public void AddCoin(int amount)
    {
        GameData.Coin += amount;
        SaveGame();
    }

    public void RemoveCoin(int amount)
    {
        GameData.Coin = Mathf.Max(0, GameData.Coin - amount);
        SaveGame();
    }

    public void SetCoin(int newAmount)
    {
        GameData.Coin = Mathf.Max(0, newAmount);
        SaveGame();
    }

    public int GetCoin() => GameData.Coin;
    #endregion
}
