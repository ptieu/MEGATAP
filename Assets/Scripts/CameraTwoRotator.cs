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

                if(currentPos == 1)
                {
                    StartMove(basePositions[currentPos-1], basePositions[basePositions.Length-1], baseRotations[currentPos-1], baseRotations[baseRotations.Length-1], basePositions.Length);
                }
                else
                {
                    StartMove(basePositions[currentPos - 1], basePositions[currentPos - 2], baseRotations[currentPos - 1], baseRotations[currentPos-2], currentPos - 1);
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                mouseEnabled = false;

                if (currentPos == basePositions.Length)
                {
                    StartMove(basePositions[basePositions.Length - 1], basePositions[0], baseRotations[baseRotations.Length - 1], baseRotations[0], 1);
                }
                else
                {
                    StartMove(basePositions[currentPos - 1], basePositions[currentPos], baseRotations[currentPos - 1], baseRotations[currentPos], currentPos + 1);
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
            playerTwoCam.transform.position = Vector3.Lerp(currentPos, targetPos, t/time);
            playerTwoCam.transform.rotation = Quaternion.Slerp(currentRot, targetRot, t/time);
            yield return null;
        }

        playerTwoCam.transform.position = targetPos;
        playerTwoCam.transform.rotation = targetRot;

        mouseEnabled = true;
        camTween = null;
    }
}
