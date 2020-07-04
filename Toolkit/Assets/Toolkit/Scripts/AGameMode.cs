using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class AGameMode : ToolkitBehaviour
{

    [Header("Player Settings")]
    public GameObject DefaultPawn = null;

    public int MaxPlayers = 1;

    public bool InfiniteTime = false;
    [Tooltip("in seconds")]
    public float RoundTime = 300f;
    
    private float timeElapsed;
    private bool roundStarted = false;

    // Start is called before the first frame update
    protected virtual void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
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


    #region Round time

    public new virtual IEnumerator StartRound()
    {
        roundStarted = true;
        yield return StartCoroutine(RoundTimer());
    }

    public new virtual void EndRound()
    {
        roundStarted = false;
        StopCoroutine(RoundTimer());
    }

    public bool HasBegun()
    {
        return roundStarted;
    }

    public float TimeElapsed()
    {
        return timeElapsed;
    }

    public float TimeLeft()
    {
        return RoundTime - timeElapsed;
    }

    private IEnumerator RoundTimer()
    {
        while (timeElapsed < RoundTime)
        {
            yield return new WaitForSeconds(0.01f);
            Debug.Log("GameMode time elapsed: " + TimeElapsed());
            timeElapsed += 0.01f;
        }

        if (InfiniteTime != true)
        {
            EndRound();
        }
    }

    #endregion


}
