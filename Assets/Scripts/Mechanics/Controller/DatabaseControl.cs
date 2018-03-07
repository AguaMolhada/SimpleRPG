﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ItemDatabase.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Vianna 2018.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using QuestSystem;

public class DatabaseControl : MonoBehaviour
{
    public static DatabaseControl Instance;
    private readonly List<Item> _itemDb = new List<Item>();
    public RarityController RarityColor;
    public readonly List<Quest> QuestDatabaseList = new List<Quest>();

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
        ConstructItemDatabase();
        ConstructQuestDatabase();
        print(QuestDatabaseList[0].Objectives[0].GetType());
    }
    /// <summary>
    /// Find a certain item instance.
    /// </summary>
    /// <param name="id">Desired item ID</param>
    /// <returns>A certain item in the database</returns>
    public Item FetchItem(int id) => _itemDb.FirstOrDefault(t => t.Id == id);
    /// <summary>
    /// Find a certain item instance.
    /// </summary>
    /// <param name="iname">Desired item name</param>
    /// <returns>A certain item in the database</returns>
    public Item FetchItem(String iname) => _itemDb.FirstOrDefault(t => t.Title == iname);
    /// <summary>
    /// Find all items with the specific param
    /// </summary>
    /// <param name="type">Item type</param>
    /// <returns>List with all items.</returns>
    public List<Item> FetchItems(ItemType type)
    {
        var temp = new List<Item>();
        foreach (var item in _itemDb)
        {
            if (item.ItemT == type && item.BuyValue != 0)
            {
                temp.Add(item);
            }
        }

        return temp;
    }
    /// <summary>
    /// Show the total amount of items avaliable in the database.
    /// </summary>
    /// <returns></returns>
    public int ItemsCount()
    {
        return _itemDb.Count;
    }
    /// <summary>
    /// Construct Item database thought items.json file.
    /// </summary>
    private void ConstructItemDatabase()
    {
        var temp = File.ReadAllText(Application.dataPath + "/StreamingAssets/Items.json");
        Item[] tempDatabase = JsonHelper.FromJsonWrapped<Item>(temp);
        Debug.Log(tempDatabase[0].Title);
        for (int i = 0; i < tempDatabase.Length; i++)
        {
            _itemDb.Add(tempDatabase[i]);
        }
    }
    public Color SelectOneColor(double roll)
    {
        var cumulative = 0.0;
        for (int i = 0; i < RarityColor.Colors.Count; i++)
        {
            cumulative += RarityColor.Colors[i].Value;
            if (roll < cumulative)
            {
                return RarityColor.Colors[i].Key;
            }
        }
        return Color.white;
    }

    private void ConstructQuestDatabase()
    {
        QuestDatabaseList.Add(
            new Quest(
                new QuestIdentifier(QuestDatabaseList.Count, -1, 1),
                new QuestText("Starting the Adventure",
                    "You will need to join the Adventure's Guild to start your jurney to save Ecria",
                    "Search the guild hall"),
                    new List<QuestObjective>
                    {
                        new CollectionObjective("Collect", 1, "Adventure's License", false)
                    }
                )
            );


    }

}
/// <summary>
/// Default Item Class.
/// </summary>
[Serializable]
public class Item
{
    /// <summary>
    /// Unique Item ID. 
    /// </summary>
    [SerializeField] public int Id;
    /// <summary>
    /// Item Name.
    /// </summary>
    [SerializeField] public string Title;
    /// <summary>
    /// Item value to buy on shop.
    /// </summary>
    [SerializeField] public int BuyValue;
    /// <summary>
    /// Item value to sell on shop.
    /// </summary>
    [SerializeField] public int SellValue;
    /// <summary>
    /// Item Type. Ex. Chest, Sword, Helmet...
    /// </summary>
    [SerializeField] public ItemType ItemT;
    /// <summary>
    /// List of bonus atrributes that you will gain when equip the item.
    /// </summary>
    [SerializeField] public BonusAttribute[] BonusAttributes;
    /// <summary>
    /// Can stack multiple itens in the same slot?
    /// </summary>
    [SerializeField] public bool Stackable;
    /// <summary>
    /// Icon Sprite name. All sprites need to be in the "Resources/Sprites/" Path
    /// </summary>
    [SerializeField] public string Sprite;
    /// <summary>
    /// Can you right click and use the item?
    /// </summary>
    [SerializeField] public bool Usable;

    /// <summary>
    /// Generate a dull item.
    /// </summary>
    public Item()
    {
        this.Id = -1;
    }
}
[Serializable]
public class ColorRarity
{
    [SerializeField]public Color Key;
    [SerializeField]public double Value;
    [SerializeField]public string RarityName;

    public ColorRarity(Color k, double v)
    {
        this.Key = k;
        this.Value = v;
    }

    public ColorRarity()
    {

    }

}