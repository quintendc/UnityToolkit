using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class AWidget : ToolkitBehaviour
{
    public WidgetTypes WidgetType;
    public EventSystem EventSystem;

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        if (EventSystem != null)
        {

            if (EventSystem.firstSelectedGameObject == null)
            {
                Debug.LogWarning("No initial selected element provided ot the EventSystem, Some functions may not work.");
            }
        
        }
        else
        {
            Debug.LogWarning("No EventSystem provided");
        }
    }

    protected virtual void Update()
    {

    }

}
