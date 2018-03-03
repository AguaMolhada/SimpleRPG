// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Quest.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2018.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace QuestSystem
{
    public class Quest
    {
        /// <summary>
        /// Quest Identifier.
        /// </summary>
        private QuestIdentifier _identifier;
        /// <summary>
        /// Access to the quest identifier.
        /// </summary>
        public QuestIdentifier Identifier => _identifier;
        /// <summary>
        /// Quest information text.
        /// </summary>
        private QuestText _text;
        /// <summary>
        /// Access to the quest information text.
        /// </summary>
        public QuestText Text => _text;
        /// <summary>
        /// List with all objectives to do.
        /// </summary>
        private List<IQuestObjective> _objectives;
        /// <summary>
        /// Access to the objectives list.
        /// </summary>
        public List<IQuestObjective> Objectives => _objectives;
        /// <summary>
        /// Quest Constructor.
        /// </summary>
        public Quest(QuestIdentifier id, QuestText info, List<IQuestObjective> objectives)
        {
            _identifier = id;
            _text = info;
            _objectives = objectives;
        }
        /// <summary>
        /// Check the overall progress.
        /// </summary>
        /// <returns>Value btween 0 and 100</returns>
        private float CheckOverallProgress()
        {
            var temp = 0;
            for (int i = 0; i < _objectives.Count; i++)
            {
                if (_objectives[i].IsComplete && _objectives[i].IsBonus == false)
                {
                    temp++;
                }
            }
            return Ultility.GetPercentValue(_objectives.Count,temp);
        }

        public override string ToString()
        {
            return "";
        }
    }
}