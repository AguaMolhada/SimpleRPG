using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

    #region Private Vars
    private static Player player;
    private Enemy enemy;

    #endregion

    #region Public Vars
    public Text exploreLog;
    public Text cityName;
    public GameObject enemyPanel;
    public GameObject optionsPanel;
    public GameObject storePanel;
    public Image enemyHp;
    public Text enemyHpTxt;
    public Text enemyName;

    #endregion

    void Start () {
	    if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
	}

    private void Update()
    {
        if (enemy)
        {
            enemyHpTxt.text = enemy.hp.ToString() + "/" + enemy.hpMax.ToString();
            enemyName.text = enemy.eName;
            enemyHp.fillAmount = (float)((float)enemy.hp / (float)enemy.hpMax);
        }
    }

    public void ExploreWorld()
    {
        if (enemy == null && player.attributesLeft == 0)
        {
            enemy = ScriptableObject.CreateInstance("Enemy") as Enemy ;
            enemy.Init(player.level, (int)(50 + (player.con * 1.42)), player);
            cityName.text = Ultility.CityNameGenerator();
            this.gameObject.GetComponent<GUIController>().SetActiveMenu(enemyPanel);
            this.gameObject.GetComponent<GUIController>().SetActiveMenu(optionsPanel);
            exploreLog.text = "";
            exploreLog.text += "\n\r You have found a Enemy name: " + enemy.eName + " | hp: " + enemy.hp + "/" + enemy.hpMax;
        }
        else if(player.attributesLeft != 0)
        {
            exploreLog.text += "\n\r Tou cannot explorer need to assign your attributes. attribute left: "+player.attributesLeft;
        }
        else {
            exploreLog.text += "\n\r You cannot explore because there is an enemy in front of you";
        }
    }

    public void OpenStore()
    {
        this.gameObject.GetComponent<GUIController>().SetActiveMenu(enemyPanel);
        this.gameObject.GetComponent<GUIController>().SetActiveMenu(optionsPanel);
    }

    public void Attack()
    {
        if (enemy != null)
        {
            int dmg = player.Attack();
            if (enemy)
            {
                exploreLog.text += "\n\r You have deal " + dmg + " dmg to the enemy";
            }
            exploreLog.text = "";
            enemy.RecieveDmg(dmg);
            var dmgRecieve = Random.Range(enemy.dmg[0], enemy.dmg[1]);
            if (enemy.hp > 0)
            {
                player.TakeDamage(dmgRecieve);
            }
        }
        else
        {
            
            exploreLog.text += "\n\r You need to find an Enemy to battle";
        }
    }

    public void Run()
    {
        var chance = Random.Range((int)(0+(player.chanceRun/10)), 100);
        if (chance >= 30)
        {
            exploreLog.text += "\n\r You have left the battle!";
            this.gameObject.GetComponent<GUIController>().SetActiveMenu(enemyPanel);
            this.gameObject.GetComponent<GUIController>().SetActiveMenu(optionsPanel);
            enemy = null;
        }
        else
        {
            exploreLog.text += " \n\r You failed to escape :O";
            var dmgRecieve = Random.Range(enemy.dmg[0], enemy.dmg[1]);
            player.TakeDamage(dmgRecieve);
            exploreLog.text += "\n\r You recieved "+ dmgRecieve + " dmg";
        }
    }

    public void LoadingThings()
    {
        enemy = ScriptableObject.CreateInstance("Enemy") as Enemy;
        this.gameObject.GetComponent<GUIController>().SetActiveMenu(enemyPanel);
        this.gameObject.GetComponent<GUIController>().SetActiveMenu(optionsPanel);
    }

    public static Player getPlayer()
    {
        return player;
    }
    public static Enemy getEnemy()
    {
        return GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().enemy;
    }

}
