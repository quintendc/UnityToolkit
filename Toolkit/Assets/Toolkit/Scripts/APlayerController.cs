using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class APlayerController : ToolkitBehaviour
{
    [HideInInspector]
    public Player PlayerRef = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(Player player)
    {
        PlayerRef = player;
    }

}
