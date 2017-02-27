using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class PlayerInventory : MonoBehaviour
{

    GameObject inventoryPanel;
    GameObject slotPanel;
    [SerializeField]
    ItemDatabase database;

    public GameObject inventorySlot;
    public GameObject inventoryItem;


    int slotAmount;
    public List<Item> inventoryItems = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();

    void Start()
    {
        slotAmount = 21;
        inventoryPanel = GameObject.Find("Inventory Panel");
        slotPanel = inventoryPanel.transform.FindChild("Slot Panel").gameObject;
        for (int i = 0; i < slotAmount; i++)
        {
            inventoryItems.Add(new Item());
            slots.Add(Instantiate(inventorySlot));
            slots[i].transform.SetParent(slotPanel.transform);
            slots[i].GetComponent<Slot>().slotID = i;
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
        Item itemToRemove = database.FetchItemByID(id);
        if (itemToRemove.Stackable && CheckIfItemIsInInventory(itemToRemove))
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].ID == itemToRemove.ID)
                {
                    ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();

                    if(data.ammount -1 == 0)
                    {
                        inventoryItems[i] = new Item();
                    }
                    data.ammount -= 1;
                    data.transform.GetChild(0).GetComponent<Text>().text = data.ammount.ToString();
                }
            }
        }
        else if(CheckIfItemIsInInventory(itemToRemove))
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].ID == itemToRemove.ID)
                {
                    ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
                    inventoryItems[i] = new Item();
                    data.ammount = 0;
                }
            }
        }
    }

    public void AddItem(int id)
    {
        if (id != -1)
        {
            Item itemToAdd = database.FetchItemByID(id);
            if (itemToAdd.Stackable && CheckIfItemIsInInventory(itemToAdd))
            {
                for (int i = 0; i < inventoryItems.Count; i++)
                {
                    if (inventoryItems[i].ID == itemToAdd.ID)
                    {
                        ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();

                        data.ammount += 1;
                        data.transform.GetChild(0).GetComponent<Text>().text = data.ammount.ToString();
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < inventoryItems.Count; i++)
                {
                    if (inventoryItems[i].ID == -1)
                    {
                        inventoryItems[i] = itemToAdd;
                        GameObject itemObj = Instantiate(inventoryItem);
                        itemObj.GetComponent<ItemData>().item = itemToAdd;
                        itemObj.GetComponent<ItemData>().slot = i;
                        if (itemObj.transform.parent == null)
                        {
                            itemObj.transform.SetParent(slots[i].transform);
                            itemObj.transform.localPosition = Vector3.zero;
                        }
                        itemObj.GetComponent<Image>().sprite = itemToAdd.ISprite;
                        itemObj.name = itemToAdd.Title;
                        itemObj.transform.GetChild(0).GetComponent<Text>().text = "";
                        break;
                    }
                }
            }
        }
    }

    bool CheckIfItemIsInInventory(Item item)
    {
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if(inventoryItems[i].ID == item.ID)
            {
                return true;
            }
        }

        return false;
    }

}