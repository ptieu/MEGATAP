using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//<alexc> This class rotates the Player 1 (left side) camera when the player runs into triggers at
//        the edge of each face of the tower. 
public class CameraOneRotator : MonoBehaviour {

    [SerializeField] private Camera playerOneCam;
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject playerModel;
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject[] rotateTriggers;
    [SerializeField] private Transform floorSpawn;

    //Change these if the tower is scaled
    private static int camPosHorizontal = 45;
    private static int camPosVertical   = 7;
    private static int camRotationX     = 10;
    private static int camRotationY     = 0;
    private static int numFloors = 10;

    private Vector3[] basePositions = new[] {  new Vector3(0,                 camPosVertical, -camPosHorizontal),
                                               new Vector3(camPosHorizontal,  camPosVertical, 0),
                                               new Vector3(0,                 camPosVertical, camPosHorizontal),
                                               new Vector3(-camPosHorizontal, camPosVertical, 0)};

    private Quaternion[] rotations = new[] { Quaternion.Euler(camRotationX, camRotationY, 0),
                                             Quaternion.Euler(camRotationX, camRotationY - 90, 0),
                                             Quaternion.Euler(camRotationX, camRotationY - 180, 0),
                                             Quaternion.Euler(camRotationX, camRotationY - 270, 0)};

    private IEnumerator camTween;
    private int cameraState, floor;
    private Rigidbody rb;

    private void Start()
    {
        playerOneCam.transform.localPosition= basePositions[0];
        playerOneCam.transform.rotation = rotations[0];
        cameraState = 1;
        floor = 1;
        rb = GetComponent<Rigidbody>();
    }

    //4 triggers for rotating camera, 3 for putting up walls behind the player
    private void OnTriggerEnter(Collider other)
    {
        Vector3 wallPos = wall.transform.position;
        Quaternion wallRot = wall.transform.rotation;
        float wallY = wall.transform.position.y + 10 * (floor - 1);

        switch (other.tag)
        {
            case "Trigger1":
                StartMove(basePositions[1], rotations[1], 2);
                break;
            case "Trigger2":
                StartMove(basePositions[2], rotations[2], 3);
                break;
            case "Trigger3":
                StartMove(basePositions[3], rotations[3], 4);
                break;
            case "Trigger4":
                if (floor < numFloors)
                {
                    floor++;
                    MovePlayerUp();
                    StartMove(basePositions[0], rotations[0], 1);
                    break;
                }
                else
                {
                    StartMove(basePositions[0], rotations[0], 1);
                    break;
                }
            case "Wall1":
                wallPos = new Vector3(rotateTriggers[0].transform.position.x, wallY, rotateTriggers[0].transform.position.z + 6);
                wallRot = Quaternion.Euler(wall.transform.rotation.x, wall.transform.rotation.y + 90, wall.transform.rotation.z);
                Instantiate(wall, wallPos, wallRot);
                break;
            case "Wall2":
                wallPos = new Vector3(rotateTriggers[1].transform.position.x - 5, wallY, rotateTriggers[1].transform.position.z);
                Instantiate(wall, wallPos, wallRot);
                break;
            case "Wall3":
                wallPos = new Vector3(rotateTriggers[2].transform.position.x, wallY, rotateTriggers[1].transform.position.z - 5);
                wallRot = Quaternion.Euler(wall.transform.rotation.x, wall.transform.rotation.y + 90, wall.transform.rotation.z);
                Instantiate(wall, wallPos, wallRot);
                break;
        }
    }

    //Initialize camera movement variables and start movement coroutine
    private void StartMove(Vector3 goalPos, Quaternion goalRot, int camState)
    {
        rb.velocity = Vector3.zero;
        RotatePlayer();
        cameraState  = camState;

        if(camTween != null)
        {
            StopCoroutine(camTween);
        }
        camTween = TweenToPosition(goalPos, goalRot, moveSpeed);
        StartCoroutine(camTween);
    }

    //Camera movement coroutine
    private IEnumerator TweenToPosition(Vector3 targetPos, Quaternion targetRot, float time)
    {
        Vector3 currentPos = playerOneCam.transform.localPosition;
        Quaternion currentRot = playerOneCam.transform.rotation;

        for (float t = 0; t < time; t += Time.deltaTime)
        {
            playerOneCam.transform.localPosition = Vector3.Lerp(currentPos, targetPos, t/time);
            playerOneCam.transform.rotation = Quaternion.Slerp(currentRot, targetRot, t/time);
            yield return null;
        }

        playerOneCam.transform.localPosition = targetPos;
        playerOneCam.transform.rotation = targetRot;
        camTween = null;
    }
    
    //TODO: Change this. It's not good. Bugs out sometimes and sends people all the way up. Replace with a Lerp & a ladder? 
    private void MovePlayerUp()
    {
        this.transform.position = floorSpawn.position + Vector3.up * 10 * (floor - 2);
    }

    //Rotate the player model when you move around the tower
    private void RotatePlayer()
    {
        float rotY = playerModel.transform.localRotation.eulerAngles.y;
        playerModel.transform.localRotation = Quaternion.Euler(0, rotY - 90, 0);
    }


    public int GetState()
    {
        return cameraState;
    }
}
