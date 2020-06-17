using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UIElements;
using System;

public class SaveGameManager : MonoBehaviour
{

    public static SaveGameManager Instance = null;
    public string SaveGameBaseDirectory = Application.persistentDataPath;
    public SaveGameObject SaveGameObject = null;
    private SaveGameObject currentSaveGame = null;


    private void Awake()
    {
        #region Singleton Pattern SaveGameManager instance

        // check if instance already exists
        if (Instance == null)
        {
            // if not, set instance to this
            Debug.Log("SaveGameManager Instance created");
            Instance = this;
        }
        // if instance already exists and it's not this:
        else if (Instance != null)
        {
            // then destroy this. this enforces our singleton pattern, meaning there can only ever be one instance of the SaveGameManager
            Debug.Log("SaveGameManager Instance already exists this.destroyed");
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this);

        #endregion
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /// <summary>
    /// validate whether all necessary files have been delivered
    /// </summary>
    private void Validate()
    {
        if (SaveGameObject == null)
        {
            Debug.LogWarning("No SaveGameObject provided to the SaveGameManager!");
        }
    }


    #region Save & Load Methods

    [Obsolete]
    public List<SaveGameObject> GetAllSaveGames()
    {
        List<SaveGameObject> foundedSaveGames = new List<SaveGameObject>();


        return foundedSaveGames;
    }

    [Obsolete]
    public SaveGameObject GetSaveGameById(int id)
    {

        List<SaveGameObject> saveGames = GetAllSaveGames();

        SaveGameObject saveGame = saveGames.Find(s => s.Id == id);


        return saveGame;
    }

    [Obsolete]
    public SaveGameObject GetSaveGameByName(string name)
    {
        List<SaveGameObject> saveGames = GetAllSaveGames();

        SaveGameObject saveGame = saveGames.Find(s => s.Name == name);

        return saveGame;
    }


    [Obsolete]
    public void SaveNewGame(PersistentData persistentData, string saveGameName)
    {
        SaveGameObject saveGame = new SaveGameObject(persistentData);

        saveGame.Id = GetAllSaveGames().Count + 1;
    }

    [Obsolete]
    public void SaveCurrentGame(PersistentData persistentData)
    {
        // override properties in the currentSaveGameObject then save
    }



    public void SaveGame(PersistentData persistentData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = SaveGameBaseDirectory + "TestSaveGame.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveGameObject saveGame = new SaveGameObject(persistentData);

        formatter.Serialize(stream, saveGame);
        stream.Close();
    }

    public void LoadGame()
    {
        string path = SaveGameBaseDirectory + "TestSaveGame.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveGameObject saveGame = formatter.Deserialize(stream) as SaveGameObject;
            stream.Close();

            currentSaveGame = saveGame;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
        }
    }

    #endregion

}
