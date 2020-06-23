using System.Collections.Generic;
using UnityEngine;

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
        // when method is called again and game is already paused unpause the game
        if (unPauseWhenPaused == true)
        {
            if (GameState.Paused == true)
            {
                GameState.Paused = false;
            }
        }

        if (GameState.Paused == false)
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
    /// this method creates a player by calling the CreatePlayer from the GameManager
    /// </summary>
    /// <param name="spawnPawn">should a pawn be instantiated</param>
    public static void CreatePlayer(bool spawnPawn) 
    {
        GameManager.Instance.CreatePlayer(spawnPawn);
    }

    /// <summary>
    /// get a player by Id this will return the Player poco class from GameManager Players list
    /// </summary>
    /// <param name="id">player id</param>
    /// <returns>poco item from Players list</returns>
    public Player GetPlayerById(int id)
    {
        return GameState.Players.Find(p => p.Id == id);
    }


    /// <summary>
    /// move the pawn to a specific location
    /// </summary>
    /// <param name="playerIndex">player index</param>
    /// <param name="position">pawn location in the scene</param>
    /// <param name="rotation">set rotation of the pawn</param>
    public void PlacePawn(int playerIndex, Vector3 position, Quaternion rotation)
    {
        // get player by id
        Player player = GameState.Players.Find(p => p.Id == playerIndex);

        // set position and rotation of pawn
        player.Pawn.gameObject.transform.position = position;
        player.Pawn.gameObject.transform.rotation = rotation;
    }

    /// <summary>
    /// move the pawn to a specific location
    /// </summary>
    /// <param name="pawn">player pawn</param>
    /// <param name="position">pawn location in the scene</param>
    /// <param name="rotation">set rotation of the pawn</param>
    public void PlacePawn(GameObject pawn, Vector3 position, Quaternion rotation)
    {
        pawn.transform.position = position;
        pawn.transform.rotation = rotation;
    }


    /// <summary>
    /// check if player with id has a pawn assigned
    /// </summary>
    /// <param name="playerId"></param>
    /// <returns>true player has a pawn assigned, false player doesn't have a pawn assigned, null player doesn't exists</returns>
    public bool? DoesPlayerHasPawn(int playerId)
    {

        Player player = GameState.Players.Find(p => p.Id == playerId);

        if (player == null)
        {
            return null;
        }

        if (player.Pawn != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// create a pawn for a player that doesn't have a pawn yet, the pawn that will be instantiated is from the currentGameMode
    /// </summary>
    /// <param name="playerId">id of the player</param>
    /// <param name="position">position in the scene</param>
    /// <param name="rotation">rotation of the pawn</param>
    public void CreatePawnForPlayer(int playerId, Vector3 position, Quaternion rotation)
    {
        GameManager.Instance.CreatePawnForPlayer(playerId, position, rotation);
    }


    #endregion

    #region GameModeMethods

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

}

