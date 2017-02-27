using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour {


    public int amountToShow;
    public GameObject itemsPanel;
    public GameObject itemFrameHolder;

    public List<GameObject> slots = new List<GameObject>();

    private void Start()
    {
        amountToShow = GameObject.Find("Main Camera").GetComponent<ItemDatabase>().ItemsCount();
        for (int i = 0; i < amountToShow-1; i++)
        {
            slots.Add(Instantiate(itemFrameHolder));
            slots[i].transform.SetParent(itemsPanel.transform);
            slots[i].GetComponent<ShopItemDisplay>().ID = i+1;
        }
    }
}
