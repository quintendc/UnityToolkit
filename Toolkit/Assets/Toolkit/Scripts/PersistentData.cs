using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MyPersistentDataObject", menuName = "Toolkit/PeristentDataObject", order = 0)]
public class PersistentData : ScriptableObject
{
    // extend this class to your needs
    #region Varbiables
    
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

        Example = saveGame.Example;
    }

}
