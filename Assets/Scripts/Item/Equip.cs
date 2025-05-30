using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equip : Item
{
    public EquipType equipType;
    public int Hp;
    public int Mp;
    public int Damage;
    public int Defense;
    public int Speed;
    public Mesh EquipMesh;
}

public enum EquipType
{
    Helmet,
    Body,
    Legs,
    Gauntlets,
    Boots,
    Cape,
    Weapon
}
