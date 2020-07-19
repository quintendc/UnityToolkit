using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleMainMenuWidget : AWidget
{
    protected override void Awake()
    {
        base.Awake();
    }

    public void ShowSettings()
    {
        WidgetManager.Instance.ShowWidget(WidgetTypes.Settings);
    }
}
