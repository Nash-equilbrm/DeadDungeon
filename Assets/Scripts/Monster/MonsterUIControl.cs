using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Monster
{
    private Animator m_Animator;
    [SerializeField] private AudioSource m_AudioSource;

    private Vector3 m_PrevPos;

    public void OnAtackUI()
    {
        m_AudioSource.clip = m_MonsterConfig.attackSound;
        m_AudioSource.Play();

        m_Animator.SetFloat("Speed", 0f);
        m_Animator.SetBool("OnAttack", true);

    }

    public void OnMovingUI()
    {
        m_Animator.SetBool("OnAttack", false);
        m_Animator.SetFloat("Speed", 1f);
    }

    public void OnIdleUI()
    {
        m_Animator.SetBool("OnAttack", false);
        m_Animator.SetFloat("Speed", 0f);
    }

    public void OnDeadUI()
    {
        m_AudioSource.clip = m_MonsterConfig.deadSound;
        m_AudioSource.Play();

        m_Animator.SetBool("Dead", true);
        m_Animator.SetFloat("Speed", 0);
        m_Animator.SetBool("OnAttack", false);
    }
}
