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

    /// <summary>
    /// Load scene asynchronous
    /// </summary>
    /// <param name="sceneBuildIndex">index of scene</param>
    /// <param name="makeSceneActive">when true the new loaded scene will be set to active</param>
    public void LoadSceneAsync(int sceneBuildIndex, bool makeSceneActive = true)
    {
        StartCoroutine(LoadYourAsyncScene(sceneBuildIndex, makeSceneActive));
    }

    /// <summary>
    /// Load scene asynchronous
    /// </summary>
    /// <param name="sceneName">name of scene</param>
    /// <param name="makeSceneActive">when true the new loaded scene will be set to active</param>
    public void LoadSceneAsync(string sceneName, bool makeSceneActive = true)
    {
        StartCoroutine(LoadYourAsyncScene(sceneName, makeSceneActive));
    }

    /// <summary>
    /// Load scene asynchronous
    /// </summary>
    /// <param name="sceneBuildIndex">index of scene</param>
    /// <param name="mode">mode to load the scene, additive will add to current single will switch to new scene when completed</param>
    /// <param name="makeSceneActive">when true the new loaded scene will be set to active</param>
    public void LoadSceneAsync(int sceneBuildIndex, LoadSceneMode mode, bool makeSceneActive = true)
    {
        StartCoroutine(LoadYourAsyncScene(sceneBuildIndex, mode, makeSceneActive));
    }

    /// <summary>
    /// Load scene asynchronous
    /// </summary>
    /// <param name="sceneBuildIndex">index of scene</param>
    /// <param name="parameters">struct of LoadScene parameters</param>
    /// <param name="makeSceneActive">when true the new loaded scene will be set to active</param>
    public void LoadSceneAsync(int sceneBuildIndex, LoadSceneParameters parameters, bool makeSceneActive = true)
    {
        StartCoroutine(LoadYourAsyncScene(sceneBuildIndex, parameters, makeSceneActive));
    }

    /// <summary>
    /// Load scene asynchronous
    /// </summary>
    /// <param name="sceneName">name of scene</param>
    /// <param name="mode">mode to load the scene, additive will add to current single will switch to new scene when completed</param>
    /// <param name="makeSceneActive">when true the new loaded scene will be set to active</param>
    public void LoadSceneAsync(string sceneName, LoadSceneMode mode, bool makeSceneActive = true)
    {
        StartCoroutine(LoadYourAsyncScene(sceneName, mode, makeSceneActive));
    }

    /// <summary>
    /// Load scene asynchronous
    /// </summary>
    /// <param name="sceneName">name of scene</param>
    /// <param name="parameters">struct of LoadScene parameters</param>
    /// <param name="makeSceneActive">when true the new loaded scene will be set to active</param>
    public void LoadSceneAsync(string sceneName, LoadSceneParameters parameters, bool makeSceneActive = true)
    {
        StartCoroutine(LoadYourAsyncScene(sceneName, parameters, makeSceneActive));
    }

    #endregion

    #region LoadSceneAsync IEnumerators

    private IEnumerator LoadYourAsyncScene(string sceneName, bool makeSceneActive)
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

        if (makeSceneActive == true)
        {
            Scene scene = SceneManager.GetSceneByName(sceneName);
            SceneManager.SetActiveScene(scene);
        }
    }


    private IEnumerator LoadYourAsyncScene(int sceneBuildIndex, bool makeSceneActive)
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

        if (makeSceneActive == true)
        {
            Scene scene = SceneManager.GetSceneByBuildIndex(sceneBuildIndex);
            SceneManager.SetActiveScene(scene);
        }
    }


    private IEnumerator LoadYourAsyncScene(int sceneBuildIndex, LoadSceneMode mode, bool makeSceneActive)
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

        if (makeSceneActive == true)
        {
            Scene scene = SceneManager.GetSceneByBuildIndex(sceneBuildIndex);
            SceneManager.SetActiveScene(scene);

            if (mode == LoadSceneMode.Additive)
            {
                GameState.LastLoadedAdditiveScene = scene;
            }
        }
    }


    private IEnumerator LoadYourAsyncScene(int sceneBuildIndex, LoadSceneParameters parameters, bool makeSceneActive)
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

        if (makeSceneActive == true)
        {
            Scene scene = SceneManager.GetSceneByBuildIndex(sceneBuildIndex);
            SceneManager.SetActiveScene(scene);

            if (parameters.loadSceneMode == LoadSceneMode.Additive)
            {
                GameState.LastLoadedAdditiveScene = scene;
            }
        }
    }


    private IEnumerator LoadYourAsyncScene(string sceneName, LoadSceneMode mode, bool makeSceneActive)
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

        if (makeSceneActive == true)
        {
            Scene scene = SceneManager.GetSceneByName(sceneName);
            SceneManager.SetActiveScene(scene);

            if (mode == LoadSceneMode.Additive)
            {
                GameState.LastLoadedAdditiveScene = scene;
            }
        }

    }


    private IEnumerator LoadYourAsyncScene(string sceneName, LoadSceneParameters parameters, bool makeSceneActive)
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

        if (makeSceneActive == true)
        {
            Scene scene = SceneManager.GetSceneByName(sceneName);
            SceneManager.SetActiveScene(scene);

            if (parameters.loadSceneMode == LoadSceneMode.Additive)
            {
                GameState.LastLoadedAdditiveScene = scene;
            }
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
