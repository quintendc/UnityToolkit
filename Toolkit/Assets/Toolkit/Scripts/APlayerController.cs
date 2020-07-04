using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public abstract class APlayerController : ToolkitBehaviour
{

    public APawn Pawn = null;
    public Rigidbody Rigidbody = null;

    [HideInInspector]
    //public Player PlayerRef = null;
    public InputState InputState;


    protected virtual void Awake()
    {
        // validate Pawn
        if (Pawn == null)
        {
            Debug.LogWarning("No Pawn script provided to PlayerController, some functions may not work!");
        }
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

}
