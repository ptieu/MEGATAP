using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerOneMovement : MonoBehaviour {
    //camera
    [SerializeField] private CameraOneRotator cam;
    private int state = 1;

    
	//movement
	//[SerializeField] private float jumpForce;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpHeight;
	//[Range(0.0f, 1.0f)][SerializeField] private float inAirSpeed; // slow player's side-to-side movement in the air
	//[SerializeField] private float fallMultiplier;
	//[SerializeField] private float lowerJumpMultiplier;
    //[SerializeField] private float maxVelocity;
    //[SerializeField] private float maxJumpVelocity;
    [SerializeField] private GameManager gameManager;
    private float inputAxis;

    //private Vector3 tempVelocity;
    private bool grounded;
    private bool jumping;
    private float speed; //Change this when crouching, etc.; set it back to moveSpeed when done
	private Rigidbody rb;

	//private float movementMultiplier = 1f; // modify movement speed 



    private Vector3 movementVector;
	void Start() {
		rb = GetComponent<Rigidbody> ();
        speed = moveSpeed;
        //inputAxis = gameManager.GetInputAxis();
    }

	private void Update () {
        state = cam.GetState();

        //if (isGrounded) {
        //	movementMultiplier = 10;
        //} else if (!isGrounded) {
        //	movementMultiplier = inAirSpeed;
        //}
        //CheckCollision();
        inputAxis = gameManager.GetInputAxis();
        //switch (state)
        //{
        //    case 1:
        //        rb.AddForce(inputAxis * moveSpeed * movementMultiplier, 0, 0, ForceMode.Impulse);
        //        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
        //        break;
        //    case 2:
        //        rb.AddForce(0, 0, inputAxis * moveSpeed * movementMultiplier, ForceMode.Impulse);
        //        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX;
        //        break;
        //    case 3:
        //        rb.AddForce(-inputAxis * moveSpeed * movementMultiplier, 0, 0, ForceMode.Impulse);
        //        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
        //        break;
        //    case 4:
        //        rb.AddForce(0, 0, -inputAxis * moveSpeed * movementMultiplier, ForceMode.Impulse);
        //        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX;
        //        break;
        //}

        switch (state)
        {
            case 1:
                //rb.AddForce(inputAxis * moveSpeed, 0, 0);
                movementVector = new Vector3(inputAxis * speed, rb.velocity.y, 0);
                rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
                break;
            case 2:
                //rb.AddForce(0, 0, inputAxis * moveSpeed * movementMultiplier, ForceMode.Impulse);
                movementVector = new Vector3(0, rb.velocity.y, inputAxis * speed);
                rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX;
                break;
            case 3:
                //rb.AddForce(-inputAxis * moveSpeed * movementMultiplier, 0, 0, ForceMode.Impulse);
                movementVector = new Vector3(-inputAxis * speed, rb.velocity.y, 0);
                rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
                break;
            case 4:
                //rb.AddForce(0, 0, -inputAxis * moveSpeed * movementMultiplier, ForceMode.Impulse);
                movementVector = new Vector3(0, rb.velocity.y, -inputAxis * speed);
                rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX;
                break;
        }
        //Debug.Log(grounded + ", " + jumping);
        if(Input.GetButtonDown("Jump_Joy_1") && grounded)
        {
            //speed = moveSpeed / 2;
            jumping = true;
        }
        //Return to 0 (Handle drag manually)
        //if (Mathf.Abs(inputAxis) <= 0.5f && Mathf.Abs(inputAxis) >= 0f)
        //{
        //    rb.velocity = new Vector3(0, rb.velocity.y, 0);
        //}

        //Return to max velocity
        //if (rb.velocity.magnitude >= maxVelocity)
        //{
        //    rb.velocity = rb.velocity.normalized * maxVelocity;
        //}

        //jump
  //      if (Input.GetButton("Jump_Joy_1") && isGrounded) {
		//	isGrounded = false;
  //          //tempVelocity = rb.velocity;
  //          if (rb.velocity.magnitude >= maxJumpVelocity)
  //          {
  //              rb.velocity = rb.velocity.normalized * maxJumpVelocity;
  //          }
  //          rb.AddForce (0, jumpForce, 0, ForceMode.Impulse);
            
		//}

        //crouch
		if (Input.GetButton("Crouch_Joy_1") && grounded) {
			Debug.Log("Crouching");
		}	
		if (Input.GetButtonUp("Crouch_Joy_1") && grounded) {
			Debug.Log("Done Crouching");
		}


        //fall faster
        //if (rb.velocity.y < 0)
        //{
        //    rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        //}
        //else if (rb.velocity.y > 0 && !Input.GetButton("Jump_Joy_1"))
        //{
        //    rb.velocity += Vector3.up * Physics.gravity.y * (lowerJumpMultiplier - 1) * Time.deltaTime;
        //}
    }

    private void FixedUpdate()
    {
        if(jumping)
        {
            movementVector = new Vector3(movementVector.x, jumpHeight, movementVector.z);
            jumping = false;
        }
        rb.velocity = movementVector;
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
            grounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            grounded = false;
        }
    }
    //private void CheckCollision()
    //{
    //    if(Physics.Raycast(transform.position, Vector3.down, 1))
    //    {
    //        grounded = true;
    //        speed = moveSpeed;
    //    }
    //    else
    //    {
    //        grounded = false;
    //    }
    //}

    public int getState()
    {
        return state;
    }
}
