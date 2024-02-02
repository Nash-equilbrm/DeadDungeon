using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private Text m_BulletCount;
    [SerializeField] private Text m_GrenadeCount;
    [SerializeField] private Text m_MedkitCount;
    [SerializeField] private Text m_EnergyDrinkCount;
    [SerializeField] private Text m_ResourceCount;
    [SerializeField] private AudioSource m_PickupSound;
    [SerializeField] private AudioSource m_HealSound;
    [SerializeField] private AudioSource m_DrinkSound;
    [SerializeField] private AudioSource m_CraftSound;
    [SerializeField] private AudioSource m_ShootSound;

    private SaveSystem m_Save;

    private int m_NumBullets;
    private int m_NumGrenades;
    private int m_NumMedkits;
    private int m_NumEnergyDrinks;
    private int m_NumResources;

    private void Start() 
    {
        m_Save = GetComponent<SaveSystem>();
        PlayerStat stat = m_Save.LoadPlayerStat();

        m_NumBullets = stat.numBullets;
        m_NumGrenades = stat.numGrenades;
        m_NumMedkits = stat.numMedkits;
        m_NumEnergyDrinks = stat.numEnergyDrinks;
        m_NumResources = stat.numResources;

        m_BulletCount.text = m_NumBullets.ToString();
        m_GrenadeCount.text = m_NumGrenades.ToString();
        m_MedkitCount.text = m_NumMedkits.ToString();
        m_EnergyDrinkCount.text = m_NumEnergyDrinks.ToString();
        m_ResourceCount.text = m_NumResources.ToString();
    }

    public PlayerStat SaveStat(PlayerStat stat)
    {
        stat.numBullets = m_NumBullets;
        stat.numGrenades = m_NumGrenades;
        stat.numMedkits = m_NumMedkits;
        stat.numEnergyDrinks = m_NumEnergyDrinks;
        stat.numResources = m_NumResources;

        return stat;
    }


    private void OnTriggerEnter2D(Collider2D collision) 
    {   
        if (collision.gameObject.layer != LayerMask.NameToLayer("Item"))
            return;

        Item item = collision.gameObject.GetComponent<Item>();
        Animator anim = collision.gameObject.GetComponent<Animator>();
        if (item.m_IsCollected)
            return;

        m_PickupSound.Play();
        if (collision.gameObject.CompareTag("Bullet"))
        {
            item.m_IsCollected = true;
            m_NumBullets += 64;
            anim.SetTrigger("collected");
            m_BulletCount.text = m_NumBullets.ToString();
        }   
        else if (collision.gameObject.CompareTag("Grenade"))
        {
            item.m_IsCollected = true;
            m_NumGrenades += 1;
            anim.SetTrigger("collected");
            m_GrenadeCount.text = m_NumGrenades.ToString();
        }     
        else if (collision.gameObject.CompareTag("Medkit"))
        {
            item.m_IsCollected = true;
            m_NumMedkits += 1;
            anim.SetTrigger("collected");
            m_MedkitCount.text = m_NumMedkits.ToString();
        }  
        else if (collision.gameObject.CompareTag("EnergyDrink"))
        {
            item.m_IsCollected = true;
            m_NumEnergyDrinks += 1;
            anim.SetTrigger("collected");
            m_EnergyDrinkCount.text = m_NumEnergyDrinks.ToString();
        }   
        else if (collision.gameObject.CompareTag("Resource"))
        {
            item.m_IsCollected = true;
            m_NumResources += 10;
            anim.SetTrigger("collected");
            m_ResourceCount.text = m_NumResources.ToString();
        }          
    }

    public bool CanShoot()
    {
        if (m_NumBullets > 0)
        {
            m_ShootSound.Play();
            m_NumBullets--;
            m_BulletCount.text = m_NumBullets.ToString();
            return true;
        }
        return false;
    }

    public bool CanThrow()
    {
        if (m_NumGrenades > 0)
        {
            m_NumGrenades--;
            m_GrenadeCount.text = m_NumGrenades.ToString();
            return true;
        }
        return false;
    }

    public bool CanHeal()
    {
        if (m_NumMedkits > 0)
        {
            m_HealSound.Play();
            m_NumMedkits--;
            m_MedkitCount.text = m_NumMedkits.ToString();
            return true;
        }
        return false;
    }

    public bool CanDrink()
    {
        if (m_NumEnergyDrinks > 0)
        {
            m_DrinkSound.Play();
            m_NumEnergyDrinks--;
            m_EnergyDrinkCount.text = m_NumEnergyDrinks.ToString();
            return true;
        }
        return false;
    }

    public void CraftBullet()
    {
        if (m_NumResources >= 15)
        {
            m_CraftSound.Play();
            m_NumResources -= 15;
            m_NumBullets += 64;
            m_BulletCount.text = m_NumBullets.ToString();
            m_ResourceCount.text = m_NumResources.ToString();
        }
    }

    public void CraftGrenade()
    {
        if (m_NumResources >= 25)
        {
            m_CraftSound.Play();
            m_NumResources -= 25;
            m_NumGrenades += 1;
            m_GrenadeCount.text = m_NumGrenades.ToString();
            m_ResourceCount.text = m_NumResources.ToString();
        }
    }

    public void CraftMedkit()
    {
        if (m_NumResources >= 15)
        {
            m_CraftSound.Play();
            m_NumResources -= 15;
            m_NumMedkits += 1;
            m_MedkitCount.text = m_NumMedkits.ToString();
            m_ResourceCount.text = m_NumResources.ToString();
        }
    }

    public void CraftEnergyDrink()
    {
        if (m_NumResources >= 10)
        {
            m_CraftSound.Play();
            m_NumResources -= 10;
            m_NumEnergyDrinks += 1;
            m_EnergyDrinkCount.text = m_NumEnergyDrinks.ToString();
            m_ResourceCount.text = m_NumResources.ToString();
        }
    }
}
