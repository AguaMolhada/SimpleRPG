// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StatsPolygon.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class StatsPolygon : MonoBehaviour
{
    public TMP_Text[] StatsValues;
    public UIPolygon StatsUiBorder;
    public UIPolygon StatsUi;

    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("CharacterSelection").GetComponent<CharacterSelection>().Stats == null)
        {
            GameObject.FindGameObjectWithTag("CharacterSelection").GetComponent<CharacterSelection>().Stats = this;
        }
    }

    private void LateUpdate()
    {
        RedrawPolygonUI();
    }

    public void RedrawPolygonUI()
    {
        StatsUiBorder.Redraw();
        StatsUi.Redraw();
    }
}
