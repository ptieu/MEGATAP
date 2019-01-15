using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//<alexc> This class focuses on player 2 mechanics to click UI buttons and place prefab traps.
public class PlayerTwo : MonoBehaviour {
    [SerializeField] private Button[] trapButtons;
    [SerializeField] private GameObject[] traps;
    [SerializeField] private GameObject tower;
    [SerializeField] private Camera cam;
    [SerializeField] private CameraTwoRotator camRotator;
    [SerializeField] private float widthBetweenTraps;
    [SerializeField] private float heightBetweenTraps;
    [SerializeField] private int horizontalGridSize;
    [SerializeField] private int verticalGridSize; 

    private GameObject trap;
    private GameObject ghostTrap;
    private int floor;


    private string[] joysticks;
    private bool controller;
    [SerializeField] private Image trapSelector;

    private void Start()
    {
        trapButtons[0].onClick.AddListener(OnClickTrap1);
        trapButtons[1].onClick.AddListener(OnClickTrap2);
        trapButtons[2].onClick.AddListener(OnClickTrap3);
        trapButtons[3].onClick.AddListener(OnClickTrap4);

        joysticks = Input.GetJoystickNames();
        controller = false;
    }


    private void Update()
    {
        floor = camRotator.GetFloor();
        RaycastFromCam(false);


        if (Input.GetJoystickNames().Length > 0)
        {
            for (int i = 0; i < joysticks.Length; i++)
            {
                if (!string.IsNullOrEmpty(joysticks[i]))
                {
                    Debug.Log("Controller " + i + " is connected using: " + joysticks[i]);
                    if (i == 1)
                    {
                        controller = true;
                    }
                }
                else
                {
                    Debug.Log("Controller " + i + " is disconnected.");
                    if (i == 1)
                    {
                        controller = false;
                    }
                }
            }
        }
        else
        {
            controller = true;
        }

        if(controller)
        {
            trapSelector.enabled = true;
            if(Input.GetAxisRaw("DpadX_Joy_2") == 1)
            {
                MoveSelectorRight();
            }
            else if(Input.GetAxisRaw("DpadX_Joy_2") == -1)
            {
                MoveSelectorLeft();
            }
        }
        else
        {
            trapSelector.enabled = false;
        }
    }

    private void MoveSelectorRight()
    {

    }

    private void MoveSelectorLeft()
    {

    }

    public void OnClickTower()
    {
        RaycastFromCam(true);
    }


    //Raycast from camera to center column of tower. Have a ghost trap follow the mouse if a button
    //has been selected, and instantiate one if the tower is clicked.
    private void RaycastFromCam(bool clicked)
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 99999, ~LayerMask.NameToLayer("Tower")))
        {
            int hitX = Mathf.RoundToInt(hit.point.x / horizontalGridSize) * horizontalGridSize;
            int hitZ = Mathf.RoundToInt(hit.point.z / horizontalGridSize) * horizontalGridSize;
            int hitY = Mathf.RoundToInt(hit.point.y / verticalGridSize) * verticalGridSize;


            Vector3 hitPos = new Vector3(hitX, hitY, hitZ) + hit.normal * 5;
            Quaternion hitRot = Quaternion.identity;

            if (hit.normal.x == -1 || hit.normal.x == 1)
            {
                hitRot = Quaternion.Euler(0, 90, 0);
            }


            if (ghostTrap != null)
            {
                ghostTrap.transform.position = hitPos;
                ghostTrap.transform.rotation = hitRot;
            }

            if (clicked && CheckNearby(hit.point, widthBetweenTraps, heightBetweenTraps) && CheckFloor(hitPos.y) && trap != null)
            {
                Instantiate(trap, hitPos, hitRot);
                trap = null;
                Cursor.visible = true;
                DestroyGhost();
            }
        }
        else
            Cursor.visible = true;
    }

    //Check for nearby platforms/traps to see if it is too close to place a new one.
    private bool CheckNearby(Vector3 center, float width, float height)
    {
        Collider[] hitColliders = Physics.OverlapBox(center, new Vector3(width, height, width));

        int i = 0;
        while (i < hitColliders.Length)
        {
            if(hitColliders[i].tag == "Platform")
            {
                return false;
            }
            i++;
        }

        return true;
    }

    private bool CheckFloor(float hitY)
    {
        float upperLimit = floor * 20;
        float lowerLimit = upperLimit - 20;
        if(hitY >= lowerLimit && hitY <= upperLimit)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void SetGhost()
    {
        if(trap != null)
        {
            ghostTrap = Instantiate(trap, Vector3.zero, Quaternion.identity);
            Color color = ghostTrap.GetComponent<MeshRenderer>().material.color;
            color.a = 0.5f;
            ghostTrap.GetComponent<MeshRenderer>().material.color = color;
            ghostTrap.tag = "Untagged";

            Cursor.visible = false;
        }
    }


    private void DestroyGhost()
    {
        if(ghostTrap != null)
        {
            Destroy(ghostTrap);
            ghostTrap = null;
        }
    }

    //Set cursor visible when hovering over a button
    public void OnPointerEnterSetCursor()
    {
        Cursor.visible = true;
    }

    public void OnPointerExitSetCursor()
    {
        Cursor.visible = false;
    }

    private void OnClickTrap1()
    {
        trap = traps[0];
        DestroyGhost();
        SetGhost();
    }

    private void OnClickTrap2()
    {
        trap = traps[1];
        DestroyGhost();
        SetGhost();
    }

    private void OnClickTrap3()
    {
        trap = traps[2];
        DestroyGhost();
        SetGhost();
    }

    private void OnClickTrap4()
    {
        trap = traps[3];
        DestroyGhost();
        SetGhost();
    }
}
