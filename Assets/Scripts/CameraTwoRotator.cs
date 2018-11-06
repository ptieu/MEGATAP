using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTwoRotator : MonoBehaviour {

    [SerializeField] private Camera playerTwoCam;
    [SerializeField] private static int camPosHorizontal = 8;
    [SerializeField] private static int camPosVertical = 3;
    [SerializeField] private static int camRotationX = 15;
    [SerializeField] private static int camRotationY = -45;

    private Vector3 pos1 = new Vector3(camPosHorizontal, camPosVertical, -camPosHorizontal);
    private Quaternion rot1 = Quaternion.Euler(camRotationX, camRotationY, 0);

    private Vector3 pos2 = new Vector3(camPosHorizontal, camPosVertical, camPosHorizontal);
    private Quaternion rot2 = Quaternion.Euler(camRotationX, camRotationY - 90, 0);

    private Vector3 pos3 = new Vector3(-camPosHorizontal, camPosVertical, camPosHorizontal);
    private Quaternion rot3 = Quaternion.Euler(camRotationX, camRotationY - 180, 0);

    private Vector3 pos4 = new Vector3(-camPosHorizontal, camPosVertical, -camPosHorizontal);
    private Quaternion rot4 = Quaternion.Euler(camRotationX, camRotationY - 270, 0);

    private string currentPos;

    private void Start()
    {
        playerTwoCam.transform.position = pos1;
        playerTwoCam.transform.rotation = rot1;
        currentPos = "One";
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (currentPos == "One")
            {
                playerTwoCam.transform.position = pos4;
                playerTwoCam.transform.rotation = rot4;
                currentPos = "Four";
            }
            else if (currentPos == "Two")
            {
                playerTwoCam.transform.position = pos1;
                playerTwoCam.transform.rotation = rot1;
                currentPos = "One";
            }
            else if (currentPos == "Three")
            {
                playerTwoCam.transform.position = pos2;
                playerTwoCam.transform.rotation = rot2;
                currentPos = "Two";
            }
            else if (currentPos == "Four")
            {
                playerTwoCam.transform.position = pos3;
                playerTwoCam.transform.rotation = rot3;
                currentPos = "Three";
            }
        }
        if(Input.GetMouseButtonDown(1))
        {
            if(currentPos == "One")
            {
                playerTwoCam.transform.position = pos2;
                playerTwoCam.transform.rotation = rot2;
                currentPos = "Two";
            }
            else if (currentPos == "Two")
            {
                playerTwoCam.transform.position = pos3;
                playerTwoCam.transform.rotation = rot3;
                currentPos = "Three";
            }
            else if (currentPos == "Three")
            {
                playerTwoCam.transform.position = pos4;
                playerTwoCam.transform.rotation = rot4;
                currentPos = "Four";
            }
            else if (currentPos == "Four")
            {
                playerTwoCam.transform.position = pos1;
                playerTwoCam.transform.rotation = rot1;
                currentPos = "One";
            }
        }
    }
}
