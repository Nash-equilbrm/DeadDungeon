using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : IMonsterAttack
{
    private MonsterAttack m_MonsterAttack;

    public RangeAttack(MonsterAttack monsterAttack)
    {
        m_MonsterAttack = monsterAttack;
    }

    public void DoAttack(GameObject target)
    {
        string bulletTag = "ZombieBullet";
     
        // get position and rotation
        Vector3 lookVector = target.transform.position - m_MonsterAttack.transform.position;
        float angle = Mathf.Atan2(lookVector.y, lookVector.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        Vector3 position = m_MonsterAttack.gameObject.transform.position;
        // spawn bullet from pool
        GameObject bullet = ObjectPooler.Instance.SpawnFromPool(bulletTag, position, rotation);

        // get target position
        Vector3 targetDirection = target.gameObject.transform.position - m_MonsterAttack.transform.position;
        MonsterBulletMovement bulletMovement = bullet.GetComponent<MonsterBulletMovement>();
        if (bulletMovement)
        {
            bulletMovement.SetTargetDirection(targetDirection);
        }
        

    }
}
