using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonAttackRange : MonoBehaviour
{
    [SerializeField] private Monster _monster;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            _monster.CanAttack = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            _monster.CanAttack = false;
    }
}
