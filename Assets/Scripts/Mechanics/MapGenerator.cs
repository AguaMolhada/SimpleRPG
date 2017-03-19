using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapGenerator : MonoBehaviour {

    public Celula[,] Map;

    public MapGenerator(int w, int h, int nE, int nS)
    {
        Map = new Celula[w,h];
        GenerateMap(nE,nS);
    }

    public void GenerateMap(int nEnemy, int nShop)
    {
        var e = 0;
        var s = 0;
        var i = 0;

        for (int j = 0; j < Map.GetLength(0); j++)
        {
            for (int k = 0; k < Map.GetLength(1); k++)
            {
                Map[j,k] = new Celula();
            }
        }

        foreach (var c in Map)
        {
            i = Random.Range(0, 4);
            c.TypeC = (Celula.TypeCelula) i;
            if (e > nEnemy)
            {
                c.TypeC = Celula.TypeCelula.Empty;
            }
            if (s > nShop)
            {
                c.TypeC = Celula.TypeCelula.Empty;
            }
            switch (c.TypeC)
            {
                case Celula.TypeCelula.Enemy:
                    c.CelularColor = Color.red;
                    e++;
                    break;
                case Celula.TypeCelula.Shop:
                    c.CelularColor = Color.yellow;
                    s++;
                    break;
                case Celula.TypeCelula.Empty:
                    c.CelularColor = Color.cyan;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

}

[System.Serializable]
public class Celula
{
    public Enemy Enemy;
    public Color CelularColor;

    public enum TypeCelula
    {
        Empty = 0,
        Enemy = 1,
        Shop = 2
    }

    public TypeCelula TypeC;

    public Celula(int i)
    {
        TypeC = (TypeCelula) i;
    }

    public Celula()
    {
        this.CelularColor = Color.white;
        this.TypeC = TypeCelula.Empty;
    }
}