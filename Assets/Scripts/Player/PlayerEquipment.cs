using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Items;

public class PlayerEquipment : MonoBehaviour {

    #region Private Vars
    GameObject _equipmentPanel;
    PlayerInventory _inv;
    Player _player;

    [SerializeField]
    ItemDatabase _database;
    #endregion

    #region Public Vars
    public GameObject EquipmentItem;

    public List<Item> EquipmentItems = new List<Item>();
    public List<GameObject> Slots;
    #endregion

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _inv = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        _equipmentPanel = GameObject.Find("Equipment Panel");
        for (var i = 0; i < Slots.Count; i++)
        {
            EquipmentItems.Add(new Item());
        }
    }

    public void EquipItem(int id)
    {
        var itemToEquip = _database.FetchItemById(id);

        for (var i = 0; i < Slots.Count; i++)
        {
            if(Slots[i].GetComponent<Slot>().EquipType.ToString() == itemToEquip.TypeItem.ToString())
            {
                for (var j = 0; j < EquipmentItems.Count; j++)
                {
                    if (EquipmentItems[j].Id == -1)
                    {
                        EquipmentItems[j] = itemToEquip;
                        var itemObj = Instantiate(EquipmentItem);
                        itemObj.GetComponent<ItemData>().Item = itemToEquip;
                        itemObj.GetComponent<ItemData>().Slot = i;
                        itemObj.transform.SetParent(Slots[i].transform);
                        itemObj.transform.localPosition = Vector3.zero;
                        itemObj.GetComponent<Image>().sprite = itemToEquip.Sprite;
                        itemObj.name = itemToEquip.Title;
                        itemObj.transform.GetChild(0).GetComponent<Text>().text = "";
                        Debug.Log(itemObj.transform.position);
                        Debug.Log(itemObj.GetComponent<RectTransform>().transform.position);
                        _player.SetEquiStats(EquipmentItems[j].Attribute, EquipmentItems[j].TypeItem.ToString());
                        break;
                    }
                }
            }
        }
    }

    public void DeEquipItem(int id)
    {
        var itemToDeEquip = _database.FetchItemById(id);
        for (var i = 0; i < EquipmentItems.Count; i++)
        {
            if(EquipmentItems[i].TypeItem == itemToDeEquip.TypeItem)
            {
                _player.SetEquiStats(-EquipmentItems[i].Attribute, EquipmentItems[i].TypeItem.ToString());
                EquipmentItems.RemoveAt(i);
                foreach (var t in Slots)
                {
                    if(t.GetComponent<Slot>().EquipType.ToString() == itemToDeEquip.TypeItem.ToString())
                    {
                        var data = t.transform.GetChild(0).GetComponent<ItemData>();
                        data.Ammount = 0;
                        EquipmentItems[i] = new Item();
                        break;
                    }
                }
            }
        }
    }
    
    public bool CheckIsAlreadyEquiped(Item item)
    {
        return EquipmentItems.Any(t => t.TypeItem == item.TypeItem);
    }

}
