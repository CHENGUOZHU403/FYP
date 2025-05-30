//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System;
//using System.IO;
//
//public class FileDataHandler : MonoBehaviour
//{
//   private string dataDirPath = "";
//   private string dataFileName = "";
//
//   public FileDataHandler(string dataDirPath, string dataFileName)
//   {
//    this.dataDirPath = dataDirPath;
//    this.dataFileName = dataFileName;
//   }
//
//   public GameData Load()
//   {
//         //use Path.Combine to accout for different OS's having diffrent path separators
//        string fullPath = Path.Combine(dataDirPath, dataFileName);
//        GameData loadedData = null;
//        if (FileDataHandler.Exists(fullPath))
//        {
//            try
//            {
//                //Load the serialized data from the file
//                string dataToLoad = "";
//                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
//                {
//                    using (StreamReader reader = new StreamReader(stream))
//                    {
//                        dataToLoad = reader.ReadToEnd();
//                    }
//                }
//                //deserialize the data from Json back into the C# object
//                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
//            }
//            catch (Exception e)
//            {
//                Debug.LogError("Error occured when trying to load data to file " + fullPath + "\n" + e);
//            }
//        }
//   }
//
//   public void Save(GameData data)
//   {
//    //use Path.Combine to accout for different OS's having diffrent path separators
//    string fullPath = Path.Combine(dataDirPath, dataFileName);
//    try
//    {
//        //create the directory the file will be written to if it doesn't already exist
//        Directory.CreateDirectory(Path.GetDirectoryName(fullPath));    
//
//        string dataToStore = JsonUtility.ToJson(dataToStore, true);
//
//        using (FileStream stream = new FileStream(fullPath, FileMode.Create))
//        {
//            using (StreamWriter writer = new StreamWriter(stream))
//            {
//                writer.Write(dataToStore);
//            }
//        }   
//    }
//    catch (Exception e)
//    {
//        Debug.LogError("Error occured when trying to save data to file " + fullPath + "\n" + e);
//    }
//   }
//}
