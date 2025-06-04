using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private int _floor;

    public void SetFloor(int floor)
    {
        _floor = floor;
    }

    void OnTriggerEnter(Collider other)
    {
        //다음 층으로 이동
        if (other.gameObject.CompareTag("Pick"))
        {
            Destroy(gameObject);
            GameManager.Dungeon.EndDungeon();
            GameManager.Dungeon.StartDungeon(_floor + 1);
        }

    }

}
