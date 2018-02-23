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

    private System.Random _rnd;

	void Start () {
        for (int i = 0; i < MonsterAmount; i++)
	    {
	        SpawnMonster();
	    }
	}

    private void SpawnMonster()
    {
        _rnd = new System.Random();
        var color = DatabaseControl.Instance.SelectOneColor(_rnd.NextDouble());
        if (color == DatabaseControl.Instance.RarityColor.Colors[0].Key)
        {
            
        }
    }


}
