using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impaler : MonoBehaviour
{
    [SerializeField] private float m_DelayTime = 2f;
    [SerializeField] private float m_CooldownTimer = 0f;
    [SerializeField] private int m_Damage = 1;

    private Animator m_Anim;
    
    private bool m_IsPlayerInArea = false;
    private PlayerHealth m_PlayerHealth;

    private void Start()
    {
        m_Anim = GetComponent<Animator>();        
    }

    private void Update()
    {
        if (m_Anim.GetBool("active") && m_IsPlayerInArea)
        {
            m_PlayerHealth.TakeDamage(m_Damage);
        }
        
        if (m_CooldownTimer > m_DelayTime)
        {
            ActiveTrap();
        }
        else
        {
            m_CooldownTimer += Time.deltaTime;
        }
    }

    private void ActiveTrap()
    {
        m_Anim.SetBool("active", true);
    }

    private void DeactiveTrap()
    {
        m_Anim.SetBool("active", false);
        m_CooldownTimer = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_IsPlayerInArea = true;
            if (m_PlayerHealth == null)
                m_PlayerHealth = collision.gameObject.transform.GetComponent<PlayerHealth>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_IsPlayerInArea = false;
        }    
    }
}
