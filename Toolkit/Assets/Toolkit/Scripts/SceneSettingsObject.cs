using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MySceneSettingsObject", menuName = "Toolkit/SceneSettingsObject", order = 1)]
public class SceneSettingsObject : ScriptableObject
{
    [Header("Widget parameters")]
    [Tooltip("The GameManager will not try to show widget when scene is loaded")]
    public bool ShowWidgetOnSceneLoaded = true;

    [Tooltip("define which Widget should be shown when the scene is loaded.")]
    public WidgetTypes WidgetType;

    // widgetRenderCamera -> get maincamera when no RenderCamera is provided
    public Camera RenderCamera = null;

    [Header("Input handling")]
    [Tooltip("set the InputState when this scene is loaded")]
    public InputState InputState;


    [Header("GameMode settings")]
    [Tooltip("Gamemode to instantiate")]
    public GameObject GameMode = null;
    [Tooltip("The GameManager will call the StartRound when scene is loaded.")]
    public bool StartRoundWhenSceneIsLoaded = false;

}
