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

    public int PlayerLevel { get; protected set; }

    public int PlayerExperience { get; protected set; }
    public int RequiredExperience { get; protected set; }

    public int PlayerAgi;
    public int PlayerDex;
    public int PlayerInt;
    public int PlayerLuk;
    public int PlayerVit;
    public int PlayerCon;

    public int AvaliablePointsToDistribute
    {
        get { return (PlayerLevel * 5 + 25) - (PlayerInt + PlayerCon + PlayerDex + PlayerLuk + PlayerVit + PlayerAgi); }
       set { value = (PlayerLevel * 5 + 25) - (PlayerInt + PlayerCon + PlayerDex + PlayerLuk + PlayerVit + PlayerAgi); }
    }

    public int ExtraAgi { get; private set; }
    public int ExtraDex { get; private set; }
    public int ExtraInt { get; private set; }
    public int ExtraLuk { get; private set; }
    public int ExtraVit { get; private set; }
    public int ExtraCon { get; private set; }

    /// <summary>
    /// Check the level
    /// </summary>
    /// <param name="exp">Current xp to check</param>
    /// <returns>Level based on the exp.</returns>
    protected int ReturnLevel(int exp)
    {
        return (int) (exp / (GameController.ExperienceBase + Mathf.Log10(PlayerLevel))) + 1;
    }

    public void AddExtraStats(int[] extraStatas)
    {
        ExtraAgi = extraStatas[0];
        ExtraDex = extraStatas[1];
        ExtraInt = extraStatas[2];
        ExtraLuk = extraStatas[3];
        ExtraVit = extraStatas[4];
        ExtraCon = extraStatas[5];
    }

    /// <summary>
    /// Add experience to the character
    /// </summary>
    /// <param name="x">Ammout</param>
    public void AddExperience(int x)
    {
        PlayerExperience += x;
        PlayerLevel = ReturnLevel(PlayerExperience);
    }
    /// <summary>
    /// Add int stats if there is avaliable points to distribute.
    /// </summary>
    /// <param name="Int">Ammount to add</param>
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
    /// <summary>
    /// Add con stats if there is avaliable points to distribute.
    /// </summary>
    /// <param name="Con">Ammount to add</param>
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
    /// <summary>
    /// Add dex stats if there is avaliable points to distribute.
    /// </summary>
    /// <param name="Dex">Ammount to add</param>
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
    /// <summary>
    /// Add luk stats if there is avaliable points to distribute.
    /// </summary>
    /// <param name="Luk">Ammount to add</param>
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
    /// <summary>
    /// Add vit stats if there is avaliable points to distribute.
    /// </summary>
    /// <param name="Vit">Ammount to add</param>
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
    /// <summary>
    /// Add agi stats if there is avaliable points to distribute.
    /// </summary>
    /// <param name="Agi">Ammount to add</param>
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

    /// <summary>
    /// Check if the ammout of stats distributed isn't greater than the max in the actual level
    /// </summary>
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

    /// <summary>
    /// Reset all stats to 1
    /// </summary>
    public void ResetStats()
    {
        PlayerInt = 1;
        PlayerCon = 1;
        PlayerDex = 1;
        PlayerLuk = 1;
        PlayerVit = 1;
        PlayerAgi = 1;
    }

    /// <summary>
    /// Reset the expt to 0
    /// </summary>
    public void ResetExp()
    {
        PlayerExperience = 1;
        PlayerLevel = ReturnLevel(PlayerExperience);
    }

}