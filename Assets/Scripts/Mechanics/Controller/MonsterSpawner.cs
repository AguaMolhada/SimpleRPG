// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MonsterSpawner.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2018.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class MonsterSpawner : MonoBehaviour
{
    /// <summary>
    /// List with all possible Enemies to spawn in this monster spawner.
    /// </summary>
    public List<EnemyStats> Enemies;
    /// <summary>
    /// Amount of monsters that will be spawned.
    /// </summary>
    public int MonsterAmount;

    public Random rnd = new Random();

    private void Start()
    {
        for (int i = 0; i < MonsterAmount; i++)
        {
            SpawnMonster();
            rnd = new Random(GetInstanceID()+i+(int)System.DateTime.Now.Ticks);
        }
        GameController.Instance.ConstructAggroTable();
        Debug.Log("Done");
    }

    private void SpawnMonster()
    {
        var color = DatabaseControl.Instance.SelectOneColor(rnd.NextDouble());
        var temp = Instantiate(Enemies[rnd.Next(0,Enemies.Count)].MonsterSkin, transform.position, Quaternion.identity);
        var nametemp = temp.name;

        if (color == DatabaseControl.Instance.RarityColor.Colors[0].Key)
        {
            temp.GetComponentInChildren<MeshRenderer>().material.color = color;
            temp.name = DatabaseControl.Instance.RarityColor.UniqueNames[rnd.Next(0, DatabaseControl.Instance.RarityColor.UniqueNames.Count - 1)];
        }
        else if( color == DatabaseControl.Instance.RarityColor.Colors[1].Key)
        {
            temp.GetComponentInChildren<MeshRenderer>().material.color = color;
            temp.name = DatabaseControl.Instance.RarityColor.RarePrefixs[rnd.Next(0, DatabaseControl.Instance.RarityColor.RarePrefixs.Count - 1)] + " " + nametemp;
        }
        else if (color == DatabaseControl.Instance.RarityColor.Colors[2].Key)
        {
            temp.GetComponentInChildren<MeshRenderer>().material.color = color;
            temp.name = DatabaseControl.Instance.RarityColor.UncommunPrefixs[rnd.Next(0, DatabaseControl.Instance.RarityColor.UncommunPrefixs.Count - 1)] + " " + nametemp;
        }

        temp.AddComponent<EnemyAI>();
        temp.transform.tag = "Enemy";
    }
}
