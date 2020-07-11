using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Demo02MainMenuWidget : AWidget
{
    public void LoadScene01Async()
    {
        // scene 01 is a child of the PersistentScene
        GetToolkitSceneLoader().LoadSceneAsync("Scene01", LoadSceneMode.Additive);
    }

    public void LoadScene02()
    {
        // load/switch to Scene 02
        GetToolkitSceneLoader().LoadScene("Scene02");
    }
}
