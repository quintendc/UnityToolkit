using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class AWidget : ToolkitBehaviour
{
    public WidgetTypes WidgetType;
    public EventSystem EventSystem;

    [Tooltip("List of bindings")]
    public List<WidgetTextBinder> Bindings = new List<WidgetTextBinder>();

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
        Reflection();
    }


    protected virtual void Reflection()
    {
        foreach (var b in Bindings)
        {
            b.Text.text = this.GetType().GetField(b.VariableName).GetValue(this).ToString();
        }
    }

}
