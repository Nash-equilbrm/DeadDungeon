using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalProp : IPropAction
{
    private Prop m_Prop;

    public NormalProp(Prop prop)
    {
        m_Prop = prop;
    }
    public void DoAction()
    {
        // normal props do nothing
    }

   
}
