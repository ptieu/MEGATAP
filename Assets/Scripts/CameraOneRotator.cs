using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOneRotator : MonoBehaviour {

    [SerializeField] private Camera playerOneCam;
    [SerializeField] private float moveSpeed = 30;
    //Change these if the tower size changes
    private static int camPosHorizontal = 22;
    private static int camPosVertical = 5;
    private static int camRotationX = 10;
    private static int camRotationY = 0;


    //Create positions and rotations from given defaults
    private Vector3 pos1 = new Vector3(0, camPosVertical, -camPosHorizontal);
    private Quaternion rot1 = Quaternion.Euler(camRotationX, camRotationY, 0);

    private Vector3 pos2 = new Vector3(camPosHorizontal, camPosVertical, 0);
    private Quaternion rot2 = Quaternion.Euler(camRotationX, camRotationY - 90, 0);

    private Vector3 pos3 = new Vector3(0, camPosVertical, camPosHorizontal);
    private Quaternion rot3 = Quaternion.Euler(camRotationX, camRotationY + 180, 0);

    private Vector3 pos4 = new Vector3(-camPosHorizontal, camPosVertical, 0);
    private Quaternion rot4 = Quaternion.Euler(camRotationX, camRotationY + 90, 0);

    private float distance, covered, remaining;
    private float startTime;
    private Vector3 prevPosition, goalPosition;
    private Quaternion prevRotation, goalRotation;
    private bool moving = false;


    private void Start()
    {
        playerOneCam.transform.position = pos1;
        playerOneCam.transform.rotation = rot1;

        distance = Vector3.Distance(pos1, pos2);

        moving = false;
    }

    private void Update()
    {
        //Camera rotation
        if(moving)
        {
            covered = (Time.time - startTime) * moveSpeed;
            remaining = covered / distance;

            playerOneCam.transform.position = Vector3.Lerp(prevPosition, goalPosition, remaining);
            playerOneCam.transform.rotation = Quaternion.Slerp(prevRotation, goalRotation, remaining);

            if(Time.time - startTime >= 2)
            {
                moving = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(this.tag == "Trigger1")
            { 
                distance = Vector3.Distance(pos1, pos2);
                startTime = Time.time;
                moving = true;
                prevPosition = pos1;
                goalPosition = pos2;
                prevRotation = rot1;
                goalRotation = rot2;
            }
            if (this.tag == "Trigger2")
            {
                distance = Vector3.Distance(pos2, pos3);
                startTime = Time.time;
                moving = true;
                prevPosition = pos2;
                goalPosition = pos3;
                prevRotation = rot2;
                goalRotation = rot3;
            }
            if (this.tag == "Trigger3")
            {
                Debug.Log("Trigger3");
                distance = Vector3.Distance(pos3, pos4);
                startTime = Time.time;
                moving = true;
                prevPosition = pos3;
                goalPosition = pos4;
                prevRotation = rot3;
                goalRotation = rot4;
            }
            if (this.tag == "Trigger4")
            {
                distance = Vector3.Distance(pos4, pos1);
                startTime = Time.time;
                moving = true;
                prevPosition = pos4;
                goalPosition = pos1;
                prevRotation = rot4;
                goalRotation = rot1;
            }
        }
    }
}
