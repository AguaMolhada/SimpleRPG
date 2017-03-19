using UnityEngine;
using System.Collections;


public class Enemy : ScriptableObject {

    public int[] Dmg = new int[2];

    public string EName { get; private set; }
    public int Lvl { get; private set; }
    public int Hp { get; private set; }
    public int HpMax { get; private set; }
    
    private Player _player;

    public void Init(int l,int h, Player p)
    {
        EName = Ultility.nameGenerator();
        Lvl = (int)(Random.Range(1, l));
        Hp = h;
        HpMax = Hp;
        _player = p;
        Dmg[0] = (int)(5 + 2*(Lvl / 4));
        Dmg[1] = (int)(10 + 2*(Lvl / 2));
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
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        EName = n;
        Hp = h;
        HpMax = hmax;
        this.Lvl = lvl;
        Dmg[0] = dmin;
        Dmg[1] = dmax;
    }

    public void RecieveDmg(int ammout)
    {
        Hp -= ammout;
        if(Hp > 0)
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().ExploreLog.text += "\n\r You have deal " + ammout + " dmg to the " + EName;
            return;
        } 
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GUIController>().SetActiveMenu(GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().EnemyPanel);
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GUIController>().SetActiveMenu(GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().OptionsPanel);
        Die();
    }

    private void Die()
    {
        var xp = (int)(Random.Range(5, 15) * (Lvl / 1.25));
        var gold = (int)(Random.Range(3, 10) * (Lvl / 1.25));
        _player.AddExperience(xp);
        _player.AddGold(gold);
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().ExploreLog.text += EName + " Morreu, " + _player.PlayerName + " Ganhou " + xp + " experience";
        Destroy(this);
    }
}
