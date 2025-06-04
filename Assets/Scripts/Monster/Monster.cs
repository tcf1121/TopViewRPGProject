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
    public bool IsDie;
    UnityAction OnDie;
    public GameObject Player;

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
        if (!IsDie)
        {
            if (CanAttack)
            {
                if (!IsAttack)
                {
                    IsAttack = true;
                    Animator.SetTrigger("IsAttack");
                }
                Animator.SetBool("IsMove", false);
            }
            else
            {
                Animator.SetBool("IsMove", true);
                Move();
            }
        }
    }

    private void Init()
    {
        Player = GameObject.Find("Player");
        _currentHp = MaxHp;
        OnDie += Die;
    }

    private void Move()
    {
        Vector3 LookDirection = new Vector3(Player.transform.position.x,
        transform.position.y, Player.transform.position.z);
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
                OnDie?.Invoke();
        }
    }

    private void SetAttack(int value)
    {
        if (value == 1)
        {
            AttackRange.tag = "MonsterAttack";
        }
        else
        {
            AttackRange.tag = "Untagged";
            IsAttack = false;
        }
    }

    private void Die()
    {
        IsDie = true;
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
