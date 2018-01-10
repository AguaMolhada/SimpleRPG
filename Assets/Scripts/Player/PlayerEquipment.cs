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
    public TMP_Text[] ExtraStats = new TMP_Text[6];

    public int[] ExtraStatsAmmout = new int[6];

    private void Start()
    {
        foreach (var slot in Slots)
        {
            slot.OnItemChanged += UpdateEquipmentGui;
        }
    }

    private void UpdateEquipmentGui()
    {
        ExtraStatsAmmout = new int[6] {0,0,0,0,0,0};
        
        foreach (var slot in Slots)
        {
            if (slot.SlotItem != null)
            {
                foreach (var itemAtribute in slot.SlotItem.Item.BonusAttributes)
                {
                    switch (itemAtribute.AttributeBonus)
                    {
                        case ItemBonusAttribute.Agi:
                            ExtraStatsAmmout[0] += itemAtribute.BonusAmmout;
                            break;
                        case ItemBonusAttribute.Dex:
                            ExtraStatsAmmout[1] += itemAtribute.BonusAmmout;
                            break;
                        case ItemBonusAttribute.Int:
                            ExtraStatsAmmout[2] += itemAtribute.BonusAmmout;
                            break;
                        case ItemBonusAttribute.Luk:
                            ExtraStatsAmmout[3] += itemAtribute.BonusAmmout;
                            break;
                        case ItemBonusAttribute.Vit:
                            ExtraStatsAmmout[4] += itemAtribute.BonusAmmout;
                            break;
                        case ItemBonusAttribute.Con:
                            ExtraStatsAmmout[5] += itemAtribute.BonusAmmout;
                            break;
                    }
                }
            }
        }

        for (int i = 0; i < ExtraStatsAmmout.Length; i++)
        {
            ExtraStats[i].text = "+" + ExtraStatsAmmout[i].ToString();
        }
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBase>().PlayerStats.AddExtraStats(ExtraStatsAmmout);

    }

}
