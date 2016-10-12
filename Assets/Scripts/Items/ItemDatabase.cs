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
        for (int i = 0; i < database.Count; i++)
        {
            Debug.Log(database[i].typeItem);

        }
    }

    void ConstructItemDatabase()
    {
        for (int i = 0; i < itemData.Count; i++)
        {
            database.Add(new Item((int)itemData[i]["id"], itemData[i]["title"].ToString(), (int)itemData[i]["buyvalue"], (int)itemData[i]["sellvalue"],
                (Item.TItem)Enum.Parse(typeof(Item.TItem),itemData[i]["typeitem"].ToString()), //Convertendo a string para entrar dentro do Enum da classe item
                itemData[i]["attribute"]
                ));
        }

    }

}

public class Item
{
    public int ID { get; set; }
    public string Title { get; set; }
    public int BuyValue { get; set; }
    public int SellValue { get; set; }
    public enum TItem { test,oneweapon,twohanded,shield,armor,helmet,consumable }
    public TItem typeItem { get; set; }
    public int attribute; // atribute a ser modificado pelo item defesa/atk

    public Item(int id, string title, int bvalue,int svalue, TItem t,JsonData attr)
    {
        this.ID = id;
        this.Title = title;
        this.BuyValue = bvalue;
        this.SellValue = svalue;
        this.typeItem = t;
        if (t != TItem.consumable && t != TItem.test)
        {
            this.attribute = (int)attr;
        }
        else
        {
            this.attribute = 0;
        }
    }


}