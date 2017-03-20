using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : PlayerStats {

    public Text PlayerNameTxt;
    public Text HpText;
    public Text ExpText;
    public Text LvlText;
    public Text GoldText;

    public Image[] ClassImg;
    public Image[] ClassImgs;
    public Image PlayerImage;
    public Image HpBar;
    public Image ExpBar;


    public void Start () {
        switch (Pclass)
        {
            case PlayerClass.Warrior:
                for (var i = 0; i < ClassImgs.Length+1; i++)
                {
                    ClassImg[i].sprite = ClassImgs[0].sprite;
                }

                Initialize(200,1,3,0);
                break;
            case PlayerClass.Mage:
                for (var i = 0; i < ClassImgs.Length+1; i++)
                {
                    ClassImg[i].sprite = ClassImgs[1].sprite;
                }
                Initialize(100,0.4f,1,0);
                break;
            case PlayerClass.Archer:
                for (var i = 0; i < ClassImgs.Length+1; i++)
                {
                    ClassImg[i].sprite = ClassImgs[2].sprite;
                }
                Initialize(100,0.8f,2.3f,0);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        base.Start();
	}

    private void Update () {
        UpdateGuiPlayer();
	}

    private void UpdateGuiPlayer()
    {
        
        LvlText.text = "Level: " + Level;
        HpText.text = Hp + "/" + Maxhp;
        HpBar.fillAmount = (float)Hp / (float)Maxhp;
        ExpText.text = System.Math.Round(Ultility.PercentValue(Tnl, Exp),2) + "%";
        ExpBar.fillAmount = Ultility.PercentValue(Tnl, Exp)/100;

        AttleftTxt.text = "Atributes left: " + AttributesLeft.ToString();
        StrTxt.text = "Str: " + Str.ToString();
        ConTxt.text = "Con: " + Con.ToString();
        DexTxt.text = "Dex: " + Dex.ToString();
        TnlTxt.text = "TnL: " + (Tnl - Exp).ToString();
        InteTxt.text = "Int: " + Inte.ToString();
        XpTxt.text = "XP: " + Exp.ToString();
        LuckTxt.text = "Luck: need implement";
        DefInfoTxt.text = "Def: " + Def.ToString("F") + "%";
        DmgInfoTxt.text = "Dmg: " + DmgMin.ToString() + "-" + DmgMax.ToString();
        CritInfoTxt.text = "Crit:" + Crit.ToString("F") + "%";
        GoldText.text = Gold.ToString();

    }

    public void TakeDamage(int ammout)
    {
        Hp -= (int)(ammout - Ultility.GetPercent(ammout, (float)Def));
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().ExploreLog.text += "You recieved " + (int)(ammout - Ultility.GetPercent(ammout, (float)Def)) + " Damage \n\r";
        if (Hp <= 0)
        {
            Die();
        }
    }

    public void Heal(int ammount)
    {
        Hp += ammount;
        Hp = Mathf.Clamp(Hp, 0, Maxhp);
    }

    private void Die()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().ExploreLog.text += "You`re Dead! You lost " + (int)Ultility.GetPercent(Gold, 10) + " Gold and " + (int)Ultility.GetPercent(Exp, 7) + " experience points";
        Gold -= (int)Ultility.GetPercent(Gold, 30);
        Hp = HealthBase;
        Exp = Exp - (int)Ultility.GetPercent(Exp, 20);
    }

}
