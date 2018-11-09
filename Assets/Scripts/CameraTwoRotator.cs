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

    private Vector3 pos1 = new Vector3(camPosHorizontal, camPosVertical, -camPosHorizontal);
    private Quaternion rot1 = Quaternion.Euler(camRotationX, camRotationY, 0);

    private Vector3 pos2 = new Vector3(camPosHorizontal + 5, camPosVertical, 0);
    private Quaternion rot2 = Quaternion.Euler(camRotationX, camRotationY - 45, 0);

    private Vector3 pos3 = new Vector3(camPosHorizontal, camPosVertical, camPosHorizontal);
    private Quaternion rot3 = Quaternion.Euler(camRotationX, camRotationY - 90, 0);

    private Vector3 pos4 = new Vector3(0, camPosVertical, camPosHorizontal + 5);
    private Quaternion rot4 = Quaternion.Euler(camRotationX, camRotationY - 135, 0);

    private Vector3 pos5 = new Vector3(-camPosHorizontal, camPosVertical, camPosHorizontal);
    private Quaternion rot5 = Quaternion.Euler(camRotationX, camRotationY - 180, 0);

    private Vector3 pos6 = new Vector3(-(camPosHorizontal + 5), camPosVertical, 0);
    private Quaternion rot6 = Quaternion.Euler(camRotationX, camRotationY - 225, 0);

    private Vector3 pos7 = new Vector3(-camPosHorizontal, camPosVertical, -camPosHorizontal);
    private Quaternion rot7 = Quaternion.Euler(camRotationX, camRotationY - 270, 0);

    private Vector3 pos8 = new Vector3(0, camPosVertical, -(camPosHorizontal + 5));
    private Quaternion rot8 = Quaternion.Euler(camRotationX, camRotationY - 315, 0);

    private int currentPos;
    private float distance, covered, remaining;
    private float startTime;
    private Vector3 prevPosition, goalPosition;
    private Quaternion prevRotation, goalRotation;

    private bool moving = false;
    private bool mouseEnabled = true;

    private void Start()
    {
        playerTwoCam.transform.position = pos1;
        playerTwoCam.transform.rotation = rot1;

        currentPos = 1;

        distance = Vector3.Distance(pos1, pos2);

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

            if (Time.time - startTime >= 0.75)
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
                    startMove(pos1, pos8, rot1, rot8, 8);
                }
                else if (currentPos == 2)
                {
                    startMove(pos2, pos1, rot2, rot1, 1);
                }
                else if (currentPos == 3)
                {
                    startMove(pos3, pos2, rot3, rot2, 2);
                }
                else if (currentPos == 4)
                {
                    startMove(pos4, pos3, rot4, rot3, 3);
                }
                else if (currentPos == 5)
                {
                    startMove(pos5, pos4, rot5, rot4, 4);
                }
                else if (currentPos == 6)
                {
                    startMove(pos6, pos5, rot6, rot5, 5);
                }
                else if (currentPos == 7)
                {
                    startMove(pos7, pos6, rot7, rot6, 6);
                }
                else if (currentPos == 8)
                {
                    startMove(pos8, pos7, rot8, rot7, 7);
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                mouseEnabled = false;
                if (currentPos == 1)
                {
                    startMove(pos1, pos2, rot1, rot2, 2);
                }
                else if (currentPos == 2)
                {
                    startMove(pos2, pos3, rot2, rot3, 3);
                }
                else if (currentPos == 3)
                {
                    startMove(pos3, pos4, rot3, rot4, 4);
                }
                else if (currentPos == 4)
                {
                    startMove(pos4, pos5, rot4, rot5, 5);
                }
                else if (currentPos == 5)
                {
                    startMove(pos5, pos6, rot5, rot6, 6);
                }
                else if (currentPos == 6)
                {
                    startMove(pos6, pos7, rot6, rot7, 7);
                }
                else if (currentPos == 7)
                {
                    startMove(pos7, pos8, rot7, rot8, 8);
                }
                else if (currentPos == 8)
                {
                    startMove(pos8, pos1, rot8, rot1, 1);
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
