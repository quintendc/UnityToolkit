using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFeelWidget : AWidget
{

    public void CameraShake()
    {
        ToolkitCameraShake();
    }

    public void FreezeGame()
    {
        GameFreeze(10f);
    }
}
