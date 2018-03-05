// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CollectionObjective.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2018.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using UnityEngine;

namespace QuestSystem
{
    [System.Serializable]
    public class CollectionObjective : IQuestObjective
    {
        /// <summary>
        /// Access to the objective title.
        /// </summary>
        public string Title {
            get { return Verb + " " + CollectionAmount + " " + ItemToCollect.name; }
        }

        /// <summary>
        /// Verb action to do.
        /// </summary>
        public string Verb;

        /// <summary>
        /// Access to the objective description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Is this objective complete?
        /// </summary>
        private bool _isComplete;
        /// <summary>
        /// Access to check if is complete.
        /// </summary>
        public bool IsComplete => _isComplete;

        /// <summary>
        /// Access if this is bonus.
        /// </summary>
        public bool IsBonus { get; set; }

        /// <summary>
        /// Access to the total amount of things that we need.
        /// </summary>
        public int CollectionAmount { get; set; }

        /// <summary>
        /// Access to the current amount of things that we need.
        /// </summary>
        public int CurrentAmount { get; }

        /// <summary>
        /// Access to the thing that we need to collect.
        /// </summary>
        public GameObject ItemToCollect { get; set; }

        /// <summary>
        /// Constructor that builds a collection objective.
        /// </summary>
        /// <param name="tileVerb">Action required on the quest. ex.: Collect.</param>
        /// <param name="totalAmount">Total of things that we need.</param>
        /// <param name="item">Thing that we need.</param>
        /// <param name="descrip">What will be collected.</param>
        /// <param name="bonus">is bonus objective?</param>
        public CollectionObjective(string tileVerb, int totalAmount, GameObject item, string descrip, bool bonus)
        {
            Verb = tileVerb;
            Description = descrip;
            CollectionAmount = totalAmount;
            CurrentAmount = 0;
            ItemToCollect = item;
            IsBonus = bonus;
            _isComplete = false;
        }

        public void UpdateProgress()
        {
            throw new System.NotImplementedException();
        }

        public void CheckProgress()
        {
            _isComplete = CurrentAmount >= CollectionAmount;
        }

        public override string ToString()
        {
            return CurrentAmount + "/" + CollectionAmount + " " + ItemToCollect.name + " " + Verb + "ed.";
        }
    }
}
