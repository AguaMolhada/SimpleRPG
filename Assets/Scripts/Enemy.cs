using UnityEngine;
using System.Collections;


public class Enemy : ScriptableObject {

    private string _eName;
    private int _lvl;
    private int _hp;
    private int _hpMax;
    public int[] dmg = new int[2];

    public string eName { get { return _eName; } }
    public int lvl { get { return _lvl; } }
    public int hp { get { return _hp; } }
    public int hpMax { get { return _hpMax; } }


    private Player player;

    public void Init(int l,int h, Player p)
    {
        _eName = Ultility.nameGenerator();
        _lvl = (int)(Random.Range(1, l));
        _hp = h;
        _hpMax = hp;
        player = p;
        dmg[0] = (int)(5 + 2*(lvl / 4));
        dmg[1] = (int)(10 + 2*(lvl / 2));
    }

    /// <summary>
    /// Used to Load the enemy
    /// </summary>
    /// <param name="n"> String Name</param>
    /// <param name="h"> Int hp</param>
    /// <param name="hmax">int hpmax</param>
    /// <param name="lvl">int lvl</param>
    /// <param name="dmin">int damage min</param>
    /// <param name="dmax">int damage max</param>
    public void SetStats(string n,int h, int hmax,int lvl,int dmin,int dmax)
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _eName = n;
        _hp = h;
        _hpMax = hmax;
        _lvl = lvl;
        dmg[0] = dmin;
        dmg[1] = dmax;
    }

    public void RecieveDmg(int ammout)
    {
        _hp -= ammout;
        if(hp > 0)
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().exploreLog.text += "\n\r You have deal " + ammout + " dmg to the " + eName;
        }
        if(hp <= 0)
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GUIController>().SetActiveMenu(GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().enemyPanel);
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GUIController>().SetActiveMenu(GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().adventureBtn);
            Die();
        }
    }

    void Die()
    {
        var xp = (int)(Random.Range(5, 15) * (lvl / 1.25));
        var gold = (int)(Random.Range(3, 10) * (lvl / 1.25));
        player.AddExperience(xp);
        player.AddGold(gold);
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().exploreLog.text += eName + " Morreu, " + player.playerName + " Ganhou " + xp + " experience";
        Destroy(this);
    }
}
