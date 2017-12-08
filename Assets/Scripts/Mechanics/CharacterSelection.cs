// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CharacterSelection.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    public Character SelectCharacter { get; private set; }
    public PlayerStats SelectStats { get; private set; }
    public Character[] Characters;
    public PlayerStats[] CharacterStatsBase;


    private int _selectedCharacterIndex = 0;
    void Start()
    {
        SelectStats = CharacterStatsBase[0];
        DontDestroyOnLoad(gameObject);
        OnCharacterSelect(_selectedCharacterIndex);
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

    public void OnCharacterSelect(int characterChoice)
    {
        _selectedCharacterIndex += characterChoice;
        SelectCharacter = Characters[_selectedCharacterIndex];
        var childs = new List<GameObject>();
        foreach (Transform child in transform)
        {
            childs.Add(child.gameObject);
        }
        childs.ForEach(child => Destroy(child));
        transform.Rotate(Vector3.up*180);
        var temp = Instantiate(SelectCharacter.CharacterObj, transform.position, Quaternion.identity);
        temp.transform.parent = transform;
        temp.transform.localScale = Vector3.one;
        temp.transform.rotation = transform.rotation;
    }

    public void OnStatsSelect(int statsChoice)
    {
        SelectStats = CharacterStatsBase[statsChoice];
    }
}
