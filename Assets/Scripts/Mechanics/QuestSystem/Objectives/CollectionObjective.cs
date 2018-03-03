// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CollectionObjective.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2018.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using UnityEngine;

namespace QuestSystem
{
    public class CollectionObjective : IQuestObjective
    {
        /// <summary>
        /// What do you will do.
        /// </summary>
        private string _title;
        /// <summary>
        /// Access to the objective title.
        /// </summary>
        public string Title => _title;
        /// <summary>
        /// Description that what we need to do.
        /// </summary>
        private string _description;
        /// <summary>
        /// Access to the objective description.
        /// </summary>
        public string Description => _description;
        /// <summary>
        /// Is this objective complete?
        /// </summary>
        private bool _isComplete;
        /// <summary>
        /// Access to check if is complete.
        /// </summary>
        public bool IsComplete => _isComplete;
        /// <summary>
        /// Is this a bonus objective?
        /// </summary>
        private bool _isBonus;
        /// <summary>
        /// Access if this is bonus.
        /// </summary>
        public bool IsBonus => _isBonus;
        /// <summary>
        /// Total amount of what we need.
        /// </summary>
        private int _collectionAmount;
        /// <summary>
        /// Access to the total amount of things that we need.
        /// </summary>
        public int CollectionAmount => _collectionAmount;
        /// <summary>
        /// Current amount of what we need. 
        /// </summary>
        private int _currentAmount;
        /// <summary>
        /// Access to the current amount of things that we need.
        /// </summary>
        public int CurrentAmount => _currentAmount;
        /// <summary>
        /// What we need to collect.
        /// </summary>
        private GameObject _itemToCollect;
        /// <summary>
        /// Access to the thing that we need to collect.
        /// </summary>
        public GameObject ItemToCollect => _itemToCollect;
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
            _title = tileVerb + " " + totalAmount + " " + item.name;
            _description = descrip;
            _collectionAmount = totalAmount;
            _currentAmount = 0;
            _itemToCollect = item;
            _isBonus = bonus;
            _isComplete = false;
        }

        public void UpdateProgress()
        {
            throw new System.NotImplementedException();
        }

        public void CheckProgress()
        {
            throw new System.NotImplementedException();
        }
    }
}
