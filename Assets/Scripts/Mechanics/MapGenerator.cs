using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    public Celula[,] Map;

    public MapGenerator(int w, int h, int nE, int nS)
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