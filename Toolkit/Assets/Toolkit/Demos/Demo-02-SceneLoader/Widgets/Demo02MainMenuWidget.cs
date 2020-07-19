﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Demo02MainMenuWidget : AWidget
{
    public void LoadScene01Async()
    {
        // scene 01 is a child of the PersistentScene when loading Additive, 
        // when loading Single you see the Loading screen until "new" Scene loading is completed and Persistent Scene will be unloaded
        ToolkitSceneManager .Instance.LoadSceneAsync("Scene01", false, LoadSceneMode.Additive);
    }

    public void LoadScene02()
    {
        // load/switch to Scene 02
        ToolkitSceneManager .Instance.LoadScene("Scene02");
    }
}
