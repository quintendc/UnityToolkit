using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class AWidget : ToolkitBehaviour
{
    //public Selectable InitialSelectedElement = null;
    public WidgetTypes WidgetType;
    private Selectable selectedElement = null;
    public EventSystem EventSystem;

    public void Start()
    {
        if (EventSystem != null)
        {

            if (EventSystem.firstSelectedGameObject != null)
            {
                //InitialSelectedElement.Select();
                selectedElement = EventSystem.firstSelectedGameObject.GetComponent<Selectable>();
                EventSystem.firstSelectedGameObject.GetComponent<Selectable>().Select();

                //selectedElement.FindSelectableOnUp();
                //selectedElement.FindSelectableOnDown();
                //selectedElement.FindSelectableOnLeft();
                //selectedElement.FindSelectableOnRight();
            }
            else
            {
                Debug.LogWarning("No initial selected element provided, Some functions may not work");
            }
        
        }
        else
        {
            Debug.LogWarning("No EventSystem provided");
        }
    }


    #region Navigation

    public void NavigateUp()
    {
        Debug.Log("navigate up");

    }

    public void NavigateDown()
    {
        Debug.Log("navigate down");
    }

    public void NavigateLeft()
    {
        Debug.Log("navigate left");
    }

    public void NavigateRight()
    {
        Debug.Log("navigate right");
    }

    #endregion

    private void Update()
    {

    }

}
