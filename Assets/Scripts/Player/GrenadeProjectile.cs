using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeProjectile : MonoBehaviour
{
    [SerializeField] private float m_Speed = 20f;
    [SerializeField] private PlayerShooting m_PlayerShooting;
    [SerializeField] private AudioSource m_ExplosionAudio;
    
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
        m_ExplosionAudio.Play();
        m_Hit = true;
        m_Coll.enabled = false;
        m_Anim.SetTrigger("explode");

        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(transform.position, m_PlayerShooting.m_GrenadeRange);
        foreach(Collider2D coll in enemiesHit)
        {
            if (coll.gameObject.CompareTag("Enemy"))
            {
                MonsterHealth monsterHealth = coll.gameObject.transform.GetComponent<MonsterHealth>();
                monsterHealth.TakeDamage(m_PlayerShooting.m_GrenadeDamage);
            }
            if (coll.gameObject.layer == LayerMask.NameToLayer("Props"))
            {
                Prop prop = coll.gameObject.transform.GetComponent<Prop>();
                prop.TakeDamage(m_PlayerShooting.m_ShootDamage);
            }
        }
    }

    private void Update()
    {
        if (m_Hit) return;
        float movementSpeed = m_Speed * Time.deltaTime;

        // preventing the grenade go through gameObject
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

        transform.position += new Vector3(m_Direction.x, m_Direction.y, 0) * movementSpeed;
        transform.Rotate(0, 0, 360 * Time.deltaTime);  

        m_LifeTime += Time.deltaTime;

        if (m_LifeTime > 2)
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
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Props"))
        {
            Explode();
        }
    }

    public void SetDirection(Vector2 _direction)
    {   
        m_LifeTime = 0f;
        m_Direction = _direction.normalized;
        gameObject.SetActive(true);
        m_Hit = false;
        m_Coll.enabled = true;
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
