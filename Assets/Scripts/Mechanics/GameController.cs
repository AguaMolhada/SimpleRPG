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



    #endregion

    void Start () {
	    if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
	}

    public void ExploreWorld()
    {
        if (enemy == null)
        {
            enemy = new Enemy(player.level, (int)(50 + (player.con * 1.42)), player) as Enemy;
            exploreLog.text = "";
            exploreLog.text += "\n\r You have found a Enemy name: " + enemy.eName + "hp: " + enemy.hp + "/" + enemy.hpMax;
        }
        else
        {
            exploreLog.text += "\n\r You cannot explore because there is an enemy in front of you";
        }
    }

    public void Attack()
    {
        if (enemy != null)
        {
            exploreLog.text = "";
            enemy.RecieveDmg(player.dmg);
            var dmgRecieve = Random.Range(enemy.dmg[0], enemy.dmg[1]);
            player.TakeDamage(dmgRecieve);
            exploreLog.text += "\n\r You have deal " + player.dmg + " dmg to the enemy";
            exploreLog.text += "\n\r You have recieved " + dmgRecieve + "dmg";
            exploreLog.text += "\n\r Enemy name: " + enemy.eName + "hp: " + enemy.hp + "/" + enemy.hpMax;
        }
        else
        {
            exploreLog.text += "\n\r You need to find an Enemy to battle";
        }
    }

    public void Run()
    {
        exploreLog.text += "\n\r You have left the battle!";
        enemy = null;
    }

    public void HealPlayer()
    {
        if(enemy == null)
        {
            player.Heal(50);
        }
    }


}
