using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerHealth : MonoBehaviour
{   
    [SerializeField] private Behaviour[] m_Components;
    [SerializeField] private HealthBar m_HealthBar;
    [SerializeField] private Transform m_Effect;
    [SerializeField] private GameObject m_DieMenu;
    [SerializeField] private AudioSource m_HurtSound;

    private SpriteRenderer m_Sprite;
    private Animator m_Animator;
    private PlayerInventory m_Inventory;
    private SaveSystem m_Save;

    private int m_StartingHealth;
    private int m_CurrentHealth;
    private float m_HurtDelayTime;
    private int m_MedkitHeal;


    private bool m_Invincible = false;
    private bool m_IsDeath = false;

    private void Start() 
    {
        m_Sprite = GetComponent<SpriteRenderer>();
        m_Animator = GetComponent<Animator>();
        m_Inventory = GetComponent<PlayerInventory>();
        m_Save = GetComponent<SaveSystem>();

        PlayerStat stat = m_Save.LoadPlayerStat();
        m_StartingHealth = stat.startingHealth;
        m_CurrentHealth = stat.currentHealth;
        m_HurtDelayTime = stat.hurtDelayTime; 
        m_MedkitHeal = stat.medkitHeal;

        if (m_CurrentHealth == 0)
            m_CurrentHealth = m_StartingHealth;

        m_HealthBar.SetMaxHealth(m_StartingHealth);
        m_HealthBar.SetHealth(m_CurrentHealth);
    }

    public PlayerStat SaveStat(PlayerStat stat)
    {
        stat.startingHealth = m_StartingHealth;
        stat.currentHealth = m_CurrentHealth;
        stat.hurtDelayTime = m_HurtDelayTime; 
        stat.medkitHeal = m_MedkitHeal;

        return stat;
    }

    private void Update()
    {
        if  (m_IsDeath)
            return;

        if (Input.GetKeyDown(KeyCode.Q) && m_Inventory.CanHeal())
            Heal();
    }
    private void Heal()
    {
        Animator effectAnim = m_Effect.GetComponent<Animator>();
        effectAnim.SetTrigger("heal");
        m_CurrentHealth += m_MedkitHeal;
        if (m_CurrentHealth > m_StartingHealth)
            m_CurrentHealth = m_StartingHealth;

        m_HealthBar.SetHealth(m_CurrentHealth); 
    }
    public void TakeDamage(int damage)
    {
        if (m_Invincible == true || m_IsDeath)
            return;

        m_HurtSound.Play();
        m_Invincible = true;
        m_CurrentHealth = m_CurrentHealth - damage;

        if (m_CurrentHealth < 0)
            m_CurrentHealth = 0;

        m_HealthBar.SetHealth(m_CurrentHealth);
        
        if (m_CurrentHealth > 0)
            StartCoroutine(Hurt());
        else 
            Die();
    }

    private IEnumerator Hurt()
    {
        Color originColor = m_Sprite.color;
        m_Sprite.color = Color.red;

        yield return new WaitForSeconds(m_HurtDelayTime);

        for (float t = 0; t < 1.0f; t += Time.deltaTime/m_HurtDelayTime)
        {
            m_Sprite.color = Color.Lerp(Color.red, originColor, t);
            yield return null;
        }

        m_Sprite.color = originColor;
        m_Invincible = false;
    }

    public void Die()
    {
        if (m_IsDeath)
            return;
            
        m_IsDeath = true;
        m_HealthBar.SetHealth(0);
        m_Animator.SetTrigger("death");
        foreach (Behaviour component in m_Components)
            component.enabled = false;  

        m_DieMenu.SetActive(true);
    }
}
