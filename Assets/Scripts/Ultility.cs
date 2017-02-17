using System.Text;
using UnityEngine;
using System.Collections;

public static class Ultility {

    public static T[] ShuffleArray<T>(T[] array, string seed)
    {
        System.Random rnd = new System.Random(seed.GetHashCode());

        for (int i = 0; i < array.Length-1; i++)
        {
            int randomIndex = rnd.Next(i, array.Length);
            T tempItem = array[randomIndex];
            array[randomIndex] = array[i];
            array[i] = tempItem;
        }
        return array;
    }

    public static string GetRandomString(System.Random rnd, int length)
    {
        int x = length;
        string charPool = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890!@#$%&*()[]{}<>,.;:/?";
        StringBuilder rs = new StringBuilder();

        while (x != 0)
        {
            rs.Append(charPool[(int)(rnd.NextDouble() * charPool.Length)]);
            x--;
        }
        return rs.ToString();
    }

    public static float GetPercent(float total, float percent)
    {
        return (percent / 100) * total;
    }

    public static float PercentValue(float total,float percent)
    {
        return (percent / total) * 100;
    }

    #region Name Generator
    public static string nameGenerator()
    {
        int pattern = Random.Range(0, 100);
        if (pattern >= 0 && pattern < 40)        {  return StartName() + NameEnd();                                                                                         }
        else if (pattern >= 40 && pattern < 50)  {  return NameVowel() + NameLink() + NameEnd();                                                                            }
        else if (pattern >= 50 && pattern < 60)  {  return StartName() + NameVowel() + NameLink() + NameEnd();                                                              }
        else if (pattern >= 60 && pattern < 70)  {  return NameVowel() + NameLink() + NameVowel() + NameLink() + NameEnd();                                                 }
        else if (pattern >= 70 && pattern < 80)  {  return StartName() + NameVowel() + NameLink() + NameVowel() + NameLink() + NameEnd();                                   }
        else if (pattern >= 80 && pattern < 90)  {  return NameVowel() + NameLink() + NameVowel() + NameLink() + NameVowel() + NameLink() + NameEnd();                      }
        else if (pattern >= 90 && pattern < 100) {  return StartName() + NameVowel() + NameLink() + NameVowel() + NameLink() + NameVowel() + NameLink() + NameEnd();        }
        return null;
    }

    static string StartName()
    {
        int a, b;
        string[,] startName = new string[3, 19] {{"B","C","D","F","G","H","J","K","L","M","N","P","R","S","T","V","W","X","Z"},
                                                 {"B","C","Ch","D","F","G","K","P","Ph","S","T","V","Z","R","L","","","",""},
                                                 {"Ch","St","Th","Ct","Ph","Qu","Squ","Sh","","","","","","","","","","",""}};
        a = Random.Range(0, startName.GetLength(0));
        for (int i = 0; i < 1; i++)
        {
            b = Random.Range(0, startName.GetLength(1));
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

    static string NameVowel()
    {
        int a, b;
        string[,] nameVowel = new string[2, 22]{{"a","e","i","o","u","","","","","","","","","","","","","","","","",""},
            {"ao","ae","ai","au","ay","eo","ea","ei","ey","io","ia","iu","oa","oe","oi","ou","oy","ui","uo","uy","ee","oo"}};
        a = Random.Range(0, nameVowel.GetLength(0));
        for (int i = 0; i < 1; i++)
        {
            b = Random.Range(0, nameVowel.GetLength(1));
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

    static string NameLink()
    {
        int a, b;
        string[,] nameLink = new string[3, 34] {{"b","c","d","f","g","h","j","k","l","m","n","p","r","s","t","v","w","x","z","","","","","","","","","","","","","","",""},
                                                {"b","c","ch","d","f","g","k","p","ph","r","s","t","v","z","r","l","n","","","","","","","","","","","","","","","","",""},
            {"ch","rt","rl","rs","rp","rb","rm","st","th","ct","ph","qu","tt","bb","nn","mm","gg","cc","dd","ff","pp","rr","ll","vv","ww","ck","squ","lm","sh","wm","wb","wt","lb","rg"}};
        a = Random.Range(0, nameLink.GetLength(0));
        for (int i = 0; i < 1; i++)
        {
            b = Random.Range(0, nameLink.GetLength(1));
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

    static string NameEnd()
    {
        int a;
        string[] nameEnd = new string[42] { "id", "ant", "on", "ion", "an", "in", "at", "ate", "us", "oid", "aid", "al", "ark", "ork", "irk", "as", "os", "e", "o", "a", "y", "or", "ore", "es", "ot", "at", "ape", "ope", "el", "er", "ex", "ox", "ax", "ie", "eep", "ap", "op", "oop", "aut", "ond", "ont", "oth" };
        a = Random.Range(0, nameEnd.Length);
        return nameEnd[a];
    }
    #endregion

    #region City Name Generator
    public static string CityNameGenerator()
    {
        int pattern = Random.Range(0, 100);
        if (pattern >= 0 && pattern < 20) { return StartName() + CityNameEnd(); }
        else if (pattern >= 20 && pattern < 40) { return NameVowel() + NameLink() + CityNameEnd(); }
        else if (pattern >= 40 && pattern < 60) { return StartName() + NameVowel() + NameLink() + CityNameEnd(); }
        else if (pattern >= 60 && pattern < 80) { return NameVowel() + NameLink() + NameVowel() + NameLink() + CityNameEnd(); }
        else if (pattern >= 80 && pattern < 100) { return StartName() + NameVowel() + NameLink() + NameVowel() + NameLink() + CityNameEnd(); }
        return null;
    }
    static string CityNameEnd()
    {
        int a;
        string[] nameEnd = new string[] { "ville", "polis", " City", " Village", "town", "port", "boro", "burg", "burgh", "carden", "field","ness"};
        a = Random.Range(0, nameEnd.Length);
        return nameEnd[a];
    }
    #endregion
}
