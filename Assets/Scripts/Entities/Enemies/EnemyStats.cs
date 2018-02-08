// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnemyStats.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "New Enemy Stats")]
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
    /// Material Color to change.
    /// </summary>
    public Color MaterialColor;
    /// <summary>
    /// Monster skin to instantiate.
    /// </summary>
    public GameObject MonsterSkin;


}
