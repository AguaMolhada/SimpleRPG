using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    public static MapGenerator Instance;

    public bool Map3D;
    public float ScaleFactor;
    public GameObject WorldBaseObj;
    public List<GameObject> Player;
    public List<GameObject> Floor;
    public List<GameObject> Shops;
    public List<GameObject> Enemys;


    public Celula[,] Map;

    public void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("2 map controler");
            return;
        }
        Instance = this;
    }

    public void MapGeneratorInit(int w, int h, int nE, int nS)
    {
        Map = new Celula[w,h];
        GenerateMap(nE,nS);
    }

    public void GenerateMap(int nEnemy, int nShop)
    {
        for (var j = 0; j < Map.GetLength(0); j++)
        {
            for (var k = 0; k < Map.GetLength(1); k++)
            {
                Map[j,k] = new Celula();
            }
        }

        Map[Map.GetLength(0)/2,Map.GetLength(1)/2] = new Celula(3) {CelularColor = Color.blue};
        GenerateEnemy(nEnemy);
        GenerateShop(nShop);
        Map = Ultility.ShuffleArray(Map);

        GenerateAssets(Map);
    }

    public void GenerateAssets(Celula[,] map)
    {
        Debug.Log("Assets");
        for (var j = 0; j < map.GetLength(0); j++)
        {
            for (var k = 0; k < map.GetLength(1); k++)
            {
                var pos = new Vector3((map.GetLength(0) / 2) + j * ScaleFactor, WorldBaseObj.transform.position.y, (map.GetLength(1) / 2) + k * ScaleFactor);
                var temp = Instantiate(Floor[UnityEngine.Random.Range(0, Floor.Count - 1)], pos, Quaternion.identity);
                temp.transform.localScale = new Vector3(2, 1, 2);
                temp.transform.parent = WorldBaseObj.transform;

                if (map[j, k].TypeC == Celula.TypeCelula.Enemy)
                {
                    pos.y = 0.1f;
                    temp = Instantiate(Enemys[UnityEngine.Random.Range(0, Floor.Count - 1)], pos, Quaternion.identity);
                    temp.transform.parent = WorldBaseObj.transform;

                }
                if (map[j, k].TypeC == Celula.TypeCelula.Shop)
                {
                    pos.y = 0.1f;
                    temp = Instantiate(Shops[UnityEngine.Random.Range(0, Floor.Count - 1)], pos, Quaternion.identity);
                    temp.transform.parent = WorldBaseObj.transform;
                }
                if (map[j, k].TypeC == Celula.TypeCelula.Player)
                {
                    pos.y = 0.1f;
                    temp = Instantiate(Player[UnityEngine.Random.Range(0, Floor.Count - 1)], pos, Quaternion.identity);
                    temp.name = "PlayerOBJ";
                    temp.transform.parent = WorldBaseObj.transform;
                }
            }
        }
    }

    public void GenerateEnemy(int nEnemy)
    {
        var i = 0;
        foreach (var ce in Map)
        {
            if (ce.TypeC == Celula.TypeCelula.Empty)
            {
                ce.CelularColor = Color.red;
                ce.TypeC = Celula.TypeCelula.Enemy;
                i++;
            }
            if (i == nEnemy)
            {
                return;
            }
        }
    }

    public void GenerateShop(int nShop)
    {
        var i = 0;
        foreach (var ce in Map)
        {
            if (ce.TypeC == Celula.TypeCelula.Empty)
            {
                ce.CelularColor = Color.yellow;
                ce.TypeC = Celula.TypeCelula.Shop;
                i++;
            }
            if (i == nShop)
            {
                return;
            }
        }
    }

}

[Serializable]
public class Celula
{
    public Enemy Enemy;
    public Color CelularColor;

    public enum TypeCelula
    {
        Empty = 0,
        Enemy = 1,
        Shop = 2,
        Player = 3,
        Used = 4
    }

    public TypeCelula TypeC;

    public Celula(int i)
    {
        TypeC = (TypeCelula) i;
    }

    public Celula()
    {
        CelularColor = Color.white;
        TypeC = TypeCelula.Empty;
    }
}