using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceFace : MonoBehaviour
{
    [SerializeField] private List<GameObject> TowerFaces;
    [SerializeField] private CameraTwoRotator cam;

    private int pos = 1;
    private int floor = 0;
    private GameObject ToBePlaced;

    private static int TotalPos = 9;
    private static int TotalFloors = 3;
    private int[,] FacesPlaced = new int[TotalPos, TotalFloors];

    void Start()
    {
        //Setting the 2 dimension array to 0s for checking if a face has been placed at a side later. <AL>
        for (int row = 0; row < TotalPos; row++)
        {
            for(int column = 0; column < TotalFloors; column++)
            {
                FacesPlaced[row, column] = 0;
            }
        }
    }

    void Update()
    {
        //Place a face. <AL>
        if (Input.GetKeyDown(KeyCode.Return))
        {
            pos = cam.GetState();
            floor = cam.GetFloor();

            ToBePlaced = TowerFaces[Random.Range(1, 3)];
            ToBePlaced.gameObject.transform.localScale = new Vector3(2.5f, 2.65f, 1f);

            //Face placing per side per floor. <AL>
            if (pos == 2)
            {
                if (FacesPlaced[pos, floor] == 0)
                {
                    Instantiate(ToBePlaced, new Vector3(11f, (-4.3f + 4.1f * floor * 2f), -10f), Quaternion.Euler(0, 90, 0));
                    FacesPlaced[pos, floor]++;
                }
            }
            else if (pos == 4)
            {
                if (FacesPlaced[pos, floor] == 0)
                {
                    Instantiate(ToBePlaced, new Vector3(10f, (-4.3f + 4.1f * floor * 2f), 11f), Quaternion.Euler(0, 0, 0));
                    FacesPlaced[pos, floor]++;
                }
            }
            else if (pos == 6)
            {
                if (FacesPlaced[pos, floor] == 0)
                {
                    Instantiate(ToBePlaced, new Vector3(-11f, (-4.3f + 4.1f * floor * 2f), 10f), Quaternion.Euler(0, 270, 0));
                    FacesPlaced[pos, floor]++;
                }
            }
            else if (pos == 8)
            {
                if (FacesPlaced[pos, floor] == 0 && floor != 0)
                {
                    Instantiate(ToBePlaced, new Vector3(-10f, (-4.3f + 4.1f * floor * 2f), -11f), Quaternion.Euler(0, 180, 0));
                    FacesPlaced[pos, floor]++;
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.L))
        {
            //For debugging. Shows me position, floor, and array data in console. Remove when unneeded. <AL>
            pos = cam.GetState();
            floor = cam.GetFloor();
            print("Pos: " + pos +"," + "Floor: " + floor + ", Array: " + FacesPlaced[pos, floor]);
        }
    }
}
