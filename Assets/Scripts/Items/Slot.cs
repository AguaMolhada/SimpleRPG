using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class Slot : MonoBehaviour, IDropHandler
{
    /// <summary>
    /// Slot type. Inventory or Equipment.
    /// </summary>
    public SlotType SlotT;
    /// <summary>
    /// If sloty type is Equipment wich one will be used.
    /// </summary>
    public ItemType EquipType;
    /// <summary>
    /// Item that is in the slot.
    /// </summary>
    public ItemData SlotItem;

    public void OnDrop(PointerEventData eventData)
    {
        if (SlotT == SlotType.Inventory)
        {
            var droppedItem = eventData.pointerDrag.GetComponent<ItemData>();
            droppedItem.MySlot.SlotItem = null;
            if (SlotItem == null)
            {
                AssignItemToEmptySlot(droppedItem);
            }
            else
            {
                AssignItemToSlot(droppedItem);
            }

        }
    }
    /// <summary>
    /// Assign itemData to the respective slot.
    /// </summary>
    /// <param name="itemData">itemData data to be assigned.</param>
    private void AssignItemToSlot(ItemData itemData)
    {
        switch (SlotT)
        {
            case SlotType.Inventory:
                if (SlotItem.Item == itemData.Item && SlotItem.Item.Stackable)
                {
                    itemData.MySlot = gameObject.GetComponent<Slot>();
                    itemData.Ammount += SlotItem.Ammount;
                    SlotItem = itemData;
                    return;
                }
                else if (SlotItem.Item != itemData.Item)
                {
                    var temp = SlotItem;
                    temp.MySlot = itemData.MySlot;
                    itemData.MySlot = gameObject.GetComponent<Slot>();
                    return;
                }

                break;
            case SlotType.Equipment:
                if (SlotItem.Item != itemData.Item)
                {
                    var temp = SlotItem;
                    temp.MySlot = itemData.MySlot;
                    itemData.MySlot = gameObject.GetComponent<Slot>();
                    return;
                }
                break;
        }
    }

    /// <summary>
    /// Assign itemData to the respective slot.
    /// </summary>
    /// <param name="itemData">itemData data to be assigned.</param>
    private void AssignItemToEmptySlot(ItemData itemData)
    {
        switch (SlotT)
        {
            case SlotType.Inventory:
                SlotItem = itemData;
                itemData.MySlot = gameObject.GetComponent<Slot>();
                break;
            case SlotType.Equipment:
                if (itemData.Item.ItemT == EquipType)
                {
                    SlotItem = itemData;
                    itemData.MySlot = gameObject.GetComponent<Slot>();
                    return;
                }
                break;
        }
    }

}

