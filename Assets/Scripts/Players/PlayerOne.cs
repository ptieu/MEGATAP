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

	private float movementMultiplier = 1f; // modify movement speed 

	void Start() {
		rb = GetComponent<Rigidbody> ();
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

        switch (state)
        {
            case 1:
                rb.AddForce(Input.GetAxis("Horizontal_Joy_1") * moveSpeed * movementMultiplier, 0, 0, ForceMode.Impulse);
                break;
            case 2:
                rb.AddForce(0, 0, Input.GetAxis("Horizontal_Joy_1") * moveSpeed * movementMultiplier, ForceMode.Impulse);
                break;
            case 3:
                rb.AddForce(-Input.GetAxis("Horizontal_Joy_1") * moveSpeed * movementMultiplier, 0, 0, ForceMode.Impulse);
                break;
            case 4:
                rb.AddForce(0, 0, -Input.GetAxis("Horizontal_Joy_1") * moveSpeed * movementMultiplier, ForceMode.Impulse);
                break;
        }

        //Return to 0 (Handle drag manually)
        if(Mathf.Abs(Input.GetAxis("Horizontal_Joy_1")) <= 0.5f && Mathf.Abs(Input.GetAxis("Horizontal_Joy_1")) >= 0f)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }

        //Return to max velocity
        if (rb.velocity.magnitude >= maxVelocity)
        {
            rb.velocity = rb.velocity.normalized * maxVelocity;
        }

        //jump
        if (Input.GetKeyDown (KeyCode.Space) && isGrounded) {
			isGrounded = false;
            tempVelocity = rb.velocity;
            if (rb.velocity.magnitude >= maxJumpVelocity)
            {
                rb.velocity = rb.velocity.normalized * maxJumpVelocity;
            }
            rb.AddForce (0, jumpForce, 0, ForceMode.Impulse);
            
		}

        //crouch
        float tempSpeed = moveSpeed;
		if (Input.GetKeyDown (KeyCode.S) && isGrounded) {
			Debug.Log("S is pressed");
            tempSpeed = moveSpeed;
			moveSpeed = 0;
		}	
		if (Input.GetKeyUp(KeyCode.S) && isGrounded) {
			Debug.Log("S is released");
            moveSpeed = tempSpeed;
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
