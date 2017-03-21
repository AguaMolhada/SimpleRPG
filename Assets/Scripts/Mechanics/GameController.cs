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
    public Vector2 MapPlayerPos;
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
        Explore(15,15,35,2);
	}

    private void Update()
    {
        if (!_mapG.Map[(int)MapPlayerPos.x, (int)MapPlayerPos.y].Enemy)
        {
            _enemy = null;
            return;
        }
        EnemyHpTxt.text = _enemy.Hp.ToString() + "/" + _enemy.HpMax.ToString();
        EnemyName.text = _enemy.EName;
        EnemyHp.fillAmount = (float)((float)_enemy.Hp / (float)_enemy.HpMax);
    }

    private void Explore(int wid, int hei,int e, int s)
    {
        CityName.text = Ultility.CityNameGenerator();
        _mapG = new MapGenerator(wid,hei,e,s);
        MapPlayerPos = GetPlayerPos();
        InstanciateMap();

    }

    private void InstanciateMap()
    {
        foreach (var c in _mapG.Map)
        {
            var mapobj = Instantiate(MapObj, transform.position, Quaternion.identity);
            mapobj.GetComponent<Image>().color = c.CelularColor;
            mapobj.transform.SetParent(MapPanel.transform);
            mapobj.name = c.TypeC.ToString();
        }
    }

    public void Move(int x)
    {
        if (_player.AttributesLeft == 0)
        {
            foreach (Transform child in MapPanel.transform)
            {
                Destroy(child.gameObject);
            }

            _mapG.Map[(int) MapPlayerPos.x, (int) MapPlayerPos.y].TypeC = Celula.TypeCelula.Used;
            _mapG.Map[(int) MapPlayerPos.x, (int) MapPlayerPos.y].CelularColor = Color.black;

            switch (x)
            {
                case 1:
                    MapPlayerPos += Vector2.up;
                    break;
                case 2:
                    MapPlayerPos += Vector2.left;
                    break;
                case 3:
                    MapPlayerPos += Vector2.down;
                    break;
                case 4:
                    MapPlayerPos += Vector2.right;
                    break;
            }
            if (MapPlayerPos.x < 0)
            {
                MapPlayerPos.x = 0;
            }
            else if (MapPlayerPos.y < 0)
            {
                MapPlayerPos.y = 0;
            }
            else if (MapPlayerPos.x >= _mapG.Map.GetLength(0))
            {
                MapPlayerPos.x = _mapG.Map.GetLength(0) - 1;
            }
            else if (MapPlayerPos.y >= _mapG.Map.GetLength(1))
            {
                MapPlayerPos.y = _mapG.Map.GetLength(1) - 1;
            }

            if (_mapG.Map[(int) MapPlayerPos.x, (int) MapPlayerPos.y].TypeC == Celula.TypeCelula.Enemy)
            {
                GenerateEnemy();
                _mapG.Map[(int)MapPlayerPos.x, (int)MapPlayerPos.y].Enemy = new Enemy();
                _mapG.Map[(int)MapPlayerPos.x, (int)MapPlayerPos.y].Enemy.Init(_player.Level, (int)(50 + (_player.Con * 1.42)), _player);
                _enemy = _mapG.Map[(int) MapPlayerPos.x, (int) MapPlayerPos.y].Enemy;
                ActivateGui(EnemyPanel);
            }
            if (_mapG.Map[(int) MapPlayerPos.x, (int) MapPlayerPos.y].TypeC == Celula.TypeCelula.Shop)
            {
                ActivateGui(StorePanel);
            }
            _mapG.Map[(int) MapPlayerPos.x, (int) MapPlayerPos.y].TypeC = Celula.TypeCelula.Player;
            _mapG.Map[(int) MapPlayerPos.x, (int) MapPlayerPos.y].CelularColor = Color.blue;
            InstanciateMap();
        }
    }

    public void GenerateEnemy()
    {
        if (_enemy == null)
        {
            CityName.text = Ultility.CityNameGenerator();
            ExploreLog.text = "";
            if (_enemy != null)
            {
                ExploreLog.text += "\n\r You have found a Enemy name: " + _enemy.EName + " | hp: " + _enemy.Hp + "/" +
                                   _enemy.HpMax;
            }
        }
    }

    public void ActivateGui(GameObject x)
    {
        gameObject.GetComponent<GUIController>().SetActiveMenu(x);
        gameObject.GetComponent<GUIController>().SetActiveMenu(OptionsPanel);
    }

    public void Attack()
    {
        if (_mapG.Map[(int)MapPlayerPos.x, (int)MapPlayerPos.y].Enemy != null)
        {
            var dmg = _player.Attack();
            if (_mapG.Map[(int)MapPlayerPos.x, (int)MapPlayerPos.y].Enemy)
            {
                ExploreLog.text += "\n\r You have deal " + dmg + " dmg to the enemy";
            }
            ExploreLog.text = "";
            _mapG.Map[(int)MapPlayerPos.x, (int)MapPlayerPos.y].Enemy.RecieveDmg(dmg);
            var dmgRecieve = Random.Range(_mapG.Map[(int)MapPlayerPos.x, (int)MapPlayerPos.y].Enemy.Dmg[0], _mapG.Map[(int)MapPlayerPos.x, (int)MapPlayerPos.y].Enemy.Dmg[1]);
            if (_mapG.Map[(int)MapPlayerPos.x, (int)MapPlayerPos.y].Enemy.Hp > 0)
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
            _mapG.Map[(int)MapPlayerPos.x, (int)MapPlayerPos.y].Enemy = null;
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
        _mapG.Map[(int)MapPlayerPos.x, (int)MapPlayerPos.y].Enemy = ScriptableObject.CreateInstance("Enemy") as Enemy;
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

    public Vector2 GetPlayerPos()
    {
        for (var i = 0; i < _mapG.Map.GetLength(0); i++)
        {
            for (var j = 0; j < _mapG.Map.GetLength(1); j++)
            {
                if (_mapG.Map[i, j].TypeC == Celula.TypeCelula.Player)
                {
                    return new Vector2(i,j);
                }
            }
        }
        return Vector2.zero;
    }

}
