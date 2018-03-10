// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CharacterSelection.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Vianna 2018.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CharacterSelection : MonoBehaviour
{
    /// <summary>
    /// Selected character sking.
    /// </summary>
    public CharacterSkin SelectCharacterSkin { get; private set; }
    public PlayerStats SelectStats { get; private set; }
    public CharacterSkin[] CharactersSkin;
    public PlayerStats[] CharacterStatsBase;
    public Image LockedImg;
    public TMP_Text SkinName;
    public StatsUiPolygon StatsUi;
    public string NickName;
    public TMP_InputField NickNameInput;

    private int _selectedCharacterIndex = 0;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        SelectStats = CharacterStatsBase[0];
        OnCharacterSelect(_selectedCharacterIndex);
        SelectStats.ResetStats();
        SelectStats.ResetExp();
        StartCoroutine("Rotate");
        StatsUi.UpdateStatsGui();
    }

    private void OnEnable()
    {
        StopCoroutine(Rotate());
        StartCoroutine("Rotate");
        StatsUi.UpdateStatsGui();
    }

    /// <summary>
    /// Cotinuos rotation of the character obj preview.
    /// </summary>
    /// <returns></returns>
    IEnumerator Rotate()
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

    public void UpdateStatsGui()
    {
        var skin = SelectCharacterSkin.CharacterObj.name.Split('_');
        SkinName.text = skin[1];
        StatsUi.UpdateStatsGui();
    }
    /// <summary>
    /// Mehtod to assign player nickname.
    /// </summary>
    /// <param name="n"></param>
    public void AssignNickname(string n)
    {
        NickName = n;
    }
    /// <summary>
    /// Method called to change the current selected character obj.
    /// </summary>
    /// <param name="characterChoice">Character index</param>
    public void OnCharacterSelect(int characterChoice)
    {
        _selectedCharacterIndex += characterChoice;
        SelectCharacterSkin = CharactersSkin[Mathf.Abs(_selectedCharacterIndex%CharactersSkin.Length)];
        ClearAllChildern();
        transform.Rotate(Vector3.up*180);
        var temp = Instantiate(SelectCharacterSkin.CharacterObj, transform.position, Quaternion.identity);
        temp.transform.parent = transform;
        temp.transform.localScale = Vector3.one;
        temp.transform.rotation = transform.rotation;
        if (SelectCharacterSkin.Unlocked)
        {
            SelectCharacterSkin.CharacterObj.transform.GetChild(0).GetComponent<Renderer>().sharedMaterial.color = new Color(0.8f, 0.8f, 0.8f);
            LockedImg.gameObject.SetActive(false);
        }
        else
        {
            SelectCharacterSkin.CharacterObj.transform.GetChild(0).GetComponent<Renderer>().sharedMaterial.color = new Color(0.15f, 0.15f, 0.15f);
            LockedImg.gameObject.SetActive(true);
        }
        UpdateStatsGui();
    }
    /// <summary>
    /// Method called by changing the character class.
    /// </summary>
    /// <param name="statsChoice">Id of dropdown menu</param>
    public void OnStatsSelect(int statsChoice)
    {
        SelectStats = CharacterStatsBase[statsChoice];
        UpdateStatsGui();
        Debug.Log(SelectStats.PlayerClass);
    }

    public PlayerStats ApplyStats()
    {
        var temp = ScriptableObject.CreateInstance<PlayerStats>();
        temp.PlayerClass = SelectStats.PlayerClass;
        temp.PlayerAgi = SelectStats.PlayerAgi;
        temp.PlayerCon = SelectStats.PlayerCon;
        temp.PlayerDex = SelectStats.PlayerDex;
        temp.PlayerInt = SelectStats.PlayerInt;
        temp.PlayerLuk = SelectStats.PlayerLuk;
        temp.PlayerVit = SelectStats.PlayerVit;
        temp.ResetExp();

        return temp;
    }
}
