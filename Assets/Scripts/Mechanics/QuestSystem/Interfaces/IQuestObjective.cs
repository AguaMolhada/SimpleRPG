// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IQuestObjective.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2018.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace QuestSystem
{
    [System.Serializable]
    public class QuestObjective
    {
        public string Title { get; }
        public string Description { get; }
        public bool IsComplete { get; }
        public bool IsBonus { get; }
        public void UpdateProgress() { }
        public void CheckProgress() { }
    }
}
