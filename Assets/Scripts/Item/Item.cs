using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public ItemType itemType;
    public String ItemCode;
    public String _name;
    public String Description;
    public Sprite sprite;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick"))
        {
            Player player = GameObject.Find("Player").GetComponent<Player>();
            player.inventory.AddItem(gameObject.name);
            // gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    void Awake()
    {
        Init();
    }

    protected void Init()
    {
        ItemCode = gameObject.name;
    }

}

public enum ItemType
{
    Equip,
    Use,
    Etc
}