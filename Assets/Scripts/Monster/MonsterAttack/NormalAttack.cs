using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttack : IMonsterAttack
{
    private MonsterAttack m_MonsterAttack;

    public NormalAttack(MonsterAttack monsterAttack)
    {
        this.m_MonsterAttack = monsterAttack;
    }

    public void DoAttack(GameObject target)
    {
        if (TargetInRange(target))
        {
            // Do attack
            target.GetComponent<PlayerHealth>().TakeDamage((int)m_MonsterAttack.GetDamage());
        }
    }

    private bool TargetInRange(GameObject target)
    {
        Vector3 curPosition = m_MonsterAttack.transform.position;

        float distance = Vector3.Distance(curPosition, target.transform.position);
        float attackRange = m_MonsterAttack.GetAttackRange();

        return distance <= attackRange;

    }

}
