using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//<alexc> This class rotates the Player 1 (left side) camera when the player runs into triggers at
//        the edge of each face of the tower. 
public class CameraOneRotator : MonoBehaviour {

    [SerializeField] private Camera playerOneCam;
    [SerializeField] private int moveSpeed = 2;

    //Change these if the tower size changes
    private static int camPosHorizontal = 45;
    private static int camPosVertical   = 5;
    private static int camRotationX     = 10;
    private static int camRotationY     = 0;


    //TODO: Create more positions as we add more height/levels to the tower. 
    //TODO: Once we have a ton of positions, create a method to generate Vectors instead of having a huge array.
    private Vector3[] basePositions = new[] {  new Vector3(0,                 camPosVertical, -camPosHorizontal),
                                               new Vector3(camPosHorizontal,  camPosVertical, 0),
                                               new Vector3(0,                 camPosVertical, camPosHorizontal),
                                               new Vector3(-camPosHorizontal, camPosVertical, 0)};

    private Quaternion[] rotations = new[] { Quaternion.Euler(camRotationX, camRotationY, 0),
                                             Quaternion.Euler(camRotationX, camRotationY - 90, 0),
                                             Quaternion.Euler(camRotationX, camRotationY - 180, 0),
                                             Quaternion.Euler(camRotationX, camRotationY - 270, 0)};

    private IEnumerator camTween;
    private int cameraState = 1;

    private void Start()
    {
        playerOneCam.transform.position = basePositions[0];
        playerOneCam.transform.rotation = rotations[0];
        cameraState = 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        switch(other.tag)
        {
            case "Trigger1":
                StartMove(basePositions[0], basePositions[1], rotations[0], rotations[1], 2);
                break;
            case "Trigger2":
                StartMove(basePositions[1], basePositions[2], rotations[1], rotations[2], 3);
                break;
            case "Trigger3":
                StartMove(basePositions[2], basePositions[3], rotations[2], rotations[3], 4);
                break;
            case "Trigger4":
                StartMove(basePositions[3], basePositions[0], rotations[3], rotations[0], 1);
                break;
        }
    }


    private void StartMove(Vector3 prevPos, Vector3 goalPos, Quaternion prevRot, Quaternion goalRot, int camState)
    {
        cameraState  = camState;

        if(camTween != null)
        {
            StopCoroutine(camTween);
        }
        camTween = TweenToPosition(goalPos, goalRot, moveSpeed);
        StartCoroutine(camTween);
    }

    private IEnumerator TweenToPosition(Vector3 targetPos, Quaternion targetRot, float time)
    {
        Vector3 currentPos = playerOneCam.transform.position;
        Quaternion currentRot = playerOneCam.transform.rotation;

        for (float t = 0; t < time; t += Time.deltaTime)
        {
            playerOneCam.transform.position = Vector3.Lerp(currentPos, targetPos, t);
            playerOneCam.transform.rotation = Quaternion.Slerp(currentRot, targetRot, t);
            yield return null;
        }

        playerOneCam.transform.position = targetPos;
        playerOneCam.transform.rotation = targetRot;

        camTween = null;
    }

    public int GetState()
    {
        return cameraState;
    }
}
