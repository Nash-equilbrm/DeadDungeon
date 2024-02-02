using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enums
{
    public enum MonsterBehavior
    {
        IDLE = 0,
        APPROACH_FOR_TARGET = 1,
        ATTACK = 2,
        DEAD = 3,
        DO_NOTHING = 4
    }



    public enum MonsterAttackType
    {
        // Close-ranged attack without any weapons
        NORMAL = 0,
        // Use weapons to attack if targets are in range of monsters attack
        RANGE = 1,
        // Explode
        EXPLODE = 2
    }

    public enum MonsterMovementType
    {
        WALKING = 0,
        JUMPING = 1
    }


    public enum PropType
    {
        // prop can be destroy an do nothing
        NORMAL = 0,
        // explode after being destroy
        EXPLODE
    }
}
