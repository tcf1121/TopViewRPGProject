using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public string Name;
    public MonsterType monsterType;
    public int Damage;
    public int MaxHp;
    public int MoveSpeed;
    public int Exp;

    private int _currentHp;

    void Awake() => Init();

    void OnEnable()
    {

    }

    void OnDisable()
    {
        RoomManager.DieMonster();
    }

    private void Init()
    {
        _currentHp = MaxHp;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Attack"))
            Destroy(gameObject);

    }
}

public enum MonsterType
{
    Normal, // 기본 몬스터
    Boss,   // 보스 몬스터
    Special // 특수 몬스터 (ex 미믹)
}
