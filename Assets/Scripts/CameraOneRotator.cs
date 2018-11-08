using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOneRotator : MonoBehaviour {

    [SerializeField] private Camera playerOneCam;
    [SerializeField] private float moveSpeed = 30;

    //Change these if the tower size changes
    private static int camPosHorizontal = 22;
    private static int camPosVertical   = 5;
    private static int camRotationX     = 10;
    private static int camRotationY     = 0;

    private Vector3[] positions = new[] {  new Vector3(0, camPosVertical, -camPosHorizontal),
                                           new Vector3(camPosHorizontal, camPosVertical, 0),
                                           new Vector3(0, camPosVertical, camPosHorizontal),
                                           new Vector3(-camPosHorizontal, camPosVertical, 0)};

    private Quaternion[] rotations = new[] { Quaternion.Euler(camRotationX, camRotationY, 0),
                                             Quaternion.Euler(camRotationX, camRotationY - 90, 0),
                                             Quaternion.Euler(camRotationX, camRotationY - 180, 0),
                                             Quaternion.Euler(camRotationX, camRotationY - 270, 0)};

    //Create positions and rotations from given defaults
    //private Vector3 pos1 = new Vector3(0, camPosVertical, -camPosHorizontal);
    //private Quaternion rot1 = Quaternion.Euler(camRotationX, camRotationY, 0);

    //private Vector3 pos2 = new Vector3(camPosHorizontal, camPosVertical, 0);
    //private Quaternion rot2 = Quaternion.Euler(camRotationX, camRotationY - 90, 0);

    //private Vector3 pos3 = new Vector3(0, camPosVertical, camPosHorizontal);
    //private Quaternion rot3 = Quaternion.Euler(camRotationX, camRotationY + 180, 0);

    //private Vector3 pos4 = new Vector3(-camPosHorizontal, camPosVertical, 0);
    //private Quaternion rot4 = Quaternion.Euler(camRotationX, camRotationY + 90, 0);

    private float distance, covered, remaining;
    private float startTime;
    private Vector3 prevPosition, goalPosition;
    private Quaternion prevRotation, goalRotation;
    private bool moving = false;

    private int cameraState = 1;

    private void Start()
    {
        playerOneCam.transform.position = positions[0];
        playerOneCam.transform.rotation = rotations[0];
        cameraState = 1;

        distance = Vector3.Distance(positions[0], positions[1]);

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
        if(other.tag == "Trigger1")
        { 
            distance = Vector3.Distance(positions[0], positions[1]);
            startTime = Time.time;
            moving = true;
            prevPosition = positions[0];
            goalPosition = positions[1];
            prevRotation = rotations[0];
            goalRotation = rotations[1];
            cameraState = 2;
        }
        if (other.tag == "Trigger2")
        {
            distance = Vector3.Distance(positions[1], positions[2]);
            startTime = Time.time;
            moving = true;
            prevPosition = positions[1];
            goalPosition = positions[2];
            prevRotation = rotations[1];
            goalRotation = rotations[2];
            cameraState = 3;
        }
        if (other.tag == "Trigger3")
        { 
            distance = Vector3.Distance(positions[2], positions[3]);
            startTime = Time.time;
            moving = true;
            prevPosition = positions[2];
            goalPosition = positions[3];
            prevRotation = rotations[2];
            goalRotation = rotations[3];
            cameraState = 4;
        }
        if (other.tag == "Trigger4")
        {
            distance = Vector3.Distance(positions[3], positions[0]);
            startTime = Time.time;
            moving = true;
            prevPosition = positions[3];
            goalPosition = positions[0];
            prevRotation = rotations[3];
            goalRotation = rotations[0];
            cameraState = 1;
        }
    }

    public int getState()
    {
        return cameraState;
    }
}
