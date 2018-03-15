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
public class NPCBasicInformationData : ScriptableObject
{
    public string NPCName;
    public GameObject NpcGameobject;
    public List<Quest> QuestsToGive;

    public void GiveQuestToPlayer(PlayerController player)
    {
        var temp = (Quest)QuestsToGive.Find(quest => quest.Identifier.IsQuestComplete == false).Clone();
        foreach (var completedQuest in player.CompletedQuests)
        {
            if (temp.Identifier.PrQuest == completedQuest.Identifier.ID)
            {
                player.AddQuest(false,temp);
                break;
            }

            if (temp.Identifier.PrQuest == -1)
            {
                player.AddQuest(false, temp);
                break;
            }
        }
        Debug.Log("Dont have prquest!");           
    }


}