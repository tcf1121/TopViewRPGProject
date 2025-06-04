using UnityEngine;

public class Equip : Item
{
    private ParticleSystemRenderer _particleRenderer;
    public EquipType equipType;
    public State state;
    public Mesh EquipMesh;

    void Awake() => Init();

    protected new void Init()
    {
        base.Init();
        _particleRenderer = GetComponent<ParticleSystemRenderer>();
        _particleRenderer.mesh = EquipMesh;
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

public enum EquipType
{
    Helmet,
    Body,
    Legs,
    Gauntlets,
    Boots,
    Weapon
}
