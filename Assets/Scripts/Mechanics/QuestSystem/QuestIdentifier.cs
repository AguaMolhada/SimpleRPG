// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QuestIdentifier.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2018.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace QuestSystem
{
    [System.Serializable]
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
        private int _previusID;
        /// <summary>
        /// Access to where the quest belongs.
        /// </summary>
        public int PreviusID => _previusID;
        /// <summary>
        /// Next quest ID in the chain.
        /// </summary>
        private int _chainQuestID;
        /// <summary>
        /// Access to the next quest ID in the chain.
        /// </summary>
        public int ChainQuestID => _chainQuestID;

        public QuestIdentifier(int id)
        {
            _ID = id;
        }

        /// <summary>
        /// Set previus Quest ID
        /// </summary>
        /// <param name="id">previus Quest ID</param>
        public void SetSourceID(int id)
        {
            _previusID = id;
        }

        /// <summary>
        /// Set the next quest on the chain.
        /// </summary>
        /// <param name="id">Quest unique ID</param>
        public void SetChainQuestID(int id)
        {
            _chainQuestID = id;
        }
    }
}
