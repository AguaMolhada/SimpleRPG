using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Items;


public class PlayerInventory : MonoBehaviour
{

    GameObject _inventoryPanel;
    GameObject _slotPanel;
    [SerializeField]
    ItemDatabase _database;

    public GameObject InventorySlot;
    public GameObject InventoryItem;


    int _slotAmount;
    public List<Item> InventoryItems = new List<Item>();
    public List<GameObject> Slots = new List<GameObject>();

    void Start()
    {
        _slotAmount = 21;
        _inventoryPanel = GameObject.Find("Inventory Panel");
        _slotPanel = _inventoryPanel.transform.FindChild("Slot Panel").gameObject;
        for (var i = 0; i < _slotAmount; i++)
        {
            InventoryItems.Add(new Item());
            Slots.Add(Instantiate(InventorySlot));
            Slots[i].transform.SetParent(_slotPanel.transform);
            Slots[i].GetComponent<Slot>().SlotId = i;
        }

/*        AddItem(3);
        AddItem(4);
        AddItem(7); AddItem(7); AddItem(7); AddItem(7); AddItem(7); AddItem(7);
        AddItem(8); AddItem(8); AddItem(8); AddItem(8); AddItem(8); AddItem(8); AddItem(8); AddItem(8); AddItem(8); AddItem(8); AddItem(8); AddItem(8); AddItem(8); AddItem(8);
        AddItem(9); AddItem(9); AddItem(9); AddItem(9); AddItem(9); AddItem(9); AddItem(9); AddItem(9); AddItem(9); AddItem(9); AddItem(9); AddItem(9);
        AddItem(10);
*/    }

    public void RemoveItem(int id)
    {
        var itemToRemove = _database.FetchItemById(id);
        if (itemToRemove.Stackable && CheckIfItemIsInInventory(itemToRemove))
        {
            for (var i = 0; i < InventoryItems.Count; i++)
            {
                if (InventoryItems[i].Id == itemToRemove.Id)
                {
                    var data = Slots[i].transform.GetChild(0).GetComponent<ItemData>();

                    if(data.Ammount -1 == 0)
                    {
                        InventoryItems[i] = new Item();
                    }
                    data.Ammount -= 1;
                    data.transform.GetChild(0).GetComponent<Text>().text = data.Ammount.ToString();
                }
            }
        }
        else if(CheckIfItemIsInInventory(itemToRemove))
        {
            for (var i = 0; i < InventoryItems.Count; i++)
            {
                if (InventoryItems[i].Id == itemToRemove.Id)
                {
                    var data = Slots[i].transform.GetChild(0).GetComponent<ItemData>();
                    InventoryItems[i] = new Item();
                    data.Ammount = 0;
                }
            }
        }
    }

    public void AddItem(int id)
    {
        if (id != -1)
        {
            var itemToAdd = _database.FetchItemById(id);
            if (itemToAdd.Stackable && CheckIfItemIsInInventory(itemToAdd))
            {
                for (var i = 0; i < InventoryItems.Count; i++)
                {
                    if (InventoryItems[i].Id == itemToAdd.Id)
                    {
                        var data = Slots[i].transform.GetChild(0).GetComponent<ItemData>();

                        data.Ammount += 1;
                        data.transform.GetChild(0).GetComponent<Text>().text = data.Ammount.ToString();
                        break;
                    }
                }
            }
            else
            {
                for (var i = 0; i < InventoryItems.Count; i++)
                {
                    if (InventoryItems[i].Id == -1)
                    {
                        InventoryItems[i] = itemToAdd;
                        var itemObj = Instantiate(InventoryItem);
                        itemObj.GetComponent<ItemData>().Item = itemToAdd;
                        itemObj.GetComponent<ItemData>().Slot = i;
                        if (itemObj.transform.parent == null)
                        {
                            itemObj.transform.SetParent(Slots[i].transform);
                            itemObj.transform.localPosition = Vector3.zero;
                        }
                        itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite;
                        itemObj.name = itemToAdd.Title;
                        itemObj.transform.GetChild(0).GetComponent<Text>().text = "";
                        break;
                    }
                }
            }
        }
    }

    private bool CheckIfItemIsInInventory(Item item)
    {
        return InventoryItems.Any(t => t.Id == item.Id);
    }

}