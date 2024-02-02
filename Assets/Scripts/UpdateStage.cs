using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class UpdateStage : MonoBehaviour
{
    [SerializeField] private int m_CompletedStage;
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

        if (stat.stage <= m_CompletedStage)    
            stat.stage = m_CompletedStage + 1;

        BinaryFormatter save_formatter = new BinaryFormatter();
        FileStream save_stream = new FileStream(path, 
                                           FileMode.Create,
                                           FileAccess.ReadWrite, 
                                           FileShare.None);

        save_formatter.Serialize(save_stream, stat);
        save_stream.Close();
    }
}
