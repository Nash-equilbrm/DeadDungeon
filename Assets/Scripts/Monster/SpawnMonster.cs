using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class SpawnMonster : MonoBehaviour
{   
    [SerializeField] private SaveSystem m_Save;
    [SerializeField] private float m_SpawnDelayTime;
    [SerializeField] private int m_WaveNumber;
    
    private void Start()
    {
        MonsterManager.Instance.SpawnNewMonsterWave(m_WaveNumber, m_SpawnDelayTime, transform.position);
    }

    private void Update() 
    {
        if (MonsterManager.Instance.NoMonsterLeft())
        {
            StartCoroutine(StageComplete());
        }    
    }

    private IEnumerator StageComplete()
    {
        SaveStat();
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);   
    }

    private void SaveStat()
    {
        m_Save.SavePlayerStat();
    }
}
