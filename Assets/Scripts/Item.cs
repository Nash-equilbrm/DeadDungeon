using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool m_IsCollected = false;

    private float m_Speed = 5f;
    private float m_Height = 0.1f;
    private float m_OriginalY;
    
    void Start()
    {
        m_OriginalY = transform.position.y;
    }
    void Update() {
        if (m_IsCollected)
            return;
        
        Idle();
    }

    private void Idle()
    {
        Vector3 pos = transform.position;
        float newY = m_OriginalY + m_Height  * Mathf.Sin(Time.time * m_Speed);
        transform.position = new Vector3(pos.x, newY, pos.z);
    }

    private void DestroyItem()
    {
        Destroy(gameObject);
    }
}

