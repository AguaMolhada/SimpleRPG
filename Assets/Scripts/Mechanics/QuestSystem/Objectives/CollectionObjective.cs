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
    public class CollectionObjective : QuestObjective
    {
        /// <summary>
        /// Access to the objective title.
        /// </summary>
        public new string Title
        {
            get
            {
                base.Title = Verb + " " + CollectionAmount + " " + ItemToCollect.name;
                return base.Title;
            }
        }

        /// <summary>
        /// Verb action to do.
        /// </summary>
        public string Verb;
        /// <summary>
        /// Access to the objective description.
        /// </summary>
        public new string Description;
        /// <summary>
        /// Is this objective complete?
        /// </summary>
        public new bool IsComplete;
        /// <summary>
        /// Access if this is bonus.
        /// </summary>
        public bool IsBonus;
        /// <summary>
        /// Access to the total amount of things that we need.
        /// </summary>
        [SerializeField]
        public int CollectionAmount;
        /// <summary>
        /// Access to the current amount of things that we need.
        /// </summary>
        public int CurrentAmount { get; }
        /// <summary>
        /// Access to the thing that we need to collect.
        /// </summary>
        [SerializeField]
        public GameObject ItemToCollect;

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
            IsComplete = false;
        }

        public new void UpdateProgress()
        {
            throw new System.NotImplementedException();
        }

        public new void CheckProgress()
        {
            IsComplete = CurrentAmount >= CollectionAmount;
        }

        public override string ToString()
        {
            return CurrentAmount + "/" + CollectionAmount + " " + ItemToCollect.name + " " + Verb + "ed.";
        }
    }
}
