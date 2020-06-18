using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamplePlayerController : APlayerController
{

    private Vector3 movement;
    private float angle;


    // Start is called before the first frame update
    void Start()
    {
        InputState = InputState.Widget;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {
            AWidget widget = GetCurrentWidget();
            widget.NavigateDown();
        }


        // input
        // gives float value between -1 and 1
        if (PlayerRef.Pawn != null)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.z = Input.GetAxisRaw("Vertical");
        }
    }

    // handle input
    private void FixedUpdate()
    {
        if (PlayerRef.Pawn != null)
        {
            // movement
            Rigidbody rb = PlayerRef.Pawn.gameObject.GetComponent<Rigidbody>();
            rb.MovePosition(rb.position + movement * PlayerRef.Pawn.MoveSpeed * Time.fixedDeltaTime);

            // rotation -> try to fix to keep rotation when no input is provided
            if (movement != new Vector3())
            {
                angle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;
                rb.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
            }
        }
    }
}
