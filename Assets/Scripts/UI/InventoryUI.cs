using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Toggle[] _toggle;
    [SerializeField] private List<ItemConpartment> _iconList;
    [SerializeField] private Inventory _inventory;

    private ItemType ItemType = ItemType.Equip;
    private List<Item> items;

    void Awake() => Init();

    public void setInven(Inventory inventory)
    {
        items = new();
        _inventory = inventory;
    }

    private void Init()
    {
        for (int i = 0; i < _iconList.Count; i++)
            _iconList[i].at = i;
    }

    public void OnEnable()
    {
        if (items == null)
            return;
        SetInventory();
        _toggle[0].isOn = true;
        ChangeType();
    }




    public void ChangeType()
    {
        for (int i = 0; i < _toggle.Length; i++)
        {
            ColorBlock colorBlock = _toggle[i].colors;
            if (_toggle[i].isOn)
            {
                ItemType = (ItemType)i;

                colorBlock.normalColor = Color.white;
            }
            else colorBlock.normalColor = Color.gray;

            _toggle[i].colors = colorBlock;
        }
        SetInventory();
    }

    public void SetInventory()
    {
        if (ItemType == ItemType.Equip)
            items = _inventory.Equipitems;
        else if (ItemType == ItemType.Use)
            items = _inventory.Useitems;
        else
            items = _inventory.Etcitems;

        for (int i = 0; i < 20; i++)
        {
            if (i < items.Count)
            {
                _iconList[i].Icon.SetActive(true);
                _iconList[i].ItemImage.sprite = items[i].sprite;
                _iconList[i].item = items[i];
            }
            else
            {
                _iconList[i].Icon.SetActive(false);
                _iconList[i].ItemImage.sprite = null;
                _iconList[i].item = null;
            }
        }
    }

    public void AddItem(Item item)
    {
        if (ItemType == item.itemType)
            items.Add(item);
        _iconList[items.Count].Icon.SetActive(true);
        _iconList[items.Count].ItemImage.sprite = item.sprite;
    }

    public void ExitButton()
    {
        GameManager.player.playerController.OnInven();

    }
}
