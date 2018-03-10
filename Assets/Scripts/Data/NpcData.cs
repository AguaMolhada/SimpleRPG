// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NpcData.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2018.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using QuestSystem;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Data / New NPC")]
public class NpcData : ScriptableObject
{
    public string NPCName;
    public GameObject NpcGameobject;
    public List<Quest> QuestsToGive;
}