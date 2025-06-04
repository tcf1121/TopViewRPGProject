using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStatus : MonoBehaviour
{
    public Player player;

    [field: SerializeField]
    [field: Range(0, 10)]
    public float MoveSpeed { get; set; }

    [field: SerializeField]
    [field: Range(0, 10)]
    public float JumpPower { get; set; }

    [field: SerializeField]
    [field: Range(0, 10)]
    public float DashPower { get; set; }


    public int Level;
    private int currenthp;
    public int CurrentHP { get { return currenthp; } set { currenthp = value; ChangeHp.Invoke(); } }
    UnityAction ChangeHp;
    private int currentmp;
    public int CurrentMP { get { return currentmp; } set { currentmp = value; ChangeMp.Invoke(); } }
    UnityAction ChangeMp;
    UnityAction IsDie;

    public State state;
    public Vector2 InputDirection;

    public bool IsJump;
    public bool[] IsDoing = new bool[7];
    public bool IsOnUI;

    [SerializeField] private GameObject DownBar;
    [SerializeField] private Material material;
    private MaterialPropertyBlock mb;

    void Awake() => Init();


    private void Init()
    {
        IsDie += Die;
        ChangeHp += SetHp;
        ChangeMp += SetMp;
        state.Hp = 30;
        state.Mp = 10;
        state.Damage = 5;
        state.Defense = 2;
        state.Speed = 0;
        CurrentHP = state.Hp;
        CurrentMP = state.Mp;
        Instantiate(DownBar);
    }

    public void SetHp()
    {
        material.SetFloat("_FillLevel1", (float)CurrentHP / (float)state.Hp);
        //mb.SetFloat("_FillLevel1", (float)CurrentHP / (float)state.Hp);
    }

    public void SetMp()
    {
        material.SetFloat("_FillLevel", (float)CurrentHP / (float)state.Hp);
        //mb.SetFloat("_FillLevel", (float)CurrentHP / (float)state.Mp);
    }

    public void SpeedUp()
    {
        MoveSpeed = 5f + 0.1f * state.Speed;
        DashPower = 3f + 0.1f * state.Speed;
    }

    public void Die()
    {
        // 마을로 이동
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MonsterAttack"))
        {
            int MonsterDamage = other.GetComponentInParent<Monster>().Damage - state.Defense;
            if (MonsterDamage < 0) MonsterDamage = 0;
            CurrentHP -= MonsterDamage;
            if (CurrentHP < 0)
                IsDie.Invoke();
        }
    }
}



public enum doName
{
    Dash,
    Skill1,
    Skill2,
    Skill3,
    Num1,
    Num2,
    Num3
}

[Serializable]
public struct State
{
    public int Hp;
    public int Mp;
    public int Damage;
    public int Defense;
    public int Speed;

    public void AddState(State state)
    {
        this.Hp += state.Hp;
        this.Mp += state.Mp;
        this.Damage += state.Damage;
        this.Defense += state.Defense;
        this.Speed += state.Speed;
    }

    public void SubState(State state)
    {
        this.Hp -= state.Hp;
        this.Mp -= state.Mp;
        this.Damage -= state.Damage;
        this.Defense -= state.Defense;
        this.Speed -= state.Speed;
    }

}
