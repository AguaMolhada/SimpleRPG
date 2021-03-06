﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CollectionObjective.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2018.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using UnityEngine;

namespace QuestSystem
{
    [System.Serializable]
    public class CollectionObjective : QuestObjective
    {
        /// <summary>
        /// Verb action to do.
        /// </summary>
        public string Verb;
        /// <summary>
        /// Name of the thing that we will collect.
        /// </summary>
        public string ToCollect;
        /// <summary>
        /// Access to the total amount of things that we need.
        /// </summary>
        [SerializeField]
        public int CollectionAmount;
        /// <summary>
        /// Access to the current amount of things that we need.
        /// </summary>
        public int CurrentAmount { get; private set; }
        /// <summary>
        /// Player Inventory to check if itens will be avaliable.
        /// </summary>
        private PlayerInventory _inventory;
        /// <summary>
        /// Constructor that builds a collection objective.
        /// </summary>
        /// <param name="tileVerb">Action required on the quest. ex.: Collect.</param>
        /// <param name="totalAmount">Total of things that we need.</param>
        /// <param name="tocollect">Thing that we need.</param>
        /// <param name="descrip">What will be collected.</param>
        /// <param name="bonus">is bonus objective?</param>
        public CollectionObjective(string tileVerb,string tocollect, int totalAmount, string descrip, bool bonus)
        {
            Verb = tileVerb;
            ToCollect = tocollect;
            Description = descrip;
            CollectionAmount = totalAmount;
            CurrentAmount = 0;
            IsBonus = bonus;
            IsComplete = false;
            Title = Verb + " " + CollectionAmount + " " + tocollect;
        }

        public void AssignInventry(PlayerInventory inv)
        {
            _inventory = inv;
            QuestEvent.AddListener(UpdateProgress);
            QuestEvent.AddListener(CheckProgress);
        }

        public override void UpdateProgress()
        {
            CurrentAmount = _inventory.CheckAmount(ToCollect);
        }

        public override void CheckProgress()
        {
            IsComplete = CurrentAmount >= CollectionAmount;
        }

        public new string ToStringFormat()
        {
            return base.ToString() + CurrentAmount + "/" + CollectionAmount + " " + ToCollect + " " + Verb + "ed.";
        }
    }
}
