using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerInputManager))]
public class GameManager : ToolkitBehaviour
{

    public static GameManager Instance = null;
    //public List<Player> Players = new List<Player>();
    public PersistentData PersistentData = null;
    public SceneSettingsObject InitialSceneSettings = null;
    private AGameMode currentGameMode = null;

    public GameObject SaveGameManagerPrefab = null;
    public GameObject WidgetManagerPrefab = null;
    public PlayerInputManager PlayerInputManager = null;

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

        if (PlayerInputManager == null)
        {
            Debug.LogWarning("No Player Input Manager component is provided!");
        }
    }


    /// <summary>
    /// this is called only once when the Initial Scene is Loaded
    /// </summary>
    private void Init()
    {
        HandleSceneSettings(InitialSceneSettings);
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
        SceneSettingsObject sceneSettingsObject = GameObject.FindObjectOfType<SceneSettingsProvider>().SceneSettings;
        HandleSceneSettings(sceneSettingsObject);
    }


    /// <summary>
    /// handle scenesettings provided by the SceneSettingsProvider
    /// </summary>
    /// <param name="sceneSettingsObject"></param>
    private void HandleSceneSettings(SceneSettingsObject sceneSettingsObject)
    {
        // change input State
        GameState.InputState = sceneSettingsObject.InputState;

        // show widget
        if (sceneSettingsObject.ShowWidgetOnSceneLoaded == true)
        {
            ShowWidget(sceneSettingsObject.WidgetType);
        }

        // instantiate GameMode
        currentGameMode = GameObject.Instantiate(sceneSettingsObject.GameMode).GetComponent<AGameMode>();

        // place pawns to spawnpoints

        // start roundtimer
        if (sceneSettingsObject.StartRoundWhenSceneIsLoaded == true)
        {
            currentGameMode.StartRound();
        }
    }


    #region Player Methods

    /// <summary>
    /// this will create a player instance by calling JoinPlayer for the PlayerInputManager
    /// </summary>
    /// <param name="newPlayerPrefab">optional parameter, create a player with a specific prefab, Note! it will only instantiate one player with this prefab if you want to set a new PlayerPrefab for all upcoming players to join use the method "ReplacePlayerPrefab"</param>
    /// <param name="playerIndex">optional paramater</param>
    /// <param name="splitScreenIndex">optional parameter</param>
    /// <param name="controlScheme">optional paramater</param>
    public new void CreatePlayer(GameObject newPlayerPrefab = null, int playerIndex = -1, int splitScreenIndex = -1, string controlScheme = null)
    {

        PlayerInput player;

        if (newPlayerPrefab != null)
        {
            // store old playerprefab
            GameObject oldPlayerPrefab = PlayerInputManager.playerPrefab;

            // override player prefab with a new one
            PlayerInputManager.playerPrefab = newPlayerPrefab;
            
            // join player
            player = PlayerInputManager.JoinPlayer(playerIndex, splitScreenIndex, controlScheme);

            // restore old playerPrefab
            PlayerInputManager.playerPrefab = oldPlayerPrefab;
        }
        else
        {
            player = PlayerInputManager.JoinPlayer(playerIndex, splitScreenIndex, controlScheme);
        }
    }


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


    #region GameMode Methods
    public new AGameMode GetCurrentGameMode()
    {
        return currentGameMode;
    }

    #endregion


}
