// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IQuestObjective.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2018.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Reflection;
using UnityEngine;

namespace QuestSystem
{
    [System.Serializable]
    public class QuestObjective
    {
        public string Title;
        public string Description;
        public bool IsComplete;
        public bool IsBonus;

        public void UpdateProgress() { }
        public void CheckProgress() { }

    }
}
