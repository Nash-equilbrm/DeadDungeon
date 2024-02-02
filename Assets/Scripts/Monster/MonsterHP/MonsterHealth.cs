using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHealth : MonoBehaviour
{
    [SerializeField] private Monster m_Monster;
    [SerializeField] private SpriteRenderer m_SpriteRenderer;

    private Color originColor;
    private float m_StartHealth;
    private float m_CurrentHealth;
    private float m_HurtDelayTime = 0.2f;


    private void OnEnable()
    {
        originColor = m_SpriteRenderer.color;
    }

    private void OnDeath()
    {
        MonsterManager.Instance.RemoveMonster(m_Monster);
        m_Monster.OnDeadUI();
        m_Monster.SetCurrentStatus(Enums.MonsterBehavior.DEAD);
    }

    private void KillMonster()
    {
        Destroy(gameObject);
    }

    private IEnumerator Hurt()
    {
        m_SpriteRenderer.color = Color.red;

        yield return new WaitForSeconds(m_HurtDelayTime);

        m_SpriteRenderer.color = originColor;
    }


    public void ConfigMonsterData(MonsterConfig config)
    {
        SetStartHealth(config.health);
    }

    private void SetStartHealth(float amount)
    {
        m_StartHealth = amount;
        m_CurrentHealth = m_StartHealth;
    }

    public void TakeDamage(float amount)
    {
        m_CurrentHealth -= amount;
        if (m_CurrentHealth <= 0)
        {
            OnDeath();
        }
        else
        {
            StartCoroutine(Hurt());
        }
    }

}
