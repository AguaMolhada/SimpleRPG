// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnemyRarityAndPrefix.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

public static class EnemyRarityAndPrefix{

    public static List<string> UncommunPrefixs = new List<string>()
    {
        "The Blind",

    };

    public static List<string> RarePrefixs = new List<string>()
    {
        "The Haunting",
        "The Deadly",
        "The Primitive",
        "The Fiery Horror",
        "The Quick",
        "The Evasive",
        "The Horrible",
        "The Supreme",
        "The Forsaken",
        "The Matriarch",
        "The Evil",
        "The Reckless",
        "The Dark"
    };

    public static List<string> UniqueNames = new List<string>()
    {
        "Argathor, The Mighty Platypus",
        ""
    };

    public static List<KeyValuePair<Color, double>> ColorDataBase = new List<KeyValuePair<Color, double>>(6)
    {
        new KeyValuePair<Color, double>(Color.black, 0.02),
        new KeyValuePair<Color, double>(Color.yellow, 0.03),
        new KeyValuePair<Color, double>(Color.red, 0.08),
        new KeyValuePair<Color, double>(Color.blue, 0.12),
        new KeyValuePair<Color, double>(Color.green, 0.20),
        new KeyValuePair<Color, double>(Color.white,0.55)
    };

    public static Color SelectOneColor()
    {
        var r = new System.Random();
        var pick = r.NextDouble();

        var cumulative = 0.0;

        for (int i = 0; i < ColorDataBase.Count; i++)
        {
            cumulative += ColorDataBase[i].Value;
            if (pick < cumulative)
            {
                return ColorDataBase[i].Key;
            }
        }
        return Color.white;
    }



}
