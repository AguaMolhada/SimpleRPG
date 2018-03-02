// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameController.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Vianna 2018.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class GameController : MonoBehaviour
{
    /// <summary>
    /// Singleton.
    /// </summary>
    public static GameController Instance;
    /// <summary>
    /// Max Gold.
    /// </summary>
    public const int MaxGold = 999999999;
    /// <summary>
    /// Max Cash.
    /// </summary>
    public const int MaxCash = 999999999;
    /// <summary>
    /// Max Level.
    /// </summary>
    public const int MaxLevel = 100;
    /// <summary>
    /// Amount of experience to start.
    /// </summary>
    public const int ExperienceBase = 100;
    /// <summary>
    /// Max experience to obtain.
    /// </summary>
    public const int MaxExperience = 1000000;
    /// <summary>
    /// Hello Player.
    /// </summary>
    public PlayerController Player;
    /// <summary>
    /// Is the Game Paused?
    /// </summary>
    public bool IsPaused; 
    /// <summary>
    /// List with all enemies and respectives Aggros.
    /// </summary>
    public List<AggroStructure> EnemyAgroTabble;
    /// <summary>
    /// List with all clients
    /// </summary>
    public List<GameObject> PlayersClients;
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
    
    /// <summary>
    /// Construc Aggro Table.
    /// </summary>
    public void ConstructAggroTable()
    {
        PlayersClients = GameObject.FindGameObjectsWithTag("Player").ToList();
        EnemyAgroTabble = new List<AggroStructure>();
        var tempE = GameObject.FindObjectsOfType<EnemyAI>();
        foreach (var ai in tempE)
        {
            var tempAggro = new AggroStructure(ai);
            foreach (var player in PlayersClients)
            {
                tempAggro.AddPlayerAggro(new AggroNode(player, 0));
            }
            var y = tempAggro.PlayerAggro.OrderByDescending(x => x.AggroAmount).ToList();
            tempAggro.PlayerAggro = y;
            EnemyAgroTabble.Add(tempAggro);
        }
    }


}
