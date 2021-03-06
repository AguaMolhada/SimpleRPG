﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlayerStats.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Vianna 2018.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(menuName = "Data / New PlayerStats Base")]
public class PlayerStats : ScriptableObject
{
    /// <summary>
    /// Public current Player Health.
    /// </summary>
    public int Health { get; protected set; }
    /// <summary>
    /// Public get max ammount of helth
    /// </summary>
    public int MaxHealth 
    {
        get { return (int)(((PlayerVit + ExtraCon) * 0.25f) * 100); }
        set { value = (int)(((PlayerVit + ExtraCon) * 0.25f) * 100); }
    }
    /// <summary>
    /// Player CharacterSkin Class
    /// </summary>
    public PlayerClass PlayerClass = PlayerClass.Warrior;
    /// <summary>
    /// Current Player Level.
    /// </summary>
    public int PlayerLevel { get; protected set; }
    /// <summary>
    /// Total Amount exp.
    /// </summary>
    public int PlayerExperience { get; protected set; }
    /// <summary>
    /// Required exp to next lvl.
    /// </summary>
    public int RequiredExperience { get; protected set; }
    /// <summary>
    /// Amount player Agility.
    /// </summary>
    public int PlayerAgi;
    /// <summary>
    /// Ammmount player Dexterity.
    /// </summary>
    public int PlayerDex;
    /// <summary>
    /// Amount player Intelligence..
    /// </summary>
    public int PlayerInt;
    /// <summary>
    /// Amount player Lucky.
    /// </summary>
    public int PlayerLuk;
    /// <summary>
    /// Amount player Vitality
    /// </summary>
    public int PlayerVit;
    /// <summary>
    /// Amount player Constitution.
    /// </summary>
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
    /// Level Up
    /// </summary>
    protected void LevelUp()
    {
        PlayerLevel += 1;
        PlayerExperience -= RequiredExperience;
        RequiredExperience = GameController.Instance.ExperienceCurve(PlayerLevel);
    }
    /// <summary>
    /// Add health to the player if amount negative subdract.
    /// </summary>
    /// <param name="x">Amount</param>
    public void AddHealth(int x)
    {
        Health -= x;
        if (Health <= 0)
        {
            Die();
        }
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
    }
    /// <summary>
    /// When the caracter dies.
    /// </summary>
    public void Die()
    {
        Health = 
        PlayerExperience -= (int) (PlayerExperience * 0.25);
        if (PlayerExperience <= 0)
        {
            PlayerExperience = 0;
        }
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
        if (PlayerExperience >= RequiredExperience)
        {
            if (PlayerLevel < GameController.MaxLevel)
            {
                LevelUp();
            }
        }
    }
    /// <summary>
    /// Add int stats if there is avaliable points to distribute.
    /// </summary>
    /// <param name="Int">Amount to add</param>
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
    /// <param name="Con">Amount to add</param>
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
    /// <param name="Dex">Amount to add</param>
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
    /// <param name="Luk">Amount to add</param>
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
    /// <param name="Vit">Amount to add</param>
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
    /// <param name="Agi">Amount to add</param>
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
        RequiredExperience = GameController.ExperienceBase;
        PlayerLevel = 1;
    }

}