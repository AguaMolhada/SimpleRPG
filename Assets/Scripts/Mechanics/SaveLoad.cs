using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Assets.Scripts.Items;

public class SaveLoad : MonoBehaviour
{

    public static string gamesave = "GameSave.bin";
    Player pl;
    PlayerEquipment equipItems;
    PlayerInventory invItems;

    public void Save()
    {
        pl = GameController.GetPlayer();
        var enemy = GameController.GetEnemy();
        equipItems = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
        invItems = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();

        var binaryFormatter = new BinaryFormatter();
        using (var fs = new FileStream(gamesave, FileMode.Create, FileAccess.Write))
        {
            binaryFormatter.Serialize(fs, pl.name);
            binaryFormatter.Serialize(fs, pl.Hp);
            binaryFormatter.Serialize(fs, pl.Pclass);
            binaryFormatter.Serialize(fs, pl.ExpTotal);
            binaryFormatter.Serialize(fs, pl.AttributesLeft);
            binaryFormatter.Serialize(fs, pl.Str);
            binaryFormatter.Serialize(fs, pl.Dex);
            binaryFormatter.Serialize(fs, pl.Con);
            binaryFormatter.Serialize(fs, pl.Inte);
            binaryFormatter.Serialize(fs, pl.Gold);

            var equip = equipItems.Slots.Count;
            var slot = invItems.Slots.Count;

            #region For Saving all equiped itens
            var equipCount = 0;
            for (var i = 0; i < slot; i++)
            {
                if (invItems.Slots[i].transform.childCount > 0)
                {
                    equipCount++;
                }
            }
            binaryFormatter.Serialize(fs, equipCount);
            if (equip != 0)
            {
                for (var j = 0; j < equip; j++)
                {
                    if (equipItems.Slots[j].transform.childCount > 0)
                    {
                        binaryFormatter.Serialize(fs, equipItems.Slots[j].transform.GetChild(0).GetComponent<ItemData>().Item.Id);
                    }
                }
            }
            #endregion

            #region saving all itens in the inventory
            var invCount = 0;   
            for (var i = 0; i < slot; i++)
            {
                if (invItems.Slots[i].transform.childCount > 0)
                {
                    invCount++;
                }
            }
            binaryFormatter.Serialize(fs, invCount);
            if (slot != 0)
            {
                for (var j = 0; j < slot; j++)
                {
                    if (invItems.Slots[j].transform.childCount > 0)
                    {
                        binaryFormatter.Serialize(fs, invItems.Slots[j].transform.GetChild(0).GetComponent<ItemData>().Ammount);
                        for (var i = 0; i < invItems.Slots[j].transform.GetChild(0).GetComponent<ItemData>().Ammount; i++)
                        {
                            binaryFormatter.Serialize(fs, invItems.Slots[j].transform.GetChild(0).GetComponent<ItemData>().Item.Id);
                        }                                    
                    }
                }
            }
            #endregion

            if (enemy != null)
            {
                binaryFormatter.Serialize(fs, true);
                binaryFormatter.Serialize(fs, enemy.EName);
                binaryFormatter.Serialize(fs, enemy.Hp);
                binaryFormatter.Serialize(fs, enemy.HpMax);
                binaryFormatter.Serialize(fs, enemy.Lvl);
                binaryFormatter.Serialize(fs, enemy.Dmg[0]);
                binaryFormatter.Serialize(fs, enemy.Dmg[1]);
            }
            else
            {
                binaryFormatter.Serialize(fs, false);
            }
            fs.Close();
        }
    }

    public void Load()
    {
        if (!File.Exists(gamesave))
        {
            return;
        }

        pl = GameController.GetPlayer();
        equipItems = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
        invItems = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();

        var binaryFormatter = new BinaryFormatter();
        using (var fs = new FileStream(gamesave, FileMode.Open, FileAccess.Read))
        {
            pl.name = (string)binaryFormatter.Deserialize(fs);
            pl.SetStats((int)binaryFormatter.Deserialize(fs), //hp
         (Player.PlayerClass)binaryFormatter.Deserialize(fs), //CLass
                        (int)binaryFormatter.Deserialize(fs), //exp
                        (int)binaryFormatter.Deserialize(fs), //attrLeft
                        (int)binaryFormatter.Deserialize(fs), //str
                        (int)binaryFormatter.Deserialize(fs), //dex
                        (int)binaryFormatter.Deserialize(fs), //con
                        (int)binaryFormatter.Deserialize(fs), //int
                        (int)binaryFormatter.Deserialize(fs));//Gold

            var equip = (int)binaryFormatter.Deserialize(fs);
            if( equip != 0)
            {
                for (var j = 0; j < equip; j++)
                {
                    equipItems.EquipItem((int)binaryFormatter.Deserialize(fs));
                }
            }
            var invCount = (int)binaryFormatter.Deserialize(fs);
            if (invCount != 0)
            {
                for (var j = 0; j < invCount; j++)
                {
                    var a = (int)binaryFormatter.Deserialize(fs);
                    for (var i = 0; i < a; i++)
                    {
                        invItems.AddItem((int)binaryFormatter.Deserialize(fs));
                    }
                }
            }

            if ((bool)binaryFormatter.Deserialize(fs))
            {
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().LoadingThings();
                var enemy = GameController.GetEnemy();
                enemy.SetStats((string)binaryFormatter.Deserialize(fs),
                                (int)binaryFormatter.Deserialize(fs),
                                (int)binaryFormatter.Deserialize(fs),
                                (int)binaryFormatter.Deserialize(fs),
                                (int)binaryFormatter.Deserialize(fs),
                                (int)binaryFormatter.Deserialize(fs));
            }
            fs.Close();
        }
        
    }

}
