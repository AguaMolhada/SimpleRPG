// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IQuestObjective.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2018.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

namespace QuestSystem
{
    [System.Serializable]
    public class QuestObjective
    {
        public string Title;
        public string Description;
        public bool IsComplete;
        public bool IsBonus;

        public UnityEvent QuestEvent;

        public virtual void UpdateProgress() { }

        public virtual void CheckProgress() { }

        public string ToStringFormat()
        {
            return Title + Environment.NewLine;
        }
    }

}


