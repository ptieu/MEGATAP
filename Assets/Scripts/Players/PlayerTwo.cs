using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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
    private GameObject previouslySelected;
    private bool placeEnabled;
    private int floor;

    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Image controllerCursor;
    [SerializeField] private float controllerCursorSpeed;

    private string[] joysticks;
    private bool controller;

    private void Start()
    {
        //Listeners for buttons
        trapButtons[0].onClick.AddListener(OnClickTrap1);
        trapButtons[1].onClick.AddListener(OnClickTrap2);
        trapButtons[2].onClick.AddListener(OnClickTrap3);
        trapButtons[3].onClick.AddListener(OnClickTrap4);

        //Set up trap button navigation & custom cursor if controller is connected
        controller = gameManager.GetControllerTwoState();
        if(controller)
        {
            eventSystem.firstSelectedGameObject = trapButtons[0].gameObject;
            eventSystem.SetSelectedGameObject(trapButtons[0].gameObject);
            controllerCursor.enabled = true;
        }
        else
        {
            controllerCursor.enabled = false;
        }

        placeEnabled = false;
    }


    private void Update()
    {
        floor = camRotator.GetFloor();
        RaycastFromCam(false);

        controller = gameManager.GetControllerTwoState();
        if(controller)
        {
            //If controller, update controller cursor position to match joystick
            if(Mathf.Abs(Input.GetAxisRaw("Horizontal_Joy_2")) > 0.5f || Mathf.Abs(Input.GetAxisRaw("Vertical_Joy_2")) > 0.5f)
            {
                controllerCursor.transform.Translate(Input.GetAxisRaw("Horizontal_Joy_2") * controllerCursorSpeed, Input.GetAxisRaw("Vertical_Joy_2") * controllerCursorSpeed, 0);
            }
            //If controller & they want to place a trap, call raycast function
            if(Input.GetButton("Place_Joy_2") && placeEnabled)
            {
                RaycastFromCam(true);
            }
        }
    }


    public void OnClickTower()
    {
        RaycastFromCam(true);
    }


    //Raycast from camera to center column of tower. Have a ghost trap follow the mouse if a button
    //has been selected, and instantiate one if the tower is clicked.
    //"clicked" bool should be true if the tower is clicked / the player is trying to actually place a trap down
    private void RaycastFromCam(bool clicked)
    {
        RaycastHit hit;
        Ray ray;
        
        //Raycast to custom cursor if controller is connected, otherwise raycast to mouse position
        if (controller)
        {
            ray = cam.ScreenPointToRay(controllerCursor.transform.position);
        }
        else
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);
        }

        if (Physics.Raycast(ray, out hit, float.MaxValue, ~LayerMask.NameToLayer("Tower")))
        {
            //Round hitX, hitY, hitZ to grid based on grid sizes in inspector
            int hitX = Mathf.RoundToInt(hit.point.x / horizontalGridSize) * horizontalGridSize;
            int hitZ = Mathf.RoundToInt(hit.point.z / horizontalGridSize) * horizontalGridSize;
            int hitY = Mathf.RoundToInt(hit.point.y / verticalGridSize) * verticalGridSize;


            Vector3 hitPos = new Vector3(hitX, hitY, hitZ) + hit.normal * 5;
            Quaternion hitRot = Quaternion.identity;

            //Change rotation based on normal from tower 
            if (hit.normal.x == -1 || hit.normal.x == 1)
            {
                hitRot = Quaternion.Euler(0, 90, 0);
            }


            //Change position & rotation of ghost trap to follow mouse if it exists
            if (ghostTrap != null)
            {
                if (controller)
                {
                    ghostTrap.transform.position = hitPos;
                    ghostTrap.transform.rotation = hitRot;
                }
                else
                {
                    ghostTrap.transform.position = hitPos;
                    ghostTrap.transform.rotation = hitRot;
                }
            }


            //If the player clicked on the tower / if the player is trying to place a trap, and there is enough space nearby, instantiate the trap and destroy the ghost
            if (clicked && CheckNearby(hit.point, widthBetweenTraps, heightBetweenTraps) && CheckFloor(hitPos.y) && trap != null)
            {
                Instantiate(trap, hitPos, hitRot);
                trap = null;
                Cursor.visible = true;
                DestroyGhost();

                //Set currently selected trap button if there is a controller
                if (controller)
                {
                    eventSystem.SetSelectedGameObject(previouslySelected);
                    placeEnabled = false;
                }
            }
        }
        else
        {
            Cursor.visible = true;
        }
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
    
    //Set cursor not visible when not hovering over button
    public void OnPointerExitSetCursor()
    {
        Cursor.visible = false;
    }

    //Functions that are called when buttons are pressed
    private void OnClickTrap1()
    {
        ClickButton(0);       
    }

    private void OnClickTrap2()
    {
        ClickButton(1);
    }

    private void OnClickTrap3()
    {
        ClickButton(2);
    }

    private void OnClickTrap4()
    {
        ClickButton(3);
    }

    private void ClickButton(int trapNumber)
    {
        trap = traps[trapNumber];
        
        //Menu selections for controller input
        previouslySelected = trapButtons[trapNumber].gameObject;
        eventSystem.SetSelectedGameObject(null);

        DestroyGhost();
        SetGhost();

        StartCoroutine(EnableInput());
    }

    //Make player wait .5 seconds after pressing button to be able to place trap.
    //Gets rid of controller bug where pressing A to select a trap also immediately places it
    IEnumerator EnableInput()
    {
        yield return new WaitForSeconds(0.5f);
        placeEnabled = true;
    }
}
