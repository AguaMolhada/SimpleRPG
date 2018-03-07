// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KillTargetObjective.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2018.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace QuestSystem
{
    [System.Serializable]
    public class KillTargetObjective : QuestObjective
    {
        /// <summary>
        /// What do you will do.
        /// </summary>
        public new string Title { get; }
        /// <summary>
        /// Description that what we need to do.
        /// </summary>
        public new string Description { get; set; }
        /// <summary>
        /// Is this objective complete?
        /// </summary>
        public new bool IsComplete { get; }
        /// <summary>
        /// Is this a bonus objective?
        /// </summary>
        public new bool IsBonus { get; set; }
        /// <summary>
        /// Enemy To Kill.
        /// </summary>
        public EnemyStats TargetToKill { get; }
        /// <summary>
        /// Titak Amount of targets to be eliminated.
        /// </summary>
        public int KillTotalAmount { get; }
        /// <summary>
        /// Current amount of targets eliminated.
        /// </summary>
        public int KillCurrentAmount { get; protected set; }

        /// <summary>
        /// Constructor that builds a Kill target objective.        
        /// </summary>
        /// <param name="descript">Description of the monster.</param>
        /// <param name="bonus">Is a bonus quest.</param>
        /// <param name="target">Target to kill.</param>
        /// <param name="kills">Total Amount of targets needed to kill.</param>
        public KillTargetObjective(string descript, bool bonus, EnemyStats target, int kills)
        {
            Title = "Kill " + kills + " " + target.name;
            Description = descript;
            IsComplete = false;
            IsBonus = bonus;
            TargetToKill = target;
            KillTotalAmount = kills;
            KillCurrentAmount = 0;
        }
        
        public new void UpdateProgress()
        {
            throw new System.NotImplementedException();
        }

        public new void CheckProgress()
        {
            throw new System.NotImplementedException();
        }
    }
}
