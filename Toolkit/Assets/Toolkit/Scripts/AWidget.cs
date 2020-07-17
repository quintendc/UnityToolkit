using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    /// <summary>
    /// return to the previous widget in the WidgetStack
    /// </summary>
    public virtual void Back()
    {
        if (ToolkitData.WidgetStack.Count > 1)
        {
            // find lastindex of the WidgetStack => count -1
            int lastIndex = ToolkitData.WidgetStack.Count - 1;

            // get second last element => lastindex - 1
            WidgetTypes previousWidgetType = ToolkitData.WidgetStack.ElementAt(lastIndex - 1);

            // remove current widget from the stack current widget is always the last element=> element at lastindex
            ToolkitData.WidgetStack.RemoveAt(lastIndex);

            // instantiate previous widget without adding again to the stack
            ShowWidget(previousWidgetType, false);

        }
        else
        {
            Debug.LogError("Can't go to the Previous Widget, you are already at the first element in the stack");
        }

    }

    protected virtual void Reflection()
    {
        foreach (var b in Bindings)
        {
            b.Text.text = this.GetType().GetField(b.VariableName).GetValue(this).ToString();
        }
    }

}
