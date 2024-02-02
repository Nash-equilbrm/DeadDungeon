using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWalking : MonsterMoving
{

    public MonsterWalking(MonsterMovement controller): base(controller)
    {
    }


    public override void Move(Transform target)
    {
        m_Controller.GetMonster().OnMovingUI();
        BasicMove(target);
    }
}
