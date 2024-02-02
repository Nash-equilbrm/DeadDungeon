using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeProp : IPropAction
{
    private Prop m_Prop;
    private float m_ExplodeRange = 2f;
    private float m_Damage = 5f;

    public ExplodeProp(Prop prop)
    {
        m_Prop = prop;
    }
    public void DoAction()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(m_Prop.transform.position, m_ExplodeRange, m_Prop.GetDealDamageLayerMask());
        foreach (Collider2D hit in hitColliders)
        {
            // deal damage to player
            PlayerHealth playerHealth = hit.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth)
            {
                playerHealth.TakeDamage((int)m_Damage);
            }
            // deal damage to monster
            MonsterHealth monsterHealth = hit.gameObject.GetComponent<MonsterHealth>();
            if (monsterHealth)
            {
                monsterHealth.TakeDamage((int)m_Damage);
            }
            KnockBack(hit.gameObject);
        }


        // get list of object that have the same tag of this object
        List<Prop> props = GetPropListNearBy(m_Prop.transform.position, m_ExplodeRange);
        foreach (Prop prop in props)
        {
            prop.TakeDamage(100);
        }


    }


    private List<Prop> GetPropListNearBy(Vector3 position, float range)
    {
        //List<Collider2D> nearBy = Physics2D.OverlapCircleAll(position, range, layerMask).ToList();

        List<Prop> nearBy = PropManager.Instance.GetActiveProps();
        List<Prop> res = new List<Prop>();
        foreach(Prop prop in nearBy)
        {
            if(prop == m_Prop)
            {
                continue;
            }
            
            if(Vector3.Distance(m_Prop.transform.position, prop.transform.position) <= range)
            {
                res.Add(prop);
            }
        }

      
        return res;
    }


    private void KnockBack(GameObject target)
    {
        // add explosion knockback force
        Rigidbody2D rb = target.GetComponent<Rigidbody2D>();
        if (rb)
        {
            // distance vector
            Vector3 v = (target.transform.position - m_Prop.transform.position).normalized;
            Vector2 forceVector = new Vector2(v.x, v.y);
            rb.AddForce(forceVector * Time.deltaTime, ForceMode2D.Impulse);
        }
    }
}
