using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using System.IO;
using System.Linq;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase Instance;
    private List<Item> _database = new List<Item>();
    private JsonData _itemData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
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
        var temp = File.ReadAllText(Application.dataPath + "/StreamingAssets/Items.json");
        Debug.Log(temp);

        Item[] tempDatabase = JsonHelper.FromJsonWrapped<Item>(temp);
        Debug.Log(tempDatabase[0].Title);
        for (int i = 0; i < tempDatabase.Length; i++)
        {
            _database.Add(tempDatabase[i]);
        }
    }

}

[Serializable]
public class Item
{
    [SerializeField] public int Id;
    [SerializeField] public string Title;
    [SerializeField] public int BuyValue;
    [SerializeField] public int SellValue;
    [SerializeField] public ItemType ItemT;
    [SerializeField] public BonusAttribute[] BonusAttributes;
    [SerializeField] public int Attribute; // atribute a ser modificado pelo item defesa/atk/tanto heal das potion
    [SerializeField] public bool Stackable;
    [SerializeField] public string Sprite;
    [SerializeField] public bool Usable;
    public Item(int id, string title, int bvalue, int svalue, ItemType t, JsonData attr, bool stackable, string sprName, bool usable, BonusAttribute[] bonus)
    {
        this.Id = id;
        this.Title = title;
        this.BuyValue = bvalue;
        this.SellValue = svalue;
        this.ItemT = t;
        this.BonusAttributes = bonus;
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

        this.Sprite = sprName;
    }

    public Item()
    {
        this.Id = -1;
    }
}