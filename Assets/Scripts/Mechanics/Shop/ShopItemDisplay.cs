using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemDisplay : MonoBehaviour {

    [SerializeField]
    ItemDatabase database;
    public Text itemCost;
    public Image itemSpr;

    private GameObject player;

    public int ID;

    private void Start()
    {
        database = GameObject.Find("Main Camera").GetComponent<ItemDatabase>();
        player = GameObject.FindGameObjectWithTag("Player");
        Item temp = database.FetchItemByID(ID);

        itemCost.text = temp.BuyValue.ToString();
        itemSpr.sprite = temp.ISprite;
    }

    private void Update()
    {
        if(player.GetComponent<Player>().gold >= int.Parse(itemCost.text))
        {
            itemCost.color = Color.blue;
        }
        else
        {
            itemCost.color = Color.red;
        }
    }

    public void BuyThis()
    {
        if(player.GetComponent<Player>().gold >= int.Parse(itemCost.text))
        {
            player.GetComponent<Player>().AddGold(-int.Parse(itemCost.text));
            player.GetComponent<PlayerInventory>().AddItem(ID);
        }
    }
}
