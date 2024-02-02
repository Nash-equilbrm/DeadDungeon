using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterMoving
{
    protected MonsterMovement m_Controller;

    public MonsterMoving(MonsterMovement monsterMovement)
    {
        m_Controller = monsterMovement;
    }

    public void BasicMove(Transform target)
    {
        if (target != null)
        {
            Vector3 dir = target.position - m_Controller.transform.position;

            Vector3 moveDelta = new Vector3(dir.x, dir.y, 0);

            moveDelta.Normalize();

            float step = Time.deltaTime * m_Controller.GetSpeed();
            float deltaY = moveDelta.y * step;
            float deltaX = moveDelta.x * step;

            RaycastHit2D m_Hit = Physics2D.BoxCast(m_Controller.GetCollider().bounds.center, m_Controller.GetCollider().size, 0,
                                    new Vector2(0, moveDelta.y),
                                    Mathf.Abs(deltaY),
                                    m_Controller.GetBlockLayerMask());
            if (m_Hit.collider == null)
            {
                m_Controller.transform.Translate(0, deltaY, 0);
            }

            m_Hit = Physics2D.BoxCast(m_Controller.GetCollider().bounds.center, m_Controller.GetCollider().size, 0,
                                    new Vector2(moveDelta.x, 0),
                                    Mathf.Abs(deltaX),
                                    m_Controller.GetBlockLayerMask());
            if (m_Hit.collider == null)
            {
                m_Controller.transform.Translate(deltaX, 0, 0);
            }
        }
    }

    public abstract void Move(Transform target);
   
}
