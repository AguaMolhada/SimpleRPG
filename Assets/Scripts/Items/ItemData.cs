using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class ItemData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler {

    public Item item;
    public int ammount;
    public int slot;

    private Transform originalParent;
    private Vector2 itemOffset;
    private PlayerInventory inv;

    void Start()
    {
        inv = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
    }

    void Update()
    {
        if(ammount == 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(item != null)
        {
            itemOffset = eventData.position - new Vector2(this.transform.position.x, this.transform.position.y);
            originalParent = this.transform.parent;
            this.transform.SetParent(this.transform.parent.parent);
            this.transform.position = eventData.position - itemOffset;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            this.transform.position = eventData.position - itemOffset;
 
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.transform.SetParent(inv.slots[slot].transform);
        this.transform.position = inv.slots[slot].transform.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (this.item.Usable && ammount > 0)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Heal(item.Attribute);
                GameObject.Find("GameController").GetComponent<GameController>().exploreLog.text += "\n\r You have used 1 " + item.Title + " ! and healed " + item.Attribute + "hp.";
                inv.RemoveItem(item.ID);
            }
        }

    }
}
