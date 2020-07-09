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
    [Tooltip("when this widget is active the PauseGame method is disabled")]
    public bool DisableToPauseGame = true;

    protected virtual void Awake()
    {
        if (DisableToPauseGame == true)
        {
            GameState.PauseGameDisabled = true;
            Debug.Log("Pausing game is disabled");
        }
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

    private void OnDestroy()
    {
        GameState.PauseGameDisabled = false;
    }

}
