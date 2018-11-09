using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTwoRotator : MonoBehaviour {

    [SerializeField] private Camera playerTwoCam;
    [SerializeField] private float moveSpeed = 30;

    //Change these if tower is scaled
    private static int camPosHorizontal = 30;
    private static int camPosVertical = 9;
    private static int camRotationX = 15;
    private static int camRotationY = -45;


    //TODO: Create more positions as we add more height/levels to the tower. 
    //TODO: Once we have a ton of positions, create a method to generate Vectors instead of having a huge array.
    private Vector3[] basePositions = new [] { new Vector3(camPosHorizontal,        camPosVertical, -camPosHorizontal),
                                               new Vector3(camPosHorizontal + 5,    camPosVertical, 0),
                                               new Vector3(camPosHorizontal,        camPosVertical, camPosHorizontal),
                                               new Vector3(0,                       camPosVertical, camPosHorizontal + 5),
                                               new Vector3(-camPosHorizontal,       camPosVertical, camPosHorizontal),
                                               new Vector3(-(camPosHorizontal + 5), camPosVertical, 0),
                                               new Vector3(-camPosHorizontal,       camPosVertical, -camPosHorizontal),
                                               new Vector3(0,                       camPosVertical, -(camPosHorizontal + 5))};

    private Quaternion[] baseRotations = new[] { Quaternion.Euler(camRotationX, camRotationY, 0),
                                                 Quaternion.Euler(camRotationX, camRotationY - 45, 0),
                                                 Quaternion.Euler(camRotationX, camRotationY - 90, 0),
                                                 Quaternion.Euler(camRotationX, camRotationY - 135, 0),
                                                 Quaternion.Euler(camRotationX, camRotationY - 180, 0),
                                                 Quaternion.Euler(camRotationX, camRotationY - 225, 0),
                                                 Quaternion.Euler(camRotationX, camRotationY - 270, 0),
                                                 Quaternion.Euler(camRotationX, camRotationY - 315, 0)};

    private int currentPos;
    private float distance, covered, remaining;
    private float startTime;
    private Vector3 prevPosition, goalPosition;
    private Quaternion prevRotation, goalRotation;

    private bool moving = false;
    private bool mouseEnabled = true;

    private void Start()
    {
        playerTwoCam.transform.position = basePositions[0];
        playerTwoCam.transform.rotation = baseRotations[0];

        currentPos = 1;

        distance = Vector3.Distance(basePositions[0], basePositions[1]);

        moving = false;
        mouseEnabled = true;
    }

    private void Update()
    {
        if(moving)
        {
            covered = (Time.time - startTime) * moveSpeed;
            remaining = covered / distance;

            playerTwoCam.transform.position = Vector3.Lerp(prevPosition, goalPosition, remaining);
            playerTwoCam.transform.rotation = Quaternion.Slerp(prevRotation, goalRotation, remaining);

            if (Time.time - startTime >= 1)
            {
                moving = false;
                mouseEnabled = true;
            }
        }

        if (mouseEnabled)
        {
            if (Input.GetMouseButtonDown(0))
            {
                mouseEnabled = false;
                if (currentPos == 1)
                {
                    startMove(basePositions[0], basePositions[7], baseRotations[0], baseRotations[7], 8);
                }
                else if (currentPos == 2)
                {
                    startMove(basePositions[1], basePositions[0], baseRotations[1], baseRotations[0], 1);
                }
                else if (currentPos == 3)
                {
                    startMove(basePositions[2], basePositions[1], baseRotations[2], baseRotations[1], 2);
                }
                else if (currentPos == 4)
                {
                    startMove(basePositions[3], basePositions[2], baseRotations[3], baseRotations[2], 3);
                }
                else if (currentPos == 5)
                {
                    startMove(basePositions[4], basePositions[3], baseRotations[4], baseRotations[3], 4);
                }
                else if (currentPos == 6)
                {
                    startMove(basePositions[5], basePositions[4], baseRotations[5], baseRotations[4], 5);
                }
                else if (currentPos == 7)
                {
                    startMove(basePositions[6], basePositions[5], baseRotations[6], baseRotations[5], 6);
                }
                else if (currentPos == 8)
                {
                    startMove(basePositions[7], basePositions[6], baseRotations[7], baseRotations[6], 7);
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                mouseEnabled = false;
                if (currentPos == 1)
                {
                    startMove(basePositions[0], basePositions[1], baseRotations[0], baseRotations[1], 2);
                }
                else if (currentPos == 2)
                {
                    startMove(basePositions[1], basePositions[2], baseRotations[1], baseRotations[2], 3);
                }
                else if (currentPos == 3)
                {
                    startMove(basePositions[2], basePositions[3], baseRotations[2], baseRotations[3], 4);
                }
                else if (currentPos == 4)
                {
                    startMove(basePositions[3], basePositions[4], baseRotations[3], baseRotations[4], 5);
                }
                else if (currentPos == 5)
                {
                    startMove(basePositions[4], basePositions[5], baseRotations[4], baseRotations[5], 6);
                }
                else if (currentPos == 6)
                {
                    startMove(basePositions[5], basePositions[6], baseRotations[5], baseRotations[6], 7);
                }
                else if (currentPos == 7)
                {
                    startMove(basePositions[6], basePositions[7], baseRotations[6], baseRotations[7], 8);
                }
                else if (currentPos == 8)
                {
                    startMove(basePositions[7], basePositions[0], baseRotations[7], baseRotations[0], 1);
                }
            }
        }
    }

    private void startMove(Vector3 prevPos, Vector3 goalPos, Quaternion prevRot, Quaternion goalRot, int goal)
    {
        distance = Vector3.Distance(prevPos, goalPos);
        startTime = Time.time;
        moving = true;
        prevPosition = prevPos;
        goalPosition = goalPos;
        prevRotation = prevRot;
        goalRotation = goalRot;

        currentPos = goal;
    }
}
