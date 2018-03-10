// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnemyStats.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2018.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "Data / New Enemy Stats")]
public class EnemyStats : ScriptableObject
{
    /// <summary>
    /// Public Base Enemy Health.
    /// </summary>
    public int BaseHealth;
    /// <summary>
    /// Public base enemy moviment speed.
    /// </summary>
    public float BaseSpeed;
    /// <summary>
    /// Public base enemy damage.
    /// </summary>
    public int BaseDmg;
    /// <summary>
    /// The enemy can attack ranged?
    /// </summary>
    public bool Ranged;
    /// <summary>
    /// The enemy can attack meele?
    /// </summary>
    public bool Meele;
    /// <summary>
    /// Monster skin to instantiate.
    /// </summary>
    public GameObject MonsterSkin;
    
}
