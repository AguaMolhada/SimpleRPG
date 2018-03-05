// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KillTargetObjective.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2018.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace QuestSystem
{
    [System.Serializable]
    public class KillTargetObjective : IQuestObjective
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
        /// Enemy To Kill.
        /// </summary>
        private EnemyStats _targetToKill;
        /// <summary>
        /// Access to the target to be killed.
        /// </summary>
        public EnemyStats TargetToKill => _targetToKill;
        /// <summary>
        /// Titak Amount of targets to be eliminated.
        /// </summary>
        private int _killTotalAmount;
        /// <summary>
        /// Access to the total Amount of targets needed.
        /// </summary>
        public int KillTotalAmount => _killTotalAmount;
        /// <summary>
        /// Current amount of targets eliminated.
        /// </summary>
        private int _killCurrentAmount;
        /// <summary>
        /// Access to the current amount of targets killed.
        /// </summary>
        public int KillCurrentAmount => _killCurrentAmount;

        /// <summary>
        /// Constructor that builds a Kill target objective.        
        /// </summary>
        /// <param name="descript">Description of the monster.</param>
        /// <param name="bonus">Is a bonus quest.</param>
        /// <param name="target">Target to kill.</param>
        /// <param name="kills">Total Amount of targets needed to kill.</param>
        public KillTargetObjective(string descript, bool bonus, EnemyStats target, int kills)
        {
            _title = "Kill " + kills + " " + target.name;
            _description = descript;
            _isComplete = false;
            _isBonus = bonus;
            _targetToKill = target;
            _killTotalAmount = kills;
            _killCurrentAmount = 0;
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
