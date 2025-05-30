using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwanTester : MonoBehaviour
{
    [SerializeField] MonsterSpawner _monsterSpawner;

    void Awake()
    {
        _monsterSpawner.SetFloor(1);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
            _monsterSpawner.SpawnNormalMonster(new Vector2Int(10, 10));
        if (Input.GetKeyDown(KeyCode.K))
            _monsterSpawner.SpawnNormalMonster(new Vector2Int(10, 10), true);
        if (Input.GetKeyDown(KeyCode.L))
            _monsterSpawner.SpawnBossMonster(new Vector2Int(10, 10));
    }
}
