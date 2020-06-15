using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : AWidget
{
    public void ShowMainMenu()
    {
        WidgetManager.Instance.ShowWidget(WidgetTypes.MainMenu);
    }
}
