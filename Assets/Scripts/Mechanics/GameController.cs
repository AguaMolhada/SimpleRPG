using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

    #region Private Vars
    private static Player _player;
    private Enemy _enemy;
    private MapGenerator _mapG;
    #endregion

    #region Public Vars
    public Text ExploreLog;
    public Text CityName;
    public GameObject EnemyPanel;
    public GameObject OptionsPanel;
    public GameObject StorePanel;
    public GameObject MapPanel;
    public GameObject MapObj;
    public Image EnemyHp;
    public Text EnemyHpTxt;
    public Text EnemyName;

    #endregion

    private void Start () {
	    if(_player == null)
        {
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
        Explore(15,15,10,2);
	}

    private void Update()
    {
        if (!_enemy)
        {
            return;
        }
        EnemyHpTxt.text = _enemy.Hp.ToString() + "/" + _enemy.HpMax.ToString();
        EnemyName.text = _enemy.EName;
        EnemyHp.fillAmount = (float)((float)_enemy.Hp / (float)_enemy.HpMax);
    }

    public void Explore(int wid, int hei,int e, int s)
    {
        CityName.text = Ultility.CityNameGenerator();
        _mapG = new MapGenerator(wid,hei,e,s);
        foreach (var c in _mapG.Map)
        {
            var mapobj = Instantiate(MapObj, transform.position, Quaternion.identity);
            mapobj.GetComponent<Image>().color = c.CelularColor;
        }
    }



    public void ExploreWorld()
    {
        if (_enemy == null && _player.AttributesLeft == 0)
        {
            _enemy = ScriptableObject.CreateInstance("Enemy") as Enemy ;
            _enemy.Init(_player.Level, (int)(50 + (_player.Con * 1.42)), _player);
            CityName.text = Ultility.CityNameGenerator();
            gameObject.GetComponent<GUIController>().SetActiveMenu(EnemyPanel);
            gameObject.GetComponent<GUIController>().SetActiveMenu(OptionsPanel);
            ExploreLog.text = "";
            ExploreLog.text += "\n\r You have found a Enemy name: " + _enemy.EName + " | hp: " + _enemy.Hp + "/" + _enemy.HpMax;
        }
        else if(_player.AttributesLeft != 0)
        {
            ExploreLog.text += "\n\r Tou cannot explorer need to assign your attributes. attribute left: "+_player.AttributesLeft;
        }
        else {
            ExploreLog.text += "\n\r You cannot explore because there is an enemy in front of you";
        }
    }

    public void OpenStore()
    {
        gameObject.GetComponent<GUIController>().SetActiveMenu(EnemyPanel);
        gameObject.GetComponent<GUIController>().SetActiveMenu(OptionsPanel);
    }

    public void Attack()
    {
        if (_enemy != null)
        {
            var dmg = _player.Attack();
            if (_enemy)
            {
                ExploreLog.text += "\n\r You have deal " + dmg + " dmg to the enemy";
            }
            ExploreLog.text = "";
            _enemy.RecieveDmg(dmg);
            var dmgRecieve = Random.Range(_enemy.Dmg[0], _enemy.Dmg[1]);
            if (_enemy.Hp > 0)
            {
                _player.TakeDamage(dmgRecieve);
            }
        }
        else
        {
            
            ExploreLog.text += "\n\r You need to find an Enemy to battle";
        }
    }

    public void Run()
    {
        var chance = Random.Range(0+(_player.ChanceRun/10), 100);
        if (chance >= 30)
        {
            ExploreLog.text += "\n\r You have left the battle!";
            gameObject.GetComponent<GUIController>().SetActiveMenu(EnemyPanel);
            gameObject.GetComponent<GUIController>().SetActiveMenu(OptionsPanel);
            _enemy = null;
        }
        else
        {
            ExploreLog.text += " \n\r You failed to escape :O";
            var dmgRecieve = Random.Range(_enemy.Dmg[0], _enemy.Dmg[1]);
            _player.TakeDamage(dmgRecieve);
            ExploreLog.text += "\n\r You recieved "+ dmgRecieve + " dmg";
        }
    }

    public void LoadingThings()
    {
        _enemy = ScriptableObject.CreateInstance("Enemy") as Enemy;
        gameObject.GetComponent<GUIController>().SetActiveMenu(EnemyPanel);
        gameObject.GetComponent<GUIController>().SetActiveMenu(OptionsPanel);
    }

    public static Player GetPlayer()
    {
        return _player;
    }
    public static Enemy GetEnemy()
    {
        return GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>()._enemy;
    }

}
