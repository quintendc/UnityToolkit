using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MySceneSettingsObject", menuName = "Toolkit/SceneSettingsObject", order = 2)]
public class SceneSettingsObject : ScriptableObject
{
    [Header("Widget parameters")]
    [Tooltip("define which Widget should be shown when the scene is loaded.")]
    public WidgetTypes WidgetType;

    // widgetRenderCamera -> get maincamera when no RenderCamera is provided
    public Camera Camera = null;

    [Header("GameMode parameters")]
    // gameMode to instantiate
    public GameObject GameMode = null;

    [Tooltip("set the InputState when this scene is loaded")]
    public InputState InputState;

    [Header("Player Settings")]
    [Header("has no impact on the Initial scene")]

    [Tooltip("when set to true the existing pawns will be replaced by the defaultPawn of the currentGameMode")]
    public bool OverridePlayerPawns = true;
    [Tooltip("when set to true the existing playercontrollers will be replaced by the defaultPlayerController of the currentGameMode")]
    public bool OverridePlayerPlayerControllers = true;


    [Header("GameMode")]
    [Tooltip("The GameManager will call the StartRound when scene is loaded.")]
    public bool StartRoundWhenSceneIsLoaded = false;

}
