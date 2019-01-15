using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOne : MonoBehaviour {
	// test comment

    //camera
    [SerializeField] private CameraOneRotator cam;
    private int state = 1;

	//movement
	[SerializeField] private float jumpForce;
    [SerializeField] private float moveSpeed;
	[Range(0.0f, 1.0f)][SerializeField] private float inAirSpeed; // slow player's side-to-side movement in the air
	[SerializeField] private float fallMultiplier;
	[SerializeField] private float lowerJumpMultiplier;
    [SerializeField] private float maxVelocity;
    [SerializeField] private float maxJumpVelocity;

    private Vector3 tempVelocity;
    private bool isGrounded;
	private Rigidbody rb;
    private float inputAxis;
    private string[] joysticks;

	private float movementMultiplier = 1f; // modify movement speed 

	void Start() {
		rb = GetComponent<Rigidbody> ();
        joysticks = Input.GetJoystickNames();
	}

	void OnCollisionEnter() {
		isGrounded = true;
        rb.velocity = tempVelocity;
	}

	void FixedUpdate () {
        state = cam.GetState();

		if (isGrounded) {
			movementMultiplier = 10;
		} else if (!isGrounded) {
			movementMultiplier = inAirSpeed;
		}
        
        //TODO: Later, get rid of this and allow them to choose keyboard or controller
        //Right now, if one controller is connected, it defaults to player 1.
        if(Input.GetJoystickNames().Length > 0)
        {
            for(int i = 0; i < joysticks.Length; i++)
            {
                if(!string.IsNullOrEmpty(joysticks[i]))
                {
                    Debug.Log("Controller " + i + " is connected using: " + joysticks[i]);
                    if(i == 0)
                    {
                        inputAxis = Input.GetAxis("Horizontal_Joy_1");
                    }
                }
                else
                {
                    Debug.Log("Controller " + i + " is disconnected.");
                    if(i == 0)
                    {
                        inputAxis = Input.GetAxis("Horizontal_Keyboard");
                    }
                }
            }
        }
        else
        {
            inputAxis = Input.GetAxis("Horizontal_Keyboard");
        }

        switch (state)
        {
            case 1:
                rb.AddForce(inputAxis * moveSpeed * movementMultiplier, 0, 0, ForceMode.Impulse);
                break;
            case 2:
                rb.AddForce(0, 0, inputAxis * moveSpeed * movementMultiplier, ForceMode.Impulse);
                break;
            case 3:
                rb.AddForce(-inputAxis * moveSpeed * movementMultiplier, 0, 0, ForceMode.Impulse);
                break;
            case 4:
                rb.AddForce(0, 0, -inputAxis * moveSpeed * movementMultiplier, ForceMode.Impulse);
                break;
        }

        //Return to 0 (Handle drag manually)
        if(Mathf.Abs(inputAxis) <= 0.5f && Mathf.Abs(inputAxis) >= 0f)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }

        //Return to max velocity
        if (rb.velocity.magnitude >= maxVelocity)
        {
            rb.velocity = rb.velocity.normalized * maxVelocity;
        }

        //jump
        if (Input.GetButton("Jump_Joy_1") && isGrounded) {
			isGrounded = false;
            tempVelocity = rb.velocity;
            if (rb.velocity.magnitude >= maxJumpVelocity)
            {
                rb.velocity = rb.velocity.normalized * maxJumpVelocity;
            }
            rb.AddForce (0, jumpForce, 0, ForceMode.Impulse);
            
		}

        //crouch
		if (Input.GetButton("Crouch_Joy_1") && isGrounded) {
			Debug.Log("Crouching");
		}	
		if (Input.GetButtonUp("Crouch_Joy_1") && isGrounded) {
			Debug.Log("Done Crouching");
		}


        //fall faster
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump_Joy_1"))
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowerJumpMultiplier - 1) * Time.deltaTime;
        }
    }
}
