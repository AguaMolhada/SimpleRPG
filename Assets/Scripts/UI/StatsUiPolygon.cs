// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StatsUiPolygon.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class StatsUiPolygon : MonoBehaviour
{
    public TMP_Text[] StatsValues;
    public UIPolygon StatsUiBorder;
    public UIPolygon StatsUi;
    public Button[] Buttons;

    private void LateUpdate()
    {
        RedrawPolygonUi();
    }

    public void RedrawPolygonUi()
    {
        foreach (var button in Buttons)
        {
            button.gameObject.SetActive(GameController.Instance.Player.PlayerStats.AvaliablePointsToDistribute > 0);
        }

        StatsUiBorder.Redraw();
        StatsUi.Redraw();
    }

    /// <summary>
    /// Chacnge determined playerstats.
    /// </summary>
    /// <param name="t">First Letter of the stats to change;Ammout (Ex.: i;1)</param>
    public void ChangeStats(string t)
    {
        var splitstring = t.Split(';');
        var x = int.Parse(splitstring[2]);
        if (splitstring[1] == "-")
        {
            x = x * -1;
        }
        switch (splitstring[0])
        {
            case "int":
                GameController.Instance.Player.PlayerStats.AddStatsInt(x);
                break;
            case "luk":
                GameController.Instance.Player.PlayerStats.AddStatsLuk(x);
                break;
            case "vit":
                GameController.Instance.Player.PlayerStats.AddStatsVit(x);
                break;
            case "con":
                GameController.Instance.Player.PlayerStats.AddStatsCon(x);
                break;
            case "agi":
                GameController.Instance.Player.PlayerStats.AddStatsAgi(x);
                break;
            case "dex":
                GameController.Instance.Player.PlayerStats.AddStatsDex(x);
                break;
            default:
                break;
        }
    }

    public void UpdateStatsGui()
    {
        StatsUi.VerticesDistances[0] = GetPercentValue(GameController.Instance.Player.PlayerStats.PlayerAgi);
        StatsUi.VerticesDistances[1] = GetPercentValue(GameController.Instance.Player.PlayerStats.PlayerDex);
        StatsUi.VerticesDistances[2] = GetPercentValue(GameController.Instance.Player.PlayerStats.PlayerInt);
        StatsUi.VerticesDistances[3] = GetPercentValue(GameController.Instance.Player.PlayerStats.PlayerLuk);
        StatsUi.VerticesDistances[4] = GetPercentValue(GameController.Instance.Player.PlayerStats.PlayerVit);
        StatsUi.VerticesDistances[5] = GetPercentValue(GameController.Instance.Player.PlayerStats.PlayerCon);
        StatsValues[0].text = GameController.Instance.Player.PlayerStats.PlayerAgi.ToString();
        StatsValues[1].text = GameController.Instance.Player.PlayerStats.PlayerDex.ToString();
        StatsValues[2].text = GameController.Instance.Player.PlayerStats.PlayerInt.ToString();
        StatsValues[3].text = GameController.Instance.Player.PlayerStats.PlayerLuk.ToString();
        StatsValues[4].text = GameController.Instance.Player.PlayerStats.PlayerVit.ToString();
        StatsValues[5].text = GameController.Instance.Player.PlayerStats.PlayerCon.ToString();
        RedrawPolygonUi();
    }

    private static float GetPercentValue(int x)
    {
        var tempPlayer = GameController.Instance.Player.PlayerStats;
        var points = new int[] {tempPlayer.PlayerAgi,tempPlayer.PlayerDex ,tempPlayer.PlayerInt , tempPlayer.PlayerLuk ,
                          tempPlayer.PlayerVit , tempPlayer.PlayerCon};
        var maxPoints = points.Max();
        if(maxPoints <=11){
            maxPoints = 12;
        }
        maxPoints += 5;
        var y = (float) x / maxPoints;
        y = Mathf.Clamp(y, 0.1f, 1f);
        return y;
    }
}
