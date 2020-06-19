using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuDemo : AWidget
{
    public void Play()
    {
        // load scene1
        SceneManager.LoadScene("Scene1");
    }

    public void Settings()
    {
        // open settings widget
        ShowWidget(WidgetTypes.Settings);
    }
}
