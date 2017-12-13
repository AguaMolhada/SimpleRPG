// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlayerInventory.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    private GameObject _openedSlotsPefab;
    [SerializeField]
    private GameObject _closedSlotsPrefab;
    [SerializeField]
    private TMP_Text _playerGold;
    [SerializeField]
    private TMP_Text _playerCash;

    private GameObject _MySelf;

    public Item[] InventoryItems;

    public List<GameObject> Slots;
    public List<GameObject> LockSlots;

    void Start()
    {
        _MySelf = gameObject;
        Init(16,36);
        _playerGold.text = GameController.MaxCash.ToString("##,###");
    }

    void Init(int open, int max)
    {
        for (int i = 0; i < max; i++)
        {
            if (i < open)
            {
                var slot = Instantiate(_openedSlotsPefab, transform);
                slot.name = i+"_"+_openedSlotsPefab.name;
                Slots.Add(slot);
                
            }
            else
            {
                var slot = Instantiate(_closedSlotsPrefab, transform);
                slot.name = "_" + _closedSlotsPrefab;
                slot.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => Unlock(slot));
                LockSlots.Add(slot);
            }
        }
    }

    private void Unlock(GameObject x)
    {
        if (GameObject.Find("Jogador").GetComponent<PlayerBase>().CashAmmount < 0)
        {
            Debug.LogError("Not Enought Cash");
        }
        LockSlots.Remove(x);
        Destroy(x);
        var slot = Instantiate(_openedSlotsPefab, _MySelf.transform);
        slot.name = name[0] + "_" + _openedSlotsPefab;
        slot.transform.SetSiblingIndex(Slots.Count);
        Slots.Add(slot);
        
    }
}
