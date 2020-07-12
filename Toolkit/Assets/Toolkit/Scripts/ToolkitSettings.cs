using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class ToolkitSettings
{

    [Header("Player Settings")]
    [Tooltip("Amount of players that can join is limited by current GameMode Settings")]
    public bool LimitPlayersByGameMode = true;

    [Header("Pause screen settings")]
    [Tooltip("Ässign Widgets to this list where you can call the PauseGame method, always include your PauseScreen")]
    public List<AWidget> PauseGameEnabledWidgets = new List<AWidget>();

    [Header("Loading screen settings")]
    [Tooltip("when true the loading screen will automatically displayed when scene is loading. Note! When a Scene is Loaded the LoadingScreen will be removed automatically depending on the SceneSettingsManager (ShowWidgetOnSceneLoaded).")]
    public bool ShowLoadingScreenWhenLoadingScene = true;

}