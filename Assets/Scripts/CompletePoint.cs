using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class CompletePoint : MonoBehaviour
{
    [SerializeField] SaveSystem m_Save;
    private bool m_IsComplete = false;

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("Player") && !m_IsComplete)
            StageComplete();    
    }

    private async void StageComplete()
    {
        m_IsComplete = true;
        await SaveStat();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);   
    }

    private async Task SaveStat()
    {
        m_Save.SavePlayerStat();
    }
}
