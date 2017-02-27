using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SaveLoad : MonoBehaviour
{

    public static string gamesave = "GameSave.bin";
    Player pl;
    PlayerEquipment equipItems;
    PlayerInventory invItems;

    public void Save()
    {
        pl = GameController.getPlayer();
        Enemy enemy = GameController.getEnemy();
        equipItems = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
        invItems = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        using (FileStream fs = new FileStream(gamesave, FileMode.Create, FileAccess.Write))
        {
            binaryFormatter.Serialize(fs, pl.name);
            binaryFormatter.Serialize(fs, pl.hp);
            binaryFormatter.Serialize(fs, pl.pclass);
            binaryFormatter.Serialize(fs, pl.expTotal);
            binaryFormatter.Serialize(fs, pl.attributesLeft);
            binaryFormatter.Serialize(fs, pl.str);
            binaryFormatter.Serialize(fs, pl.dex);
            binaryFormatter.Serialize(fs, pl.con);
            binaryFormatter.Serialize(fs, pl.inte);
            binaryFormatter.Serialize(fs, pl.gold);

            int equip = equipItems.slots.Count;
            int slot = invItems.slots.Count;

            #region For Saving all equiped itens
            int equipCount = 0;
            for (int i = 0; i < slot; i++)
            {
                if (invItems.slots[i].transform.childCount > 0)
                {
                    equipCount++;
                }
            }
            binaryFormatter.Serialize(fs, equipCount);
            if (equip != 0)
            {
                for (int j = 0; j < equip; j++)
                {
                    if (equipItems.slots[j].transform.childCount > 0)
                    {
                        binaryFormatter.Serialize(fs, equipItems.slots[j].transform.GetChild(0).GetComponent<ItemData>().item.ID);
                    }
                }
            }
            #endregion

            #region saving all itens in the inventory
            int invCount = 0;   
            for (int i = 0; i < slot; i++)
            {
                if (invItems.slots[i].transform.childCount > 0)
                {
                    invCount++;
                }
            }
            binaryFormatter.Serialize(fs, invCount);
            if (slot != 0)
            {
                for (int j = 0; j < slot; j++)
                {
                    if (invItems.slots[j].transform.childCount > 0)
                    {
                        binaryFormatter.Serialize(fs, invItems.slots[j].transform.GetChild(0).GetComponent<ItemData>().ammount);
                        for (int i = 0; i < invItems.slots[j].transform.GetChild(0).GetComponent<ItemData>().ammount; i++)
                        {
                            binaryFormatter.Serialize(fs, invItems.slots[j].transform.GetChild(0).GetComponent<ItemData>().item.ID);
                        }                                    
                    }
                }
            }
            #endregion

            if (enemy != null)
            {
                binaryFormatter.Serialize(fs, true);
                binaryFormatter.Serialize(fs, enemy.eName);
                binaryFormatter.Serialize(fs, enemy.hp);
                binaryFormatter.Serialize(fs, enemy.hpMax);
                binaryFormatter.Serialize(fs, enemy.lvl);
                binaryFormatter.Serialize(fs, enemy.dmg[0]);
                binaryFormatter.Serialize(fs, enemy.dmg[1]);
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

        pl = GameController.getPlayer();
        equipItems = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
        invItems = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        using (FileStream fs = new FileStream(gamesave, FileMode.Open, FileAccess.Read))
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

            int equip = (int)binaryFormatter.Deserialize(fs);
            if( equip != 0)
            {
                for (int j = 0; j < equip; j++)
                {
                    equipItems.EquipItem((int)binaryFormatter.Deserialize(fs));
                }
            }
            int invCount = (int)binaryFormatter.Deserialize(fs);
            if (invCount != 0)
            {
                for (int j = 0; j < invCount; j++)
                {
                    int a = (int)binaryFormatter.Deserialize(fs);
                    for (int i = 0; i < a; i++)
                    {
                        invItems.AddItem((int)binaryFormatter.Deserialize(fs));
                    }
                }
            }

            if ((bool)binaryFormatter.Deserialize(fs))
            {
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().LoadingThings();
                Enemy enemy = GameController.getEnemy();
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
