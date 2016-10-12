using UnityEngine;
using System.Collections;

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

    private int _defenseLvl;
    private int _damageBase;
    private int _damageLvl;
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
    public int def { get { return _defenseLvl; } }
    public int dmg { get { return _damageLvl; } }
    public int gold { get { return _gold; } }
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
        checkAtributes();
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

    public void setStats(int hpbase, int dmgBase)
    {
        _damageLvl = dmgBase;
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

    public void TakeDamage(int ammout)
    {
        _health -= (int)(ammout - Ultility.GetPercent(ammout, def));
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
    }

    public void AddGold(int ammout)
    {
        _gold += ammout;
    }

    public void addStats(string s)
    {
        if(s == "dex")
        {
            _dexterityLvl++;
            if (pclass == PlayerClass.archer)
            {
                _damageLvl = (int)(_damageBase * (_dexterityLvl * 1.35f));
            }
        }
        if(s == "str")
        {
            _strenghtLvl++;
            if(pclass == PlayerClass.warrior)
            {
                _damageLvl = (int)(_damageBase * (_strenghtLvl * 1.15f));
            }
        }
        if(s == "inte")
        {
            _inteligenceLvl++;
            if(pclass == PlayerClass.mage)
            {
                _damageLvl = (int)(_damageBase * (_inteligenceLvl * 1.28f));
            }
        }
        if(s == "con")
        {
            _constitutionLvl++;
            _Maxhealth = (int)(_healthBase + (5 * con));
        }
        if (s == "luck")
        {
            _luckyLvl++;
        }
    }


}
