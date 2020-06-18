using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : AWidget
{

    public string saveGameName = "";
    public Text sliderValue = null;

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
        LoadGameByName(saveGameName);
        //LoadGameByIndex(0);
    }

    public void ListSaveGamesBtn()
    {
        List<SaveGame> x = GetAllSaveGames();
    }

    public void saveGameNameInput(string input)
    {
        saveGameName = input;
    }

    public void Slider(float value)
    {
        Debug.Log(value);
        sliderValue.text = value.ToString();

        PersistentData data = GetPersistentData();
        data.Example = value;

    }
}
