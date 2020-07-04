using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SamplePlayerController : APlayerController
{
    #region Private properties
    
    Vector3 movement;

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
        if (movement != Vector3.zero)
        {
            Move();
        }
    }


    private void Move()
    {
        // movement
        Rigidbody.MovePosition(Rigidbody.position + movement * Pawn.MoveSpeed * Time.fixedDeltaTime);

        // rotation
        float angle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;
        Rigidbody.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
    }

    private void OnMovement(InputValue value)
    {
        Vector2 m = value.Get<Vector2>();
        movement = new Vector3(m.x, 0, m.y);
    }

}
