// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QuestIdentifier.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2018.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace QuestSystem
{
    public class QuestIdentifier : IQuestIdentifier
    {
        /// <summary>
        /// Quest Unique Identifier.
        /// </summary>
        private int _ID;
        /// <summary>
        /// Acces to quest Unique Identifier.
        /// </summary>
        public int ID => _ID;
        /// <summary>
        /// Indentifier to where the quest belongs.
        /// </summary>
        private int _sourceID;
        /// <summary>
        /// Access to where the quest belongs.
        /// </summary>
        public int SourceID => _sourceID;
        /// <summary>
        /// Next quest ID in the chain.
        /// </summary>
        private int _chainQuestID;
        /// <summary>
        /// Access to the next quest ID in the chain.
        /// </summary>
        public int ChainQuestID => _chainQuestID;
    }
}
