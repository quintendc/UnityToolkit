using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[System.Serializable]
public class SaveGame
{

    public float Example;


    /// <summary>
    /// mapping peristentData to SaveGame data
    /// </summary>
    /// <param name="persistentData"></param>
    public SaveGame(PersistentData persistentData)
    {
        Example = persistentData.Example;
    }

}

