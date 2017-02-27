using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class PlayerStats : MonoBehaviour {

    #region Private Vars
    
    #region Life
    protected int _health;
    protected int _Maxhealth;
    #endregion

    #region Level
    protected int _level;
    protected int _atributesLeft;
    protected int _atributesUsed;
    protected int _experience;
    protected int _experienceTotal;
    protected int _toNextLevel;
    #endregion

    #region Stats
    protected int _dexterityLvl;
    protected int _strenghtLvl;
    protected int _inteligenceLvl;
    protected int _constitutionLvl;
    protected int _luckyLvl;
    #endregion

    #region baseStats
    protected int _healthBase;
    protected double _defenseBase;
    protected float _damageBase;
    #endregion

    #region equipStats
    protected double _defenseEquip;
    protected int _damageEquip;
    #endregion

    protected double _defense;
    protected int _minDamage;
    protected int _maxDamage;
    protected int _chanceRun;
    protected double _chanceCrit;
    #endregion

    protected int _gold;


    #region Public Vars
    public string playerName;
    public int hp { get { return _health; } }
    public int maxhp { get { return _Maxhealth; } }
    public int level { get { return _level; } }
    public int expTotal { get { return _experienceTotal; } }
    public int exp { get { return _experience; } }
    public int tnl { get { return _toNextLevel; } }

    public int dex { get { return _dexterityLvl; } }
    public int str { get { return _strenghtLvl; } }
    public int inte { get { return _inteligenceLvl; } }
    public int con { get { return _constitutionLvl; } }

    public double def { get { return _defense+(_defenseEquip/100); } }
    public double crit { get { return _chanceCrit; } }
    public int dmgMin { get { return _minDamage + _damageEquip; } }
    public int dmgMax { get { return _maxDamage + _damageEquip; } }

    public int chanceRun { get { return _chanceRun; } }
    public int gold { get { return _gold; } }
    public int attributesLeft { get { return _atributesLeft; } }

    #region TextsGUI
    public Text strTxt;
    public Text conTxt;
    public Text dexTxt;
    public Text tnlTxt;
    public Text inteTxt;
    public Text xpTxt;
    public Text luckTxt;
    public Text attleftTxt;
    public Text dmgInfoTxt;
    public Text critInfoTxt;
    public Text defInfoTxt;
    #endregion

    #endregion

    public enum PlayerClass
    {
        warrior,
        mage,
        archer,
    }

    public PlayerClass pclass;

    public void Start()
    {
        _atributesLeft = _level * 5;
        checkAtributes();
    }

    protected void checkAtributes()
    {
        Debug.Log("Iniciando Analize");
        if (_atributesLeft + _atributesUsed > _level * 5)
        {
            Debug.Log("Tem coisa errada");
        }
        if (_dexterityLvl + _strenghtLvl + _inteligenceLvl + _constitutionLvl > _atributesUsed)
        {
            Debug.Log("Tem coisa errada");
        }
        Debug.Log("Analize Terminada");
    }

    public void Initialize(int hpbase, float dmgBase, float defBase, int exp)
    {
        _damageBase = dmgBase;
        _defenseBase = defBase / 100;
        _healthBase = hpbase;
        _Maxhealth = (int)(_healthBase + (5 * con));
        _health = _Maxhealth;
        _level = 1;
        _toNextLevel = 100;
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
        pclass = clas;
        _atributesLeft = attr;
        _atributesUsed = s + d + c + i;
        _strenghtLvl = s;
        _dexterityLvl = d;
        _constitutionLvl = c;
        _inteligenceLvl = i;
        _health = h;
        _gold = g;
        UpdateStats();
    }

    public void SetEquiStats(int ammount, string type)
    {
        if(type == "armor")
        {
            _defenseEquip += ammount;
        }
        if(type == "helmet")
        {
            _defenseEquip += ammount;
        }
        if(type == "weapon")
        {
            _damageEquip += ammount;
        }
    }

    public int Attack()
    {
        return UnityEngine.Random.Range((int)(_minDamage+_damageEquip), (int)(_maxDamage+_damageEquip));
    }

    public void AddExperience(int ammout)
    {
        
        _experience += ammout;
        _experienceTotal += ammout;
        if(_experience >= _toNextLevel)
        {
            while (_experience > _toNextLevel)
            {
                LevelUp();
            }
        }
    }

    void LevelUp()
    {
        _level++;
        _experience -= _toNextLevel;
        _toNextLevel = (int)(_toNextLevel * 1.15f);
        _health = _Maxhealth;
        _atributesLeft += 5;
        Debug.Log("Level UP");
        checkAtributes();
    }

    public void AddGold(int ammout)
    {
        _gold += ammout;
    }
    
    public void addStats(string s)
    {
        if (attributesLeft > 0)
        {
#warning Need to implement Lucky Stat
            if (s == "dex")
            {
                _dexterityLvl++;

            }
            if (s == "str")
            {
                _strenghtLvl++;
            }
            if (s == "inte")
            {
                _inteligenceLvl++;

            }
            if (s == "con")
            {
                _constitutionLvl++;

            }
            if (s == "luck")
            {
                _luckyLvl++;
            }
            UpdateStats();
            _atributesLeft--;
            _atributesUsed++;
        }
    }

    void UpdateStats()
    {
        #region class Archer
        if (pclass == PlayerClass.archer)
        {
            _minDamage = (int)(_damageBase * (_dexterityLvl * .75f));
            _maxDamage = (int)(_damageBase * (_dexterityLvl * 1.35f));
            _chanceCrit = (_dexterityLvl * 1.82f) / 10;
            _defense = _defenseBase + (_strenghtLvl * .15f) / 10;
            _chanceRun = (int)(_inteligenceLvl * 1.35f);
            _Maxhealth = (int)(_healthBase + (3.25f * con));

        }
        #endregion
        #region class Warrior
        if (pclass == PlayerClass.warrior)
        {
            _chanceCrit = (_dexterityLvl * 1.05f) / 10;
            _minDamage = (int)(_damageBase * (_strenghtLvl * .65f));
            _maxDamage = (int)(_damageBase * (_strenghtLvl * 1.15f));
            _defense = _defenseBase + (_strenghtLvl * .85f) / 10;
            _chanceRun = (int)(_inteligenceLvl * .85f);
            _Maxhealth = (int)(_healthBase + (7.25f * con));

        }
        #endregion
        #region class Mage
        if (pclass == PlayerClass.mage)
        {
            _chanceCrit = (_dexterityLvl * 1.25f) / 10;
            _defense = _defenseBase + (_strenghtLvl * .45f) / 10;
            _minDamage = (int)(_damageBase * (_inteligenceLvl * .64f));
            _maxDamage = (int)(_damageBase * (_inteligenceLvl * 1.28f));
            _Maxhealth = (int)(_healthBase + (5 * con));
        }
        #endregion
    }

}
