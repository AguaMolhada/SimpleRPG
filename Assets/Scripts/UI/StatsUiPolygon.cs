// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StatsUiPolygon.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Vianna 2018.
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

    private static StatsUiPolygon _mySelft;
    private PlayerStats _player;

    private void LateUpdate()
    {
        _mySelft = this;
        if (GameController.Instance == null)
        {
            _player = FindObjectOfType<CharacterSelection>().SelectStats;
            UpdateStatsGui();
        }
        else
        {
            _player = GameController.Instance.Player.PlayerStats;
            UpdateStatsGui();
        }
        RedrawPolygonUi();
    }

    public void RedrawPolygonUi()
    {
        foreach (var button in Buttons)
        {
            button.gameObject.SetActive(_player.AvaliablePointsToDistribute > 0);
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
                _player.AddStatsInt(x);
                break;
            case "luk":
                _player.AddStatsLuk(x);
                break;
            case "vit":
                _player.AddStatsVit(x);
                break;
            case "con":
                _player.AddStatsCon(x);
                break;
            case "agi":
                _player.AddStatsAgi(x);
                break;
            case "dex":
                _player.AddStatsDex(x);
                break;
            default:
                break;
        }
    }

    public void UpdateStatsGui()
    {
        StatsUi.VerticesDistances[0] = GetPercentValue(_player.PlayerAgi);
        StatsUi.VerticesDistances[1] = GetPercentValue(_player.PlayerDex);
        StatsUi.VerticesDistances[2] = GetPercentValue(_player.PlayerInt);
        StatsUi.VerticesDistances[3] = GetPercentValue(_player.PlayerLuk);
        StatsUi.VerticesDistances[4] = GetPercentValue(_player.PlayerVit);
        StatsUi.VerticesDistances[5] = GetPercentValue(_player.PlayerCon);
        StatsValues[0].text = _player.PlayerAgi.ToString();
        StatsValues[1].text = _player.PlayerDex.ToString();
        StatsValues[2].text = _player.PlayerInt.ToString();
        StatsValues[3].text = _player.PlayerLuk.ToString();
        StatsValues[4].text = _player.PlayerVit.ToString();
        StatsValues[5].text = _player.PlayerCon.ToString();
        RedrawPolygonUi();
    }

    private static float GetPercentValue(int x)
    {
        var tempPlayer = _mySelft._player;
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
