using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOne : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }
}
