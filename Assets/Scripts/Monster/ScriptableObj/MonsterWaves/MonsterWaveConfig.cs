using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewMonsterWave", menuName = "Monster Wave")]
public class MonsterWaveConfig : ScriptableObject
{
    public int TotalNormalMonster;
    public int TotalRangeMonster;
    public int TotalExplodeMonster;
    public float SpawnTimeInterval;
}
