using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : AWidget
{
    public void ShowSettings()
    {
        WidgetManager.Instance.ShowWidget(WidgetTypes.Settings);
    }
}
