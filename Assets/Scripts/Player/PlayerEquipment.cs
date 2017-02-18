using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerEquipment : MonoBehaviour {

    #region Private Vars
    GameObject equipmentPanel;
    PlayerInventory inv;

    [SerializeField]
    ItemDatabase database;
    #endregion

    #region Public Vars
    public GameObject equipmentSlot;
    public GameObject equipmentItem;

    public List<Item> equipmentItems = new List<Item>();
    public List<GameObject> slots;
    #endregion

    void Start()
    {
        inv = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        equipmentPanel = GameObject.Find("Equipment Panel");
        for (int i = 0; i < slots.Count; i++)
        {
            equipmentItems.Add(new Item());
        }
    }

    public void EquipItem(int id)
    {
        Item itemToEquip = database.FetchItemByID(id);

        for (int i = 0; i < slots.Count; i++)
        {
            if(slots[i].GetComponent<Slot>().equipType.ToString() == itemToEquip.typeItem.ToString())
            {
                for (int j = 0; j < equipmentItems.Count; j++)
                {
                    if (equipmentItems[j].ID == -1)
                    {
                        equipmentItems[j] = itemToEquip;
                        GameObject itemObj = Instantiate(equipmentItem);
                        itemObj.GetComponent<ItemData>().item = itemToEquip;
                        itemObj.GetComponent<ItemData>().slot = i;
                        itemObj.transform.SetParent(slots[i].transform);
                        itemObj.transform.localPosition = Vector3.zero;
                        itemObj.GetComponent<Image>().sprite = itemToEquip.ISprite;
                        itemObj.name = itemToEquip.Title;
                        itemObj.transform.GetChild(0).GetComponent<Text>().text = "";
                        Debug.Log(itemObj.transform.position);
                        Debug.Log(itemObj.GetComponent<RectTransform>().transform.position);
                        break;
                    }
                }
            }
        }
    }

    public void DeEquipItem(int id)
    {
        Item itemToDeEquip = database.FetchItemByID(id);
        for (int i = 0; i < equipmentItems.Count; i++)
        {
            if(equipmentItems[i].typeItem == itemToDeEquip.typeItem)
            {
                equipmentItems.RemoveAt(i);
                for (int j = 0; j < slots.Count; j++)
                {
                    if(slots[j].GetComponent<Slot>().equipType.ToString() == itemToDeEquip.typeItem.ToString())
                    {
                        ItemData data = slots[j].transform.GetChild(0).GetComponent<ItemData>();
                        data.ammount = 0;
                        equipmentItems[i] = new Item();
                        break;
                    }
                }
            }
        }
    }
    
    public bool CheckIsAlreadyEquiped(Item item)
    {
        for (int i = 0; i < equipmentItems.Count; i++)
        {
            if(equipmentItems[i].typeItem == item.typeItem)
            {
                return true;
            }
        }
        return false;
    }

    bool CheckIfItemIsInInventory(Item item)
    {
        for (int i = 0; i < equipmentItems.Count; i++)
        {
            if (equipmentItems[i].ID == item.ID)
            {
                return true;
            }
        }
        return false;
    }
}
