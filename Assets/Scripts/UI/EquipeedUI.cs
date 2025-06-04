using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipeedUI : MonoBehaviour
{
    [SerializeField] private List<EquipItem> _iconList;
    [SerializeField] private Equipped _equipped;

    public void setEquipeed(Equipped equipped)
    {
        _equipped = equipped;
    }

    public void setEquip()
    {
        for (int i = 0; i < 5; i++)
        {
            if (_equipped.EquipPart[i] != null)
            {
                _iconList[i].equip = _equipped.EquipPart[i];
                _iconList[i].Icon.SetActive(true);
                _iconList[i].SetImage();
            }
            else
            {
                _iconList[i].equip = null;
                _iconList[i].Icon.SetActive(false);
            }
        }
        if (_equipped._weaponEquip != null)
        {
            Debug.Log(_equipped._weaponEquip);
            _iconList[5].weapon = _equipped._weaponEquip.GetComponent<Weapon>();
            _iconList[5].Icon.SetActive(true);
            _iconList[5].SetImage(true);
        }
        else
        {
            _iconList[5].equip = null;
            _iconList[5].Icon.SetActive(false);
        }
    }

    public void ExitButton()
    {
        GameManager.player.playerController.OnEquip();

    }
}
