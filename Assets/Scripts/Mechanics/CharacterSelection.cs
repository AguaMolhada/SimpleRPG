// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CharacterSelection.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    public Character[] Characters;

    public void OnCharacterSelect(int characterChoice)
    {
        Character SelectCharacter = Characters[characterChoice];
    }
}
