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

}
