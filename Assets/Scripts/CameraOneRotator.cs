using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOneRotator : MonoBehaviour {

    [SerializeField] private Camera playerOneCam;

    private Vector3 pos1 = new Vector3(0, 2, -10);
    private Quaternion rot1 = Quaternion.Euler(10, 0, 0);

    private Vector3 pos2 = new Vector3(10, 2, 0);
    private Quaternion rot2 = Quaternion.Euler(10, -90, 0);

    private Vector3 pos3 = new Vector3(0, 2, 10);
    private Quaternion rot3 = Quaternion.Euler(10, 180, 0);

    private Vector3 pos4 = new Vector3(-10, 2, 0);
    private Quaternion rot4 = Quaternion.Euler(10, 90, 0);

    private void Start()
    {
        playerOneCam.transform.position = pos1;
        playerOneCam.transform.rotation = rot1;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(this.tag == "Trigger1")
            {
                Debug.Log("One");
                playerOneCam.transform.position = pos2;
                playerOneCam.transform.rotation = rot2;
            }
            if (this.tag == "Trigger2")
            {
                Debug.Log("Two");
                playerOneCam.transform.position = pos3;
                playerOneCam.transform.rotation = rot3;
            }
            if (this.tag == "Trigger3")
            {
                Debug.Log("Three");
                playerOneCam.transform.position = pos4;
                playerOneCam.transform.rotation = rot4;
            }
            if (this.tag == "Trigger4")
            {
                Debug.Log("Four");
                playerOneCam.transform.position = pos1;
                playerOneCam.transform.rotation = rot1;
            }
        }
    }
}
