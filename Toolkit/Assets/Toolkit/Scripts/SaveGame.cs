using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[System.Serializable]
public class SaveGame
{
    // extend this class to your needs
    #region Variables
    
    public float Example;

    #endregion


    /// <summary>
    /// mapping peristentData to SaveGame data
    /// </summary>
    /// <param name="persistentData"></param>
    public SaveGame(PersistentData persistentData)
    {

        // set saveGame data from the persistentData object
        // example
        // int x = persistentData.x;

        Example = persistentData.Example;
    }

}

