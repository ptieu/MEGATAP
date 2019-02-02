using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlaceTrap : MonoBehaviour {
    [SerializeField] private Button[] trapButtons;
    [SerializeField] private TrapBase[] trapPrefabs;
    [SerializeField] private Image controllerCursor;
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private GameManager gm;
    [SerializeField] private Camera cam;


    [SerializeField] private int cursorSpeed;
    [SerializeField] private int gridSize;

    private TrapBase trap;
    private GameObject ghostTrap;
    private int previouslySelected;

    private bool p2Controller;
    //private bool placeEnabled;

    //bool m_Started;
	void Start () {
        //m_Started = true;
        //Add click listeners for all trap buttons
		for(int trapNum = 0; trapNum < trapButtons.Length; trapNum++)
        {
            int closureCopy = trapNum;
            trapButtons[trapNum].onClick.AddListener(() => OnClickTrap(closureCopy));
        }

        //Handle cursor or set buttons if controller connected
        p2Controller = gm.GetControllerTwoState();
        if(p2Controller)
        {
            eventSystem.firstSelectedGameObject = trapButtons[0].gameObject;
            SetSelectedButton(0);
            controllerCursor.enabled = true;
        }
        else
        {
            controllerCursor.enabled = false;
        }

        //placeEnabled = false;
    }
	

	void Update () {
        //Move controller cursor & get input
        p2Controller = gm.GetControllerTwoState();
        if (p2Controller)
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal_Joy_2")) > 0.6f || Mathf.Abs(Input.GetAxisRaw("Vertical_Joy_2")) > 0.6f)
            {
                controllerCursor.transform.Translate(Input.GetAxisRaw("Horizontal_Joy_2") * cursorSpeed, Input.GetAxisRaw("Vertical_Joy_2") * cursorSpeed, 0);
            }
            
            if (Input.GetButton("Place_Joy_2") /*&& placeEnabled*/)
            {
                //RaycastFromCam(true);
            }
        }

        MoveGhost();
        if (trap != null && ghostTrap != null) CheckValidLocation();
    }

    private Vector3? GetGridPosition()
    {
        if (RaycastFromCam() != null)
        {
            RaycastHit hit = RaycastFromCam().Value;
            int hitX = -1;
            int hitZ = -1;
            switch (cam.GetComponent<CameraTwoRotator>().GetState())
            {
                case 1:
                    hitX = Mathf.RoundToInt((hit.point.x - 1) / gridSize) * gridSize + 1;
                    hitZ = Mathf.RoundToInt(hit.point.z + -2);
                    break;
                case 2:
                    hitX = Mathf.RoundToInt(hit.point.x + 2);
                    hitZ = Mathf.RoundToInt((hit.point.z - 1) / gridSize) * gridSize + 1;
                    break;
                case 3:
                    hitX = Mathf.RoundToInt((hit.point.x - 1) / gridSize) * gridSize + 1;
                    hitZ = Mathf.RoundToInt(hit.point.z + 2);
                    break;
                case 4:
                    hitX = Mathf.RoundToInt(hit.point.x + -2);
                    hitZ = Mathf.RoundToInt((hit.point.z - 1) / gridSize) * gridSize + 1;
                    break;
            }
            int hitY = Mathf.RoundToInt((hit.point.y - 1)/ gridSize) * gridSize + 1;
            return new Vector3(hitX, hitY, hitZ);
        }
        else return null;
    }

    private RaycastHit? RaycastFromCam()
    {
        RaycastHit hit;
        Ray ray;
        if (p2Controller)
        {
            ray = cam.ScreenPointToRay(controllerCursor.transform.position);
        }
        else
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);
        }
        if (Physics.Raycast(ray, out hit, float.MaxValue, ~LayerMask.NameToLayer("Tower")))
        {
            return hit;
        }
        else
        {
            return null;
        }
    }

    //Called from event trigger on center column of tower when player clicks on it
    public void OnClickTower()
    {
        if(!Input.GetMouseButtonUp(1))
        {
            SetTrap();
        }
    }

    private void SetTrap()
    {
        if (GetGridPosition() != null)
        {

            Vector3 position = GetGridPosition().Value;
            if (ghostTrap != null && CheckFloor(position.y))
            {
                trap.InstantiateTrap(position, ghostTrap.transform.rotation);
                trap = null;
                DestroyGhost();

                if (p2Controller)
                {
                    SetSelectedButton(previouslySelected);
                    //placeEnabled = false;
                }
            }
        }
    }

    //Check to see that mage is clicking on correct floor
    private bool CheckFloor(float hitY)
    {
        int floor = cam.GetComponent<CameraTwoRotator>().GetFloor();
        float upperLimit = floor * 20;
        float lowerLimit = upperLimit - 20;

        return (hitY >= lowerLimit && hitY <= upperLimit);
    }

    //Vector3 scale = new Vector3(2, 4, 4);
    private bool CheckValidLocation()
    {
        Debug.Log(trap.ValidLocations);
        return true;

    }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    //Check that it is being run in Play Mode, so it doesn't try to draw this in Editor mode
    //    if (m_Started && ghostTrap != null)
    //        //Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size)
    //        Gizmos.DrawWireCube(ghostTrap.transform.position + new Vector3(0, 2, 0), scale);
    //}
    private void SetGhost()
    {
        if(trap != null)
        {
            ghostTrap = trap.InstantiateTrap(Vector3.zero);
        }

        //Destroy scripts & collider on ghost
        foreach (MonoBehaviour script in ghostTrap.GetComponents<MonoBehaviour>())
        {
            Destroy(script);
        }
        Destroy(ghostTrap.GetComponent<Collider>());

        //Make half transparent
        Color color = ghostTrap.GetComponent<MeshRenderer>().material.color;
        color.a = 0.5f;
        ghostTrap.GetComponent<MeshRenderer>().material.color = color;

    }

    private void MoveGhost()
    {
        if (ghostTrap != null)
        {
            UpdateRotationInput();
            FinalizeRotationInput();

            if (GetGridPosition() != null)
            {
                Vector3 position = GetGridPosition().Value;
                ghostTrap.transform.position = position;

                if (Input.GetMouseButton(1) || Input.GetButton("Cancel_Joy_2"))
                {
                    DestroyGhost();

                    if (p2Controller)
                    {
                        SetSelectedButton(previouslySelected);
                        //placeEnabled = false;
                    }
                }
            }
        }
    }

    //Change x/z rotation based on player input
    private int trapRot = 0;
    private void UpdateRotationInput()
    {
        if (RaycastFromCam() != null)
        {
            RaycastHit hit = RaycastFromCam().Value;

            if (Input.GetButtonDown("RotateLeft_Joy_2"))
            {
                if (hit.normal.x == -1 || hit.normal.x == 1)
                {
                    trapRot--;
                }
                else
                {
                    trapRot++;
                }
            }
            else if (Input.GetButtonDown("RotateRight_Joy_2"))
            {
                if (hit.normal.x == -1 || hit.normal.x == 1)
                {
                    trapRot++;
                }
                else
                {
                    trapRot--;
                }
            }
        }
    }

    //Change y rotation of hit based on current side of tower
    private void FinalizeRotationInput()
    {
        if (RaycastFromCam() != null)
        {
            RaycastHit hit = RaycastFromCam().Value;

            if (hit.normal.x == -1 || hit.normal.x == 1)
            {
                ghostTrap.transform.rotation = Quaternion.Euler(ghostTrap.transform.rotation.x, 90, 90 * trapRot);
            }
            else
            {
                ghostTrap.transform.rotation = Quaternion.Euler(ghostTrap.transform.rotation.x, 0, 90 * trapRot);
            }
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

    private void OnClickTrap(int trapNum)
    {
        trap = trapPrefabs[trapNum];
        
        previouslySelected = trapNum;
        eventSystem.SetSelectedGameObject(null);

        SetGhost();
    }

    private void SetSelectedButton(int trapNum)
    {
        eventSystem.SetSelectedGameObject(trapButtons[trapNum].gameObject);
    }
}
