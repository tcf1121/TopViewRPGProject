using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject InventoryPrefab;
    private InventoryUI InventoryUI;
    public List<Item> Equipitems;
    public List<Item> Useitems;
    public List<Item> Etcitems;
    private int _maxNum = 20;

    void Awake() => Init();

    private void Init()
    {
        GameObject InvenObj = Instantiate(InventoryPrefab, GameObject.Find("PlayerUI").transform);
        InvenObj.name = "Inventory";
        InvenObj.SetActive(false);
        InventoryUI = InvenObj.GetComponent<InventoryUI>();
        InventoryUI.setInven(this);
        Equipitems = new();
        Useitems = new();
        Etcitems = new();
    }

    public void AddItem(String name)
    {
        GameObject itemprefab = Resources.Load<GameObject>($"Item/{name}");
        Item item = itemprefab.GetComponent<Item>();
        if (item.itemType == ItemType.Equip)
            Equipitems.Add(item);
        if (item.itemType == ItemType.Use)
            Useitems.Add(item);
        if (item.itemType == ItemType.Etc)
            Etcitems.Add(item);

        if (InventoryUI.gameObject.activeSelf)
            RefreshInven();
    }

    public void DropItem(ItemType itemType, int at)
    {
        if (itemType == ItemType.Equip)
            Equipitems.RemoveAt(at);
        if (itemType == ItemType.Use)
            Useitems.RemoveAt(at);
        if (itemType == ItemType.Etc)
            Etcitems.RemoveAt(at);
    }

    public bool CheckInventory(Item item)
    {
        if (item.itemType == ItemType.Equip)
            if (Equipitems.Count < _maxNum)
                return true;
        if (item.itemType == ItemType.Use)
            if (Useitems.Count < _maxNum)
                return true;
        if (item.itemType == ItemType.Etc)
            if (Etcitems.Count < _maxNum)
                return true;
        return false;
    }

    public bool getUIOn()
    {
        return InventoryUI.gameObject.activeSelf;
    }

    public void OpenInventory(bool value)
    {
        InventoryUI.gameObject.SetActive(value);
    }

    public void RefreshInven()
    {
        InventoryUI.SetInventory();
    }
}
