using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class APlayerController : ToolkitBehaviour
{
    [HideInInspector]
    public Player PlayerRef = null;
    public InputState InputState;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    public void Init(Player player)
    {
        PlayerRef = player;
    }

}
