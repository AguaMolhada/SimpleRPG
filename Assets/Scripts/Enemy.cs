using UnityEngine;
using System.Collections;


public class Enemy : ScriptableObject {

    public string eName;
    public int lvl;
    public int hp;
    public int hpMax;
    public int[] dmg = new int[2];

    private Player player;

    public Enemy(int l,int h, Player p)
    {
        eName = Ultility.nameGenerator();
        lvl = l;
        hp = h;
        hpMax = h;
        player = p;
        dmg[0] = (int)(5 + 2*(lvl / 4));
        dmg[1] = (int)(10 + 2*(lvl / 2));
    }

    public void RecieveDmg(int ammout)
    {
        hp -= ammout;
        if(hp <= 0)
        {
            var xp = (int)(Random.Range(2, 8) * (lvl / 1.25));
            player.AddExperience(xp);
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().exploreLog.text += eName + " Morreu, " + player.playerName + " Ganhou " + xp +" experience" ;
            Destroy(this);
        }
    }

}
