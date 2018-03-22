// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QuestGiver.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2018.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using QuestSystem;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.EventSystems;

public class QuestGiver : NetworkBehaviour , IPointerDownHandler
{
    public NPCBasicInformationData NpcBasicInformationBasicInformationData;
	public List<PlayerController> PlayersAvaliableToInteract = new List<PlayerController> ();

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
			PlayersAvaliableToInteract.Add(other.gameObject.GetComponent<PlayerController>());
        }
    }
	private void OnTriggerExit(Collider other)
	{
		if(other.transform.CompareTag("Player"))
		{
			PlayersAvaliableToInteract.Remove (other.gameObject.GetComponent<PlayerController> ());
		}
	}

	/// <summary>
	/// Raises the mouse down event. On local Player
	/// </summary>
	private void OnMouseDown()
	{
		if (PlayersAvaliableToInteract.Count != 0)
		{
			ExecuteEvents.Execute(this.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerDownHandler);
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left) 
		{
			foreach (var player in PlayersAvaliableToInteract)
			{
				NpcBasicInformationBasicInformationData.GiveQuestToPlayer (player);
			}
		}
	}
}