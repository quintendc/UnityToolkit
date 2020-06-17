using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : AWidget
{
    public void ShowMainMenu()
    {
        WidgetManager.Instance.ShowWidget(WidgetTypes.MainMenu);
    }

    public void SaveGameBtn()
    {
        SaveGame();
    }

    public void LoadSaveGameBtn()
    {
        LoadGameById(0);
    }
}
