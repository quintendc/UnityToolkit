﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UIElements;
using System;
using System.Linq;

public class SaveGameManager : ToolkitBehaviour
{

    public static SaveGameManager Instance = null;
    private string SaveGameBaseDirectory;
    private SaveGame currentSaveGame = null;


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

        // remove productName from the SaveGameBaseDirectory
        string tempPath = Application.persistentDataPath;
        SaveGameBaseDirectory = tempPath.Replace(Application.productName, "");

    }


    #region Save & Load Methods


    /// <summary>
    /// get a list of all saveGames found in the SaveGameBaseDirectory
    /// </summary>
    /// <returns>list of saveGames</returns>
    public new List<SaveGame> GetAllSaveGames()
    {
        List<SaveGame> foundedSaveGames = new List<SaveGame>();

        if (Directory.Exists(SaveGameBaseDirectory))
        {

            List<string> saveGameDirectories = Directory.GetFiles(SaveGameBaseDirectory, "*.*", SearchOption.AllDirectories).ToList();

            // remove files that don't have the fileExtension .save
            saveGameDirectories.RemoveAll(s => !s.Contains(".save"));

            List<SaveGame> deserializedSaveGames = new List<SaveGame>();

            foreach (var saveGameDirectory in saveGameDirectories)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(saveGameDirectory, FileMode.Open);

                SaveGame sg = formatter.Deserialize(stream) as SaveGame;
                deserializedSaveGames.Add(sg);

                stream.Close();
            }

            return deserializedSaveGames;

        }
        else
        {
            Debug.LogError("Save file not found in " + SaveGameBaseDirectory);
        }

        return foundedSaveGames;
    }

    /// <summary>
    /// Save the game
    /// </summary>
    /// <param name="persistentData">PeristentData from the GameManager</param>
    /// <param name="saveGameName">name of the SaveGame</param>
    /// <returns>boolean is saveGame saved successfully</returns>
    public new bool SaveGame(string saveGameName = null)
    {

        try
        {
            string defaultSaveGameName = "SaveGame" + GetAllSaveGames().Count;

            if (saveGameName != string.Empty)
            {
                defaultSaveGameName = saveGameName;
            }

            PersistentData persistentData = GetPersistentData();

            BinaryFormatter formatter = new BinaryFormatter();
            string path = SaveGameBaseDirectory + defaultSaveGameName + ".save";
            FileStream stream = new FileStream(path, FileMode.Create);

            SaveGame saveGame = new SaveGame(persistentData);

            formatter.Serialize(stream, saveGame);
            stream.Close();

            return true;
        }
        catch (Exception)
        {
            Debug.LogError("SaveGame could not be saved!");
            return false;
        }
    }

    /// <summary>
    /// load saveGame based on the name, the persistentData will be overrided by the loaded SaveGame
    /// </summary>
    /// <param name="saveGameName">name of saveGame</param>
    public new bool LoadGameByName(string saveGameName)
    {
        string path = SaveGameBaseDirectory + saveGameName + ".save";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveGame saveGame = formatter.Deserialize(stream) as SaveGame;
            stream.Close();

            currentSaveGame = saveGame;

            // set current persistent Data with SaveGame Data
            PersistentData data = new PersistentData(saveGame); // fill new PersistentData object with saveGame data
            OverridePersistentData(data);

            return true;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return false;
        }

    }


    public new bool LoadGameByIndex(int index) 
    {

        try
        {
            List<SaveGame> saveGames = GetAllSaveGames();

            SaveGame saveGame = saveGames.ElementAt(index);

            currentSaveGame = saveGame;

            // set current persistent Data with SaveGame Data
            PersistentData data = new PersistentData(saveGame); // fill new PersistentData object with saveGame data
            OverridePersistentData(data);

            return true;
        }
        catch (Exception)
        {
            Debug.LogError("Can't load game by index " + index);
            return false;
        }

    }

    #endregion


}
