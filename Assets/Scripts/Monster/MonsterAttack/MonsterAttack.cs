using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    [SerializeField] private Monster m_Monster;

    private IMonsterAttack m_AttackMethod;
    private float m_Damage;
    private float m_AttackRange;
    private float m_AttackRate;

    private float m_AttackRateCountDown = 0;

    private bool m_OnAttack = false;

    private void FixedUpdate()
    {
        if(m_Monster.GetCurrentStatus() == Enums.MonsterBehavior.ATTACK)
        {
            // countdown reach 0
            if(m_AttackRateCountDown <= 0)
            {
                // start attacking anim. At a specific keypoint of animation will trigger the actual attack method
                m_Monster.OnAtackUI();
                // While on attacking anim, OnAttack = true
                m_OnAttack = true;

                m_AttackRateCountDown = m_AttackRate;
            }
            // if still in countdown
            else
            {
                // if finished attack anim then start idle anim
                if (!m_OnAttack)
                {
                    m_Monster.OnIdleUI();
                }
                // countdown here
                m_AttackRateCountDown -= Time.deltaTime;
            }


            // Distance to current target
            float distanceToTarget = Vector3.Distance(m_Monster.transform.position, m_Monster.GetTarget().transform.position);
            // if not in attack range
            if (distanceToTarget > m_AttackRange)
            {

                // if finished attack anim then ...
                if (!m_OnAttack)
                {
                    // if in detect range, switch to APPROACH_FOR_TARGET status
                    if (distanceToTarget <= m_Monster.GetDetectTargetRange())
                    {
                        m_Monster.SetCurrentStatus(Enums.MonsterBehavior.APPROACH_FOR_TARGET);
                    }
                    // if not in detect range, switch to IDLE status
                    else
                    {
                        m_Monster.SetCurrentStatus(Enums.MonsterBehavior.IDLE);
                    }
                }
            }

        }

    }

    private void DoAttack()
    {
        m_AttackMethod.DoAttack(m_Monster.GetTarget());
        // finish attack anim set onAttack = false and countdown to 0
        m_OnAttack = false;
    }

    public void ConfigMonsterData(MonsterConfig config)
    {
        m_AttackMethod = GetAttackMethod(config);
        m_Damage = config.damage;
        m_AttackRange = config.attackRange;
        m_AttackRate = config.attackRate;
    }

    private IMonsterAttack GetAttackMethod(MonsterConfig config)
    {
        switch (config.attackType)
        {
            case Enums.MonsterAttackType.NORMAL:
                {
                    return new NormalAttack(this);
                }
            case Enums.MonsterAttackType.RANGE:
                {
                    return new RangeAttack(this);
                }
            case Enums.MonsterAttackType.EXPLODE:
                {
                    return new ExplodeAttack(this);
                }
            default:
                {
                    return null;
                }
        }
    }


    #region Getter setter
    public Monster GetMonster()
    {
        return m_Monster;
    }

    public float GetAttackRange()
    {
        return m_AttackRange;
    }

    public float GetDamage()
    {
        return m_Damage;
    }

    #endregion
}
