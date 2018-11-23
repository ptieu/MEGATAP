using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOne : MonoBehaviour {
	// test comment

    //camera
    [SerializeField] private CameraOneRotator cam;
    private int state = 1;

	//movement
	[SerializeField] private float jumpForce = 45.0f;
    [SerializeField] private float moveSpeed = 0.2f;
	[Range(0.0f, 1.0f)][SerializeField] private float inAirSpeed = 0.8f; // slow player's side-to-side movement in the air
	[SerializeField] private float fallMultiplier = 2.5f;
	[SerializeField] private float lowerJumpMultiplier = 2f;
    [SerializeField] private float maxVelocity = 15;
    [SerializeField] private float maxJumpVelocity = 12;


    private Vector3 jump;
    private float tempSpeed;
    private Vector3 tempVelocity;
    private bool isGrounded;
	private Rigidbody rb;

	private float movementMultiplier = 1f; // modify movement speed 

	void Start() {
		rb = GetComponent<Rigidbody> ();
		jump = new Vector3 (0.0f, 2.0f, 0.0f);
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
                rb.AddForce(Input.GetAxis("Horizontal") * moveSpeed * movementMultiplier, 0, 0, ForceMode.Impulse);
                break;
            case 2:
                rb.AddForce(0, 0, Input.GetAxis("Horizontal") * moveSpeed * movementMultiplier, ForceMode.Impulse);
                break;
            case 3:
                rb.AddForce(-Input.GetAxis("Horizontal") * moveSpeed * movementMultiplier, 0, 0, ForceMode.Impulse);
                break;
            case 4:
                rb.AddForce(0, 0, -Input.GetAxis("Horizontal") * moveSpeed * movementMultiplier, ForceMode.Impulse);
                break;
        }

        if (rb.velocity.magnitude >= maxVelocity)
        {
            rb.velocity = rb.velocity.normalized * maxVelocity;
        }

        if(Input.GetAxis("Horizontal") == 0)
        {
            float yVelocity = rb.velocity.y;
            rb.velocity = new Vector3(0, yVelocity, 0);
        }
        //jump
        if (Input.GetKeyDown (KeyCode.Space) && isGrounded) {
			isGrounded = false;
            tempVelocity = rb.velocity;
            if (rb.velocity.magnitude >= maxJumpVelocity)
            {
                rb.velocity = rb.velocity.normalized * maxJumpVelocity;
            }
            rb.AddForce (jump * jumpForce, ForceMode.Impulse);
            
		}	
		
		//crouch
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
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowerJumpMultiplier - 1) * Time.deltaTime;
        }
    }
}
