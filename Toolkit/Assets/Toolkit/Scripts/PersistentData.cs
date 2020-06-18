using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentData
{
    // extend this class to your needs
    #region Varbiables

    public bool Paused = false;
    public float Example;

    #endregion


    /// <summary>
    /// mapping SaveGame data to this PersistentData
    /// </summary>
    /// <param name="saveGame"></param>
    public PersistentData(SaveGame saveGame)
    {
        // set saveGame data from the persistentData object
        // example
        // int x = saveGame.x;

        if (saveGame != null)
        {

            Example = saveGame.Example;

        }

    }

}
