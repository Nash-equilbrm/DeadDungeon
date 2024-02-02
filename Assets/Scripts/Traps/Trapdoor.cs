using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trapdoor : MonoBehaviour
{
    [SerializeField] private float m_DelayTime = 2f;

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
            m_PlayerHealth.Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_IsPlayerInArea = true;
            
            if (!m_Anim.GetBool("active"))
            {
                m_PlayerHealth = collision.gameObject.transform.GetComponent<PlayerHealth>();
                StartCoroutine(ActiveTrap());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_IsPlayerInArea = false;
        }    
    }

    private IEnumerator ActiveTrap()
    {
        yield return new WaitForSeconds(m_DelayTime);
        m_Anim.SetBool("active", true);
    }
}
