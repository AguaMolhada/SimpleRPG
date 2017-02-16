using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class Slot : MonoBehaviour , IDropHandler {

    public enum SlotType
    {
        inventory,
        equipment,
    }

    public enum EquipmentType
    {
        none,
        helmet,
        armor,
        boots,
        weapon,
        shield,
        necklace,
        ring,
    }

    public EquipmentType equipType;
    public SlotType slotType;
    public int slotID;

    private PlayerInventory inv;
    private PlayerEquipment equip;

    void Start()
    {
        inv = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        equip = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (slotType == SlotType.inventory)
        {
            ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData>();
            Debug.Log(inv.inventoryItems[slotID].ID);
            if (inv.inventoryItems[slotID].ID == -1)
            {
                inv.inventoryItems[droppedItem.slot] = new Item();
                inv.inventoryItems[slotID] = droppedItem.item;
                droppedItem.slot = slotID;
            }
            else
            {
                Transform item = this.transform.GetChild(0);
                item.GetComponent<ItemData>().slot = droppedItem.slot;
                item.transform.SetParent(inv.slots[droppedItem.slot].transform);
                item.transform.position = inv.slots[droppedItem.slot].transform.position;

                droppedItem.slot = slotID;
                droppedItem.transform.SetParent(this.transform);
                droppedItem.transform.position = this.transform.position;

                inv.inventoryItems[droppedItem.slot] = item.GetComponent<ItemData>().item;
                inv.inventoryItems[slotID] = droppedItem.item;
            }
        }
    }
}
