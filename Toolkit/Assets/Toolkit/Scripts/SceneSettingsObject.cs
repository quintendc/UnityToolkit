using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MySceneSettingsObject", menuName = "Toolkit/SceneSettingsObject", order = 2)]
public class SceneSettingsObject : ScriptableObject
{
    [Tooltip("define which Widget should be shown when the scene is loaded.")]
    public Widgets WidgetType;

    // widgetRenderCamera -> get maincamera when no RenderCamera is provided

    // gameMode to instantiate


}
