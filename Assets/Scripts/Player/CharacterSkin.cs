// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CharacterSkin.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Character Skin")]
public class CharacterSkin : ScriptableObject
{
    /// <summary>
    /// Player Game object.
    /// </summary>
    public GameObject CharacterObj;
    /// <summary>
    /// Is this sking avaliable?
    /// </summary>
    public bool Unlocked;


}
