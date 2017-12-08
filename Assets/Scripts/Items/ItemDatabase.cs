using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using System.IO;
using System.Linq;

public class ItemDatabase : MonoBehaviour
{

    private readonly List<Item> _database = new List<Item>();
    private JsonData _itemData;

    void Start()
    {
        _itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Items.json"));
        Debug.Log(Application.dataPath + "/StreamingAssets/Items.json");
        ConstructItemDatabase();
    }

    public Item FetchItemById(int id)
    {
        return _database.FirstOrDefault(t => t.Id == id);
    }

    public int ItemsCount()
    {
        return _database.Count;
    }

    private void ConstructItemDatabase()
    {
        for (var i = 0; i < _itemData.Count; i++)
        {
            _database.Add(new Item((int)_itemData[i]["id"], _itemData[i]["title"].ToString(), (int)_itemData[i]["buyvalue"], (int)_itemData[i]["sellvalue"],
                (Item.ItemType)Enum.Parse(typeof(Item.ItemType), _itemData[i]["typeitem"].ToString()), //Convertendo a string para entrar dentro do Enum da classe item
                _itemData[i]["attribute"], bool.Parse(_itemData[i]["stackable"].ToString()), _itemData[i]["sprname"].ToString(), bool.Parse(_itemData[i]["usable"].ToString())
                ));
        }

    }

}

[System.Serializable]
public class Item
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int BuyValue { get; set; }
    public int SellValue { get; set; }
    public enum ItemType { Test = 0,Weapon = 1,Shield = 2,Armor = 3,Helmet = 4,Consumable = 5}
    public ItemType TypeItemType { get; set; }
    public int Attribute { get; set; } // atribute a ser modificado pelo item defesa/atk/tanto heal das potion
    public bool Stackable { get; set; }
    public Sprite Sprite { get; set; }
    public bool Usable { get; set; }

    public Item(int id, string title, int bvalue,int svalue, ItemType t,JsonData attr,bool stackable,string sprName, bool usable)
    {
        this.Id = id;
        this.Title = title;
        this.BuyValue = bvalue;
        this.SellValue = svalue;
        this.TypeItemType = t;
        this.Stackable = stackable;
        this.Usable = usable;
        if (t != ItemType.Test)
        {
            this.Attribute = (int)attr;
        }
        else
        {
            this.Attribute = 0;
        }
        this.Sprite = Resources.Load<Sprite>("Sprites/"+ sprName);
    }

    public Item()
    {
        this.Id = -1;
    }

}