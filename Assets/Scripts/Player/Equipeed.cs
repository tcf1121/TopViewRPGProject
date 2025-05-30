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


    private void Equip()
    {
        for (int i = 0; i < 7; i++)
        {
            if (EquipPart[i] != null)
            {
                _part[i].sharedMesh = EquipPart[i].EquipMesh;
            }
            else
            {
                _part[i].sharedMesh = _nakedPart[i];
            }
        }

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
            Equip();
    }

}
