using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : PlayerStats {

    public Text playerNameTxt;
    public Text hpText;
    public Text expText;
    public Text LvlText;

    public Image PlayerImage;
    public Image HpBar;
    public Image ExpBar;




	// Use this for initialization
	void Start () {
        switch (pclass)
        {
            case PlayerClass.warrior:
                setStats(200,1);
                break;
            case PlayerClass.mage:
                setStats(100,0.4f);
                break;
            case PlayerClass.archer:
                setStats(100,0.8f);
                break;
            default:
                break;
        }
        base.Start();
	}
	
	// Update is called once per frame
	void Update () {
        UpdateGUIPlayer();

	}

    void UpdateGUIPlayer()
    {
        LvlText.text = "Level:" + level;
        hpText.text = hp + "/" + maxhp;
        HpBar.fillAmount = hp / maxhp;
        expText.text = Ultility.GetPercent(tnl, exp) + "%";
        ExpBar.fillAmount = Ultility.GetPercent(tnl, exp)/100;
    }
}
