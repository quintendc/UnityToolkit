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
    public List<Player> Players = new List<Player>();
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

        SetupInitialScene();
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
    /// the scene will be setup by the provided InitialSceneSettingsObject
    /// </summary>
    private void SetupInitialScene()
    {
        // spawn widget
        if (InitialSceneSettings != null)
        {
            // spawn widget
            WidgetManager.Instance.ShowWidget(InitialSceneSettings.WidgetType);

            // spawn gameMode
            currentGameMode = GameObject.Instantiate(InitialSceneSettings.GameMode);

            // spawn at least 1 PlayerController
            CreatePlayer(SpawnPawn);
        }
    }


    /// <summary>
    /// callback when scene is loaded
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("scene loaded");
        //do stuff

        // get sceneSettingsObject
        SceneSettingsObject sceneSettingsObject = GameObject.FindGameObjectWithTag("SceneSettingsProvider").GetComponent<SceneSettingsProvider>().SceneSettings;

        // set WidgetRenderCamera first before instantiating a widget
        //WidgetManager.Instance.SetRenderCamera();

        // handle SceneSettingsObject
        // set widget to be shown
        WidgetManager.Instance.ShowWidget(sceneSettingsObject.WidgetType);

        // set currentGameMode to new GameMode
        currentGameMode = GameObject.Instantiate(sceneSettingsObject.GameMode);

        // replace Pawns and playerControllers
        Debug.Log("log" + sceneSettingsObject);
        UpdatePlayersForGameMode(sceneSettingsObject.OverridePlayerPawns, sceneSettingsObject.OverridePlayerPlayerControllers);        
    }


    #region Player Methods

    /// <summary>
    /// this method creates a new player "PlayerController" Pawn is optional
    /// </summary>
    /// <param name="spawnPawn">optional parameter should pawn be instantiated?</param>
    public new void CreatePlayer(bool spawnPawn = false)
    {

        // check if there are still playersslots left for the currentGameMode
        if (Players.Count < currentGameMode.GetComponent<AGameMode>().MaxPlayers)
        {
            // playerController is always created, Pawn is optional
            AGameMode gm = currentGameMode.GetComponent<AGameMode>();

            GameObject pawn = null;

            if (spawnPawn == true)
            {
                pawn = GameObject.Instantiate(gm.DefaultPawn);
            }

            GameObject pc = GameObject.Instantiate(gm.DefaultPlayerController);

            // bug pawn is not set -> can't get component of null object
            Player newPlayer = new Player(Players.Count, pawn != null ? pawn.gameObject.GetComponent<APawn>() : null, pc.GetComponent<APlayerController>());
            //Player newPlayer = new Player(Players.Count, pawn.gameObject.GetComponent<APawn>(), pc.GetComponent<APlayerController>());

            // add to players array
            Players.Add(newPlayer);
        }
        else
        {
            Debug.LogWarning("Can't create player because there are no empty player slots for the current GameMode.");
        }
    }


    /// <summary>
    /// destroy player pawn, playercontroller or both
    /// </summary>
    /// <param name="id">index of player</param>
    /// <param name="destroyPawn">should pawn be destroyed</param>
    /// <param name="destroyPlayerController">should playercntroller be destroyed</param>
    public void DestroyPlayerById(int id, bool destroyPawn = true, bool destroyPlayerController = true)
    {
        if (destroyPawn == true)
        {
            Destroy(Players.ElementAt(id).Pawn.gameObject);
        }

        if (destroyPlayerController == true)
        {
            Destroy(Players.ElementAt(id).PlayerController.gameObject);
        }

        // destroy player poco class when both pawn and Playercontroller are destroyed
        if (destroyPawn == true && destroyPlayerController == true)
        {
            Players.RemoveAt(id);
        }
    }

    /// <summary>
    /// this method will replace all pawns and playerscontrollers to the currentGameMode default Pawn and PlayerController
    /// </summary>
    public void UpdatePlayersForGameMode(bool updatePawn, bool updatePlayerController)
    {
        foreach (var player in Players)
        {
            // track playerid
            int id = player.Id;

            // destroy current gameobjects for pawn and playercontroller
            if (player.Pawn != null)
            {
                Destroy(player.Pawn.gameObject);
            }

            if (player.PlayerController != null)
            {
                Destroy(player.PlayerController.gameObject);
            }

            GameObject newPawn = null;
            GameObject newPlayerController = null;

            // instantiate new pawn and new playercontroller
            if (updatePawn == true)
            {
                newPawn = GameObject.Instantiate(currentGameMode.GetComponent<AGameMode>().DefaultPawn);
            }

            if (updatePlayerController == true)
            {
                newPlayerController = GameObject.Instantiate(currentGameMode.GetComponent<AGameMode>().DefaultPlayerController);
            }

            player.UpdatePlayer(id, newPawn.GetComponent<APawn>(), newPlayerController.GetComponent<APlayerController>());
        }
    }


    /// <summary>
    /// create a pawn for a player that doesn't have a pawn yet, the pawn that will be instantiated is from the currentGameMode
    /// </summary>
    /// <param name="playerId">id of the player</param>
    /// <param name="position">position in the scene</param>
    /// <param name="rotation">rotation of the pawn</param>
    public new void CreatePawnForPlayer(int playerId, Vector3 position, Quaternion rotation)
    {
        Player player = Players.Find(p => p.Id == playerId);

        if (player.Pawn != null)
        {
            Debug.LogWarning("the player with Id:" + playerId + " already has a pawn assigned, call is aborted!");
        }
        else
        {
            GameObject pawn = GameObject.Instantiate(currentGameMode.GetComponent<AGameMode>().DefaultPawn, position, rotation);

            player.UpdatePlayer(null, pawn.GetComponent<APawn>(), null);
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
    public new GameObject GetCurrentGameMode()
    {
        return currentGameMode;
    }

    #endregion


}
