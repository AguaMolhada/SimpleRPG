// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Ultility.cs" company="Dauler Palhares">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

public static class Ultility
{
    public static bool Btween(int value, int min, int max, bool inclusive)
    {
        if (inclusive)
        {
            return value >= min && value <= max;
        }
        else
        {
            return value > min && value < max;
        }
    }

    public static Vector3 ClampVector3(Vector3 value, Vector3 min, Vector3 max)
    {
        return new Vector3(Mathf.Clamp(value.x, min.x, max.x), Mathf.Clamp(value.y, min.y, max.y), Mathf.Clamp(value.z, min.z, max.z));
    }

    public static Vector3 DivVector3(Vector3 first, Vector3 second)
    {
        return new Vector3((first.x / second.x), (first.y / second.y), (first.z / second.z));
    }

    public static T[] ShuffleArray<T>(T[] array, string seed)
    {
        var rnd = new System.Random(seed.GetHashCode());

        for (var i = 0; i < array.Length - 1; i++)
        {
            var randomIndex = rnd.Next(i, array.Length);
            var tempItem = array[randomIndex];
            array[randomIndex] = array[i];
            array[i] = tempItem;
        }
        return array;
    }

    public static void Shuffle<T>(T[,] array)
    {
        var rnd = new System.Random();
        var lengthRow = array.GetLength(1);

        for (var i = array.Length - 1; i > 0; i--)
        {
            var i0 = i / lengthRow;
            var i1 = i % lengthRow;

            var j = rnd.Next(i + 1);
            var j0 = j / lengthRow;
            var j1 = j % lengthRow;

            var temp = array[i0, i1];
            array[i0, i1] = array[j0, j1];
            array[j0, j1] = temp;
        }
    }

    public static string GetRandomString(System.Random rnd, int length)
    {
        var x = length;
        var charPool = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890!@#$%&*()[]{}<>,.;:/?";
        var rs = new StringBuilder();

        while (x != 0)
        {
            rs.Append(charPool[(int)(rnd.NextDouble() * charPool.Length)]);
            x--;
        }
        return rs.ToString();
    }

    public static float GetPercentValue(float total, float percent)
    {
        return (percent / total) * 100;
    }
    /// <summary>
    /// Percent value btween 0,1
    /// </summary>
    /// <param name="total">Total</param>
    /// <param name="percent">Ammount to check</param>
    /// <returns>value bteween 0 and 1</returns>
    public static float GetPercent(float total, float percent)
    {
        return (percent / total);
    }

    #region Name Generator

    public static string NameGenerator()
    {
        var pattern = Random.Range(0, 100);
        if (pattern >= 0 && pattern < 40)
        {
            return StartName() + NameEnd();
        }
        else if (pattern >= 40 && pattern < 50)
        {
            return NameVowel() + NameLink() + NameEnd();
        }
        else if (pattern >= 50 && pattern < 60)
        {
            return StartName() + NameVowel() + NameLink() + NameEnd();
        }
        else if (pattern >= 60 && pattern < 70)
        {
            return NameVowel() + NameLink() + NameVowel() + NameLink() + NameEnd();
        }
        else if (pattern >= 70 && pattern < 80)
        {
            return StartName() + NameVowel() + NameLink() + NameVowel() + NameLink() + NameEnd();
        }
        else if (pattern >= 80 && pattern < 90)
        {
            return NameVowel() + NameLink() + NameVowel() + NameLink() + NameVowel() + NameLink() + NameEnd();
        }
        else if (pattern >= 90 && pattern < 100)
        {
            return StartName() + NameVowel() + NameLink() + NameVowel() + NameLink() + NameVowel() + NameLink() +
                   NameEnd();
        }
        return null;
    }

    private static string StartName()
    {
        var startName = new[,]
        {
            {"B", "C", "D", "F", "G", "H", "J", "K", "L", "M", "N", "P", "R", "S", "T", "V", "W", "X", "Z"},
            {"B", "C", "Ch", "D", "F", "G", "K", "P", "Ph", "S", "T", "V", "Z", "R", "L", "", "", "", ""},
            {"Ch", "St", "Th", "Ct", "Ph", "Qu", "Squ", "Sh", "", "", "", "", "", "", "", "", "", "", ""}
        };
        var a = Random.Range(0, startName.GetLength(0));
        for (var i = 0; i < 1; i++)
        {
            var b = Random.Range(0, startName.GetLength(1));
            if (startName[a, b] == "")
            {
                i--;
            }
            else
            {
                return startName[a, b];
            }
        }
        return null;
    }

    private static string NameVowel()
    {
        var nameVowel = new[,]
        {
            {"a", "e", "i", "o", "u", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""},
            {
                "ao", "ae", "ai", "au", "ay", "eo", "ea", "ei", "ey", "io", "ia", "iu", "oa", "oe", "oi", "ou", "oy",
                "ui",
                "uo", "uy", "ee", "oo"
            }
        };
        var a = Random.Range(0, nameVowel.GetLength(0));
        for (var i = 0; i < 1; i++)
        {
            var b = Random.Range(0, nameVowel.GetLength(1));
            if (nameVowel[a, b] == "")
            {
                i--;
            }
            else
            {
                return nameVowel[a, b];
            }
        }
        return null;
    }

    private static string NameLink()
    {
        var nameLink = new[,]
        {
            {
                "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "n", "p", "r", "s", "t", "v", "w", "x", "z", "", "",
                "",
                "", "", "", "", "", "", "", "", "", "", "", ""
            },
            {
                "b", "c", "ch", "d", "f", "g", "k", "p", "ph", "r", "s", "t", "v", "z", "r", "l", "n", "", "", "", "",
                "",
                "", "", "", "", "", "", "", "", "", "", "", ""
            },
            {
                "ch", "rt", "rl", "rs", "rp", "rb", "rm", "st", "th", "ct", "ph", "qu", "tt", "bb", "nn", "mm", "gg",
                "cc",
                "dd", "ff", "pp", "rr", "ll", "vv", "ww", "ck", "squ", "lm", "sh", "wm", "wb", "wt", "lb", "rg"
            }
        };
        var a = Random.Range(0, nameLink.GetLength(0));
        for (var i = 0; i < 1; i++)
        {
            var b = Random.Range(0, nameLink.GetLength(1));
            if (nameLink[a, b] == "")
            {
                i--;
            }
            else
            {
                return nameLink[a, b];
            }
        }
        return null;
    }

    private static string NameEnd()
    {
        var nameEnd = new[]
        {
            "id", "ant", "on", "ion", "an", "in", "at", "ate", "us", "oid", "aid", "al", "ark", "ork", "irk", "as",
            "os", "e", "o", "a", "y", "or", "ore", "es", "ot", "at", "ape", "ope", "el", "er", "ex", "ox", "ax", "ie",
            "eep", "ap", "op", "oop", "aut", "ond", "ont", "oth"
        };
        var a = Random.Range(0, nameEnd.Length);
        return nameEnd[a];
    }

    #endregion

    #region City Name Generator

    public static string CityNameGenerator()
    {
        var pattern = Random.Range(0, 100);
        if (pattern >= 0 && pattern < 20)
        {
            return StartName() + CityNameEnd();
        }
        else if (pattern >= 20 && pattern < 40)
        {
            return NameVowel() + NameLink() + CityNameEnd();
        }
        else if (pattern >= 40 && pattern < 60)
        {
            return StartName() + NameVowel() + NameLink() + CityNameEnd();
        }
        else if (pattern >= 60 && pattern < 80)
        {
            return NameVowel() + NameLink() + NameVowel() + NameLink() + CityNameEnd();
        }
        else if (pattern >= 80 && pattern < 100)
        {
            return StartName() + NameVowel() + NameLink() + NameVowel() + NameLink() + CityNameEnd();
        }
        return null;
    }

    private static string CityNameEnd()
    {
        var nameEnd = new[]
            {"ville", "polis", " City", " Village", "town", "port", "boro", "burg", "burgh", "garden", "field", "ness"};
        var a = Random.Range(0, nameEnd.Length);
        return nameEnd[a];
    }

    #endregion
}
/// <summary>
/// Class structure for rooms.
/// </summary>
public class Room
{
    public int XPos;                      // The x coordinate of the lower left tile of the room.
    public int YPos;                      // The y coordinate of the lower left tile of the room.
    public int RoomWidth;                 // How many tiles wide the room is.
    public int RoomHeight;                // How many tiles high the room is.
    public Direction EnteringCorridor;    // The direction of the corridor that is entering this room.


    // This is used for the first room.  It does not have a Corridor parameter since there are no corridors yet.
    public void SetupRoom(IntRange widthRange, IntRange heightRange, int columns, int rows)
    {
        // Set a random width and height.
        RoomWidth = widthRange.Random;
        RoomHeight = heightRange.Random;

        // Set the x and y coordinates so the room is roughly in the middle of the board.
        XPos = Mathf.RoundToInt(columns / 2f - RoomWidth / 2f);
        YPos = Mathf.RoundToInt(rows / 2f - RoomHeight / 2f);
    }


    // This is an overload of the SetupRoom function and has a corridor parameter that represents the corridor entering the room.
    public void SetupRoom(IntRange widthRange, IntRange heightRange, int columns, int rows, Corridor corridor)
    {
        // Set the entering corridor direction.
        EnteringCorridor = corridor.Direction;

        // Set random values for width and height.
        RoomWidth = widthRange.Random;
        RoomHeight = heightRange.Random;

        switch (corridor.Direction)
        {
            // If the corridor entering this room is going north...
            case Direction.North:
                // ... the height of the room mustn't go beyond the board so it must be clamped based
                // on the height of the board (rows) and the end of corridor that leads to the room.
                RoomHeight = Mathf.Clamp(RoomHeight, 1, rows - corridor.EndPositionY);

                // The y coordinate of the room must be at the end of the corridor (since the corridor leads to the bottom of the room).
                YPos = corridor.EndPositionY;

                // The x coordinate can be random but the left-most possibility is no further than the width
                // and the right-most possibility is that the end of the corridor is at the position of the room.
                XPos = Random.Range(corridor.EndPositionX - RoomWidth + 1, corridor.EndPositionX);

                // This must be clamped to ensure that the room doesn't go off the board.
                XPos = Mathf.Clamp(XPos, 0, columns - RoomWidth);
                break;
            case Direction.East:
                RoomWidth = Mathf.Clamp(RoomWidth, 1, columns - corridor.EndPositionX);
                XPos = corridor.EndPositionX;

                YPos = Random.Range(corridor.EndPositionY - RoomHeight + 1, corridor.EndPositionY);
                YPos = Mathf.Clamp(YPos, 0, rows - RoomHeight);
                break;
            case Direction.South:
                RoomHeight = Mathf.Clamp(RoomHeight, 1, corridor.EndPositionY);
                YPos = corridor.EndPositionY - RoomHeight + 1;

                XPos = Random.Range(corridor.EndPositionX - RoomWidth + 1, corridor.EndPositionX);
                XPos = Mathf.Clamp(XPos, 0, columns - RoomWidth);
                break;
            case Direction.West:
                RoomWidth = Mathf.Clamp(RoomWidth, 1, corridor.EndPositionX);
                XPos = corridor.EndPositionX - RoomWidth + 1;

                YPos = Random.Range(corridor.EndPositionY - RoomHeight + 1, corridor.EndPositionY);
                YPos = Mathf.Clamp(YPos, 0, rows - RoomHeight);
                break;
        }
    }
}
/// <summary>
/// Class structure for corridors.
/// </summary>
public class Corridor
{
    public int StartXPos;         // The x coordinate for the start of the corridor.
    public int StartYPos;         // The y coordinate for the start of the corridor.
    public int CorridorLength;    // How many units long the corridor is.
    public Direction Direction;   // Which direction the corridor is heading from it's room.


    // Get the end position of the corridor based on it's start position and which direction it's heading.
    public int EndPositionX {
        get {
            if (Direction == Direction.North || Direction == Direction.South)
                return StartXPos;
            if (Direction == Direction.East)
                return StartXPos + CorridorLength - 1;
            return StartXPos - CorridorLength + 1;
        }
    }


    public int EndPositionY {
        get {
            if (Direction == Direction.East || Direction == Direction.West)
                return StartYPos;
            if (Direction == Direction.North)
                return StartYPos + CorridorLength - 1;
            return StartYPos - CorridorLength + 1;
        }
    }


    public void SetupCorridor(Room room, IntRange length, IntRange roomWidth, IntRange roomHeight, int columns, int rows, bool firstCorridor)
    {
        // Set a random direction (a random index from 0 to 3, cast to Direction).
        Direction = (Direction)Random.Range(0, 4);

        // Find the direction opposite to the one entering the room this corridor is leaving from.
        // Cast the previous corridor's direction to an int between 0 and 3 and add 2 (a number between 2 and 5).
        // Find the remainder when dividing by 4 (if 2 then 2, if 3 then 3, if 4 then 0, if 5 then 1).
        // Cast this number back to a direction.
        // Overall effect is if the direction was South then that is 2, becomes 4, remainder is 0, which is north.
        Direction oppositeDirection = (Direction)(((int)room.EnteringCorridor + 2) % 4);

        // If this is noth the first corridor and the randomly selected direction is opposite to the previous corridor's direction...
        if (!firstCorridor && Direction == oppositeDirection)
        {
            // Rotate the direction 90 degrees clockwise (North becomes East, East becomes South, etc).
            // This is a more broken down version of the opposite direction operation above but instead of adding 2 we're adding 1.
            // This means instead of rotating 180 (the opposite direction) we're rotating 90.
            int directionInt = (int)Direction;
            directionInt++;
            directionInt = directionInt % 4;
            Direction = (Direction)directionInt;

        }

        // Set a random length.
        CorridorLength = length.Random;

        // Create a cap for how long the length can be (this will be changed based on the direction and position).
        int maxLength = length.MMax;

        switch (Direction)
        {
            // If the choosen direction is North (up)...
            case Direction.North:
                // ... the starting position in the x axis can be random but within the width of the room.
                StartXPos = Random.Range(room.XPos, room.XPos + room.RoomWidth - 1);

                // The starting position in the y axis must be the top of the room.
                StartYPos = room.YPos + room.RoomHeight;

                // The maximum length the corridor can be is the height of the board (rows) but from the top of the room (y pos + height).
                maxLength = rows - StartYPos - roomHeight.MMin;
                break;
            case Direction.East:
                StartXPos = room.XPos + room.RoomWidth;
                StartYPos = Random.Range(room.YPos, room.YPos + room.RoomHeight - 1);
                maxLength = columns - StartXPos - roomWidth.MMin;
                break;
            case Direction.South:
                StartXPos = Random.Range(room.XPos, room.XPos + room.RoomWidth);
                StartYPos = room.YPos;
                maxLength = StartYPos - roomHeight.MMin;
                break;
            case Direction.West:
                StartXPos = room.XPos;
                StartYPos = Random.Range(room.YPos, room.YPos + room.RoomHeight);
                maxLength = StartXPos - roomWidth.MMin;
                break;
        }

        // We clamp the length of the corridor to make sure it doesn't go off the board.
        CorridorLength = Mathf.Clamp(CorridorLength, 1, maxLength);
    }
}

[Serializable]
public class IntRange
{
    public int MMin;       // The minimum value in this range.
    public int MMax;       // The maximum value in this range.
    
    // Constructor to set the values.
    public IntRange(int min, int max)
    {
        MMin = min;
        MMax = max;
    }


    // Get a random value from the range.
    public int Random
    {
        get { return UnityEngine.Random.Range(MMin, MMax); }
    }
}
[Serializable]
public class BonusAttribute
{
    public ItemBonusAttribute AttributeBonus;
    public int BonusAmmout;

    public BonusAttribute(ItemBonusAttribute a, int b)
    {
        AttributeBonus = a;
        BonusAmmout = b;
    }
}

/// <summary>
/// Player Classes.
/// </summary>
public enum PlayerClass
{
    Warrior = 0,
    Mage = 1,
    Archer = 2,
    Thief = 3
}
/// <summary>
/// Type of slots.
/// </summary>
public enum SlotType
{
    Inventory = 0,
    Equipment = 1
}
/// <summary>
/// Item Types.
/// </summary>
public enum ItemType
{
    Test = 0,
    Helmet = 1,
    Armor = 2,
    Gloves = 3,
    Legs = 4,
    Boots = 5,
    Weapon = 6,
    Shield = 7,
    Backpack = 8,
    Consumable = 9
}
/// <summary>
/// Bonus attribute types.
/// </summary>
public enum ItemBonusAttribute
{
    MinDmg = 0,
    MaxDmg = 1,
    Agi = 2,
    Dex = 3,
    Int = 4,
    Luk = 5,
    Vit = 6,
    Con = 7
}

/// <summary>
/// Directions that will face.
/// </summary>
public enum Direction
{
    North = 0, East = 1, South = 2, West = 3
}
public enum CellType
{
    Wall = 0,
    Floor = 1
}