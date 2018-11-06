using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTwoRotator : MonoBehaviour {

    [SerializeField] private Camera playerTwoCam;

    private Vector3 pos1 = new Vector3(8, 3, -8);
    private Quaternion rot1 = Quaternion.Euler(15, -45, 0);

    private Vector3 pos2 = new Vector3(8, 3, 8);
    private Quaternion rot2 = Quaternion.Euler(10, -135, 0);

    //private Vector3 pos3 = new Vector3(0, 2, 10);
    //private Quaternion rot3 = Quaternion.Euler(10, 180, 0);

    //private Vector3 pos4 = new Vector3(-10, 2, 0);
    //private Quaternion rot4 = Quaternion.Euler(10, 90, 0);

    private string currentPos = "One";

    private void Start()
    {
        playerTwoCam.transform.position = pos1;
        playerTwoCam.transform.rotation = rot1;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(currentPos == "One")
            {

            }
        }
        if(Input.GetMouseButtonDown(1))
        {
            if(currentPos == "One")
            {
                playerTwoCam.transform.position = pos2;
                playerTwoCam.transform.rotation = rot2;
            }
        }
    }
}
