// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlayerInventory.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PlayerInventory : MonoBehaviour
{
    /// <summary>
    /// Max slots number.
    /// </summary>
    public const int NumItemSlots = 36;
    /// <summary>
    /// Number of slots unlocked at start. 
    /// </summary>
    public const int UnlockedSlots = 16;
    /// <summary>
    /// Number of slots currenty unlocked.
    /// </summary>
    public int NumIntemSlotsUnlocked { get; private set; }
    /// <summary>
    /// Empyt slots prefab.
    /// </summary>
    [SerializeField] private GameObject _openedSlotsPefab;
    /// <summary>
    /// Locked slots prefab.
    /// </summary>
    [SerializeField] private GameObject _closedSlotsPrefab;
    /// <summary>
    /// Item data holder prefab.
    /// </summary>
    [SerializeField] private GameObject _itemDataPrefab;
    /// <summary>
    /// Gold text.
    /// </summary>
    [SerializeField] private TMP_Text _playerGold;
    /// <summary>
    /// Cash text.
    /// </summary>
    [SerializeField] private TMP_Text _playerCash;
    /// <summary>
    /// Nickname text.
    /// </summary>
    [SerializeField] private TMP_Text _playerNickname;
    /// <summary>
    /// Player selected class.
    /// </summary>
    [SerializeField] private TMP_Text _playerClass;
    /// <summary>
    /// Reference to the player.
    /// </summary>

    public List<Slot> Slots = new List<Slot>();

    void Start()
    {
        NumIntemSlotsUnlocked = UnlockedSlots;
        Init(NumIntemSlotsUnlocked, NumItemSlots);
        AddItem(ItemDatabase.Instance.FetchItemById(2));
        AddItem(ItemDatabase.Instance.FetchItemById(4));
        AddItem(ItemDatabase.Instance.FetchItemById(3));
        AddItem(ItemDatabase.Instance.FetchItemById(1));
        AddItem(ItemDatabase.Instance.FetchItemById(9));
        AddItem(ItemDatabase.Instance.FetchItemById(9));
        AddItem(ItemDatabase.Instance.FetchItemById(9));
        AddItem(ItemDatabase.Instance.FetchItemById(9));
        AddItem(ItemDatabase.Instance.FetchItemById(9));
        AddItem(ItemDatabase.Instance.FetchItemById(9));
        AddItem(ItemDatabase.Instance.FetchItemById(9));
        AddItem(ItemDatabase.Instance.FetchItemById(9));
        AddItem(ItemDatabase.Instance.FetchItemById(9));
        UpdateUiElemets();
    }
    /// <summary>
    /// Update the ui elements.
    /// </summary>
    private void UpdateUiElemets()
    {

        _playerCash.text = GameController.Instance.Player.GoldAmmount.ToString("##,###");
        _playerGold.text = GameController.Instance.Player.CashAmmount.ToString("##,###");
        _playerNickname.text = GameController.Instance.Player.NickName;
        _playerClass.text = GameController.Instance.Player.PlayerStats.PlayerClass.ToString();
    }
    /// <summary>
    /// to add item to the inventory
    /// </summary>
    /// <param name="itemToAdd">item data to add</param>
    public void AddItem(Item itemToAdd)
    {
        //Check if item exist in the inventory if exists add 1 to the ammout.
        if (itemToAdd.Stackable)
        {
            for (int i = 0; i < NumIntemSlotsUnlocked; i++)
            {
                if (Slots[i].SlotItem != null && Slots[i].SlotItem.Item == itemToAdd)
                {
                    Slots[i].SlotItem.Ammount += 1;
                    return;
                }
            }
        }
        //If the item to add doesn't exist add in new slot.
        for (var i = 0; i < NumIntemSlotsUnlocked; i++)
        {
            if (Slots[i].SlotItem == null)
            {
                var temp = Instantiate(_itemDataPrefab, Slots[i].transform);
                temp.GetComponent<ItemData>().Init(itemToAdd, Slots[i]);
                Slots[i].SlotItem = temp.GetComponent<ItemData>();
                return;
            }
        }
        Debug.LogWarning("Full Inventory");
    }
    /// <summary>
    /// Remove the item. if ammount > 0 remove only 1 unity.
    /// </summary>
    /// <param name="itemToRemove">Item data to remove</param>
    public void RemoveItem(Item itemToRemove)
    {
        for (var i = 0; i < NumIntemSlotsUnlocked; i++)
        {
            if (Slots[i].SlotItem.Item == itemToRemove)
            {
                Slots[i].SlotItem.Ammount -= 1;
                return;
            }
        }
    }
    /// <summary>
    /// Initialize the inventory.
    /// </summary>
    /// <param name="open">Opened Slots.</param>
    /// <param name="max">Max Slots.</param>
    void Init(int open, int max)
    {
        for (var i = 0; i < max; i++)
        {
            if (i < open)
            {
                var slot = Instantiate(_openedSlotsPefab, transform);
                slot.name = i+"_"+_openedSlotsPefab.name;
                Slots.Add(slot.GetComponent<Slot>());
            }
            else
            {
                var slot = Instantiate(_closedSlotsPrefab, transform);
                slot.name = "_" + _closedSlotsPrefab;
                slot.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => Unlock(slot));
            }
        }
    }
    /// <summary>
    /// Unlock the inventory cell.
    /// </summary>
    /// <param name="x">Cell to unlock</param>
    private void Unlock(GameObject x)
    {
        int slotCost;
        if (NumIntemSlotsUnlocked - UnlockedSlots == 0)
        {
            slotCost = 50 * (NumIntemSlotsUnlocked - UnlockedSlots + 1);
        }
        else
        {
            slotCost = (int)(50f * ((NumIntemSlotsUnlocked - UnlockedSlots + 1) * 0.7f))+1;
        }

        if (GameController.Instance.Player.CashAmmount < slotCost)
        {
            Debug.LogError("Not Enought Cash, You have" + GameController.Instance.Player.CashAmmount +
                           "$ and you need " + slotCost);
            return;
        }
        Destroy(x);
        var slot = Instantiate(_openedSlotsPefab, transform);
        slot.name = NumIntemSlotsUnlocked + "_" + _openedSlotsPefab.name;
        slot.transform.SetSiblingIndex(NumIntemSlotsUnlocked);
        NumIntemSlotsUnlocked += 1;
        Slots.Add(slot.GetComponent<Slot>());
    } 
    /// <summary>
    /// Sorting the inventory orderby alphabetical.
    /// </summary>
    public void SortAlphabetical()
    {
        var temp = Slots.OrderBy(x=> x.SlotItem != null ? x.SlotItem.Item.Title : "zzzzzzzzzzzz").ToList();
        Slots = temp;

        for (int i = 0; i < Slots.Count; i++)
        {
            Slots[i].transform.SetSiblingIndex(i);
            Slots[i].name = i.ToString() + "_" + _openedSlotsPefab.name;
        }

    }
   
}
