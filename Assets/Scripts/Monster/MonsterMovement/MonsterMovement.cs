using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    [SerializeField] private Monster m_Monster;
    [SerializeField] private LayerMask m_BlockLayerMask;
    [SerializeField] private BoxCollider2D m_BoxCollider;
    private float m_Speed;
    private float m_AttackRange;


    
    private MonsterMoving m_MovingMethod;


    private void FixedUpdate()
    {
        Enums.MonsterBehavior monsterBehavior = m_Monster.GetCurrentStatus();

        switch (m_Monster.GetCurrentStatus())
        {
            case Enums.MonsterBehavior.IDLE:
                {
                    m_Monster.OnIdleUI();
                    DetectTarget();

                    // if status changed after the methods was apply, return to update the status in the next update 
                    if(monsterBehavior != m_Monster.GetCurrentStatus())
                    {
                        return;
                    }

                    if (m_Monster.GetTarget() != null)
                    {
                        m_Monster.SetCurrentStatus(Enums.MonsterBehavior.APPROACH_FOR_TARGET);
                    }
                    else {
                        m_Monster.SetCurrentStatus(Enums.MonsterBehavior.IDLE);

                    }
                    return;
                }
            case Enums.MonsterBehavior.APPROACH_FOR_TARGET:
                {
                    Move();

                    // if status changed after the methods was apply, return to update the status in the next update 
                    if (monsterBehavior != m_Monster.GetCurrentStatus())
                    {
                        return;
                    }

                    // get distance to current target
                    float distanceToTarget = Vector3.Distance(m_Monster.transform.position, m_Monster.GetTarget().transform.position);
                    if (distanceToTarget <= m_AttackRange)
                    {
                        m_Monster.SetCurrentStatus(Enums.MonsterBehavior.ATTACK);
                    }
                    else if (distanceToTarget <= m_Monster.GetDetectTargetRange())
                    {
                        m_Monster.SetCurrentStatus(Enums.MonsterBehavior.APPROACH_FOR_TARGET);
                    }
                    else
                    {
                        m_Monster.SetCurrentStatus(Enums.MonsterBehavior.IDLE);
                    }

                    return;
                }
            
            case Enums.MonsterBehavior.DEAD:
                {
                    m_BoxCollider.enabled = false;
                    return;
                }
           
            default:
                {
                    return;
                }
        }
    }

    

    public void Move()
    {
        m_MovingMethod.Move(m_Monster.GetTarget().transform);
    }

    public void ConfigMonsterData(MonsterConfig config)
    {
        m_Speed = config.speed;
        m_AttackRange = config.attackRange;
        m_MovingMethod = GetMovingMethod(config);
        // Later for further develop

    }


    private MonsterMoving GetMovingMethod(MonsterConfig config)
    {
        switch (config.movementType)
        {
            case Enums.MonsterMovementType.WALKING:
                {
                    return new MonsterWalking(this);
                }
            case Enums.MonsterMovementType.JUMPING:
                {
                    return new MonsterJumping(this);
                }
            default:
                {
                    return null;
                }
        }
    }


    private void DetectTarget()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(m_Monster.transform.position, m_Monster.GetDetectTargetRange(), m_Monster.GetPlayerMask());
        // if detect no target, return false
        if (hitColliders.Length == 0)
        {
            m_Monster.SetTarget(null);
            return;
        }

        float minDistance = float.MaxValue;

        foreach (Collider2D hitCollider in hitColliders)
        {
            float distanceToHitCollider = Vector2.Distance(m_Monster.transform.position, hitCollider.gameObject.transform.position);
            if (minDistance > distanceToHitCollider)
            {
                minDistance = distanceToHitCollider;
                m_Monster.SetTarget(hitCollider.gameObject);
            }
        }
    }



    public float GetSpeed()
    {
        return m_Speed;
    }

    public BoxCollider2D GetCollider()
    {
        return m_BoxCollider;
    }

    public LayerMask GetBlockLayerMask()
    {
        return m_BlockLayerMask;
    }

    public Monster GetMonster()
    {
        return m_Monster;
    }
}
