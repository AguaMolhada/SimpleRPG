// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IQuestIdentifier.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2018.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace QuestSystem
{
    public interface IQuestIdentifier
    {
        int ID { get; }
        int SourceID { get; }
        int ChainQuestID { get; }

    }
}
