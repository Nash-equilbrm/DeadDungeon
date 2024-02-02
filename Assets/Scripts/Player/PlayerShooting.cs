using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private Transform m_Gun;

    [SerializeField] private GameObject[] m_Bullets;
    [SerializeField] private float m_ShootCooldown = 0.1f;
    [SerializeField] private GameObject[] m_Grenades;
    [SerializeField] private float m_GrenadeCooldown = 0.1f;
    [SerializeField] private Transform m_FirePoint;

    public float m_ShootDamage = 2f;
    public float m_GrenadeDamage = 100f;
    public float m_GrenadeRange = 1.75f;

    private SpriteRenderer m_GunSprite;
    private PlayerInventory m_Inventory;
    private Vector2 m_Direction;

    private float m_ShootCooldownTimer;
    private float m_GrenadeCooldownTimer;


    private void Start()
    {
        m_GunSprite = m_Gun.GetComponent<SpriteRenderer>();
        m_Inventory = GetComponent<PlayerInventory>();
        m_ShootCooldownTimer = m_ShootCooldown;
        m_GrenadeCooldownTimer = m_GrenadeCooldown;
    }

    private void Update()
    {
        FaceMouse();

        if (Input.GetMouseButton(0) && m_ShootCooldownTimer > m_ShootCooldown)
        {
            if (m_Inventory.CanShoot())
                Shoot();
        }

        if (m_ShootCooldownTimer <= m_ShootCooldown)
        {
            m_ShootCooldownTimer += Time.deltaTime;
        }

        if (Input.GetMouseButtonDown(1) && m_GrenadeCooldownTimer > m_GrenadeCooldown)
        {
            if (m_Inventory.CanThrow())
                Throw();
        }

        if (m_GrenadeCooldownTimer <= m_GrenadeCooldown)
        {
            m_GrenadeCooldownTimer += Time.deltaTime;
        }
    }

    private void FaceMouse()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 screenPoint = Camera.main.ScreenToWorldPoint(transform.position);
        m_Direction = mousePos - (Vector2)m_Gun.position;
        m_Gun.transform.right = m_Direction;
        
        float rotZ = Mathf.Atan2(m_Direction.y, m_Direction.x) * Mathf.Rad2Deg;
        if (rotZ < 90 && rotZ > -90)
            m_GunSprite.flipY = false;
        else
            m_GunSprite.flipY = true;
    }

    private void Shoot()
    {   
        m_ShootCooldownTimer = 0;
        int bulletNumber = FindBullet();
        m_Bullets[bulletNumber].transform.position = m_FirePoint.position;
        m_Bullets[bulletNumber].GetComponent<BulletProjectile>().SetDirection(m_Direction);
    }

    private int FindBullet()
    {
        for (int i = 0; i < m_Bullets.Length; i++)
        {
            if (!m_Bullets[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }

    private void Throw()
    {   
        m_ShootCooldownTimer = 0;
        int grenadeNumber = FindGrenade();
        m_Grenades[grenadeNumber].transform.position = m_FirePoint.position;
        m_Grenades[grenadeNumber].GetComponent<GrenadeProjectile>().SetDirection(m_Direction);
    }

    private int FindGrenade()
    {
        for (int i = 0; i < m_Grenades.Length; i++)
        {
            if (!m_Grenades[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
}
