// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameController.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Vianna 2018.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public const int MaxGold = 999999999;
    public const int MaxCash = 999999999;
    public const int MaxLevel = 100;
    public const int ExperienceBase = 100;
    public const int MaxExperience = 1000000;
    public PlayerController Player;
    public bool IsPaused;

    /// <summary>
    /// Player experience curve
    /// </summary>
    /// <param name="level">Level to set the required experience</param>
    /// <returns></returns>
    public int ExperienceCurve(int level)
    {
        var b = Math.Log((double) MaxExperience / ExperienceBase) / (MaxLevel - 1);
        var a = (double) ExperienceBase / (Math.Exp(b) - 1.0);
        int oldXp = (int) Math.Round(a * Math.Exp(b * (level - 1)));
        int newXp = (int) Math.Round(a * Math.Exp(b * level));
        return (newXp - oldXp);
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
