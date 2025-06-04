using UnityEngine;

public class Weapon : Equip
{
    public GameObject weoponPrefab;
    public WeoponType weoponType;

    void Awake() => Init();

    private new void Init()
    {
        EquipMesh = weoponPrefab.GetComponent<MeshFilter>().sharedMesh;
        base.Init();
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.gameObject.CompareTag("Player"))
    //     {
    //         //인벤토리 확인 후 비어 있으면 인벤토리에 아이템 추가
    //         Destroy(gameObject);
    //     }
    // }
}

public enum WeoponType
{
    OneHandSword,
    TwoHandSword
}