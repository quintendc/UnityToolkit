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
            Debug.LogWarning("No initial selected element provided, Some functions may not work");
        }
    }


    #region Navigation

    public void NavigateUp()
    {
        Selectable currentSelectable = InitialSelectedElement;
        Selectable newSelectable = InitialSelectedElement.navigation.selectOnUp;
        InitialSelectedElement = newSelectable;
        InitialSelectedElement.Select();

    }

    public void NavigateDown()
    {
        Selectable currentSelectable = InitialSelectedElement;
        Selectable newSelectable = InitialSelectedElement.navigation.selectOnDown;
        InitialSelectedElement = newSelectable;
        InitialSelectedElement.Select();

    }

    public void NavigateLeft()
    {
        Selectable currentSelectable = InitialSelectedElement;
        Selectable newSelectable = InitialSelectedElement.navigation.selectOnLeft;
        InitialSelectedElement = newSelectable;
        InitialSelectedElement.Select();

    }

    public void NavigateRight()
    {
        Selectable currentSelectable = InitialSelectedElement;
        Selectable newSelectable = InitialSelectedElement.navigation.selectOnRight;
        InitialSelectedElement = newSelectable;
        InitialSelectedElement.Select();

    }

    #endregion

}
