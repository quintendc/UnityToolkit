using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class ToolkitBehaviour : MonoBehaviour
{

    #region General Methods

    /// <summary>
    /// pause or unpause the game
    /// 
    /// default settings when paused are:
    /// timeScale 0
    /// GamePaused boolean in persistentData is true
    /// default widget to show "Pause"
    /// 
    /// default settings when unpaused
    /// time scale 1
    /// GamePaused boolean in PersistentData is false
    /// default widget to show "HUD"
    /// 
    /// </summary>
    /// <param name="overrideDefaultWidgetType">override the default WidgetType (pause)</param>
    /// <param name="unPause">When the method is called again the game will be unpaused again</param>
    public void PauseGame(WidgetTypes? overrideDefaultWidgetType = null, bool unPauseWhenPaused = true)
    {

        bool skip = false;

        // when method is called again and game is already paused unpause the game
        if (unPauseWhenPaused == true)
        {
            if (GameState.Paused == true)
            {
                skip = true;
                HideCurrentWidget();
                GameState.Paused = false;
            }
        }

        if (GameState.Paused == false && skip == false)
        {
            Time.timeScale = 0;

            GameState.Paused = true;

            // show pause widget (default setting)
            if (overrideDefaultWidgetType == null)
            {
                ShowWidget(WidgetTypes.Pause);
            }
            else
            {
                ShowWidget((WidgetTypes)overrideDefaultWidgetType);
            }
        }
        else
        {
            Time.timeScale = 1;
            GameState.Paused = false;

            // show HUD again (default setting)
            if (overrideDefaultWidgetType == null)
            {
                ShowWidget(WidgetTypes.HUD);
            }
            else
            {
                ShowWidget((WidgetTypes)overrideDefaultWidgetType);
            }
        }
    }

    /// <summary>
    /// get a boolean if the game is set to pause
    /// </summary>
    /// <returns></returns>
    public bool IsGamePaused()
    {
        return GameState.Paused;
    }

    #endregion

    #region Player, Pawn and PlayerController Methods

    /// <summary>
    /// this will create a player instance by calling JoinPlayer for the PlayerInputManager
    /// </summary>
    /// <param name="newPlayerPrefab">optional parameter, create a player with a specific prefab, Note! it will only instantiate one player with this prefab if you want to set a new PlayerPrefab for all upcoming players to join use the method "ReplacePlayerPrefab"</param>
    /// <param name="playerIndex">optional paramater</param>
    /// <param name="splitScreenIndex">optional parameter</param>
    /// <param name="controlScheme">optional paramater</param>
    public void CreatePlayer(GameObject newPlayerPrefab = null, int playerIndex = -1, int splitScreenIndex = -1, string controlScheme = null)
    {
        GameManager.Instance.CreatePlayer(newPlayerPrefab, playerIndex, splitScreenIndex, controlScheme);
    }

    /// <summary>
    /// replace PlayerPrefab for upcoming players to join
    /// </summary>
    /// <param name="newPlayerPrefab">new GameObject as PlayerPrefab</param>
    public void ReplacePlayerPrefab(GameObject newPlayerPrefab)
    {
        GameManager.Instance.ReplacePlayerPrefab(newPlayerPrefab);
    }


    /// <summary>
    /// get PlayerInput by player index
    /// </summary>
    /// <param name="index">index of playerInput</param>
    /// <returns>corresponding PlayerInput</returns>
    public PlayerInput GetPlayerByIndex(int index)
    {
        return GameState.Players.Find(p => p.playerIndex == index);
    }


    ///// <summary>
    ///// move the pawn to a specific location
    ///// </summary>
    ///// <param name="pawn">player pawn</param>
    ///// <param name="position">pawn location in the scene</param>
    ///// <param name="rotation">set rotation of the pawn</param>
    //public void PlacePawn(GameObject pawn, Vector3 position, Quaternion rotation)
    //{
    //    pawn.transform.position = position;
    //    pawn.transform.rotation = rotation;
    //}
 

    ///// <summary>
    ///// create a pawn for a player that doesn't have a pawn yet, the pawn that will be instantiated is from the currentGameMode
    ///// </summary>
    ///// <param name="playerId">id of the player</param>
    ///// <param name="position">position in the scene</param>
    ///// <param name="rotation">rotation of the pawn</param>
    //public void CreatePawnForPlayer(int playerId, Vector3 position, Quaternion rotation)
    //{
    //    GameManager.Instance.CreatePawnForPlayer(playerId, position, rotation);
    //}

    /// <summary>
    /// get all spawnpoints for the scene
    /// </summary>
    /// <returns>list of spawnpoints</returns>
    public List<Spawnpoint> GetSpawnpoints()
    {
        return FindObjectsOfType<Spawnpoint>().ToList();
    }


    #endregion

    #region PlayerInputManager methods

    /// <summary>
    /// change settings for PlayerInputManager on runtime
    /// </summary>
    /// <param name="playerJoinBehavior">set joinBehavior</param>
    /// <param name="joiningEnabled">enable or disable joining for player</param>
    public void ChangePlayerInputManagerJoinSettings(PlayerJoinBehavior playerJoinBehavior, bool joiningEnabled)
    {
        GameManager.Instance.ChangePlayerInputManagerJoinSettings(playerJoinBehavior, joiningEnabled);
    }

    #endregion

    #region GameMode Methods

    /// <summary>
    /// get the currentGameMode that is running
    /// </summary>
    /// <returns></returns>
    public AGameMode GetCurrentGameMode()
    {
        return GameManager.Instance.GetCurrentGameMode().GetComponent<AGameMode>();
    }


    /// <summary>
    /// Start the round of the GameMode
    /// </summary>
    public void StartRound()
    {
        GetCurrentGameMode().StartRound();
    }

    /// <summary>
    /// End the round of the GameMode
    /// </summary>
    public void EndRound()
    {
        GetCurrentGameMode().EndRound();
    }

    /// <summary>
    /// how many time is already elapsed for this Round
    /// </summary>
    /// <returns>time elapsed for current round if the GameMode</returns>
    public float RoundElapsedTime()
    {
        return GetCurrentGameMode().TimeElapsed();
    }

    /// <summary>
    /// how many time is left before the round ends
    /// </summary>
    /// <returns>time left before round ends</returns>
    public float RoundTimeLeft()
    {
        return GetCurrentGameMode().TimeLeft();
    }

    #endregion

    #region PersistentData

    /// <summary>
    /// get the persistentData object from the GameManager
    /// </summary>
    /// <returns>current persistent data object from the gameManager</returns>
    public PersistentData GetPersistentData()
    {
        return GameManager.Instance.PersistentData;
    }


    /// <summary>
    /// override the PersistentData in the GameManager -> call this methdo after loading a saveGame
    /// </summary>
    /// <param name="persistentData">new persistentData object</param>
    public void OverridePersistentData(PersistentData persistentData)
    {
        GameManager.Instance.OverridePersistentData(persistentData);
    }

    #endregion

    #region Widget Methods

    /// <summary>
    /// this will ask the WidgetManager to show Widget X
    /// </summary>
    /// <param name="widget">widget to show</param>
    public void ShowWidget(WidgetTypes widget)
    {
        WidgetManager.Instance.ShowWidget(widget);
    }


    /// <summary>
    /// get current widget that is displayed
    /// </summary>
    /// <returns>current AWidget object</returns>
    public AWidget GetCurrentWidget()
    {
        return WidgetManager.Instance.GetCurrentWidget();
    }


    /// <summary>
    /// hide the current diplayed widget
    /// </summary>
    public void HideCurrentWidget()
    {
        WidgetManager.Instance.HideCurrentWidget();
    }

    #endregion

    #region Save & Load Methods

    /// <summary>
    /// this will save the current Game
    /// </summary>
    /// <param name="saveGameName">this is an optional parameter, by default the saveGame will be named like SaveGameX.save where X is an Id based on the found SaveGames</param>
    /// <returns>is saveGame saved successfully</returns>
    public bool SaveGame(string saveGameName = null)
    {
        // pass saveGameName to SaveGameManager
        return SaveGameManager.Instance.SaveGame(saveGameName);
    }

    /// <summary>
    /// load game by name
    /// </summary>
    /// <param name="saveGameName">name of the saveGame you want to be loaded</param>
    /// <returns>is loading teh saveGame successfull</returns>
    public bool LoadGameByName(string saveGameName)
    {
        return SaveGameManager.Instance.LoadGameByName(saveGameName);
    }

    /// <summary>
    /// load game by index
    /// </summary>
    /// <param name="index">index of saveGame</param>
    /// <returns>is loading the saveGame successfull</returns>
    public bool LoadGameByIndex(int index)
    {
        return SaveGameManager.Instance.LoadGameByIndex(index);
    }


    /// <summary>
    /// get a list of all saveGames found on the local Disk
    /// </summary>
    /// <returns>list of saveGames</returns>
    public List<SaveGame> GetAllSaveGames()
    {
        return SaveGameManager.Instance.GetAllSaveGames();
    }

    #endregion

    #region GameFeel

    /// <summary>
    /// shake the Toolkit camera
    /// </summary>
    /// <param name="ForceShake">when true the shake FX will be applied even when there is another FX active like MotionFX</param>
    /// <param name="overrideDefaultDuration">duration of the shake in seconds</param>
    /// <param name="overrideDefaultMagnitude">the magnitude of the shake</param>
    /// <param name="overrideShakeX">override default camers X Axis setting</param>
    /// <param name="overrideShakeY">override default camers Y Axis setting</param>
    /// <param name="overrideShakeZ">override default camers Z Axis setting</param>
    public void ToolkitCameraShake(bool ForceShake = false, float? overrideDefaultDuration = null, float? overrideDefaultMagnitude = null, bool overrideShakeX = true, bool overrideShakeY = true, bool overrideShakeZ = false)
    {
        ToolkitCamera toolkitCamera = FindObjectOfType<ToolkitCamera>();

        // always shake even when frozen
        if (ForceShake == true)
        {
            toolkitCamera.ShakeToolkitCamera(overrideDefaultDuration, overrideDefaultMagnitude, overrideShakeX, overrideShakeY, overrideShakeZ);
        }
        else
        {
            // only shake timescale is 1
            if (Time.timeScale == 1)
            {
                toolkitCamera.ShakeToolkitCamera(overrideDefaultDuration, overrideDefaultMagnitude, overrideShakeX, overrideShakeY, overrideShakeZ);
            }
            else
            {
                Debug.LogWarning("can't shake camera when games timeScale is not 1, this means there is an other MotionFX applied already");
            }
        }   
    }


    /// <summary>
    /// apply a motion effect to the game for a period of time
    /// </summary>
    /// <param name="timescale">a value under 1 will give a Slow Motion effect, a value greater then 1 will give a Fast Motion effect</param>
    /// <param name="duration">time of the motion effect in seconds</param>
    public void MotionFX(float timescale, float duration)
    {
        StartCoroutine(Motion(timescale, duration));
    }

    private IEnumerator Motion(float newTimeScale, float duration)
    {
        float storedTimeScale = Time.timeScale;

        Time.timeScale = newTimeScale;

        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = storedTimeScale;
    }

    #endregion

}

