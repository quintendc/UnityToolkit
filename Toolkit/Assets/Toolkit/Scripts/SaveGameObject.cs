using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MySaveGameObject", menuName = "Toolkit/SaveGameObject", order = 1)]
[System.Serializable]
public class SaveGameObject : ScriptableObject
{
    public int Id { get; set; }
    public string Name { get; set; }

    public SaveGameObject(PersistentData persistentData, string name = null, int? id = null)
    {

        // override id and/or name
        if (id != null)
        {
            Id = (int)id;
        }

        if (name != null)
        {
            Name = name;
        }

        // override local properties with persistentData properties

        // XX = persistentData.YY
        // ...
        // ...

    }
}
