using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SaveLoad : MonoBehaviour
{

    public static string gamesave = "GameSave.bin";


    public void Save()
    {

        Player pl = GameController.getPlayer();
        Enemy enemy = GameController.getEnemy();

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

        Player pl = GameController.getPlayer();


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
                        (int)binaryFormatter.Deserialize(fs));//int
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
