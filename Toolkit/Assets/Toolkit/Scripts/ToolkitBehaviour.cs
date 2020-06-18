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
    /// default settinsg when unpaused
    /// time scale 1
    /// GamePaused boolean in PersistentData is false
    /// default widget to show "HUD"
    /// 
    /// </summary>
    /// <param name="overrideDefaultWidgetType">override the default WidgetType (pause)</param>
    public void PauseGame(WidgetTypes? overrideDefaultWidgetType = null)
    {

        if (GameManager.Instance.PersistentData.Paused == false)
        {
            Time.timeScale = 0;

            GameManager.Instance.PersistentData.Paused = true;

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
            GameManager.Instance.PersistentData.Paused = false;

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
        return GameManager.Instance.PersistentData.Paused;
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
        return GameManager.Instance.Players.Find(p => p.Id == id);
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
        Player player = GameManager.Instance.Players.Find(p => p.Id == playerIndex);

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


    #endregion

    #region GameModeMethods

    public GameObject GetCurrentGameMode()
    {
        return GameManager.Instance.GetCurrentGameMode();
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
    public void SaveGame(string saveGameName)
    {
        // pass saveGameName to SaveGameManager
        SaveGameManager.Instance.SaveGame(saveGameName);
    }

    /// <summary>
    /// load game by name
    /// </summary>
    /// <param name="saveGameName"></param>
    public void LoadGameByName(string saveGameName)
    {
        SaveGameManager.Instance.LoadGameByName(saveGameName);
    }

    /// <summary>
    /// load game by id
    /// </summary>
    /// <param name="id"></param>
    public void LoadGameByIndex(int index)
    {
        SaveGameManager.Instance.LoadGameByIndex(index);
    }

    public List<SaveGame> GetAllSaveGames()
    {
        return SaveGameManager.Instance.GetAllSaveGames();
    }

    #endregion

}

