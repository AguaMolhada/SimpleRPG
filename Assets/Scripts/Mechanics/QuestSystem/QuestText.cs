// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QuestInformation.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2018.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace QuestSystem
{
    [System.Serializable]
    public class QuestText : IQuestText
    {
        private string _title;
        private string _descriptionSummary;
        private string _hint;

        public string Title
        {
            get { return _title; }
        }

        public string DescriptionSummary
        {
            get { return _descriptionSummary; }
        }
        public string Hint
        {
            get { return _hint; }
        }

        public QuestText(string title, string descript, string hint)
        {
            _title = title;
            _descriptionSummary = descript;
            _hint = hint;
        }

    }
}
