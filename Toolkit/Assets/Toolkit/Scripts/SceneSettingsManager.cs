using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSettingsManager : MonoBehaviour
{
    [Tooltip("Check this when this is the SceneSettingsManager for the PersistentScene")]
    public bool PersistentSceneSceneSettingsManager = false;

    [Header("Widget parameters")]
    [Tooltip("The GameManager will not try to show widget when scene is loaded, Note! this will remove automatically the Loading screen widget")]
    public bool ShowWidgetOnSceneLoaded = true;
    [Tooltip("when true the current WidgetStack will be cleared, you probably want to clear the current Stack when you do a scene travel")]
    public bool NewWidgetStack = true;

    [Tooltip("define which Widget should be shown when the scene is loaded.")]
    public WidgetTypes WidgetType;

    // widgetRenderCamera -> get main camera when no RenderCamera is provided
    public Camera RenderCamera = null;

    [Header("Input Settings")]
    [Tooltip("set the InputState when this scene is loaded")]
    public InputStates InputState;


    [Header("GameMode settings")]
    [Tooltip("Game mode to instantiate")]
    public GameObject GameMode = null;
    [Tooltip("The GameManager will call the StartRound when scene is loaded.")]
    public bool StartRoundWhenSceneIsLoaded = false;

    private void Awake()
    {
        
    }

}
