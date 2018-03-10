// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ItemDatabase.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2018.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(menuName = "Data / New ItemData Base")]
public class ItemDatabase : ScriptableObject
{
    public List<Item> ItemsDatabase;

    /// <summary>
    /// Construct Item database thought items.json file.
    /// </summary>
    public void ConstructItemDatabase()
    {
        ItemsDatabase = new List<Item>();
        var temp = File.ReadAllText(Application.dataPath + "/StreamingAssets/ItemsTest.json");
        Item[] tempDatabase = JsonHelper.FromJsonWrapped<Item>(temp);
        Debug.Log(tempDatabase[0].Title);
        for (int i = 0; i < tempDatabase.Length; i++)
        {
            ItemsDatabase.Add(tempDatabase[i]);
        }
    }
    /// <summary>
    /// Save the item list to the json file.
    /// </summary>
    public void SaveItemDatabase()
    {
        var tempString = "";
        Item[] tempDatabase = new Item[ItemsDatabase.Count];
        for (int i = 0; i < ItemsDatabase.Count; i++)
        {
            tempDatabase[i] = ItemsDatabase[i];
        }
        tempString = JsonHelper.ToJson(tempDatabase);
        File.WriteAllText(Application.dataPath + "/StreamingAssets/ItemsTest.json", tempString);
    }
}
