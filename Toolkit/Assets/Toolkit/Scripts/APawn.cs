using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class APawn : ToolkitBehaviour
{
    #region Properties

    [HideInInspector]
    public Player PlayerRef = null;

    public float Health = 100f;
    public float MoveSpeed = 1f;
    public float RotationSpeed = 1f;

    #endregion


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

    protected virtual void Hit(Damager damager)
    {
        if (damager.InstantKill == true)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Health -= damager.Damage;
        }
    }


}
