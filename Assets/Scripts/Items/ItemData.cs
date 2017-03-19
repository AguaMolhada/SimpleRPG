using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Items
{
    public class ItemData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler {

        public Item Item;
        public int Ammount;
        public int Slot;

        private Vector2 _itemOffset;
        private PlayerInventory _inv;
        private PlayerEquipment _equip;

        private void Start()
        {
            _inv = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
            _equip = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
        }

        private void Update()
        {
            if(Ammount == 0)
            {
                Destroy(gameObject);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if(Item != null)
            {
                _itemOffset = eventData.position - new Vector2(transform.position.x, transform.position.y);
                transform.SetParent(transform.parent.parent);
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
            transform.SetParent(_inv.Slots[Slot].transform);
            transform.position = _inv.Slots[Slot].transform.position;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                if (Item.Usable && Ammount > 0)
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Heal(Item.Attribute);
                    GameObject.Find("GameController").GetComponent<GameController>().ExploreLog.text += "\n\r You have used 1 " + Item.Title + " ! and healed " + Item.Attribute + "hp.";
                    _inv.RemoveItem(Item.Id);
                }

                if (Item.TypeItem.ToString() != "consumable")
                {
                    Debug.Log(_equip.CheckIsAlreadyEquiped(Item));
                    if (!_equip.CheckIsAlreadyEquiped(Item))
                    {
                        _equip.EquipItem(Item.Id);
                        _inv.RemoveItem(Item.Id);
                    }
                    else
                    {
                        _equip.DeEquipItem(Item.Id);
                        _inv.AddItem(Item.Id);
                    }
                }

            }

        }
    }
}
