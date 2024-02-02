using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonGas : MonoBehaviour
{
    [SerializeField] private float m_Speed = 2f;
    
    private BoxCollider2D m_Coll;


    private void Start()
    {
        m_Coll = GetComponent<BoxCollider2D>();     
    }

    private void FixedUpdate()
    {
        transform.Translate(Time.deltaTime * m_Speed, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.transform.GetComponent<PlayerHealth>();
            playerHealth.Die();
        }
    }
}
