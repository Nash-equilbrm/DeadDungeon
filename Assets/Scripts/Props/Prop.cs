using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : MonoBehaviour
{
    [SerializeField] private Enums.PropType m_PropType;
    [SerializeField] private LayerMask m_DealDamageToLayerMask;
    [SerializeField] private LayerMask m_InteractiveLayerMask;
    [SerializeField] private Animator m_Animator;
    [SerializeField] private float m_HP;
    [SerializeField] private AudioSource m_AudioSource;


    private float m_CurrentHP;
    private IPropAction m_PropAction;

    private void Start()
    {
        PropManager.Instance.AddNewProp(this);

        GetNewPropAction();
        m_CurrentHP = m_HP;
    }




    public void TakeDamage(float amount)
    {
        OnInteractiveUI();

        m_CurrentHP -= amount;
        if(m_CurrentHP <= 0)
        {
            PropManager.Instance.RemoveProp(this);
            OnDestroyUI();
        }
    }


    private void DestroyProp()
    {
        Destroy(gameObject);
    }

    private void DoPropAction()
    {
        m_PropAction.DoAction();
    }

  

    private void GetNewPropAction()
    {
        switch (m_PropType)
        {
            case Enums.PropType.NORMAL:
                {
                    m_PropAction = new NormalProp(this);
                    return;
                }
            case Enums.PropType.EXPLODE:
                {
                    m_PropAction = new ExplodeProp(this);
                    return;
                }
            default:
                {
                    return;
                }
        }
    }

    #region Animation + UI
    private void OnInteractiveUI()
    {
        m_Animator.SetBool("OnInteract", true);
        m_Animator.SetBool("OnDestroy", false);

    }

    private void OnDestroyUI()
    {
        m_AudioSource.Play();

        m_Animator.SetBool("OnInteract", false);
        m_Animator.SetBool("OnDestroy", true);
    }

    private void OnIdleUI()
    {
        m_Animator.SetBool("OnInteract", false);
        m_Animator.SetBool("OnDestroy", false);
    }

    #endregion



    #region getter setter

    public LayerMask GetDealDamageLayerMask()
    {
        return m_DealDamageToLayerMask;
    }

   

    #endregion
}
