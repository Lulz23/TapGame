using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{

    public static void Save(GameData data) {

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(GetSavePath(), FileMode.Create);
        formatter.Serialize(fileStream, data);

        fileStream.Close(); 
      
    }

    public static GameData Load() {

        //If there is no existing save data
        if (!File.Exists(GetSavePath())) { 
            GameData emptyData = new GameData();
            Save(emptyData);
            return emptyData;
        }

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(GetSavePath(), FileMode.Open);
        GameData data = formatter.Deserialize(fileStream) as GameData;
        
        fileStream.Close();

        return data;
    }

    private static string GetSavePath() { 
        
        return Application.persistentDataPath + "/data.qnd";
    }
}
