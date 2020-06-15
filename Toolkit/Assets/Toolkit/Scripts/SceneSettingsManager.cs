using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSettingsManager : MonoBehaviour
{

    public SceneSettingsObject SceneSettings = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void ValidateSceneSettingsObject()
    {
        if (SceneSettings == null)
        {
            Debug.LogWarning("No SceneSettingsObject provided to the SceneSettingsManager! Some functions may not work.");
        }
        else
        {
            if (SceneSettings.WidgetType == WidgetTypes.Default)
            {
                Debug.LogWarning("WidgetType is set to Default!");
            }

            if (SceneSettings.Camera == null)
            {
                Debug.LogWarning("Camera is not Provided the GameManager will take the MainCamera!");
            }

            if (SceneSettings.GameMode == null)
            {
                Debug.LogWarning("No GameMode provided to the SceneSettingsObject, There will be no GameMode instantiated");
            }
        }
    }

}
