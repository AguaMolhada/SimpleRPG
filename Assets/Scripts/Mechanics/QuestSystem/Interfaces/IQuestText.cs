// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IQuestInformation.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2018.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace QuestSystem
{
    public interface IQuestText
    {
        string Title { get; }
        string DescriptionSummary { get; }
        string Hint { get; }
    }
}
