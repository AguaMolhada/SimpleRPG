// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Reward.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2018.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Reward
{
    public int ExperienceReward;
    public int GoldReward;
    public List<Item> ItemsRewards;

    public Reward(int exp, int gold, List<int> itemById)
    {
        ExperienceReward = exp;
        GoldReward = gold;
        foreach (var id in itemById)
        {
            ItemsRewards.Add(DatabaseControl.Instance.FetchItem(id));
        }
    }

    public Reward(int exp, int gold, List<string> itemBtName)
    {
        ExperienceReward = exp;
        GoldReward = gold;
        foreach (var name in itemBtName)
        {
            ItemsRewards.Add(DatabaseControl.Instance.FetchItem(name));
        }
    }

    public Reward()
    {
        ExperienceReward = 0;
        GoldReward = 0;
        ItemsRewards = new List<Item>();
    }
}
