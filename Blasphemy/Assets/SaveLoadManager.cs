using System.Collections;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoadManager
{
    public static void SavePlayer(Ouros player)
    {
        
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.dataPath + "/player.sav", FileMode.Create);

        PlayerData data = new PlayerData(player);

        bf.Serialize(stream, data);
        stream.Close();
    }

    public static int[] LoadStats()
    {
        if (File.Exists(Application.dataPath + "/player.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.dataPath + "/player.sav", FileMode.Open);

            PlayerData data = bf.Deserialize(stream) as PlayerData;

            stream.Close();
            return data.stats;
        }
        else
        {
            Debug.LogError("File does not exist.");
            return new int[5];
        }
    }

    public static float[] LoadPosition()
    {

        if (File.Exists(Application.dataPath + "/player.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.dataPath + "/player.sav", FileMode.Open);

            PlayerData data = bf.Deserialize(stream) as PlayerData;

            stream.Close();
            return data.pos;
        }
        else
        {
            Debug.LogError("File does not exist.");
            return new float[3];
        }
    }
}

[Serializable]
public class PlayerData
{
    public int[] stats;
    public float[] pos;
    public PlayerData(Ouros player)
    {
        stats = new int[5];
        stats[0] = (int)player.healthBar.value;
        stats[1] = player.maxHP;
        stats[2] = player.HP;
        stats[3] = player.ATK;
        stats[4] = player.DEF;

        pos = new float[3];
        // player.transform.position = new Vector3(pos[0], pos[1], pos[2]);
        pos[0] = player.xAxis;
        pos[1] = player.yAxis;
        pos[2] = player.zAxis;
    }
}