﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ItemData.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Vianna 2018.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    /// <summary>
    /// Item assigned to the slot.
    /// </summary>
    public Item Item;
    /// <summary>
    /// Ammout of the item.
    /// </summary>
    public int Amount;
    /// <summary>
    /// Max itens allowed in stack.
    /// </summary>
    public const int MaxAmount = 255;
    /// <summary>
    /// Slot that i'm assigned.
    /// </summary>
    public Slot MySlot;
    /// <summary>
    /// Item offset to dragging be in the center.
    /// </summary>
    private Vector2 _itemOffset;
    /// <summary>
    /// Amount texto to display
    /// </summary>
    [SerializeField] private TMP_Text _amountText;


    public void Init(Item i, Slot s)
    {
        Item = i;
        MySlot = s;
        Amount = 1;
        gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + Item.Sprite);
    }

    private void Update()
    {
        if (Amount <= 0)
        {
            Destroy(gameObject);
        }
        _amountText.text = Item.Stackable ? Amount.ToString() : "";
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Item != null)
        {
            _itemOffset = eventData.position - new Vector2(transform.position.x, transform.position.y);
            transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform);
            transform.position = eventData.position - _itemOffset;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Item != null)
        {
            transform.position = eventData.position - _itemOffset;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        UpdateTransform();
    }

    public void UpdateTransform()
    {
        gameObject.transform.SetParent(MySlot.transform, false);
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(45, 45);
        transform.position = MySlot.transform.position;
    }

    ///
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (Item.Usable)
            {
                Debug.Log("Usando Item");
                Amount -= 1;
            }
            else if (!Item.Usable)
            {
                if (MySlot.SlotT == SlotType.Equipment)
                {
                    Debug.Log("DeEquip");
                }
                else if (MySlot.SlotT == SlotType.Inventory)
                {
                    Debug.Log("Equip");
                }
            }
        }
    }
}