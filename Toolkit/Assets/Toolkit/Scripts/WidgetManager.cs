using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WidgetManager : MonoBehaviour
{
    public static WidgetManager Instance = null;
    public List<GameObject> Widgets = new List<GameObject>();
    private GameObject currentWidget = null;



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


    public void ShowWidget(Widgets widgetType)
    {
        // destroy current widget
        if (currentWidget != null)
        {
            Destroy(currentWidget);
        }


        // instantiate new widget
        currentWidget = GameObject.Instantiate(Widgets.Find(w => w.GetComponent<AWidget>().WidgetType == widgetType));

    }
}
