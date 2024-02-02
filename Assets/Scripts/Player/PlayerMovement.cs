using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform m_Effect;

    private Animator m_Animator;
    private PlayerInventory m_Inventory;
    private RaycastHit2D m_Hit;
    private BoxCollider2D m_BoxCollider;
    private SpriteRenderer m_Sprite;
    private SaveSystem m_Save;

    private float m_Speed;
    private float m_SpeedUp;
    private float m_SpeedUpCoolDown;

    private Vector3 m_MoveDelta;
    private float m_OriginalSpeed;
    private float m_SpeedUpCoolDownTimer = 0;
    
    
    private void Start()
    {
        m_BoxCollider = GetComponent<BoxCollider2D>();    
        m_Animator = GetComponent<Animator>();  
        m_Sprite = GetComponent<SpriteRenderer>();
        m_Inventory = GetComponent<PlayerInventory>();
        m_Save = GetComponent<SaveSystem>();

        PlayerStat stat = m_Save.LoadPlayerStat();
        m_Speed = stat.speed;
        m_SpeedUp = stat.speedUp;
        m_SpeedUpCoolDown = stat.speedUpCoolDown;

        m_OriginalSpeed = m_Speed;
    }

    public PlayerStat SaveStat(PlayerStat stat)
    {
        stat.speed = m_OriginalSpeed;
        stat.speedUp = m_SpeedUp;
        stat.speedUpCoolDown = m_SpeedUpCoolDown;

        return stat;
    }

    private void Update()
    {    
        if (Input.GetKeyDown(KeyCode.E) && m_Inventory.CanDrink())
        {
            SpeedUp();
        }

        if (m_SpeedUpCoolDownTimer < m_SpeedUpCoolDown)
        {
            m_SpeedUpCoolDownTimer += Time.deltaTime;
            if (m_SpeedUpCoolDownTimer > m_SpeedUpCoolDown)
            {
                m_Speed = m_OriginalSpeed;
            }
        }
    }

    private void FixedUpdate() 
    {
        FaceMouse();
        Moving();
    }

    private void SpeedUp()
    {
        Animator effectAnim = m_Effect.GetComponent<Animator>();
        effectAnim.SetTrigger("speed");
        m_SpeedUpCoolDownTimer = 0;
        m_Speed = m_OriginalSpeed + m_SpeedUp;
    }
    
    private void Moving()
    {
        float dirX = Input.GetAxisRaw("Horizontal");
        float dirY = Input.GetAxisRaw("Vertical");

        m_MoveDelta = new Vector3(dirX, dirY, 0);

        if (m_MoveDelta != Vector3.zero)
            m_Animator.SetBool("running", true);
        else
            m_Animator.SetBool("running", false);

        float step = Time.deltaTime * m_Speed;
        float deltaY = m_MoveDelta.y * step;
        float deltaX = m_MoveDelta.x * step;

        m_Hit = Physics2D.BoxCast(m_BoxCollider.bounds.center, m_BoxCollider.size, 0, 
                                new Vector2(0, m_MoveDelta.y),
                                Mathf.Abs(deltaY),
                                LayerMask.GetMask("Blocking"));
        if (m_Hit.collider == null)
        {
            transform.Translate(0, deltaY, 0);
        }

        m_Hit = Physics2D.BoxCast(m_BoxCollider.bounds.center, m_BoxCollider.size, 0, 
                                new Vector2(m_MoveDelta.x, 0),
                                Mathf.Abs(deltaX),
                                LayerMask.GetMask("Blocking"));
        if (m_Hit.collider == null)
        {
            transform.Translate(deltaX, 0, 0);
        }
    }

    private void FaceMouse()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 screenPoint = Camera.main.ScreenToWorldPoint(transform.position);
        Vector2 direction = mousePos - (Vector2)transform.position;
        
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (rotZ < 90 && rotZ > -90)
            m_Sprite.flipX = false;
        else
            m_Sprite.flipX = true;
    }
}