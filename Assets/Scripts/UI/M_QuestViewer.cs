// --------------------------------------------------------------------------------------------------------------------
// <copyright file="M_QuestViewer.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2018.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class M_QuestViewer : MonoBehaviour
{
    [SerializeField] private PlayerController _player;

    [SerializeField] private TMP_Text _questTitle;
    [SerializeField] private TMP_Text _questDescription;
    [SerializeField] private TMP_Text _questHint;
    [SerializeField] private TMP_Text _questSubobjectives;
    [SerializeField] private TMP_Text _questStatus;

    [SerializeField] private UnityEventString OnStringUpdate;


    private void OnEnable()
    {
        OnStringUpdate.AddListener(UpdateGUIElement);
    }

    private void UpdateGUIElement(string newString)
    {
        string[] temp = newString.Split(';');
        _questTitle.text = temp[0];
        _questDescription.text = temp[1];
        _questHint.text = temp[2];
        _questSubobjectives.text = temp[3];
    }
}
