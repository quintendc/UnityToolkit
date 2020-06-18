using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class AWidget : ToolkitBehaviour
{
    public Selectable InitialSelectedElement = null;
    public WidgetTypes WidgetType;
    private Selectable selectedElement = null;

    public void Start()
    {
        if (InitialSelectedElement != null)
        {
            //InitialSelectedElement.Select();
            selectedElement = InitialSelectedElement;
        }
        else
        {
            Debug.LogWarning("No initial selected element provided, Some functions may not work");
        }
    }


    #region Navigation

    public void NavigateUp()
    {
        Selectable x = InitialSelectedElement.navigation.selectOnUp;
        selectedElement = x;
    }

    public void NavigateDown()
    {
        Selectable x = InitialSelectedElement.navigation.selectOnDown;
        selectedElement = x;
    }

    public void NavigateLeft()
    {
        Selectable x = InitialSelectedElement.navigation.selectOnLeft;
        selectedElement = x;
    }

    public void NavigateRight()
    {
        Selectable x = InitialSelectedElement.navigation.selectOnRight;
        selectedElement = x;
    }

    #endregion


    private void FixedUpdate()
    {
        if (selectedElement != null)
        {
            selectedElement.Select();
        }
        else
        {
            Debug.Log("SelectedElement is null");
        }
    }

}
