// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AggroDataStruct.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 20118.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class AggroStructure
{
    /// <summary>
    /// Enemy assigned to the AggroNode.
    /// </summary>
    public EnemyAI Enemy;
    /// <summary>
    /// List with all players aggros.
    /// </summary>
    public List<AggroNode> PlayerAggro;

    public AggroStructure(EnemyAI e)
    {
        Enemy = e;
        PlayerAggro = new List<AggroNode>();
    }

    public void AddPlayerAggro(AggroNode node)
    {
        PlayerAggro.Add(node);
    }
    /// <summary>
    /// Add certain aggro to the player.
    /// </summary>
    /// <param name="player">Player to recieve aggro</param>
    /// <param name="amount">Amount of agro to add.</param>
    public void AddAggro( GameObject player, float amount)
    {
        var tempPlayerAggro = PlayerAggro.Find(a => a.AggroPlayerGameObject == player);
        tempPlayerAggro.AggroAmount += amount;
        if (tempPlayerAggro.AggroAmount <= 0)
        {
            tempPlayerAggro.AggroAmount = 0;
        }
    }

    /// <summary>
    /// Set the target based on aggro.
    /// </summary>
    /// <returns>Player with more Aggro</returns>
    public GameObject SetTargetBasedOnAggro()
    {
        var tempAggro = 0;
        GameObject tempGo = null;
        for (var index = 0; index < PlayerAggro.Count; index++)
        {
            if (PlayerAggro[index].AggroAmount > tempAggro)
            {
                tempGo = PlayerAggro[index].AggroPlayerGameObject;
            }
        }

        return tempGo;
    }
}

[System.Serializable]
public class AggroNode
{
    /// <summary>
    /// Player game object.
    /// </summary>
    public GameObject AggroPlayerGameObject;
    /// <summary>
    /// Aggro Amount.
    /// </summary>
    public float AggroAmount;

    public AggroNode(GameObject p, float a)
    {
        AggroPlayerGameObject = p;
        AggroAmount = a;
    }
}
