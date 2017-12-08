using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using Assets.Scripts.Items;

public class Slot : MonoBehaviour, IDropHandler
{

    public enum SlotType
    {
        Inventory,
        Equipment,
    }

    public enum EquipmentType
    {
        None,
        Helmet,
        Armor,
        Boots,
        Weapon,
        Shield,
        Necklace,
        Ring,
    }

    public EquipmentType EquipType;
    public SlotType SlotT;
    public int SlotId;

//    private PlayerInventory _inv;
//    private PlayerEquipment _equip;

    private void Start()
    {
//        _inv = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
//        _equip = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        /*       if (SlotT == SlotType.Inventory)
               {
                   var droppedItem = eventData.pointerDrag.GetComponent<ItemData>();
                   Debug.Log(_inv.InventoryItems[SlotId].Id);
                   if (_inv.InventoryItems[SlotId].Id == -1)
                   {
                       _inv.InventoryItems[droppedItem.Slot] = new Item();
                       _inv.InventoryItems[SlotId] = droppedItem.Item;
                       droppedItem.Slot = SlotId;
                   }
                   else
                   {
                       var item = transform.GetChild(0);
                       item.GetComponent<ItemData>().Slot = droppedItem.Slot;
                       item.transform.SetParent(_inv.Slots[droppedItem.Slot].transform);
                       item.transform.position = _inv.Slots[droppedItem.Slot].transform.position;

                       droppedItem.Slot = SlotId;
                       droppedItem.transform.SetParent(transform);
                       droppedItem.transform.position = transform.position;

                       _inv.InventoryItems[droppedItem.Slot] = item.GetComponent<ItemData>().Item;
                       _inv.InventoryItems[SlotId] = droppedItem.Item;
                   }
       */
    }
}

