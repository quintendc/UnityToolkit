using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance = null;
    public List<Player> Players = new List<Player>();
    public PersistentData PersistentData = null;
    public SceneSettingsObject InitialSceneSettings = null;
    private GameObject currentGameMode = null;

    public GameObject SaveGameManagerPrefab = null;
    public GameObject WidgetManagerPrefab = null;

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

        if (PersistentData == null)
        {
            Debug.LogWarning("No PersistentDataObject Provided! Some functionality may not work.");
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

            // setup playercontrollers and pawns

        }
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("scene loaded");
        //do stuff

        // get sceneSettingsObject
        SceneSettingsObject sceneSettingsObject = GameObject.FindGameObjectWithTag("SceneSettingsProvider").GetComponent<SceneSettingsProvider>().SceneSettings;

        // set WidgetRenderCamera first before instantiating a widget
        WidgetManager.Instance.SetRenderCamera();

        // handle SceneSettingsObject
        WidgetManager.Instance.ShowWidget(sceneSettingsObject.WidgetType);

        
    }


    #region Player Methods

    /// <summary>
    /// this method creates a new player "PlayerController" Pawn is optional
    /// </summary>
    /// <param name="spawnPawn">optional parameter should pawn be instantiated?</param>
    public void CreatePlayer(bool spawnPawn = false)
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

            Player newPlayer = new Player(Players.Count, pawn.gameObject.GetComponent<APawn>(), pc.GetComponent<APlayerController>());

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
    public void UpdatePlayersForGameMode()
    {
        foreach (var player in Players)
        {
            // track playerid
            int id = player.Id;

            // destroy current gameobjects for pawn and playercontroller
            Destroy(player.Pawn.gameObject);
            Destroy(player.PlayerController.gameObject);

            // instantiate new pawn and new playercontroller
            GameObject newPawn = GameObject.Instantiate(currentGameMode.GetComponent<AGameMode>().DefaultPawn);
            GameObject newPlayerController = GameObject.Instantiate(currentGameMode.GetComponent<AGameMode>().DefaultPlayerController);

            player.UpdatePlayer(id, newPawn.GetComponent<APawn>(), newPlayerController.GetComponent<APlayerController>());
        }
    }

    #endregion

}
