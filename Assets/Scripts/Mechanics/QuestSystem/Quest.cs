// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Quest.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2018.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace QuestSystem
{
    [System.Serializable]
    public class Quest
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

        [SerializeField]
        private UnityEventPlayer _onQuestComplete;

        private float _progressComplete;
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

        private void UpdateAndCheckProgress(PlayerController player)
        {
            float temp = 0;
            foreach (var questObjective in Objectives)
            {
                if (questObjective.IsComplete)
                {
                    temp++;
                }
            }
            _progressComplete = Ultility.GetPercentValue(Objectives.Count, temp);
            if (_progressComplete == 100)
            {
                if (_onQuestComplete != null)
                {
                    _onQuestComplete.Invoke(player);
                }        
            }
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
        

        public override string ToString()
        {
            return "";
        }
    }
}