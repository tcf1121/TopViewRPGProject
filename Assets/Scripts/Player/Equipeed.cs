using UnityEngine;

public class Equipped : MonoBehaviour
{
    public Player player;

    [SerializeField] private GameObject EquipeedPrefab;
    private EquipeedUI EquipeedUI;

    [Header("Part")]
    [SerializeField] private SkinnedMeshRenderer[] _part;
    [SerializeField] private GameObject _hairPrefab;

    [Header("Naked")]
    [SerializeField] private Mesh[] _nakedPart;

    [Header("Equipped")]
    [SerializeField] public Equip[] EquipPart;

    [Header("Weapon")]
    [SerializeField] private GameObject _weaponPart;
    [SerializeField] public Weapon _weaponEquip;

    void Awake() => Init();

    private void Init()
    {
        GameObject EquippedObj = Instantiate(EquipeedPrefab, GameObject.Find("PlayerUI").transform);
        EquippedObj.name = "Equipped";
        EquippedObj.SetActive(false);
        EquipeedUI = EquippedObj.GetComponent<EquipeedUI>();
        EquipeedUI.setEquipeed(this);
    }


    public void EquipItem(Equip equip)
    {
        UnEquipItem((int)equip.equipType);
        if (equip.equipType == EquipType.Weapon)
        {
            GameObject weaponObj = Resources.Load<GameObject>($"Item/{equip.ItemCode}");
            _weaponEquip = weaponObj.GetComponent<Weapon>();
            player.playerStatus.state.AddState(_weaponEquip.state);
            player.playerStatus.SpeedUp();
            Instantiate(_weaponEquip.weoponPrefab, _weaponPart.transform);
        }
        else
        {
            if (equip.equipType == 0)
                _hairPrefab.SetActive(false);
            EquipPart[(int)equip.equipType] = equip;
            player.playerStatus.state.AddState(equip.state);
            player.playerStatus.SpeedUp();
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
                player.playerStatus.state.SubState(_weaponEquip.state);
                player.playerStatus.SpeedUp();
                GameManager.player.inventory.AddItem(_weaponEquip.ItemCode);
                _weaponEquip = null;
            }
        }
        else
        {
            if (partNum == 0)
                _hairPrefab.SetActive(true);
            if (EquipPart[partNum] != null)
            {
                player.playerStatus.state.SubState(EquipPart[partNum].state);
                player.playerStatus.SpeedUp();
                GameManager.player.inventory.AddItem(EquipPart[partNum].ItemCode);
                EquipPart[partNum] = null;
                _part[partNum].sharedMesh = _nakedPart[partNum];
            }
        }
    }

    public bool getUIOn()
    {
        return EquipeedUI.gameObject.activeSelf;
    }

    public void OpenEquipeed(bool value)
    {
        EquipeedUI.gameObject.SetActive(value);
    }

    public void RefreshEquip()
    {
        EquipeedUI.setEquip();
    }
}
