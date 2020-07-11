using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToolkitSceneLoader: ToolkitBehaviour
{
    #region LoadScene methods

    public void LoadScene(int sceneBuildIndex)
    {
        SceneManager.LoadScene(sceneBuildIndex);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadScene(int sceneBuildIndex, LoadSceneMode mode)
    {
        SceneManager.LoadScene(sceneBuildIndex, mode);
    }

    public void LoadScene(int sceneBuildIndex, LoadSceneParameters parameters)
    {
        SceneManager.LoadScene(sceneBuildIndex, parameters);
    }

    public void LoadScene(string sceneName, LoadSceneMode mode)
    {
        SceneManager.LoadScene(sceneName, mode);
    }

    public void LoadScene(string sceneName, LoadSceneParameters parameters)
    {
        SceneManager.LoadScene(sceneName, parameters);
    }

    #endregion

    #region LoadScene Async methods

    public void LoadSceneAsync(int sceneBuildIndex)
    {
        StartCoroutine(LoadYourAsyncScene(sceneBuildIndex));
    }

    public void LoadSceneAsync(string sceneName)
    {
        StartCoroutine(LoadYourAsyncScene(sceneName));
    }

    public void LoadSceneAsync(int sceneBuildIndex, LoadSceneMode mode)
    {
        StartCoroutine(LoadYourAsyncScene(sceneBuildIndex, mode));
    }

    public void LoadSceneAsync(int sceneBuildIndex, LoadSceneParameters parameters)
    {
        StartCoroutine(LoadYourAsyncScene(sceneBuildIndex, parameters));
    }

    public void LoadSceneAsync(string sceneName, LoadSceneMode mode)
    {
        StartCoroutine(LoadYourAsyncScene(sceneName, mode));
    }

    public void LoadSceneAsync(string sceneName, LoadSceneParameters parameters)
    {
        StartCoroutine(LoadYourAsyncScene(sceneName, parameters));
    }

    #endregion

    #region LoadSceneAsync IEnumerators

    private IEnumerator LoadYourAsyncScene(string sceneName)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        ShowLoadingScreen();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }


    private IEnumerator LoadYourAsyncScene(int sceneBuildIndex)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        ShowLoadingScreen();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneBuildIndex);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }


    private IEnumerator LoadYourAsyncScene(int sceneBuildIndex, LoadSceneMode mode)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        ShowLoadingScreen();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneBuildIndex, mode);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }


    private IEnumerator LoadYourAsyncScene(int sceneBuildIndex, LoadSceneParameters parameters)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneBuildIndex, parameters);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }


    private IEnumerator LoadYourAsyncScene(string sceneName, LoadSceneMode mode)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        ShowLoadingScreen();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, mode);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }


    private IEnumerator LoadYourAsyncScene(string sceneName, LoadSceneParameters parameters)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        ShowLoadingScreen();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, parameters);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    #endregion

    #region Loading screen

    private void ShowLoadingScreen()
    {
        if (GameManager.Instance.ToolkitSettings.ShowLoadingScreenWhenLoadingScene == true)
        {
            ShowWidget(WidgetTypes.Loading);
        }
    }

    #endregion
}
