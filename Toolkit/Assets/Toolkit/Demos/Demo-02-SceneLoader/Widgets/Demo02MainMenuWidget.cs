using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Demo02MainMenuWidget : AWidget
{
    public void LoadScene01Additive()
    {
        GetToolkitSceneLoader().LoadSceneAsync("Scene01", LoadSceneMode.Additive);
    }

    public void LoadScene02Single()
    {
        //GetToolkitSceneLoader().LoadSceneAsync("Scene02", LoadSceneMode.Additive);
        GetToolkitSceneLoader().LoadScene("Scene02");
    }
}
