using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Monster : MonoBehaviour
{
    public string Name;
    public MonsterType monsterType;
    public int Damage;
    public int MaxHp;
    public int MoveSpeed;
    public int Exp;
    public Animator Animator;
    private int _currentHp;
    public GameObject AttackRange;
    public bool CanAttack;
    public bool IsAttack;
    UnityAction IsDie;

    void Awake() => Init();

    void OnEnable()
    {

    }

    void OnDisable()
    {
        RoomManager.DieMonster();
    }

    void Update()
    {
        if (CanAttack)
        {
            if (!IsAttack)
                Animator.SetTrigger("IsAttack");
            Animator.SetBool("IsMove", true);
        }
        else
        {
            Animator.SetBool("IsMove", true);
            Move();
        }
    }

    private void Init()
    {
        _currentHp = MaxHp;
        IsDie += Die;
    }

    private void Move()
    {
        Vector3 LookDirection = new Vector3(GameManager.player.gameObject.transform.position.x,
        transform.position.y, GameManager.player.gameObject.transform.position.z);
        transform.LookAt(LookDirection);
        transform.Translate(Vector3.forward * Time.deltaTime * MoveSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Attack"))
        {
            Animator.SetTrigger("IsHit");
            int PlayerDamage = other.GetComponentInParent<PlayerStatus>().state.Damage;
            _currentHp -= PlayerDamage;
            if (_currentHp < 0)
                IsDie?.Invoke();
        }
    }

    private void SetAttack(int value)
    {
        if (value == 1)
        {
            AttackRange.tag = "MonsterAttack";
            IsAttack = true;
        }
        else
        {
            AttackRange.tag = "Untagged";
            IsAttack = false;
        }
    }

    private void Die()
    {
        Animator.SetTrigger("IsDie");
    }

    private void DestroyMon()
    {
        Destroy(gameObject);
    }
}

public enum MonsterType
{
    Normal, // 기본 몬스터
    Boss,   // 보스 몬스터
    Special // 특수 몬스터 (ex 미믹)
}
