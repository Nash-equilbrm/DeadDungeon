using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterJumping : MonsterMoving
{
    private float m_StayStillDuration = 1f;
    private float m_StayStillDurationCount;


    private float m_JumpingDuration = 0.5f;
    private float m_JumpingDurationCount;

    private bool m_Jumping;
    private bool m_StayStill;
    public MonsterJumping(MonsterMovement controller): base(controller)
    {
        m_Jumping = false;

    }

    public override void Move(Transform target)
    {
        // if status = idle
        if (!m_Jumping && !m_StayStill)
        {
            OnStartJumping(target);
        }
        // if is jumping
        else if (m_Jumping)
        {
            OnJumping(target);
        }
        // if is staying still
        else if (m_StayStill)
        {
            OnDoNothing();
        }
       
    }
    private void OnStartJumping(Transform target)
    {
        m_JumpingDurationCount = 0;
        m_Jumping = true;
        m_StayStill = false;
        OnJumping(target);
    }

    private void OnJumping(Transform target)
    {
        m_Controller.GetMonster().OnMovingUI();
        if (m_JumpingDurationCount >= m_JumpingDuration)
        {
            OnEndJumping();
        }
        else
        {
            BasicMove(target);
            m_JumpingDurationCount += Time.deltaTime;
        }
    }

    // == OnStartDoNothing :V
    private void OnEndJumping()
    {
        m_Jumping = false;
        m_StayStill = true;
        m_StayStillDurationCount = 0;

        OnDoNothing();


    }

    private void OnDoNothing()
    {

        if (m_StayStillDurationCount >= m_StayStillDuration)
        {
            ReturnToIdle();
        }
        else
        {
            m_Controller.GetMonster().OnIdleUI();

            m_StayStillDurationCount += Time.deltaTime;
        }
    }

    private void ReturnToIdle()
    {
        m_Jumping = false;
        m_StayStill = false;
        m_StayStillDurationCount = 0;
        m_JumpingDurationCount = 0;


    }
}
