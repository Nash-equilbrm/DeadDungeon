using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropManager : MonoBehaviour
{
    [SerializeField] private List<Prop> m_ActiveProps;

    public static PropManager Instance;
    private void Awake()
    {
        Instance = this;
        m_ActiveProps = new List<Prop>();

    }

   

    public void AddNewProp(Prop prop)
    {
        m_ActiveProps.Add(prop);
    }

    public Prop RemoveProp(Prop prop)
    {
        m_ActiveProps.Remove(prop);
        return prop;
    }

    public List<Prop> GetActiveProps()
    {
        return m_ActiveProps;
    }
}
