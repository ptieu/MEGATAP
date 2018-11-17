using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOne : MonoBehaviour {
	// test comment

    //camera
    [SerializeField] private CameraOneRotator cam;
    private int state = 1;

	//jump 
	[SerializeField] private float jumpForce = 3.0f;
    [SerializeField] private float moveSpeed = 0.25f;


    private Vector3 jump;
    private bool isGrounded;
	private Rigidbody rb;

	void Start() {
		rb = GetComponent<Rigidbody> ();
		jump = new Vector3 (0.0f, 2.0f, 0.0f);
	}

	void OnCollisionEnter() {
		isGrounded = true;
	}

	void Update () {
        state = cam.GetState();

        switch (state)
        {
            case 1:
                this.transform.Translate(Input.GetAxis("Horizontal") * moveSpeed, 0, 0);
                break;
            case 2:
                this.transform.Translate(0, 0, Input.GetAxis("Horizontal") * moveSpeed);
                break;
            case 3:
                this.transform.Translate(-Input.GetAxis("Horizontal") * moveSpeed, 0, 0);
                break;
            case 4:
                this.transform.Translate(0, 0, -Input.GetAxis("Horizontal") * moveSpeed);
                break;
        }

        //jump
		if (Input.GetKeyDown (KeyCode.Space) && isGrounded) {
			isGrounded = false;
			rb.AddForce (jump * jumpForce, ForceMode.Impulse);

		}
    }
}
