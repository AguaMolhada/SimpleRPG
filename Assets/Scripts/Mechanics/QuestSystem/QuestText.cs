// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QuestInformation.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2018.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace QuestSystem
{
    [System.Serializable]
    public class QuestText
    {
        public string Title;
        public string DescriptionSummary;
        public string Hint;

        public QuestText(string title, string descript, string hint)
        {
            Title = title;
            DescriptionSummary = descript;
            Hint = hint;
        }

    }
}
