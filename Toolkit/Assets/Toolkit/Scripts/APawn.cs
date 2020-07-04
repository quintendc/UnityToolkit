﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public abstract class APawn : ToolkitBehaviour
{
    #region Properties

    public int Id;

    public float Health = 100f;
    public float MoveSpeed = 1f;
    public float RotationSpeed = 1f;

    public new bool DontDestroyOnLoad = true;

    #endregion

    protected virtual void Awake()
    {
        if (DontDestroyOnLoad == true)
        {
            DontDestroyOnLoad(gameObject);
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
