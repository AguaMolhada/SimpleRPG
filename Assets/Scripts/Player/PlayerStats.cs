using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

    #region Private Vars
    
    #region Life
    private int _health;
    private int _healthBase;
    private int _Maxhealth;
    #endregion

    #region Level
    private int _level;
    private int _atributesLeft;
    private int _atributesUsed;
    private int _experience;
    private int _toNextLevel;
    #endregion

    #region Stats
    private int _dexterityLvl;
    private int _strenghtLvl;
    private int _inteligenceLvl;
    private int _constitutionLvl;
    private int _luckyLvl;

    private double _defense;
    private float _damageBase;
    private int _minDamage;
    private int _maxDamage;
    private int _chanceRun;
    private double _chanceCrit;
    #endregion

    private int _gold;

    #endregion

    #region Public Vars
    public string playerName;
    public float hp { get { return _health; } }
    public int maxhp { get { return _Maxhealth; } }
    public int level { get { return _level; } }
    public int exp { get { return _experience; } }
    public int tnl { get { return _toNextLevel; } }

    public int dex { get { return _dexterityLvl; } }
    public int str { get { return _strenghtLvl; } }
    public int inte { get { return _inteligenceLvl; } }
    public int con { get { return _constitutionLvl; } }
    public int luck { get { return _luckyLvl; } }
    public double def { get { return _defense; } }

    public int chanceRun { get { return _chanceRun; } }
    public int gold { get { return _gold; } }
    public int attributesLeft { get { return _atributesLeft; } }


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
        GUIStatsUpdate();
    }

    void GUIStatsUpdate()
    {
        attleftTxt.text = "Atributes left: " + _atributesLeft.ToString();
        strTxt.text = "Str: " + _strenghtLvl.ToString();
        conTxt.text = "Con: "+ _constitutionLvl.ToString();
        dexTxt.text = "Dex: " + _dexterityLvl.ToString();
        tnlTxt.text = "TnL: " + (_toNextLevel-_experience).ToString();
        inteTxt.text = "Int: " + _inteligenceLvl.ToString();
        xpTxt.text = "XP: " + _experience.ToString();
        luckTxt.text = "Luck: " + _luckyLvl.ToString();
        defInfoTxt.text = "Def: " + _defense.ToString("F") + "%";
        dmgInfoTxt.text = "Dmg: " + _minDamage.ToString() + "-" + _maxDamage.ToString();
        critInfoTxt.text = "Crit:" + _chanceCrit.ToString("F") + "%";
    }

    void checkAtributes()
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

    public void setStats(int hpbase, float dmgBase)
    {
        _minDamage = str;
        _damageBase = dmgBase;
        _healthBase = hpbase;
        _Maxhealth = (int)(_healthBase + (5 * con));
        _health = _Maxhealth;
        _level = 1;
        _toNextLevel = 100;
    }

    public void Heal(int ammount)
    {
        _health += ammount;
        _health = Mathf.Clamp(_health, 0, _Maxhealth);
    }

    public int Attack()
    {
        return Random.Range((int)_minDamage, _maxDamage);
    }

    public void TakeDamage(int ammout)
    {
        _health -= (int)(ammout - Ultility.GetPercent(ammout, (float)def));
    }

    public void AddExperience(int ammout)
    {
        _experience += ammout;

        if(_experience >= _toNextLevel)
        {
            _experience -= _toNextLevel;
            _toNextLevel = (int)(_toNextLevel * 1.15f);
            _health = _Maxhealth;
            Debug.Log("Level UP");
        }
        GUIStatsUpdate();
    }

    public void AddGold(int ammout)
    {
        _gold += ammout;
        GUIStatsUpdate();
    }

    public void addStats(string s)
    {
        if (attributesLeft > 0)
        {
#warning Need to implement Lucky Stat
            #region class Archer
            if (pclass == PlayerClass.archer)
            {
                if (s == "dex")
                {
                    _dexterityLvl++;
                    _minDamage = (int)(_damageBase * (_dexterityLvl * .75f));
                    _maxDamage = (int)(_damageBase * (_dexterityLvl * 1.35f));
                    _chanceCrit = (_dexterityLvl * 1.82f)/10;
                }
                if (s == "str")
                {
                    _strenghtLvl++;
                    _defense = (_strenghtLvl * .15f)/10;
                }
                if (s == "inte")
                {
                    _inteligenceLvl++;
                    _chanceRun = (int)(_inteligenceLvl * 1.35f);
                }
                if (s == "con")
                {
                    _constitutionLvl++;
                    _Maxhealth = (int)(_healthBase + (3.25f * con));
                }
                if (s == "luck")
                {
                    _luckyLvl++;
                }
            }
            #endregion
            #region class Warrior
            if (pclass == PlayerClass.warrior)
            {
                if (s == "dex")
                {
                    _dexterityLvl++;
                    _chanceCrit = (_dexterityLvl * 1.05f)/10;
                }
                if (s == "str")
                {
                    _strenghtLvl++;
                    _minDamage = (int)(_damageBase * (_strenghtLvl * .65f));
                    _maxDamage = (int)(_damageBase * (_strenghtLvl * 1.15f));
                    _defense = (_strenghtLvl * .85f)/10;
                }
                if (s == "inte")
                {
                    _inteligenceLvl++;
                    _chanceRun = (int)(_inteligenceLvl * .85f);
                }
                if (s == "con")
                {
                    _constitutionLvl++;
                    _Maxhealth = (int)(_healthBase + (7.25f * con));
                }
                if (s == "luck")
                {
                    _luckyLvl++;
                }
            }
            #endregion
            #region class Mage
            if (pclass == PlayerClass.mage)
            {
                if (s == "dex")
                {
                    _dexterityLvl++;
                    _chanceCrit = (_dexterityLvl * 1.25f)/10;
                }
                if (s == "str")
                {
                    _strenghtLvl++;
                    _defense = (_strenghtLvl * .45f)/10;
                }
                if (s == "inte")
                {
                    _inteligenceLvl++;
                    _minDamage = (int)(_damageBase * (_inteligenceLvl * .64f));
                    _maxDamage = (int)(_damageBase * (_inteligenceLvl * 1.28f));

                }
                if (s == "con")
                {
                    _constitutionLvl++;
                    _Maxhealth = (int)(_healthBase + (5 * con));
                }
                if (s == "luck")
                {
                    _luckyLvl++;
                }
            }
            #endregion
            _atributesLeft--;
            _atributesUsed++;
            GUIStatsUpdate();
        }
    }

    public void SetActiveMenu(GameObject x)
    {
        x.SetActive(!x.activeSelf);
    }
}
