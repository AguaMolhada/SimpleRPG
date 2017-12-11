using UnityEngine;
using System;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;

[Serializable]
[CreateAssetMenu(menuName = "New PlayerStats Base")]
public class PlayerStats : ScriptableObject
{
    /// <summary>
    /// Player Character Class
    /// </summary>
    public PlayerClass PlayerClass = PlayerClass.Warrior;

    public int PlayerLevel
    {
        get { return ReturnLevel(PlayerExperience); }
        set { value = ReturnLevel(PlayerExperience); }
    }

    public int PlayerExperience { get; protected set; }
    public int PlayerInt;
    public int PlayerCon;
    public int PlayerDex;
    public int PlayerLuk;
    public int PlayerVit;
    public int PlayerAgi;

    [SerializeField]
    public int AvaliablePointsToDistribute
    {
        get { return (PlayerLevel * 5 + 25) - (PlayerInt + PlayerCon + PlayerDex + PlayerLuk + PlayerVit + PlayerAgi); }
       set { value = (PlayerLevel * 5 + 25) - (PlayerInt + PlayerCon + PlayerDex + PlayerLuk + PlayerVit + PlayerAgi); }
    }

    public int ExtraInt { get; private set; }
    public int ExtraCon { get; private set; }
    public int ExtraDex { get; private set; }
    public int ExtraLuk { get; private set; }
    public int ExtraVit { get; private set; }
    public int ExtraAgi { get; private set; }

    protected int ReturnLevel(int exp)
    {
        return (int) exp / GameController.ExperienceBase + (PlayerLevel * (GameController.ExperienceBase * 1 / 3));
    }

    public void AddExperience(int x)
    {
        PlayerExperience += x;
    }

    public void AddStatsInt(int Int)
    {
        if (AvaliablePointsToDistribute == 0 && Int > 0)
        {
            return;
        }
        PlayerInt += Int;
        PlayerInt = Mathf.Clamp(PlayerInt, 1, 255);
        CheckStatus();
    }


    public void AddStatsCon(int Con)
    {
        if (AvaliablePointsToDistribute == 0 && Con > 0)
        {
            return;
        }
        PlayerCon += Con;
        PlayerCon = Mathf.Clamp(PlayerCon, 1, 255);
        CheckStatus();
    }


    public void AddStatsDex(int Dex)
    {
        if (AvaliablePointsToDistribute == 0 && Dex > 0)
        {
            return;
        }
        PlayerDex += Dex;
        PlayerDex = Mathf.Clamp(PlayerDex, 1, 255);
        CheckStatus();

    }

    public void AddStatsLuk(int Luk)
    {
        if (AvaliablePointsToDistribute == 0 && Luk > 0)
        {
            return;
        }
        PlayerLuk += Luk;
        PlayerLuk = Mathf.Clamp(PlayerLuk, 1, 255);
        CheckStatus();
    }

    public void AddStatsVit(int Vit)
    {
        if (AvaliablePointsToDistribute == 0 && Vit > 0)
        {
            return;
        }
        PlayerVit += Vit;
        PlayerVit = Mathf.Clamp(PlayerVit, 1, 255);
        CheckStatus();
    }

    public void AddStatsAgi(int Agi)
    {
        if (AvaliablePointsToDistribute == 0 && Agi > 0)
        {
            return;
        }
        PlayerAgi += Agi;
        PlayerAgi = Mathf.Clamp(PlayerAgi, 1, 255);
        CheckStatus();
    }

    private void CheckStatus()
    {
        Debug.Log(AvaliablePointsToDistribute);
        var totalPoints = PlayerInt + PlayerCon + PlayerDex + PlayerLuk + PlayerVit + PlayerAgi;
        if (totalPoints > PlayerLevel * 5 + 25)
        {
            Debug.LogError("Cheating!");
            ResetStats();
        }
    }

    public void ResetStats()
    {
        PlayerInt = 1;
        PlayerCon = 1;
        PlayerDex = 1;
        PlayerLuk = 1;
        PlayerVit = 1;
        PlayerAgi = 1;
    }

    public void ResetExp()
    {
        PlayerExperience = 0;
    }

}

public enum PlayerClass
{
    Warrior,
    Mage,
    Archer,
    Thief,
}