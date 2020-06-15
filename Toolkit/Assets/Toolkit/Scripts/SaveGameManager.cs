using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SaveGameManager : MonoBehaviour
{

    public static SaveGameManager Instance = null;
    public string SaveGameBaseDirectory = "";
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


    #region Save & Load Methods

    public List<SaveGameObject> GetAllSaveGames()
    {
        List<SaveGameObject> foundedSaveGames = new List<SaveGameObject>();


        return foundedSaveGames;
    }


    public SaveGameObject GetSaveGameById(int id)
    {

        List<SaveGameObject> saveGames = GetAllSaveGames();

        SaveGameObject saveGame = saveGames.Find(s => s.Id == id);


        return saveGame;
    }


    public SaveGameObject GetSaveGameByName(string name)
    {
        List<SaveGameObject> saveGames = GetAllSaveGames();

        SaveGameObject saveGame = saveGames.Find(s => s.Name == name);

        return saveGame;
    }



    public void SaveNewGame(PersistentData persistentData, string saveGameName)
    {
        SaveGameObject saveGame = new SaveGameObject();

        saveGame.Id = GetAllSaveGames().Count + 1;
    }

    public void SaveCurrentGame(PersistentData persistentData)
    {
        // override properties in teh currentSaveGameObject then save
    }

    #endregion

}
