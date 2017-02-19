using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : PlayerStats {

    public Text playerNameTxt;
    public Text hpText;
    public Text expText;
    public Text LvlText;
    public Text classText;
    public Text goldText;

    public Image[] classImg;
    public Image[] classImgs;
    public Image PlayerImage;
    public Image HpBar;
    public Image ExpBar;


	void Start () {
        switch (pclass)
        {
            case PlayerClass.warrior:
                for (int i = 0; i < classImgs.Length+1; i++)
                {
                    classImg[i].sprite = classImgs[0].sprite;
                }

                setStats(200,1,3,0);
                break;
            case PlayerClass.mage:
                for (int i = 0; i < classImgs.Length+1; i++)
                {
                    classImg[i].sprite = classImgs[1].sprite;
                }
                setStats(100,0.4f,1,0);
                break;
            case PlayerClass.archer:
                for (int i = 0; i < classImgs.Length+1; i++)
                {
                    classImg[i].sprite = classImgs[2].sprite;
                }
                setStats(100,0.8f,2.3f,0);
                break;
            default:
                break;
        }
        base.Start();
	}

	void Update () {
        UpdateGUIPlayer();
        GUIStatsUpdate();
	}

    void UpdateGUIPlayer()
    {
        LvlText.text = "Level: " + level;
        hpText.text = hp + "/" + maxhp;
        HpBar.fillAmount = hp / maxhp;
        expText.text = System.Math.Round(Ultility.PercentValue(tnl, exp),2) + "%";
        ExpBar.fillAmount = Ultility.PercentValue(tnl, exp)/100;
    }

    public void TakeDamage(int ammout)
    {
        _health -= (int)(ammout - Ultility.GetPercent(ammout, (float)def));
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().exploreLog.text += "You recieved " + (int)(ammout - Ultility.GetPercent(ammout, (float)def)) + " Damage";
        if (_health <= 0)
        {
            Die();
        }
    }

    public void Heal(int ammount)
    {
        _health += ammount;
        _health = Mathf.Clamp(_health, 0, _Maxhealth);
    }

    private void Die()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().exploreLog.text += "You`re Dead! You lost " + (int)Ultility.GetPercent(_gold, 10) + " Gold and " + (int)Ultility.GetPercent(_experience, 7) + " experience points";
        _gold -= (int)Ultility.GetPercent(_gold, 30);
        _health = _healthBase;
        _experience = _experience - (int)Ultility.GetPercent(_experience, 20);
    }



}
