using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SamplePlayerController : APlayerController
{
    #region Private properties
    
    Vector2 movement;

    #endregion


    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        Move();
    }


    private void Move()
    {
        Vector3 m = new Vector3(movement.x, 0, movement.y) * Pawn.MoveSpeed * Time.deltaTime;
        Pawn.transform.Translate(m);
    }

    private void OnMovement(InputValue value)
    {
        movement = value.Get<Vector2>();
    }

}
