using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Monster : MonoBehaviour
{
    [SerializeField] private LayerMask m_PlayerMask;
    [SerializeField] private MonsterConfig m_MonsterConfig;
    [SerializeField] private MonsterMovement m_MonsterMovement;
    [SerializeField] private MonsterHealth m_MonsterHealth;
    [SerializeField] private MonsterAttack m_MonsterAttack;


    private GameObject m_Target;
    private float m_DetectTargetRange;


    // define monster states
    private Enums.MonsterBehavior m_CurrentStatus = Enums.MonsterBehavior.IDLE;

    // 1: RIGHT, -1: LEFT
    private int m_PrevDirection = 1;
    private int m_CurrentDirection = 1;
    private int RIGHT = 1;
    private int LEFT = -1;

    private void Awake()
    {
        m_PrevPos = transform.position;

        m_Animator = GetComponent<Animator>();


        if (m_MonsterConfig != null)
        {
            ConfigMonsterData(m_MonsterConfig);
        }
    }

    private void Start()
    {
        MonsterManager.Instance.AddMonster(this);
    }

    private void FixedUpdate()
    {
        FlipAnimation();
    }

    private void FlipAnimation()
    {
        // flip animation
        Vector2 curPos = transform.position;
        float dx = curPos.x - m_PrevPos.x;
        //Utils.FlipAnimation(gameObject, moveDelta);
        m_PrevPos = curPos;

        // facing right
        if (dx > 0)
        {
            m_CurrentDirection = RIGHT;
        }
        // facing left
        else if (dx < 0)
        {
            m_CurrentDirection = LEFT;
        }

        if (m_CurrentDirection != m_PrevDirection)
        {
            Utils.Flip(transform);
        }
        m_PrevDirection = m_CurrentDirection;
    }

    public void ConfigMonsterData(MonsterConfig config)
    {
        // set config for later use
        m_MonsterConfig = config;
        m_DetectTargetRange = config.detectTargetRange;
        // set animator controller
        m_Animator.runtimeAnimatorController = config.monsterAnimator;

        // set up data for different monster's component
        GetMonsterHealth().ConfigMonsterData(config);
        GetMonsterMovement().ConfigMonsterData(config);
        GetMonsterAttack().ConfigMonsterData(config);
    }


    #region getter setter
    public MonsterConfig GetMonsterConfig()
    {
        return m_MonsterConfig;
    }
    public LayerMask GetPlayerMask()
    {
        return m_PlayerMask;
    }

    public MonsterMovement GetMonsterMovement()
    {
        return m_MonsterMovement;
    }

    public MonsterHealth GetMonsterHealth()
    {
        return m_MonsterHealth;
    }

    public MonsterAttack GetMonsterAttack()
    {
        return m_MonsterAttack;
    }

    public GameObject GetTarget()
    {
        return m_Target;
    }

    public void SetTarget(GameObject target)
    {
        m_Target = target;
    }

    public Enums.MonsterBehavior GetCurrentStatus()
    {
        return m_CurrentStatus;
    }

    public void SetCurrentStatus(Enums.MonsterBehavior status)
    {
        m_CurrentStatus = status;
    }

    public float GetDetectTargetRange()
    {
        return m_DetectTargetRange;
    }

    public void SetDetecTargetRange(float value)
    {
        m_DetectTargetRange = value;
    }

    #endregion
}
