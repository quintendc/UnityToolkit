using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Demo02MainMenuWidget : AWidget
{
    public void LoadScene01()
    {
        GetToolkitSceneLoader().LoadSceneAsync("Scene01", LoadSceneMode.Additive);
    }
}
