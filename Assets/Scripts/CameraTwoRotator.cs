using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTwoRotator : MonoBehaviour {

    [SerializeField] private Camera playerTwoCam;
    [SerializeField] private float moveSpeed = 30;

    //Change these if tower is scaled
    private static int camPosHorizontal = 18;
    private static int camPosVertical = 8;
    private static int camRotationX = 15;
    private static int camRotationY = -45;

    private Vector3 pos1 = new Vector3(camPosHorizontal, camPosVertical, -camPosHorizontal);
    private Quaternion rot1 = Quaternion.Euler(camRotationX, camRotationY, 0);

    private Vector3 pos2 = new Vector3(camPosHorizontal, camPosVertical, camPosHorizontal);
    private Quaternion rot2 = Quaternion.Euler(camRotationX, camRotationY - 90, 0);

    private Vector3 pos3 = new Vector3(-camPosHorizontal, camPosVertical, camPosHorizontal);
    private Quaternion rot3 = Quaternion.Euler(camRotationX, camRotationY - 180, 0);

    private Vector3 pos4 = new Vector3(-camPosHorizontal, camPosVertical, -camPosHorizontal);
    private Quaternion rot4 = Quaternion.Euler(camRotationX, camRotationY - 270, 0);

    private string currentPos;
    private float distance, covered, remaining;
    private float startTime;
    private Vector3 prevPosition, goalPosition;
    private Quaternion prevRotation, goalRotation;
    private bool moving = false;

    private void Start()
    {
        playerTwoCam.transform.position = pos1;
        playerTwoCam.transform.rotation = rot1;

        currentPos = "One";

        distance = Vector3.Distance(pos1, pos2);

        moving = false;
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
            }
        }

        if(Input.GetMouseButtonDown(0))
        {
            if (currentPos == "One")
            {
                startMove(pos1, pos4, rot1, rot4, "Four");
            }
            else if (currentPos == "Two")
            {
                startMove(pos2, pos1, rot2, rot1, "One");
            }
            else if (currentPos == "Three")
            {
                startMove(pos3, pos2, rot3, rot2, "Two");
            }
            else if (currentPos == "Four")
            {
                startMove(pos4, pos3, rot4, rot3, "Three");
            }
        }
        if(Input.GetMouseButtonDown(1))
        {
            if(currentPos == "One")
            {
                startMove(pos1, pos2, rot1, rot2, "Two");
            }
            else if (currentPos == "Two")
            {
                startMove(pos2, pos3, rot2, rot3, "Three");
            }
            else if (currentPos == "Three")
            {
                startMove(pos3, pos4, rot3, rot4, "Four");
            }
            else if (currentPos == "Four")
            {
                startMove(pos4, pos1, rot4, rot1, "One");
            }
        }
    }

    private void startMove(Vector3 prevPos, Vector3 goalPos, Quaternion prevRot, Quaternion goalRot, string goal)
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
