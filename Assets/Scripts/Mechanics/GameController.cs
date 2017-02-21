using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

    #region Private Vars
    private Player player;
    private Enemy enemy;

    #endregion

    #region Public Vars
    public Text exploreLog;
    public Text cityName;
    public Image enemyHp;
    public Text enemyName;

    #endregion

    void Start () {
	    if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
	}

    public void ExploreWorld()
    {
        if (enemy == null && player.attributesLeft == 0)
        {
            enemy = new Enemy(player.level, (int)(50 + (player.con * 1.42)), player) as Enemy;
            cityName.text = Ultility.CityNameGenerator();
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

    public void Attack()
    {
        if (enemy != null)
        {
            int dmg = player.Attack();
            exploreLog.text = "";
            enemy.RecieveDmg(dmg);
            var dmgRecieve = Random.Range(enemy.dmg[0], enemy.dmg[1]);
            player.TakeDamage(dmgRecieve);
            if (enemy)
            {
                exploreLog.text += "\n\r You have deal " + dmg + " dmg to the enemy";
                exploreLog.text += "\n\r Enemy name: " + enemy.eName + " | hp: " + enemy.hp + "/" + enemy.hpMax;
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


}
