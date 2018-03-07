// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RarityColorPallet.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2018.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "New Enemy Rarity Contoller")]
public class RarityController : ScriptableObject
{
    /// <summary>
    /// Array with all colors.
    /// </summary>
    public List<ColorRarity> Colors;

    /// <summary>
    /// Uncommon name prefixs.
    /// </summary>
    public List<string> UncommunPrefixs;

    /// <summary>
    /// Rare name prefixs.
    /// </summary>
    public List<string> RarePrefixs;

    /// <summary>
    /// Unique names.
    /// </summary>
    public List<string> UniqueNames;

}

