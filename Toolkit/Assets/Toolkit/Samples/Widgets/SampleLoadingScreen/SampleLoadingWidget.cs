using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleLoadingWidget : AWidget
{

    public float LoadingProgress;


    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();

    }

    protected override void Update()
    {
        // get the loading progress from the GameState
        LoadingProgress = GameState.LoadingProgress * 100;

        base.Update();
    }
}
