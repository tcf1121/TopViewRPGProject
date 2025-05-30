using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    private int _floor;
    // 가지고 있는 아이템 리스트

    void OnDestroy()
    {
        RoomManager.DieMonster();
        // 맵에 아이템 생성
    }

    public void SetFloor(int floor)
    {
        _floor = floor;
    }

    public void SetItem()
    {
        // 상자 안에 아이템 생성성
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Attack"))
            Destroy(gameObject);

    }
}
