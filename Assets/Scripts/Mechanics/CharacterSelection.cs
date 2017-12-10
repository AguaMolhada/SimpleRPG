// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CharacterSelection.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class CharacterSelection : MonoBehaviour
{
    public Character SelectCharacter { get; private set; }
    public PlayerStats SelectStats { get; private set; }
    public Character[] Characters;
    public PlayerStats[] CharacterStatsBase;
    public PlayerStats AppliedPlayerStats;
    public TMP_Text SkinName;
    public TMP_Text[] StatsValues;
    public UIPolygon statsUIBorder;
    public UIPolygon StatsUI;
    public string NickName;
    public TMP_InputField NickNameInput;

    private int _selectedCharacterIndex = 0;
    void Start()
    {
        SelectStats = CharacterStatsBase[0];
        DontDestroyOnLoad(gameObject);
        OnCharacterSelect(_selectedCharacterIndex);
        AppliedPlayerStats.ResetStats();
        StartCoroutine("rotate");
    }

    IEnumerator rotate()
    {
        var rmp = 1f;
        while (true)
        {
            transform.Rotate(0,rmp,0);
            yield return null;
        }
    }

    public void ClearAllChildern()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void UpdateStatsGUI()
    {
        var skin = SelectCharacter.CharacterObj.name.Split('_');
        SkinName.text = skin[1];
        StatsUI.VerticesDistances[0] = GetPercentValue(AppliedPlayerStats.PlayerAgi);
        StatsUI.VerticesDistances[1] = GetPercentValue(AppliedPlayerStats.PlayerDex);
        StatsUI.VerticesDistances[2] = GetPercentValue(AppliedPlayerStats.PlayerInt);
        StatsUI.VerticesDistances[3] = GetPercentValue(AppliedPlayerStats.PlayerLuk);
        StatsUI.VerticesDistances[4] = GetPercentValue(AppliedPlayerStats.PlayerVit);
        StatsUI.VerticesDistances[5] = GetPercentValue(AppliedPlayerStats.PlayerCon);
        StatsValues[0].text = AppliedPlayerStats.PlayerAgi.ToString();
        StatsValues[1].text = AppliedPlayerStats.PlayerDex.ToString();
        StatsValues[2].text = AppliedPlayerStats.PlayerInt.ToString();
        StatsValues[3].text = AppliedPlayerStats.PlayerLuk.ToString();
        StatsValues[4].text = AppliedPlayerStats.PlayerVit.ToString();
        StatsValues[5].text = AppliedPlayerStats.PlayerCon.ToString();

        RedrawPolygonUI();
    }

    void OnEnable()
    {
        RedrawPolygonUI();
    }

    public void RedrawPolygonUI()
    {
        statsUIBorder.Redraw();
        StatsUI.Redraw();
    }

    public void AssignNickname(string n)
    {
        NickName = n;
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
            x = x* -1;
        }
        switch (splitstring[0])
        {
            case "int":
                AppliedPlayerStats.AddStatsInt(x);
                break;
            case "luk":
                AppliedPlayerStats.AddStatsLuk(x);
                break;
            case "vit":
                AppliedPlayerStats.AddStatsVit(x);
                break;
            case "con":
                AppliedPlayerStats.AddStatsCon(x);
                break;
            case "agi":
                AppliedPlayerStats.AddStatsAgi(x);
                break;
            case "dex":
                AppliedPlayerStats.AddStatsDex(x);
                break;
            default:
                break;
        }
        UpdateStatsGUI();
    }

    public void OnCharacterSelect(int characterChoice)
    {
        _selectedCharacterIndex += characterChoice;
        SelectCharacter = Characters[Mathf.Abs(_selectedCharacterIndex%4)];
        ClearAllChildern();
        transform.Rotate(Vector3.up*180);
        var temp = Instantiate(SelectCharacter.CharacterObj, transform.position, Quaternion.identity);
        temp.transform.parent = transform;
        temp.transform.localScale = Vector3.one;
        temp.transform.rotation = transform.rotation;
        UpdateStatsGUI();
    }

    public void OnStatsSelect(int statsChoice)
    {
        SelectStats = CharacterStatsBase[statsChoice];
        ApplyStats();
        UpdateStatsGUI();
    }

    public void ApplyStats()
    {
        AppliedPlayerStats.PlayerClass = SelectStats.PlayerClass;
        AppliedPlayerStats.PlayerAgi = SelectStats.PlayerAgi;
        AppliedPlayerStats.PlayerCon = SelectStats.PlayerCon;
        AppliedPlayerStats.PlayerDex = SelectStats.PlayerDex;
        AppliedPlayerStats.PlayerInt = SelectStats.PlayerInt;
        AppliedPlayerStats.PlayerLuk = SelectStats.PlayerLuk;
        AppliedPlayerStats.PlayerVit = SelectStats.PlayerVit;
    }

    private float GetPercentValue(int x)
    {
        var y = (float)x/(12);
        y = Mathf.Clamp(y, 0.1f, 1f);
        return y;
    }
}
