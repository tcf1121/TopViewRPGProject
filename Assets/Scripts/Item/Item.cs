using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public ItemType itemType;
    public String _name;
    public String Description;
}

public enum ItemType
{
    Equip,
    Use,
    Etc
}