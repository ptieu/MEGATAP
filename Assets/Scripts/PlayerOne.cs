using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOne : MonoBehaviour {

    [SerializeField] private CameraOneRotator cam;
    private int state = 1;

	//jump 
	private Vector3 jump;
	[SerializeField] private float jumpForce = 3.0f;
	private bool isGrounded;
	Rigidbody rb;

	void Start() {
		rb = GetComponent<Rigidbody> ();
		jump = new Vector3 (0.0f, 2.0f, 0.0f);
	}

	void OnCollisionStay() {
		isGrounded = true;
	}

	void Update () {
        state = cam.GetState();

        if (state == 1)
        {
            this.transform.Translate(Input.GetAxis("Horizontal") * 0.25f, 0,0);
        }
        else if(state == 2)
        {
            this.transform.Translate(0, 0, Input.GetAxis("Horizontal") * 0.25f);
        }
        else if(state == 3)
        {
            this.transform.Translate(-Input.GetAxis("Horizontal") * 0.25f, 0, 0);
        }
        else if (state == 4)
        {
            this.transform.Translate(0, 0, -Input.GetAxis("Horizontal") * 0.25f);
        }

		// implement simple jump
		// Currently character can double jump. Implementation is weird. 
		if (Input.GetKeyDown (KeyCode.UpArrow) && isGrounded) {
			isGrounded = false;
			rb.AddForce (jump * jumpForce, ForceMode.Impulse);

		}
    }
}
