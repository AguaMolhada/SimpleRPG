// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QuestIdentifier.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2018.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace QuestSystem
{
    [System.Serializable]
    public class QuestIdentifier
    {

        /// <summary>
        /// Quest Unique Identifier.
        /// </summary>
        public int ID;
        /// <summary>
        /// Indentifier to where the quest belongs.
        /// </summary>
        public int PrQuest;
        /// <summary>
        /// Next quest ID in the chain.
        /// </summary>
        public int ChainQuestID;
        /// <summary>
        /// Is the quest completed.
        /// </summary>
        public bool IsQuestComplete;

        public QuestIdentifier(int id,int prtQ,int nextQ)
        {
            ID = id;
            PrQuest = prtQ;
            ChainQuestID = nextQ;
        }
    }
}
