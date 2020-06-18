﻿using System.Collections.Generic;
using UnityEngine;

public class ToolkitBehaviour : MonoBehaviour
{

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

    public PersistentData GetPersistentData()
    {
        return GameManager.Instance.PersistentData;
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

