using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class PlayerStats : MonoBehaviour {

    #region Private Vars
    
    #region Level
    protected int AtributesUsed;
    #endregion

    #region Stats
    protected int LuckyLvl;
    #endregion

    #region baseStats
    protected int HealthBase;
    protected double DefenseBase;
    protected float DamageBase;
    #endregion

    #region equipStats
    protected double DefenseEquip;
    protected int DamageEquip;
    #endregion

    protected double Defense;
    protected int MinDamage;
    protected int MaxDamage;

    #endregion

    #region Public Vars
    public string PlayerName;
    public int Hp { get; protected set; }
    public int Maxhp { get; protected set; }
    public int Level { get; protected set; }
    public int ExpTotal { get; protected set; }
    public int Exp { get; protected set; }
    public int Tnl { get; protected set; }

    public int Dex { get; protected set; }
    public int Str { get; protected set; }
    public int Inte { get; protected set; }
    public int Con { get; protected set; }

    public double Def { get { return Defense+(DefenseEquip/100); } }
    public double Crit { get; protected set; }
    public int DmgMin { get { return MinDamage + DamageEquip; } }
    public int DmgMax { get { return MaxDamage + DamageEquip; } }

    public int ChanceRun { get; protected set; }
    public int Gold { get; protected set; }
    public int AttributesLeft { get; protected set; }

    #region TextsGUI
    public Text StrTxt;
    public Text ConTxt;
    public Text DexTxt;
    public Text TnlTxt;
    public Text InteTxt;
    public Text XpTxt;
    public Text LuckTxt;
    public Text AttleftTxt;
    public Text DmgInfoTxt;
    public Text CritInfoTxt;
    public Text DefInfoTxt;
    #endregion

    #endregion

    public enum PlayerClass
    {
        Warrior,
        Mage,
        Archer,
    }

    public PlayerClass Pclass;

    public void Start()
    {
        AttributesLeft = Level * 5;
        CheckAtributes();
    }

    protected void CheckAtributes()
    {
        Debug.Log("Iniciando Analize");
        if (AttributesLeft + AtributesUsed > Level * 5)
        {
            Debug.Log("Tem coisa errada");
        }
        if (Dex + Str + Inte + Con > AtributesUsed)
        {
            Debug.Log("Tem coisa errada");
        }
        Debug.Log("Analize Terminada");
    }

    public void Initialize(int hpbase, float dmgBase, float defBase, int exp)
    {
        DamageBase = dmgBase;
        DefenseBase = defBase / 100;
        HealthBase = hpbase;
        Maxhp = (int)(HealthBase + (5 * Con));
        Hp = Maxhp;
        Level = 1;
        Tnl = 100;
        AddExperience(exp);
    }

    /// <summary>
    /// Used to load the character
    /// </summary>
    /// <param name="h"> for setting health </param>
    /// <param name="clas"> for setting character class </param>
    /// <param name="exp"> for setting level </param>
    /// <param name="attr"> for setting attributes left </param>
    /// <param name="s"> for setting str </param>
    /// <param name="d"> for setting dex </param>
    /// <param name="c"> for setting con </param>
    /// <param name="i"> for setting int </param>
    /// <param name="g"> for setting gold</param>
    public void SetStats(int h,PlayerClass clas,int exp, int attr, int s,int d,int c,int i,int g)
    {
        AddExperience(exp);
        Pclass = clas;
        AttributesLeft = attr;
        AtributesUsed = s + d + c + i;
        Str = s;
        Dex = d;
        Con = c;
        Inte = i;
        Hp = h;
        Gold = g;
        UpdateStats();
    }

    public void SetEquiStats(int ammount, string type)
    {
        if(type == "armor")
        {
            DefenseEquip += ammount;
        }
        if(type == "helmet")
        {
            DefenseEquip += ammount;
        }
        if(type == "weapon")
        {
            DamageEquip += ammount;
        }
    }

    public int Attack()
    {
        return UnityEngine.Random.Range((int)(MinDamage+DamageEquip), (int)(MaxDamage+DamageEquip));
    }

    public void AddExperience(int ammout)
    {
        
        Exp += ammout;
        ExpTotal += ammout;
        if(Exp >= Tnl)
        {
            while (Exp > Tnl)
            {
                LevelUp();
            }
        }
    }

    void LevelUp()
    {
        Level++;
        Exp -= Tnl;
        Tnl = (int)(Tnl * 1.15f);
        Hp = Maxhp;
        AttributesLeft += 5;
        Debug.Log("Level UP");
        CheckAtributes();
    }

    public void AddGold(int ammout)
    {
        Gold += ammout;
    }
    
    public void AddStats(string s)
    {
        if (AttributesLeft > 0)
        {
#warning Need to implement Lucky Stat
            if (s == "dex")
            {
                Dex++;

            }
            if (s == "str")
            {
                Str++;
            }
            if (s == "inte")
            {
                Inte++;

            }
            if (s == "con")
            {
                Con++;

            }
            if (s == "luck")
            {
                LuckyLvl++;
            }
            UpdateStats();
            AttributesLeft--;
            AtributesUsed++;
        }
    }

    void UpdateStats()
    {
        #region class Archer
        if (Pclass == PlayerClass.Archer)
        {
            MinDamage = (int)(DamageBase * (Dex * .75f));
            MaxDamage = (int)(DamageBase * (Dex * 1.35f));
            Crit = (Dex * 1.82f) / 10;
            Defense = DefenseBase + (Str * .15f) / 10;
            ChanceRun = (int)(Inte * 1.35f);
            Maxhp = (int)(HealthBase + (3.25f * Con));

        }
        #endregion
        #region class Warrior
        if (Pclass == PlayerClass.Warrior)
        {
            Crit = (Dex * 1.05f) / 10;
            MinDamage = (int)(DamageBase * (Str * .65f));
            MaxDamage = (int)(DamageBase * (Str * 1.15f));
            Defense = DefenseBase + (Str * .85f) / 10;
            ChanceRun = (int)(Inte * .85f);
            Maxhp = (int)(HealthBase + (7.25f * Con));

        }
        #endregion
        #region class Mage
        if (Pclass == PlayerClass.Mage)
        {
            Crit = (Dex * 1.25f) / 10;
            Defense = DefenseBase + (Str * .45f) / 10;
            MinDamage = (int)(DamageBase * (Inte * .64f));
            MaxDamage = (int)(DamageBase * (Inte * 1.28f));
            Maxhp = (int)(HealthBase + (5 * Con));
        }
        #endregion
    }

}
