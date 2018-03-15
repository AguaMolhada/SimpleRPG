// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Quest.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2018.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace QuestSystem
{
    [System.Serializable]
    public class Quest : ICloneable
    {
        /// <summary>
        /// Quest identifier.
        /// </summary>
        public QuestIdentifier Identifier;
        /// <summary>
        /// Quest information text.
        /// </summary>
        public QuestText Text;
        /// <summary>
        /// List with all objectives to do.
        /// </summary>
        public List<QuestObjective> Objectives;

        public List<CollectionObjective> CollectObjectives;

        public List<KillTargetObjective> EliminationObjectives;

        public Reward QuestReward;

        /// <summary>
        /// Quest Constructor.
        /// </summary>
        public Quest(QuestIdentifier id, QuestText info, List<QuestObjective> objectives,Reward reward)
        {
            Identifier = id;
            Text = info;
            Objectives = objectives;
            QuestReward = reward;
        }

        public void ConstructObjectives()
        {
            CollectObjectives = new List<CollectionObjective>();
            EliminationObjectives = new List<KillTargetObjective>();

            foreach (var questObjective in Objectives)
            {
                if (questObjective is CollectionObjective)
                {
                    CollectObjectives.Add((CollectionObjective) questObjective);
                }
                else if (questObjective is KillTargetObjective)
                {
                    EliminationObjectives.Add((KillTargetObjective) questObjective);
                }
            }
        }

        public void GetObjectives()
        {
            var temp = new List<QuestObjective>();
            temp.AddRange(EliminationObjectives);
            temp.AddRange(CollectObjectives);
            Objectives = temp;
        }
        
        public object Clone()
        {
            return (Quest)this.MemberwiseClone();
        }

        public string ToStringFormat()
        {
            string objectives = "";
            foreach (var objective in Objectives)
            {
                objectives += objective.ToStringFormat();
            }
            var temp = Text.Title + ";" + Text.DescriptionSummary + ";" + Text.Hint + ";"+ objectives +";"+QuestReward.ToStringFormat();
            return temp;
        }
    }
}