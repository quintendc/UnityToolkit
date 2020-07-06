﻿using System;
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
    public PersistentData PersistentData = null;
    private AGameMode currentGameMode = null;
    [Tooltip("Amount of players that can join is limited by current GameMode Settings")]
    public bool LimitPlayersByGameMode = true;

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
        currentGameMode = GameObject.Instantiate(sceneSettings.GameMode).GetComponent<AGameMode>();

        // place pawns to spawnpoints

        // start roundtimer
        if (sceneSettings.StartRoundWhenSceneIsLoaded == true)
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
        if (LimitPlayersByGameMode == true)
        {
            if (GameState.Players.Count < currentGameMode.MaxPlayers)
            {
                CreatePlayerHandler(newPlayerPrefab, playerIndex, splitScreenIndex, controlScheme);
            }
            else
            {
                Debug.LogError("Can't create player amount is limited by GameMode");
            }
        }
        else
        {
            CreatePlayerHandler(newPlayerPrefab, playerIndex, splitScreenIndex, controlScheme);
        }
    }

    /// <summary>
    /// handles the player creation, called by CreatePlayer
    /// </summary>
    /// <param name="newPlayerPrefab">optional parameter, create a player with a specific prefab, Note! it will only instantiate one player with this prefab if you want to set a new PlayerPrefab for all upcoming players to join use the method "ReplacePlayerPrefab"</param>
    /// <param name="playerIndex">optional paramater</param>
    /// <param name="splitScreenIndex">optional paramater</param>
    /// <param name="controlScheme">optional paramater</param>
    private void CreatePlayerHandler(GameObject newPlayerPrefab = null, int playerIndex = -1, int splitScreenIndex = -1, string controlScheme = null)
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
            player = PlayerInputManager.JoinPlayer(playerIndex, splitScreenIndex, controlScheme);

            // restore old playerPrefab
            PlayerInputManager.playerPrefab = oldPlayerPrefab;
        }
        else // create player with default PlayerPrefab
        {
            player = PlayerInputManager.JoinPlayer(playerIndex, splitScreenIndex, controlScheme);
        }

        GameState.Players.Add(player);
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
