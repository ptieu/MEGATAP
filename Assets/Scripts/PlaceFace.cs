using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceFace : MonoBehaviour
{
    [SerializeField] private List<GameObject> TowerFaces;
    [SerializeField] private CameraTwoRotator cam;
    private int pos = 1;

    // Update is called once per frame
    void Update()
    {
        pos = cam.GetState();

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (pos == 2)
            {
                Instantiate(TowerFaces[1], new Vector3(12f, 0, 0), Quaternion.Euler(0, 90, 0));
            }
            else if (pos == 4)
            {
                Instantiate(TowerFaces[2], new Vector3(0, 0, 12f), Quaternion.Euler(0, 0, 0));
            }
            else if (pos == 6)
            {
                Instantiate(TowerFaces[3], new Vector3(-12f, 0, 0), Quaternion.Euler(0, 270, 0));
            }
            else if (pos == 8)
            {
                Instantiate(TowerFaces[0], new Vector3(0, 0, -20f), Quaternion.Euler(0, 0, 0));
            }
        }
    }
}
