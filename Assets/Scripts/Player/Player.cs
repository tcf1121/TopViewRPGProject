using UnityEngine;

public class Player : MonoBehaviour
{
    public Inventory inventory;
    public Equipped equipped;
    public PlayerStatus playerStatus;
    public PlayerController playerController;
    public Rigidbody Rigid;
    public Animator Animator;
    public GameObject AttackRange;
    public GameObject PickRange;

    void Awake() => Init();

    private void Init()
    {
        inventory = GetComponent<Inventory>();
        equipped = GetComponent<Equipped>();
        playerStatus = GetComponent<PlayerStatus>();
        playerController = GetComponent<PlayerController>();
        Rigid = GetComponent<Rigidbody>();
        Animator = GetComponent<Animator>();
    }
}
