using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo01GameFeelWidget : AWidget
{

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

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
