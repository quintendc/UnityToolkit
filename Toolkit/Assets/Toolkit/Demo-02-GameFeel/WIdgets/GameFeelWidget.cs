﻿using System.Collections;
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
        MotionFX(0f, 0.15f);
    }

    public void SpeedGame()
    {
        MotionFX(2f, 0.15f);
    }
}
