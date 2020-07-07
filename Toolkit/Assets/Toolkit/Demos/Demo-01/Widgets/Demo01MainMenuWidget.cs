using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Demo01MainMenuWidget : AWidget
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }


    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void LoadGameFeelScene()
    {
        SceneManager.LoadScene("GameFeelScene");
    }

    public void ShowSettings()
    {

    }
}
