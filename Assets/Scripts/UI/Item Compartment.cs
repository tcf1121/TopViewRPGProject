using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemConpartment : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject Icon;
    public Image ItemImage;
    public Item item;
    public int at;
    private float interval = 0.25f;
    private float doubleClickedTime = -1.0f;
    private bool IsDoubleClicked = false;
    public GameObject InfoUI;
    public ItemInfo Info;

    void Awake()
    {
        ItemImage = Icon.GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if ((Time.time - doubleClickedTime) < interval)
        {
            IsDoubleClicked = true;
            doubleClickedTime = -1.0f;

            if (item != null)
            {
                Debug.Log(item.ItemCode);
                GameManager.player.equipped.EquipItem(item as Equip);
                GameManager.player.inventory.DropItem(item.itemType, at);
                GameManager.player.inventory.RefreshInven();
                GameManager.player.equipped.RefreshEquip();
            }
        }
        else
        {
            IsDoubleClicked = false;
            doubleClickedTime = Time.time;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
        {
            Info.ItemName.text = item._name;
            Info.ItemImage.sprite = ItemImage.sprite;
            Info.ItemDescription.text = item.Description;
            InfoUI.SetActive(true);
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InfoUI.SetActive(false);
    }
}
