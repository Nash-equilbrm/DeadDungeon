using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField] private float m_Speed = 20f;
    [SerializeField] private PlayerShooting m_PlayerShooting;
    
    private BoxCollider2D m_Coll;
    private bool m_Hit;
    private Vector2 m_Direction;
    private float m_LifeTime;
    private Animator m_Anim;


    private void Awake()
    {
        m_Anim = GetComponent<Animator>();
        m_Coll = GetComponent<BoxCollider2D>();
    }

    public void Explode()
    {
        m_Hit = true;
        m_Coll.enabled = false;
        m_Anim.SetTrigger("explode");
    }

    private void Update()
    {
        if (m_Hit) return;
        float movementSpeed = m_Speed * Time.deltaTime;

        // preventing the bullet go through gameObject
        RaycastHit2D hit = Physics2D.Raycast(transform.position, m_Direction, 2 * movementSpeed);
        if (hit) 
        {
            if (hit.collider.name == "Collision" || 
                hit.collider.gameObject.CompareTag("Enemy")) 
            {
                transform.position = hit.point;
                OnTriggerEnter2D(hit.collider);
            }
        }

        transform.Translate(movementSpeed, 0, 0);

        m_LifeTime += Time.deltaTime;

        if (m_LifeTime > 3)
        {
            Explode();
        }   
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {   
        if (collision.gameObject.layer == LayerMask.NameToLayer("Blocking"))
        {
            Explode();
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            Explode();
            MonsterHealth monsterHealth = collision.gameObject.transform.GetComponent<MonsterHealth>();
            monsterHealth.TakeDamage(m_PlayerShooting.m_ShootDamage);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Props"))
        {
            Explode();
            Prop prop = collision.gameObject.transform.GetComponent<Prop>();
            prop.TakeDamage(m_PlayerShooting.m_ShootDamage);
        }
    }

    public void SetDirection(Vector2 _direction)
    {   
        m_LifeTime = 0f;
        m_Direction = _direction;
        gameObject.SetActive(true);
        m_Hit = false;
        m_Coll.enabled = true;

        transform.right = m_Direction;
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
