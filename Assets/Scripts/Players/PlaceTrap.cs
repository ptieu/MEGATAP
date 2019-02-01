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

	void Start () {
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
    }

    private Vector3 GetGridPosition()
    {
        RaycastHit hit;
        Ray ray;
        if(p2Controller)
        {
            ray = cam.ScreenPointToRay(controllerCursor.transform.position);
        }
        else
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);
        }

        if (Physics.Raycast(ray, out hit, float.MaxValue, ~LayerMask.NameToLayer("Tower")))
        {
            //Round position to nearest grid point
            int hitX = Mathf.RoundToInt(hit.point.x / gridSize) * gridSize;
            int hitZ = Mathf.RoundToInt(hit.point.z / gridSize) * gridSize;
            int hitY = Mathf.RoundToInt(hit.point.y / gridSize) * gridSize;
            Debug.Log(hitX + ", " + hitY + ", " + hitZ);
            return new Vector3(hitX, hitY, hitZ) + hit.normal * 5;
        }
        else
        {
            Debug.Log("NO POSITION");
            return Vector3.zero;
        }
    }


    private void SetTrap()
    {
        Vector3 position = GetGridPosition();
        trap.InstantiateTrap(position);
        trap = null;
        DestroyGhost();

        if(p2Controller)
        {
            SetSelectedButton(previouslySelected);
            //placeEnabled = false;
        }
    }

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
        //TODO: ROTATION STUFF
        if (ghostTrap != null)
        {
            Vector3 position = GetGridPosition();
            ghostTrap.transform.position = position;

            if (Input.GetMouseButton(1) || Input.GetButton("Cancel_Joy_2"))
            {
                DestroyGhost();

                if(p2Controller)
                {
                    SetSelectedButton(previouslySelected);
                    //placeEnabled = false;
                }
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
        Debug.Log(trapNum);
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
