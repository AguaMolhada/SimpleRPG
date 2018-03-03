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
        public int ID {
            get { return _ID; }
        }
        /// <summary>
        /// Indentifier to where the quest belongs.
        /// </summary>
        private int _sourceID;
        public int SourceID {
            get { return _sourceID; }
        }
        /// <summary>
        /// Next quest ID in the chain.
        /// </summary>
        private int _chainQuestID;
        public int ChainQuestID {
            get { return _chainQuestID; }
        }
    }
}
