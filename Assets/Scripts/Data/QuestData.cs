// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QuestData.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2018.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using QuestSystem;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "New QuestDatabase")]
public class QuestData : ScriptableObject
{
    public List<Quest> Quests = new List<Quest>();
}
