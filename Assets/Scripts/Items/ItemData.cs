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
    public int Ammount;
    /// <summary>
    /// Slot that i'm assigned.
    /// </summary>
    public Slot MySlot;
    /// <summary>
    /// Item offset to dragging be in the center.
    /// </summary>
    private Vector2 _itemOffset;
    [SerializeField]
    private TMP_Text _ammountText;


    public void Init(Item i, Slot s)
    {
        Item = i;
        MySlot = s;
        Ammount = 1;
        gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + Item.Sprite);
    }

    private void Update()
    {
        if (Ammount <= 0)
        {
            Destroy(gameObject);
        }

        if (Item.Stackable)
        {
            _ammountText.text = Ammount.ToString();
        }
        else
        {
            _ammountText.text = "";
        }
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

        gameObject.transform.SetParent(MySlot.transform, false);
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(45, 45);
        transform.position = MySlot.transform.position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
        
        }
    }
}