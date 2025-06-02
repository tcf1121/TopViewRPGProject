using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipped : MonoBehaviour
{

    [Header("Part")]
    [SerializeField] private SkinnedMeshRenderer[] _part;
    [SerializeField] private GameObject _hairPrefab;

    [Header("Naked")]
    [SerializeField] private Mesh[] _nakedPart;

    [Header("Equipped")]
    [SerializeField] private Equip[] EquipPart;

    [Header("Weapon")]
    [SerializeField] private GameObject _weaponPart;
    [SerializeField] private GameObject _weaponEquip;

    public void EquipItem(Equip equip)
    {
        UnEquipItem((int)equip.equipType);
        if (equip.equipType == EquipType.Weapon)
        {
            Debug.Log(equip.ItemCode);
            GameObject weaponPrefab = Resources.Load<GameObject>($"Item/{equip.ItemCode}");
            _weaponEquip = weaponPrefab.GetComponent<Weopon>().weoponPrefab;

            Instantiate(_weaponEquip, _weaponPart.transform);
        }
        else
        {
            if (equip.equipType == 0)
                _hairPrefab.SetActive(false);
            EquipPart[(int)equip.equipType] = equip;
            _part[(int)equip.equipType].sharedMesh = EquipPart[(int)equip.equipType].EquipMesh;
        }
    }

    public void UnEquipItem(int partNum)
    {
        if (partNum == 5)
        {
            foreach (Transform child in _weaponPart.transform)
                Destroy(child.gameObject);
            if (_weaponEquip != null)
            {
                GameManager.player.inventory.AddItem(_weaponEquip.name);
                _weaponEquip = null;
            }
        }
        else
        {
            if (partNum == 0)
                _hairPrefab.SetActive(true);
            if (EquipPart[partNum] != null)
            {
                GameManager.player.inventory.AddItem(_part[partNum].gameObject.name);
                EquipPart[partNum] = null;
                _part[partNum].sharedMesh = _nakedPart[partNum];
            }
        }
    }

}
