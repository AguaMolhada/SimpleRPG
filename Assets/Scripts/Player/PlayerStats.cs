using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu (menuName = "New PlayerStats Base")]
public class PlayerStats : ScriptableObject
{
    /// <summary>
    /// Player Character Class
    /// </summary>
    public PlayerClass PlayerClass = PlayerClass.Warrior;
    public int PlayerInt;
    public int PlayerCon;
    public int PlayerDex;
    public int PlayerLuck;
    public int PlayerVit;
}
public enum PlayerClass
{
    Warrior,
    Mage,
    Archer,
    Thief,
}