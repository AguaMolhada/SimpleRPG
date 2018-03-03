// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IQuestObjective.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2018.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace QuestSystem
{
    public interface IQuestObjective
    {
        string Title { get; }
        string Description { get; }
        bool IsComplete { get; }
        bool IsBonus { get; }
        void UpdateProgress();
        void CheckProgress();
    }
}
