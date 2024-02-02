using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem : MonoBehaviour
{
    private PlayerStat m_PlayerStat = new PlayerStat();

    private void Awake()
    {
        string path = Application.persistentDataPath + "/playerstat.fun";
       
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, 
                                               FileMode.Open,       
                                               FileAccess.ReadWrite, 
                                               FileShare.None);

            m_PlayerStat = formatter.Deserialize(stream) as PlayerStat;
        }
    }

    public PlayerStat LoadPlayerStat()
    {   
        return m_PlayerStat;
    }

    public void SavePlayerStat()
    {
        PlayerStat stat = GetCurrentPlayerStat();

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerstat.fun";
        FileStream stream = new FileStream(path, 
                                           FileMode.Create,
                                           FileAccess.ReadWrite, 
                                           FileShare.None);

        formatter.Serialize(stream, stat);
        stream.Close();
    }

    public void SavePlayerInventory()
    {
        PlayerInventory inventory = GetComponent<PlayerInventory>();
      
        m_PlayerStat = inventory.SaveStat(m_PlayerStat);

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerstat.fun";
        FileStream stream = new FileStream(path, 
                                           FileMode.Create,
                                           FileAccess.ReadWrite, 
                                           FileShare.None);

        formatter.Serialize(stream, m_PlayerStat);
        stream.Close();
    }


    public void SaveNewGame()
    {
        PlayerStat stat = new PlayerStat();

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerstat.fun";
        FileStream stream = new FileStream(path, 
                                           FileMode.Create,
                                           FileAccess.ReadWrite, 
                                           FileShare.None);

        formatter.Serialize(stream, stat);
        stream.Close();
    }

    private PlayerStat GetCurrentPlayerStat()
    {
        PlayerHealth health = GetComponent<PlayerHealth>();
        PlayerMovement movement = GetComponent<PlayerMovement>();
        PlayerInventory inventory = GetComponent<PlayerInventory>();

        PlayerStat stat = new PlayerStat();

        stat = health.SaveStat(stat);
        stat = movement.SaveStat(stat);
        stat = inventory.SaveStat(stat);

        return stat;
    }

}
