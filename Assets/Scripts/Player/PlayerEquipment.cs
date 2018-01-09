// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlayerEquipment.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    public const int EquipmentSlots = 8;
    /// <summary>
    /// List with all slots.
    /// </summary>
    public List<Slot> Slots =  new List<Slot>();
    /// <summary>
    /// Equipment panel with contain all slots.
    /// </summary>
    public GameObject EquipmentPanel;
    /// <summary>
    /// Text do show stats gain via equipment.
    /// </summary>
    public TMP_Text[] ExtraStats = new TMP_Text[EquipmentSlots];

    void UpdateEquipmentGui()
    {
        var ExtraAgi = 0;
        var ExtraDex = 0;
        var ExtraInt = 0;
        var ExtraLuk = 0;
        var ExtraVit = 0;
        var ExtraCon = 0;
    }

}
