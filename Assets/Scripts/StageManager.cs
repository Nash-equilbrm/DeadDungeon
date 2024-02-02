using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class StageManager : MonoBehaviour
{
   [SerializeField] private GameObject[] m_StagesList;
    private void Start()
    {
        string path = Application.persistentDataPath + "/playerstat.fun";
        PlayerStat stat = new PlayerStat();
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, 
                                               FileMode.Open,       
                                               FileAccess.ReadWrite, 
                                               FileShare.None);

            stat = formatter.Deserialize(stream) as PlayerStat;
            stream.Close();
        }
        else 
            return;


        for (int i = 0; i < m_StagesList.Length; i++) 
        {
            if (i < stat.stage)
                m_StagesList[i].SetActive(true);
        }
    }
}
