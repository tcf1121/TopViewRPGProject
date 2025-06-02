using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public ItemType itemType;
    public String _name;
    public String Description;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //인벤토리 확인 후 비어 있으면 인벤토리에 아이템 추가
            Destroy(gameObject);
        }
    }
}

public enum ItemType
{
    Equip,
    Use,
    Etc
}