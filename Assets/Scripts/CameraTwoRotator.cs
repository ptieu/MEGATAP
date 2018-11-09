using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//<alexc> This class rotates the Player 2 (right side camera) on a given input.
public class CameraTwoRotator : MonoBehaviour {

    [SerializeField] private Camera playerTwoCam;
    [SerializeField] private float moveSpeed = 2;

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

    private IEnumerator camTween;

    private int currentPos;

    private bool mouseEnabled = true;

    private void Start()
    {
        playerTwoCam.transform.position = basePositions[0];
        playerTwoCam.transform.rotation = baseRotations[0];

        currentPos = 1;

        mouseEnabled = true;
    }

    private void Update()
    {
        if (mouseEnabled)
        {
            if (Input.GetMouseButtonDown(0))
            {
                mouseEnabled = false;

                switch(currentPos)
                {
                    case 1:
                        StartMove(basePositions[0], basePositions[7], baseRotations[0], baseRotations[7], 8);
                        break;
                    case 2:
                        StartMove(basePositions[1], basePositions[0], baseRotations[1], baseRotations[0], 1);
                        break;
                    case 3:
                        StartMove(basePositions[2], basePositions[1], baseRotations[2], baseRotations[1], 2);
                        break;
                    case 4:
                        StartMove(basePositions[3], basePositions[2], baseRotations[3], baseRotations[2], 3);
                        break;
                    case 5:
                        StartMove(basePositions[4], basePositions[3], baseRotations[4], baseRotations[3], 4);
                        break;
                    case 6:
                        StartMove(basePositions[5], basePositions[4], baseRotations[5], baseRotations[4], 5);
                        break;
                    case 7:
                        StartMove(basePositions[6], basePositions[5], baseRotations[6], baseRotations[5], 6);
                        break;
                    case 8:
                        StartMove(basePositions[7], basePositions[6], baseRotations[7], baseRotations[6], 7);
                        break;
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                mouseEnabled = false;

                switch(currentPos)
                {
                    case 1:
                        StartMove(basePositions[0], basePositions[1], baseRotations[0], baseRotations[1], 2);
                        break;
                    case 2:
                        StartMove(basePositions[1], basePositions[2], baseRotations[1], baseRotations[2], 3);
                        break;
                    case 3:
                        StartMove(basePositions[2], basePositions[3], baseRotations[2], baseRotations[3], 4);
                        break;
                    case 4:
                        StartMove(basePositions[3], basePositions[4], baseRotations[3], baseRotations[4], 5);
                        break;
                    case 5:
                        StartMove(basePositions[4], basePositions[5], baseRotations[4], baseRotations[5], 6);
                        break;
                    case 6:
                        StartMove(basePositions[5], basePositions[6], baseRotations[5], baseRotations[6], 7);
                        break;
                    case 7:
                        StartMove(basePositions[6], basePositions[7], baseRotations[6], baseRotations[7], 8);
                        break;
                    case 8:
                        StartMove(basePositions[7], basePositions[0], baseRotations[7], baseRotations[0], 1);
                        break;
                }
            }
        }
    }

    private void StartMove(Vector3 prevPos, Vector3 goalPos, Quaternion prevRot, Quaternion goalRot, int goal)
    {
        currentPos = goal;

        if (camTween != null)
        {
            StopCoroutine(camTween);
        }
        camTween = TweenToPosition(goalPos, goalRot, moveSpeed);
        StartCoroutine(camTween);
    }

    private IEnumerator TweenToPosition(Vector3 targetPos, Quaternion targetRot, float time)
    {
        Vector3 currentPos = playerTwoCam.transform.position;
        Quaternion currentRot = playerTwoCam.transform.rotation;

        for (float t = 0; t < time; t += Time.deltaTime)
        {
            playerTwoCam.transform.position = Vector3.Lerp(currentPos, targetPos, t);
            playerTwoCam.transform.rotation = Quaternion.Slerp(currentRot, targetRot, t);
            yield return null;
        }

        playerTwoCam.transform.position = targetPos;
        playerTwoCam.transform.rotation = targetRot;

        mouseEnabled = true;
        camTween = null;
    }
}
