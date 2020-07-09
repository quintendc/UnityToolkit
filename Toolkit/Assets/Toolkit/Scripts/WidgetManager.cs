using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WidgetManager : MonoBehaviour
{
    public static WidgetManager Instance = null;
    public List<GameObject> Widgets = new List<GameObject>();
    private GameObject currentWidget = null;

    public Camera WidgetRenderCamera = null;



    private void Awake()
    {
        #region Singleton Pattern WidgetManager instance

        // check if instance already exists
        if (Instance == null)
        {
            // if not, set instance to this
            Debug.Log("WidgetManager Instance created");
            Instance = this;
        }
        // if instance already exists and it's not this:
        else if (Instance != null)
        {
            // then destroy this. this enforces our singleton pattern, meaning there can only ever be one instance of the WidgetManager
            Debug.Log("WidgetManager Instance already exists this.destroyed");
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this);

        #endregion
    }


    public void ShowWidget(WidgetTypes widgetType)
    {
        // destroy current widget
        if (currentWidget != null)
        {

            // destroy widget when new WidgetType is not equel to currentWidget
            if (widgetType != currentWidget.GetComponent<AWidget>().WidgetType)
            {
                Destroy(currentWidget);
            }
        }

        if (widgetType == WidgetTypes.Default)
        {
            Debug.LogWarning("Widget type is Default.");
        }
        else
        {
            // instantiate new widget
            try
            {
                currentWidget = GameObject.Instantiate(Widgets.Find(w => w.GetComponent<AWidget>().WidgetType == widgetType));
            }
            catch (System.Exception)
            {
                Debug.LogError("Can't instantiate Widget: " + widgetType + ", make sure it is provided to the WidgetManager!");
            }
        }

    }

    /// <summary>
    /// Set render Camera for Widget
    /// </summary>
    /// <param name="camera">if Camera is null the MainCamera will be picked</param>
    public void SetRenderCamera(Camera camera = null)
    {
        if (camera == null)
        {
            // pick main camera
            WidgetRenderCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }
        else
        {
            // set RenderCamera to camera
            WidgetRenderCamera = camera;
        }

        // set camera
        currentWidget.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
        currentWidget.GetComponent<Canvas>().worldCamera = WidgetRenderCamera;

    }


    /// <summary>
    /// Get the current displayed widget
    /// </summary>
    /// <returns>current widget</returns>
    public AWidget GetCurrentWidget()
    {
        return currentWidget.GetComponent<AWidget>();
    }


    /// <summary>
    /// Hide current Widget
    /// </summary>
    /// <returns>widget that is removed</returns>
    public AWidget HideCurrentWidget()
    {
        Destroy(currentWidget);

        return currentWidget.GetComponent<AWidget>();
    }

}
