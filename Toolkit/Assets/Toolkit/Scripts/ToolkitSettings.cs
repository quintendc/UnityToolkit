using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class ToolkitSettings
{
    [Header("Pause screen settings")]
    [Tooltip("Ässign Widgets to this list where you can call the PauseGame method, always include your PauseScreen")]
    public List<AWidget> PauseGameEnabledWidgets = new List<AWidget>();

    [Header("Loading screen settings")]
    [Tooltip("when true the loading screen will automatically shown when scene is loading")]
    public bool ShowLoadingScreenWhenLoadingScene = true;
    [Tooltip("when true the loading screen will automatically be removed when scene is loaded")]
    public bool HideLoadingScreenOnSceneLoaded = true;

}