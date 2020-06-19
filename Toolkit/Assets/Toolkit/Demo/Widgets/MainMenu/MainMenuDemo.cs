using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuDemo : AWidget
{
    public void Play()
    {
        // load scene1
    }

    public void Settings()
    {
        // open settings widget
        ShowWidget(WidgetTypes.Settings);
    }
}
