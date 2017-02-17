using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : PlayerStats {

    public Text playerNameTxt;
    public Text hpText;
    public Text expText;
    public Text LvlText;
    public Text classText;

    public Image PlayerImage;
    public Image HpBar;
    public Image ExpBar;


	// Use this for initialization
	void Start () {
        switch (pclass)
        {
            case PlayerClass.warrior:
                setStats(200,1,3);
                break;
            case PlayerClass.mage:
                setStats(100,0.4f,1);
                break;
            case PlayerClass.archer:
                setStats(100,0.8f,2.3f);
                break;
            default:
                break;
        }
        base.Start();
	}
	
	// Update is called once per frame
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
        classText.text = "Class: " + this.pclass.ToString();
    }
}
