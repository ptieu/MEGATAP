using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerOneMovement : MonoBehaviour {
    //camera
    [SerializeField] private CameraOneRotator cam;
    private int state = 1;

    
	//movement
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TrapBase trapBase;
    private float inputAxis;

    private bool crouching;
    private bool grounded;
    private bool jumping;
    private float speed; //Change this when crouching, etc.; set it back to moveSpeed when done

    //wall jump stuff
    private int jumpTimer;
	private Rigidbody rb;
    



    private Vector3 movementVector;
	void Start() {
		rb = GetComponent<Rigidbody> ();
        speed = moveSpeed;
    }

	private void Update () {
        state = cam.GetState();

        inputAxis = gameManager.GetInputAxis();
        switch (state)
        {
            case 1:
                movementVector = new Vector3(inputAxis * speed, rb.velocity.y, 0);
                rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
                break;
            case 2:
                movementVector = new Vector3(0, rb.velocity.y, inputAxis * speed);
                rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX;
                break;
            case 3:
                movementVector = new Vector3(-inputAxis * speed, rb.velocity.y, 0);
                rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
                break;
            case 4:
                movementVector = new Vector3(0, rb.velocity.y, -inputAxis * speed);
                rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX;
                break;
        }

        //jumping
        if(Input.GetButtonDown("Jump_Joy_1") && grounded)
        {
            jumping = true;
        }
      
        //crouch
		if (Input.GetButton("Crouch_Joy_1") && grounded) {
            crouching = true;
		}	
		if (Input.GetButtonUp("Crouch_Joy_1") && grounded) {
            crouching = false;
		}
    }

    private void FixedUpdate()
    {
        if(jumping)
        {
            movementVector = new Vector3(movementVector.x, jumpHeight, movementVector.z);
            jumping = false;
        }
        else if(crouching)
        {
            //TODO
        }
        rb.velocity = movementVector;
    }
    

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
            grounded = true;
        }
        else if (Physics.Raycast(transform.position, -transform.right, 1))
        {
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            grounded = false;
        }
    }

    public int GetState()
    {
        return state;
    }
}
