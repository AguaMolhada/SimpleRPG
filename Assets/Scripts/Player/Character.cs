// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Character.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Character")]
public class Character : ScriptableObject
{
    /// <summary>
    /// Player Game object.
    /// </summary>
    public GameObject CharacterObj;
    /// <summary>
    /// Base Character Stats.
    /// </summary>
    public PlayerStats CharacterStats;


}
