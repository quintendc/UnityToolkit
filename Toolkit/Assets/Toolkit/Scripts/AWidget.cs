using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class AWidget : ToolkitBehaviour
{
    public Selectable InitialSelectedElement = null;
    public WidgetTypes WidgetType;

    public void Start()
    {
        if (InitialSelectedElement != null)
        {
            InitialSelectedElement.Select();
        }
        else
        {
            Debug.LogWarning("No initial selected element provided some functions may not work");
        }
    }
}
