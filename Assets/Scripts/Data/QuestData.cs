﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QuestData.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2018.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using QuestSystem;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Data / New QuestDatabase")]
public class QuestData : ScriptableObject
{
    [SerializeField]
    public List<Quest> Quests;
}
