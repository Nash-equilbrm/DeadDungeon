using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Monster", menuName = "Monster")]
public class MonsterConfig : ScriptableObject
{
    public int monsterID;
    public string monsterName;

    // health
    public float health;

    // movement behaviors
    public Enums.MonsterMovementType movementType;
    public float speed;
    public float detectTargetRange;

    // attack behaviors
    public Enums.MonsterAttackType attackType;
    public float attackRange;
    public float damage;
    public float attackRate;

    // UI
    public RuntimeAnimatorController monsterAnimator;
    public AudioClip attackSound;
    public AudioClip deadSound;
}
