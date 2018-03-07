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
        public string Target;

        /// <summary>
        /// Titak Amount of targets to be eliminated.
        /// </summary>
        public int KillTotalAmount;
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
        public KillTargetObjective(string target, string descript, bool bonus, int kills)
        {
            Title = "Kill " + kills + " " + target;
            Target = target;
            Description = descript;
            IsComplete = false;
            IsBonus = bonus;
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
