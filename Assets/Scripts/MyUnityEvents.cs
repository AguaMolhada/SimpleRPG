// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MyUnityEvents.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2018.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using QuestSystem;
using UnityEngine;
using UnityEngine.Events;
[Serializable]
public class UnityEventQuest : UnityEvent<Quest> { }
[Serializable]
public class UnityEventBool : UnityEvent<bool> { }
[Serializable]
public class UnityEventString : UnityEvent<string> { }
