using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> normalMonster;
    [SerializeField] List<GameObject> bossMonster;
    private int _floor;
    private int _monNum;
    private int _monMin;
    private int _monMax;

    public void SetFloor(int floor)
    {
        _floor = floor;
        _monMin = _floor * 2 - 2;
        _monMax = _floor * 2;
    }

    public void SetNormalMonster(bool pitfall)
    {
        if (pitfall) _monNum = Random.Range(6, 10);
        else _monNum = Random.Range(1, 4);
    }

    public void SpawnNormalMonster(Vector2Int roompos, bool pitfall = false)
    {
        int xPos = (roompos.y - 10) * 10;
        int yPos = (roompos.x - 10) * 16;
        int minX = xPos - 4; int maxX = xPos + 5;
        int minY = yPos - 7; int maxY = yPos + 8;
        SetNormalMonster(pitfall);
        RoomManager.SetMonster(_monNum);
        for (int i = 0; i < _monNum; i++)
        {
            GameObject monster =
            Instantiate(normalMonster[Random.Range(_monMin, _monMax)]);
            monster.transform.position = new Vector3(Random.Range(minX, maxX),
            0, Random.Range(minY, maxY));
        }
    }

    public void SpawnBossMonster(Vector2Int roompos)
    {
        int xPos = (roompos.y - 10) * 10;
        int yPos = (roompos.x - 10) * 16;
        RoomManager.SetMonster(1);
        GameObject monster = Instantiate(bossMonster[_floor - 1]);
        monster.transform.position = new Vector3(xPos, 0, yPos);
    }
}
