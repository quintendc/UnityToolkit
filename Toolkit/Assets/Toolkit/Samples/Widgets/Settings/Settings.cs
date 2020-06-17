using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : AWidget
{

    public string saveGameName = "";

    public void ShowMainMenu()
    {
        WidgetManager.Instance.ShowWidget(WidgetTypes.MainMenu);
    }

    public void SaveGameBtn()
    {
        SaveGame(saveGameName);
    }

    public void LoadSaveGameBtn()
    {
        //LoadGameById(0);
        LoadGameByName(saveGameName);
    }

    public void saveGameNameInput(string input)
    {
        saveGameName = input;
    }
}
