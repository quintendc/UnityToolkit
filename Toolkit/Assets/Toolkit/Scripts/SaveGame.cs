using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[System.Serializable]
public class SaveGame
{

    public float Example;

    public SaveGame(PersistentData persistentData)
    {
        Example = persistentData.Example;
    }

}

