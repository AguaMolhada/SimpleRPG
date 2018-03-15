using System.Collections;
using System.Collections.Generic;
using QuestSystem;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public NPCBasicInformationData NpcBasicInformationBasicInformationData;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            Debug.Log("AAAAAA");
            NpcBasicInformationBasicInformationData.GiveQuestToPlayer(other.gameObject.GetComponent<PlayerController>());
        }
    }
}
