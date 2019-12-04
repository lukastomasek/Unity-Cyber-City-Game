using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

public static class SaveSystem 
{

    // save system is storred forever of this game becuase it is static 
  
    public static void SavePlayer( UniversalHealthController player)
    {
        Debug.Log("Saving...");

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.dat";
        FileStream file = new FileStream(path, FileMode.Create);

        try
        {
            PlayerData data = new PlayerData(player);

            formatter.Serialize(file, data);
        }
        catch(SerializationException e)
        {
            Debug.LogError("There was an issue serializing this data" + e.Message);
        }
        finally
        {
            file.Close();
        }

       
    }


    //returning data that we stored
    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.dat";

        if (File.Exists(path))
        {
            Debug.Log("Loading...");

            BinaryFormatter formatter = new BinaryFormatter();

            FileStream file = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(file) as PlayerData;
            file.Close();


            return data;

            
        }
        else
        {
            Debug.LogError("Save File Not Found in " + path);
            return null;
        }
    }



    #region  Saving And Loading Player Exp 
    public static void SavePlayerLevelAndExp(UnlockCombos player)
    {
        Debug.Log("Saving level and exp..");

        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath+ "/playerlvl.dat";

        FileStream file = new FileStream(path, FileMode.Create);

        try
        {
            PlayerStats stats = new PlayerStats(player.myexp, player.myLevel);
            bf.Serialize(file, stats);
        }
        catch(SerializationException e)
        {
            Debug.LogError($"an error saving playerstats {e.Message}");
        }
        finally
        {
            file.Close();
        }
    }


    public static PlayerStats LoadPlayerExpAndLevel()
    {
        string path = Application.persistentDataPath + "/playerlvl.dat";

        if (File.Exists(path))
        {
            Debug.Log("Loading Player exp... ");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = new FileStream(path, FileMode.Open);

            PlayerStats playerStats = (PlayerStats)bf.Deserialize(file);

            file.Close();

            return playerStats;
        }
        else
        {
            Debug.LogError("Loading player stats was not found" + path);
            return null;
        }
    }

    #endregion
}
