using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipped : MonoBehaviour
{
    [Header("Part")]
    [SerializeField] private SkinnedMeshRenderer[] _part;

    [Header("Naked")]
    [SerializeField] private Mesh[] _nakedPart;

    [Header("Equipped")]
    [SerializeField] private Equip[] EquipPart;

    [Header("Weapon")]
    [SerializeField] private GameObject _weaponPart;
    [SerializeField] private GameObject _weaponEquip;

    private void Equip(int partNum)
    {
        if (partNum == 5)
        {
            foreach (Transform child in _weaponPart.transform)
                Destroy(child.gameObject);
            if (_weaponEquip != null)
                Instantiate(_weaponEquip, _weaponPart.transform);
        }
        else
        {
            if (EquipPart[partNum] != null)
                _part[partNum].sharedMesh = EquipPart[partNum].EquipMesh;
            else
                _part[partNum].sharedMesh = _nakedPart[partNum];
        }

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
            for (int i = 0; i < 6; i++)
                Equip(i);
    }

}
