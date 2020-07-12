using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerInputManager))]
[RequireComponent(typeof(ToolkitSceneManager ))]
public class GameManager : ToolkitBehaviour
{

    public static GameManager Instance = null;
    public PersistentData PersistentData = null;
    private AGameMode currentGameMode = null;
    //[Tooltip("Amount of players that can join is limited by current GameMode Settings")]
    //public bool LimitPlayersByGameMode = true;

    [Header("Managers")]
    public GameObject ToolkitSceneManagerPrefab = null;
    public GameObject SaveGameManagerPrefab = null;
    public GameObject WidgetManagerPrefab = null;
    public PlayerInputManager PlayerInputManager = null;

    [Header("Toolkit Settings")]
    [Tooltip("define the ToolkitSettings")]
    public ToolkitSettings ToolkitSettings = new ToolkitSettings();

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

        if (PlayerInputManager == null)
        {
            Debug.LogWarning("No Player Input Manager component is provided!");
        }

        if (ToolkitSceneManagerPrefab == null)
        {
            Debug.LogWarning("No ToolkitSceneLoader Provided! Some functionality may not work.");
        }
        else
        {
            GameObject.Instantiate(ToolkitSceneManagerPrefab);
        }
    }


    /// <summary>
    /// this is called only once when the Initial Scene is Loaded
    /// </summary>
    private void Init()
    {
        SceneSettingsManager sceneSettings = GameObject.FindObjectOfType<SceneSettingsManager>();

        if (sceneSettings != null)
        {
            SceneSettingsHandler(sceneSettings);
        }
        else
        {
            Debug.LogWarning("Can't find SceneSettingsProvider");
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
        SceneSettingsManager sceneSettings = null;
        List<SceneSettingsManager> SceneSettingsManagers = GameObject.FindObjectsOfType<SceneSettingsManager>().ToList();
        sceneSettings = SceneSettingsManagers.Find(m => m.PersistentSceneSceneSettingsManager == false);

        if (sceneSettings != null)
        {
            SceneSettingsHandler(sceneSettings);
        }
        else
        {
            Debug.LogWarning("Can't find SceneSettingsManager");
        }
    }


    /// <summary>
    /// handle scenesettings provided by the SceneSettingsProvider
    /// </summary>
    /// <param name="sceneSettingsObject">scene settings object</param>
    private void SceneSettingsHandler(SceneSettingsManager sceneSettings)
    {
        // change input State
        GameState.InputState = sceneSettings.InputState;

        // show widget
        if (sceneSettings.ShowWidgetOnSceneLoaded == true)
        {
            ShowWidget(sceneSettings.WidgetType);
        }

        // instantiate GameMode
        try
        {
            currentGameMode = GameObject.Instantiate(sceneSettings.GameMode).GetComponent<AGameMode>();
        }
        catch (Exception)
        {
            Debug.LogWarning("Can't instantiate GameMode, No GameMode assigned to SceneSettingsManager!");
        }

        // place pawns to spawnpoints

        // start roundtimer
        if (sceneSettings.StartRoundWhenSceneIsLoaded == true)
        {
            currentGameMode.StartRound();
        }
    }


    #region Player Methods

    #region Create Player Methods

    /// <summary>
    /// this will create a player instance by calling JoinPlayer for the PlayerInputManager
    /// </summary>
    /// <param name="newPlayerPrefab">optional parameter, create a player with a specific prefab, Note! it will only instantiate one player with this prefab if you want to set a new PlayerPrefab for all upcoming players to join use the method "ReplacePlayerPrefab"</param>
    /// <param name="playerIndex">optional paramater</param>
    /// <param name="splitScreenIndex">optional parameter</param>
    /// <param name="controlScheme">optional paramater</param>
    /// <param name="pairWithDevice">optional paramater, input device to assign</param>
    public new void CreatePlayer(GameObject newPlayerPrefab = null, int playerIndex = -1, int splitScreenIndex = -1, string controlScheme = null, InputDevice pairWithDevice = null)
    {
        if (ToolkitSettings.LimitPlayersByGameMode == true)
        {
            if (GameState.Players.Count < currentGameMode.MaxPlayers)
            {
                CreatePlayerHandler(newPlayerPrefab, playerIndex, splitScreenIndex, controlScheme, pairWithDevice);
            }
            else
            {
                Debug.LogError("Can't create player amount is limited by GameMode");
            }
        }
        else
        {
            CreatePlayerHandler(newPlayerPrefab, playerIndex, splitScreenIndex, controlScheme, pairWithDevice);
        }
    }


    /// <summary>
    /// handles the player creation, called by CreatePlayer
    /// </summary>
    /// <param name="newPlayerPrefab">optional parameter, create a player with a specific prefab, Note! it will only instantiate one player with this prefab if you want to set a new PlayerPrefab for all upcoming players to join use the method "ReplacePlayerPrefab"</param>
    /// <param name="playerIndex">optional paramater</param>
    /// <param name="splitScreenIndex">optional paramater</param>
    /// <param name="controlScheme">optional paramater</param>
    /// <param name="pairWithDevice">optional paramater, input device to assign</param>
    private void CreatePlayerHandler(GameObject newPlayerPrefab = null, int playerIndex = -1, int splitScreenIndex = -1, string controlScheme = null, InputDevice pairWithDevice = null)
    {
        PlayerInput player;

        // create player with a specific PlayerPrefab
        if (newPlayerPrefab != null)
        {
            // store old playerprefab
            GameObject oldPlayerPrefab = PlayerInputManager.playerPrefab;

            // override player prefab with a new one (temporary)
            PlayerInputManager.playerPrefab = newPlayerPrefab;

            // join player
            player = PlayerInputManager.JoinPlayer(playerIndex, splitScreenIndex, controlScheme, pairWithDevice);

            // restore old playerPrefab
            PlayerInputManager.playerPrefab = oldPlayerPrefab;
        }
        else // create player with default PlayerPrefab
        {
            player = PlayerInputManager.JoinPlayer(playerIndex, splitScreenIndex, controlScheme, pairWithDevice);
        }
    }


    /// <summary>
    /// this will create a player instance by calling JoinPlayer for the PlayerInputManager
    /// </summary>
    /// <param name="newPlayerPrefab">optional parameter, create a player with a specific prefab, Note! it will only instantiate one player with this prefab if you want to set a new PlayerPrefab for all upcoming players to join use the method "ReplacePlayerPrefab"</param>
    /// <param name="playerIndex">optional paramater</param>
    /// <param name="splitScreenIndex">optional parameter</param>
    /// <param name="controlScheme">optional paramater</param>
    /// <param name="pairWithDevice">optional paramater, input devices to assign</param>
    public new void CreatePlayer(GameObject newPlayerPrefab = null, int playerIndex = -1, int splitScreenIndex = -1, string controlScheme = null, params InputDevice[] pairWithDevices)
    {
        if (ToolkitSettings.LimitPlayersByGameMode == true)
        {
            if (GameState.Players.Count < currentGameMode.MaxPlayers)
            {
                CreatePlayerHandler(newPlayerPrefab, playerIndex, splitScreenIndex, controlScheme, pairWithDevices);
            }
            else
            {
                Debug.LogError("Can't create player amount is limited by GameMode");
            }
        }
        else
        {
            CreatePlayerHandler(newPlayerPrefab, playerIndex, splitScreenIndex, controlScheme, pairWithDevices);
        }
    }

    /// <summary>
    /// handles the player creation, called by CreatePlayer
    /// </summary>
    /// <param name="newPlayerPrefab">optional parameter, create a player with a specific prefab, Note! it will only instantiate one player with this prefab if you want to set a new PlayerPrefab for all upcoming players to join use the method "ReplacePlayerPrefab"</param>
    /// <param name="playerIndex">optional paramater</param>
    /// <param name="splitScreenIndex">optional paramater</param>
    /// <param name="controlScheme">optional paramater</param>
    /// <param name="pairWithDevices">optional paramater, input devices to assign</param>
    private void CreatePlayerHandler(GameObject newPlayerPrefab = null, int playerIndex = -1, int splitScreenIndex = -1, string controlScheme = null, InputDevice[] pairWithDevices = null)
    {
        PlayerInput player;

        // create player with a specific PlayerPrefab
        if (newPlayerPrefab != null)
        {
            // store old playerprefab
            GameObject oldPlayerPrefab = PlayerInputManager.playerPrefab;

            // override player prefab with a new one (temporary)
            PlayerInputManager.playerPrefab = newPlayerPrefab;

            // join player
            player = PlayerInputManager.JoinPlayer(playerIndex, splitScreenIndex, controlScheme, pairWithDevices);

            // restore old playerPrefab
            PlayerInputManager.playerPrefab = oldPlayerPrefab;
        }
        else // create player with default PlayerPrefab
        {
            player = PlayerInputManager.JoinPlayer(playerIndex, splitScreenIndex, controlScheme, pairWithDevices);
        }
    }

    #endregion



    /// <summary>
    /// replace PlayerPrefab for upcoming players to join
    /// </summary>
    /// <param name="newPlayerPrefab">new GameObject as PlayerPrefab</param>
    public new void ReplacePlayerPrefab(GameObject newPlayerPrefab)
    {
        PlayerInputManager.playerPrefab = newPlayerPrefab;
    }

    #endregion


    #region PlayerInputManager Methods


    /// <summary>
    /// change settings for PlayerInputManager on runtime
    /// </summary>
    /// <param name="playerJoinBehavior">set joinBehavior</param>
    /// <param name="joiningEnabled">enable or disable joining for player</param>
    public new void ChangePlayerInputManagerJoinSettings(PlayerJoinBehavior playerJoinBehavior, bool joiningEnabled)
    {
        PlayerInputManager.joinBehavior = playerJoinBehavior;

        if (joiningEnabled == true)
        {
            PlayerInputManager.EnableJoining();
        }
        else
        {
            PlayerInputManager.DisableJoining();
        }

    }

    #endregion


    #region PersistentData Methods

    public new void OverridePersistentData(PersistentData persistentData)
    {

        PersistentData = persistentData;

    }

    #endregion

    /// <summary>
    /// gets triggered when player is joined
    /// </summary>
    private void OnPlayerJoined()
    {
        Debug.Log("Player has joined");
        UpdatePlayerList();    
    }

    /// <summary>
    /// gets triggered when player is left
    /// </summary>
    private void OnPlayerLeft()
    {
        Debug.Log("Player has left");
        UpdatePlayerList();
    }


    #region GameMode Methods
    public new AGameMode GetCurrentGameMode()
    {
        return currentGameMode;
    }

    #endregion


}
