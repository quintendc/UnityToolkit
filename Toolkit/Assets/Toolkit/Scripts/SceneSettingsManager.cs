using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSettingsManager : MonoBehaviour
{
    [Tooltip("Check this when this is the SceneSettingsManager for the PersistentScene")]
    public bool PersistentSceneSceneSettingsManager = false;

    [Header("Widget parameters")]
    [Tooltip("The GameManager will not try to show widget when scene is loaded")]
    public bool ShowWidgetOnSceneLoaded = true;

    [Tooltip("define which Widget should be shown when the scene is loaded.")]
    public WidgetTypes WidgetType;

    // widgetRenderCamera -> get maincamera when no RenderCamera is provided
    public Camera RenderCamera = null;

    [Header("Input Settings")]
    [Tooltip("set the InputState when this scene is loaded")]
    public InputState InputState;


    [Header("GameMode settings")]
    [Tooltip("Gamemode to instantiate")]
    public GameObject GameMode = null;
    [Tooltip("The GameManager will call the StartRound when scene is loaded.")]
    public bool StartRoundWhenSceneIsLoaded = false;

    private void Awake()
    {
        // validate SceneSettings

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
