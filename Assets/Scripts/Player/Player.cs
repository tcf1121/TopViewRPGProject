using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Inventory inventory;
    public Equipped equipped;
    public PlayerStatus playerStatus;
    public PlayerController playerController;

    void Awake() => Init();

    private void Init()
    {
        inventory = GetComponent<Inventory>();
        equipped = GetComponent<Equipped>();
        playerStatus = GetComponent<PlayerStatus>();
        playerController = GetComponent<PlayerController>();
    }
}
