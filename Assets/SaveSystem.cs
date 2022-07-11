using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    private static string playerSaveName = "/player.save";
 public static void SavePlayer(PlayerController player)
 {
     BinaryFormatter formatter = new BinaryFormatter();
     string path = Application.persistentDataPath + playerSaveName;
     FileStream stream = new FileStream(path, FileMode.Create);

     PlayerData data = new PlayerData(player);
     
     formatter.Serialize(stream, data);
     stream.Close();
 }


 public static PlayerData LoadPlayer()
 {
     string path = Application.persistentDataPath + playerSaveName;
     if (File.Exists(path))
     {
         BinaryFormatter formatter = new BinaryFormatter();
         FileStream stream = new FileStream(path, FileMode.Open);

         PlayerData playerData = formatter.Deserialize(stream) as PlayerData;
         stream.Close();
         
         return playerData;
     }
     else
     {
         Debug.LogError("Save File Missing from:" + path);
         return null;
     }
 }
}
