using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

    #region Private Vars
    private int _health;
    private int _Maxhealth;
    private int _level;
    private int _atributesLeft;
    private int _atributesUsed;
    private int _experience;
    private int _toNextLevel;
    private int _dexterityLvl;
    private int _strenghtLvl;
    private int _inteligenceLvl;
    private int _constitutionLvl;

    private int _defenseLvl;
    #endregion

    #region Public Vars
    public float hp { get { return _health; } }
    public int maxhp { get { return _Maxhealth; } }
    public int level { get { return _level; } }
    public int exp { get { return _experience; } }
    public int tnl { get { return _toNextLevel; } }
    public int dex { get { return _dexterityLvl; } }
    public int str { get { return _strenghtLvl; } }
    public int inte { get { return _inteligenceLvl; } }
    public int con { get { return _constitutionLvl; } }

    public int def { get { return _defenseLvl; } }
    #endregion

    public void Start()
    {
        setStats();
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

    void setStats()
    {
        _Maxhealth = (int)(100 + (5 * con));
        _health = _Maxhealth;
        _level = 1;
        _atributesLeft = 5;
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

    public void LevelUP()
    {
        if(_experience >= _toNextLevel)
        {
            _experience = 0;
            _toNextLevel = (int)(_toNextLevel * 1.15f);
            Debug.Log("Level UP");
        }
    }


}
