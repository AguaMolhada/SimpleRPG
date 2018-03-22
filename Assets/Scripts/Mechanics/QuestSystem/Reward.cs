// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Reward.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2018.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.CodeDom.Compiler;
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
            var itemToAdd = DatabaseControl.Instance.FetchItem(id);
            if (itemToAdd != null)
            {
                ItemsRewards.Add(itemToAdd);
            }
        }
    }

    public Reward(int exp, int gold, List<string> itemBtName)
    {
        ExperienceReward = exp;
        GoldReward = gold;
        foreach (var name in itemBtName)
        {
            var itemToAdd = DatabaseControl.Instance.FetchItem(name);
            if (itemToAdd != null)
            {
                ItemsRewards.Add(itemToAdd);
            }
        }
    }

    public Reward()
    {
        ExperienceReward = 0;
        GoldReward = 0;
        ItemsRewards = new List<Item>();
    }

    public string ToStringFormat()
    {
        var temp = " ";
        if (ExperienceReward != 0)
        {
            temp += ExperienceReward + " exp ";
        }

        if (GoldReward != 0)
        {
            temp += GoldReward + " gold ";
        }

        if (ItemsRewards.Count != 0)
        {
            foreach (var itemsReward in ItemsRewards)
            {
                temp += itemsReward.Title + " ";
            }
        }

        return "Reward:" + temp;
    }
}
