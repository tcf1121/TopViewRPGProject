using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipItem : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject Icon;
    public Image ItemImage;
    public Equip equip;
    public Weapon weapon;
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

    public void SetImage(bool type = false)
    {
        if (type) ItemImage.sprite = weapon.sprite;
        else ItemImage.sprite = equip.sprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if ((Time.time - doubleClickedTime) < interval)
        {
            IsDoubleClicked = true;
            doubleClickedTime = -1.0f;

            if (equip != null || weapon != null)
            {
                GameManager.player.equipped.UnEquipItem(at);
                GameManager.player.equipped.RefreshEquip();
                GameManager.player.inventory.RefreshInven();
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
        if (equip != null)
        {
            Info.ItemName.text = equip._name;
            Info.ItemImage.sprite = ItemImage.sprite;
            Info.ItemDescription.text = equip.Description;
            InfoUI.SetActive(true);
        }
        else if (weapon != null)
        {
            Info.ItemName.text = weapon._name;
            Info.ItemImage.sprite = ItemImage.sprite;
            Info.ItemDescription.text = weapon.Description;
            InfoUI.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InfoUI.SetActive(false);
    }
}
