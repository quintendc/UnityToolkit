using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MyPersistentDataObject", menuName = "Toolkit/PeristentDataObject", order = 0)]
public class PersistentData : ScriptableObject
{
    // place variables that your need throughout the entire game session here
    public float Example;


    /// <summary>
    /// mapping SaveGame data to this PersistentData
    /// </summary>
    /// <param name="saveGame"></param>
    public PersistentData(SaveGame saveGame)
    {

    }

}
