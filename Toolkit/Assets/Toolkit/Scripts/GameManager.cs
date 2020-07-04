using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : ToolkitBehaviour
{

    public static GameManager Instance = null;
    //public List<Player> Players = new List<Player>();
    public PersistentData PersistentData = null;
    public SceneSettingsObject InitialSceneSettings = null;
    private GameObject currentGameMode = null;

    public GameObject SaveGameManagerPrefab = null;
    public GameObject WidgetManagerPrefab = null;

    [Header("Initial player")]
    [Tooltip("there will at least be one playercontroller initialized")]
    public bool SpawnPawn = false;

    private void Awake()
    {
        #region Singleton Pattern GameManager instance

        // check if instance already exists
        if (Instance == null)
        {
            // if not, set instance to this
            Debug.Log("GameManager Instance created");
            Instance = this;
        }
        // if instance already exists and it's not this:
        else if (Instance != null)
        {
            // then destroy this. this enforces our singleton pattern, meaning there can only ever be one instance of the GameManager
            Debug.Log("GameManager Instance already exists this.destroyed");
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this);

        #endregion

        Validate();
    }


    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        // set persistentData
        PersistentData = new PersistentData(null);

        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /// <summary>
    /// validate whether all necessary files have been delivered, if object is provided it will be instantiated.
    /// </summary>
    private void Validate()
    {
        // check for SaveGameManager, WidgetManager, PersistentData and InitialSceneData
        if (SaveGameManagerPrefab == null)
        {
            Debug.LogWarning("No SaveGameManager Provided! Some functionality may not work.");
        }
        else
        {
            GameObject.Instantiate(SaveGameManagerPrefab);
        }

        if (WidgetManagerPrefab == null)
        {
            Debug.LogWarning("No WidgetManager Provided! Some functionality may not work.");
        }
        else
        {
            GameObject.Instantiate(WidgetManagerPrefab);
        }

        if (InitialSceneSettings == null)
        {
            Debug.LogWarning("No InitialSceneSettings object is provided!");
        }
    }


    /// <summary>
    /// this is called only once when the Initial Scene is Loaded
    /// </summary>
    private void Init()
    {
        // spawn widget
        if (InitialSceneSettings != null)
        {
            // spawn widget
            WidgetManager.Instance.ShowWidget(InitialSceneSettings.WidgetType);

            // spawn gameMode
            currentGameMode = GameObject.Instantiate(InitialSceneSettings.GameMode);

            // set input state
            GameState.InputState = InitialSceneSettings.InputState;
        }
    }


    /// <summary>
    /// callback when scene is loaded, setup scene by the provided SceneSettingsObject, not triggered when the Initial Scene is loaded
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("scene is loaded");
        // get sceneSettingsObject from the Scene
        SceneSettingsObject sceneSettingsObject = GameObject.FindGameObjectWithTag("SceneSettingsProvider").GetComponent<SceneSettingsProvider>().SceneSettings;

        // handle SceneSettingsObject
        // set widget to be shown
        WidgetManager.Instance.ShowWidget(sceneSettingsObject.WidgetType);

        // Instantiate GameMode and set currentGameMode to new instantiated GameMode
        currentGameMode = GameObject.Instantiate(sceneSettingsObject.GameMode);

        // start round when scene is loaded -> make sure to call this as last first handle all player stuff
        if (sceneSettingsObject.StartRoundWhenSceneIsLoaded == true)
        {
            currentGameMode.GetComponent<AGameMode>().StartRound();
        }
    }


    


    #region PersistentData Methods

    public new void OverridePersistentData(PersistentData persistentData)
    {

        PersistentData = persistentData;

    }

    #endregion


    #region GameMode Methods
    public new GameObject GetCurrentGameMode()
    {
        return currentGameMode;
    }

    #endregion


}
