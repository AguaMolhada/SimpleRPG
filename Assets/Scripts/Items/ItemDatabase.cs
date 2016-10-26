using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using System.IO;

public class ItemDatabase : MonoBehaviour
{

    private List<Item> database = new List<Item>();
    private JsonData itemData;

    void Start()
    {
        itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Items.json"));
        Debug.Log(Application.dataPath + "/StreamingAssets/Items.json");
        ConstructItemDatabase();
    }

    public Item FetchItemByID(int id)
    {
        for (int i = 0; i < database.Count; i++)
        {
            if(database[i].ID == id)
            {
                return database[i];
            }
        }
        return null;
    }

    void ConstructItemDatabase()
    {
        for (int i = 0; i < itemData.Count; i++)
        {
            database.Add(new Item((int)itemData[i]["id"], itemData[i]["title"].ToString(), (int)itemData[i]["buyvalue"], (int)itemData[i]["sellvalue"],
                (Item.TItem)Enum.Parse(typeof(Item.TItem), itemData[i]["typeitem"].ToString()), //Convertendo a string para entrar dentro do Enum da classe item
                itemData[i]["attribute"], bool.Parse(itemData[i]["stackable"].ToString()), itemData[i]["sprname"].ToString(), bool.Parse(itemData[i]["usable"].ToString())
                ));
        }

    }

}

[System.Serializable]
public class Item
{
    public int ID { get; set; }
    public string Title { get; set; }
    public int BuyValue { get; set; }
    public int SellValue { get; set; }
    public enum TItem { test,oneweapon,twohanded,shield,armor,helmet,consumable }
    public TItem typeItem { get; set; }
    public int Attribute { get; set; } // atribute a ser modificado pelo item defesa/atk/tanto heal das potion
    public bool Stackable { get; set; }
    public Sprite ISprite { get; set; }
    public bool Usable { get; set; }

    public Item(int id, string title, int bvalue,int svalue, TItem t,JsonData attr,bool stackable,string sprName, bool usable)
    {
        this.ID = id;
        this.Title = title;
        this.BuyValue = bvalue;
        this.SellValue = svalue;
        this.typeItem = t;
        this.Stackable = stackable;
        this.Usable = usable;
        if (t != TItem.test)
        {
            this.Attribute = (int)attr;
        }
        else
        {
            this.Attribute = 0;
        }
        this.ISprite = Resources.Load<Sprite>("Sprites/"+ sprName);
    }

    public Item()
    {
        this.ID = -1;
    }

}