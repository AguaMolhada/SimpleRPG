using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

    #region Private Vars
    private static Player _player;
    private Enemy _enemy;
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
        if (!MapGenerator.Instance.Map[(int)MapPlayerPos.x, (int)MapPlayerPos.y].Enemy)
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
        MapGenerator.Instance.MapGeneratorInit(wid,hei,e,s);
        MapPlayerPos = GetPlayerPos();
        InstanciateMap();

    }

    private void InstanciateMap()
    {
        foreach (var c in MapGenerator.Instance.Map)
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
            var playerObje = GameObject.Find("PlayerOBJ");
            foreach (Transform child in MapPanel.transform)
            {
                Destroy(child.gameObject);
            }

            MapGenerator.Instance.Map[(int) MapPlayerPos.x, (int) MapPlayerPos.y].TypeC = Celula.TypeCelula.Used;
            MapGenerator.Instance.Map[(int) MapPlayerPos.x, (int) MapPlayerPos.y].CelularColor = Color.black;

            switch (x)
            {
                case 1:
                    MapPlayerPos += Vector2.up;
                    if (MapPlayerPos.y >= MapGenerator.Instance.Map.GetLength(1))
                    {
                        MapPlayerPos.y = MapGenerator.Instance.Map.GetLength(1) - 1;
                        break;
                    }
                    playerObje.transform.position += Vector3.forward * MapGenerator.Instance.ScaleFactor;
                    break;
                case 2:
                    MapPlayerPos += Vector2.left;
                    if (MapPlayerPos.x < 0)
                    {
                        MapPlayerPos.x = 0;
                        break;
                    }
                    playerObje.transform.position += Vector3.left * MapGenerator.Instance.ScaleFactor;
                    break;
                case 3:
                    MapPlayerPos += Vector2.down;
                    if (MapPlayerPos.y < 0)
                    {
                        MapPlayerPos.y = 0;
                        break;
                    }
                    playerObje.transform.position += Vector3.back * MapGenerator.Instance.ScaleFactor;
                    break;
                case 4:
                    MapPlayerPos += Vector2.right;
                    if (MapPlayerPos.x >= MapGenerator.Instance.Map.GetLength(0))
                    {
                        MapPlayerPos.x = MapGenerator.Instance.Map.GetLength(0) - 1;
                        break;
                    }
                    playerObje.transform.position += Vector3.right * MapGenerator.Instance.ScaleFactor;
                    break;
            }




            if (MapGenerator.Instance.Map[(int) MapPlayerPos.x, (int) MapPlayerPos.y].TypeC == Celula.TypeCelula.Enemy)
            {
                GenerateEnemy();
                MapGenerator.Instance.Map[(int)MapPlayerPos.x, (int)MapPlayerPos.y].Enemy = new Enemy();
                MapGenerator.Instance.Map[(int)MapPlayerPos.x, (int)MapPlayerPos.y].Enemy.Init(_player.Level, (int)(50 + (_player.Con * 1.42)), _player);
                _enemy = MapGenerator.Instance.Map[(int) MapPlayerPos.x, (int) MapPlayerPos.y].Enemy;
                ActivateGui(EnemyPanel);
            }
            if (MapGenerator.Instance.Map[(int) MapPlayerPos.x, (int) MapPlayerPos.y].TypeC == Celula.TypeCelula.Shop)
            {
                ActivateGui(StorePanel);
            }
            MapGenerator.Instance.Map[(int) MapPlayerPos.x, (int) MapPlayerPos.y].TypeC = Celula.TypeCelula.Player;
            MapGenerator.Instance.Map[(int) MapPlayerPos.x, (int) MapPlayerPos.y].CelularColor = Color.blue;
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
        if (MapGenerator.Instance.Map[(int)MapPlayerPos.x, (int)MapPlayerPos.y].Enemy != null)
        {
            var dmg = _player.Attack();
            if (MapGenerator.Instance.Map[(int)MapPlayerPos.x, (int)MapPlayerPos.y].Enemy)
            {
                ExploreLog.text += "\n\r You have deal " + dmg + " dmg to the enemy";
            }
            ExploreLog.text = "";
            MapGenerator.Instance.Map[(int)MapPlayerPos.x, (int)MapPlayerPos.y].Enemy.RecieveDmg(dmg);
            var dmgRecieve = Random.Range(MapGenerator.Instance.Map[(int)MapPlayerPos.x, (int)MapPlayerPos.y].Enemy.Dmg[0], MapGenerator.Instance.Map[(int)MapPlayerPos.x, (int)MapPlayerPos.y].Enemy.Dmg[1]);
            if (MapGenerator.Instance.Map[(int)MapPlayerPos.x, (int)MapPlayerPos.y].Enemy.Hp > 0)
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
            MapGenerator.Instance.Map[(int)MapPlayerPos.x, (int)MapPlayerPos.y].Enemy = null;
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
        MapGenerator.Instance.Map[(int)MapPlayerPos.x, (int)MapPlayerPos.y].Enemy = ScriptableObject.CreateInstance("Enemy") as Enemy;
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
        for (var i = 0; i < MapGenerator.Instance.Map.GetLength(0); i++)
        {
            for (var j = 0; j < MapGenerator.Instance.Map.GetLength(1); j++)
            {
                if (MapGenerator.Instance.Map[i, j].TypeC == Celula.TypeCelula.Player)
                {
                    return new Vector2(i,j);
                }
            }
        }
        return Vector2.zero;
    }

}
