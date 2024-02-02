using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeAttack : IMonsterAttack
{
    private MonsterAttack m_MonsterAttack;

    public ExplodeAttack(MonsterAttack monsterAttack)
    {
        this.m_MonsterAttack = monsterAttack;
    }

    public void DoAttack(GameObject target)
    {
        m_MonsterAttack.GetMonster().GetMonsterHealth().TakeDamage(100);
        if (TargetInRange(target))
        {
            PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();
            if (playerHealth)
            {
                playerHealth.TakeDamage((int)m_MonsterAttack.GetDamage());
                // add explosion knockback force
                KnockBack(target);
            }
        }

        DoCollateralDamage();
    }


    private void DoCollateralDamage()
    {
        // deal damage to props
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(m_MonsterAttack.transform.position, m_MonsterAttack.GetAttackRange(), LayerMask.NameToLayer("Props"));
        foreach (Collider2D hit in hitColliders)
        {
            Prop prop = hit.gameObject.GetComponent<Prop>();
            if (prop)
            {
                prop.TakeDamage(100);
            }
        }

        // deal damage to monsters
        List<Monster> monstersNearby = GetMonsterNearByList(m_MonsterAttack.transform.position, m_MonsterAttack.GetAttackRange());
        foreach (Monster monster in monstersNearby)
        {
            MonsterHealth monsterHealth = monster.GetMonsterHealth();
            if (monsterHealth)
            {
                monsterHealth.TakeDamage(m_MonsterAttack.GetDamage());
            }
            KnockBack(monster.gameObject);
        }


    }


    private List<Monster> GetMonsterNearByList(Vector3 position, float range)
    {
        //List<Collider2D> nearBy = Physics2D.OverlapCircleAll(position, range, layerMask).ToList();

        List<Monster> nearBy = MonsterManager.Instance.GetMonsterList();
        List<Monster> res = new List<Monster>();
        foreach (Monster monster in nearBy)
        {
            if (monster == m_MonsterAttack.GetMonster())
            {
                continue;
            }

            if (Vector3.Distance(m_MonsterAttack.transform.position, monster.transform.position) <= range)
            {
                res.Add(monster);
            }
        }


        return res;
    }

    private void KnockBack(GameObject target)
    {
        Rigidbody2D rb = target.GetComponent<Rigidbody2D>();
        if (rb)
        {
            // distance vector
            Vector3 v = (target.transform.position - m_MonsterAttack.transform.position).normalized;
            Vector2 forceVector = new Vector2(v.x, v.y);
            rb.AddForce(forceVector * Time.deltaTime, ForceMode2D.Impulse);
        }
    }


    private bool TargetInRange(GameObject target)
    {
        Vector3 curPosition = m_MonsterAttack.transform.position;

        float distance = Vector3.Distance(curPosition, target.transform.position);
        float attackRange = m_MonsterAttack.GetAttackRange();

        return distance <= attackRange;

    }
}
