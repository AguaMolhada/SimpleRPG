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
[CreateAssetMenu(menuName = "New Color Pallet")]
public class RarityController : ScriptableObject
{
    /// <summary>
    /// Array with all colors.
    /// </summary>
    public List<ColorRarity> Colors;
    /// <summary>
    /// Uncommon name prefixs.
    /// </summary>
    public List<string> UncommunPrefixs = new List<string>()
    {
        "The Blind",
    };
    /// <summary>
    /// Rare name prefixs.
    /// </summary>
    public List<string> RarePrefixs = new List<string>()
    {
        "The Haunting",
        "The Deadly",
        "The Primitive",
        "The Fiery Horror",
        "The Quick",
        "The Evasive",
        "The Horrible",
        "The Supreme",
        "The Forsaken",
        "The Matriarch",
        "The Evil",
        "The Reckless",
        "The Dark"
    };
    /// <summary>
    /// Unique names.
    /// </summary>
    public List<string> UniqueNames = new List<string>()
    {
        "Argathor, The Mighty Platypus",
        ""
    };

}

