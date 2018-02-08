// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Slot.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Vianna 2018.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.EventSystems;

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
    private ItemData _slotItem;
    /// <summary>
    /// Item that is in the slot (when ever this is changed will call OnItemChanged() to subscribe use OnItemChanged += Mehtod).
    /// </summary>
    public ItemData SlotItem
    {
        get { return _slotItem; }
        set {
            if (_slotItem == value)
            {
                return;
            }
            _slotItem = value;
            if (OnItemChanged != null)
            {
                OnItemChanged();
            }
        }
    }
    public delegate void OnItemChangedDelegate();
    public event OnItemChangedDelegate OnItemChanged;

    public void OnDrop(PointerEventData eventData)
    {
        if (SlotT == SlotType.Inventory)
        {
            var droppedItem = eventData.pointerDrag.GetComponent<ItemData>();
            if (SlotItem == null)
            {
                AssignItemToEmptySlot(droppedItem);
            }
            else
            {
                AssignItemToSlot(droppedItem);
            }
        }
        else if (SlotT == SlotType.Equipment)
        {
            var droppedItem = eventData.pointerDrag.GetComponent<ItemData>();
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
                    itemData.Amount += SlotItem.Amount;
                    SlotItem = itemData;
                    return;
                }
                else if (SlotItem.Item != itemData.Item)
                {
                    var temp = SlotItem;
                    temp.MySlot = itemData.MySlot;
                    itemData.MySlot = gameObject.GetComponent<Slot>();
                    SlotItem = itemData;
                    temp.UpdateTransform();
                    return;
                }

                break;
            case SlotType.Equipment:
                if (SlotItem.Item != itemData.Item)
                {
                    var temp = SlotItem;
                    temp.MySlot = itemData.MySlot;
                    itemData.MySlot = gameObject.GetComponent<Slot>();
                    SlotItem = itemData;
                    temp.UpdateTransform();
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
        itemData.MySlot.SlotItem = null;
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

