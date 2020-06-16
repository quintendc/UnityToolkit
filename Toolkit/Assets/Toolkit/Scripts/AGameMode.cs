using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class AGameMode : MonoBehaviour
{

    public GameObject DefaultPawn = null;
    public GameObject DefaultPlayerController = null;

    public int MaxPlayers = 1;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    #region spawnpoint Methods


    /// <summary>
    /// get a list of all spawnpoints
    /// </summary>
    /// <returns></returns>
    public List<GameObject> GetAllSpawnpoints()
    {
        List<GameObject> spawnpoints = GameObject.FindGameObjectsWithTag("Spawnpoint").ToList();

        return spawnpoints;
    }


    /// <summary>
    /// get a list of all spawnpoints where PlayerIndex is equal to ...
    /// </summary>
    /// <param name="playerIndex">playerIndex filter</param>
    /// <returns></returns>
    public List<GameObject> GetSpawnpointsByPlayerIndex(int playerIndex)
    {
        List<GameObject> spawnpoints = GetAllSpawnpoints();
        List<GameObject> filterdSpawnpoints = (List<GameObject>)spawnpoints.Where(s => s.GetComponent<Spawnpoint>().PlayerIndex == playerIndex);

        return filterdSpawnpoints;
    }


    /// <summary>
    /// get a spawnpoint from all spawnpoints at index ...
    /// </summary>
    /// <param name="index">index of the spawnpoint</param>
    /// <returns></returns>
    public GameObject GetSpawnpointByIndex(int index)
    {
        List<GameObject> spawnpoints = GetAllSpawnpoints();

        return spawnpoints.ElementAt(index);
    }
    #endregion


}
