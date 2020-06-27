using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFeelWidget : AWidget
{

    public CameraShake cameraShake;

    public void CameraShake()
    {
        //StartCoroutine(cameraShake.Shake());

        ToolkitCameraShake();
    }
}
