using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : BaseData
{
    public override string Name => "Game Data";
    public override string Key => "GameData";

    public int Coin = 10;
}

public class PlayerData : BaseData
{
    public override string Name => "Player Data";
    public override string Key => "PlayerData";
    public int Health = 5;
    public int AttackDamage = 1;
}

public class SettingData : BaseData
{
    public override string Name => "Setting Data";
    public override string Key => "SettingData";

    public float SFXVolume = 1;
    public float MusicVolume = 1;
}
