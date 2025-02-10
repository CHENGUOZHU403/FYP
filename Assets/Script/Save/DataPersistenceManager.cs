//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System.Linq;
//
//public class DataPersistenceManager : MonoBehaviour
//{
//   private GameData gameData;
//
//   public static DataPersistenceManager instance { get; private set;}
//
//   private void Awake()
//   {
//    if (instance !=null)
//    {
//        Debug.LogError("Found more than one Data Persistance Manager in the scene.");
//    }
//    instance = this;
//   }
//   private void Start()
//   {
//          this.dataPersistenceObjects = FindAllDataPersistenceObjects();
//          LoadGame();
//   }
//   public void NewGame()
//   {
//        this.gameData = new GameData();
//   }
//   public void LoadGame()
//   {
//        //todo- Load any saved data from a file using the data handler
//        //if no data can be loaded, initialize to a new game
//        if (this.gameData == null)
//        {
//            Debug.Log("No data was found");
//            NewGame();
//        }
//
//        //push the Loaded data to all other scripts that need it
//        foreach (IDataPersistence dataPersistence in dataPersistenceObjects)
//        {
//          dataPersistenceObj.LoadData(gameData);
//        }
//
//        Debug.Log("Loaded count = " + gameData.count);
//   }
//   public void SaveGame()
//   {
//   
//        //todo - pass the data to oher scripts so they can update it
//        foreach (IDataPersistence dataPersistence in dataPersistenceObjects)
//        {
//          dataPersistenceObj.SaveData(ref gameData);
//        }
//
//        Debug.Log("Saved count = " + gameData.count);
//
//        //todo - save that data to a file using the data handler
//   }         
//   
//    private void OnApplicationQuit()
//    {
//        SaveGame();
//    }
//
//     private List<IDataPersistence> FindAllDataPersistenceObjects()
//     {
//          IEnumerable<IDataPersistence> FindAllDataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
//
//          return new List<IDataPersistence>(dataPersistenceObjects);
//     }
//}
//