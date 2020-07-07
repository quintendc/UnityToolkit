using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo01SettingsWidget : AWidget
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }


    public void ShowMainMenu()
    {
        ShowWidget(WidgetTypes.MainMenu);
    }

}
