// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Quest.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2018.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

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
        /// <summary>
        /// Quest Constructor.
        /// </summary>
        public Quest(QuestIdentifier id, QuestText info, List<QuestObjective> objectives)
        {
            Identifier = id;
            Text = info;
            Objectives = objectives;
        }
        /// <summary>
        /// Check the overall progress.
        /// </summary>
        /// <returns>Value btween 0 and 100</returns>
        private float CheckOverallProgress()
        {
            var temp = 0;
            for (int i = 0; i < Objectives.Count; i++)
            {
                if (Objectives[i].IsComplete && Objectives[i].IsBonus == false)
                {
                    temp++;
                }
            }
            return Ultility.GetPercentValue(Objectives.Count,temp);
        }

        public override string ToString()
        {
            return "";
        }
    }
}