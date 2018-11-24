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
    [SerializeField] private float widthBetweenTraps;
    [SerializeField] private float heightBetweenTraps;

    private GameObject trap;
    private GameObject ghostTrap;



    private void Start()
    {
        trapButtons[0].onClick.AddListener(OnClickTrap1);
        trapButtons[1].onClick.AddListener(OnClickTrap2);
        trapButtons[2].onClick.AddListener(OnClickTrap3);
        trapButtons[3].onClick.AddListener(OnClickTrap4);
    }


    private void Update()
    {
        RaycastFromCam(false);
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
            Vector3 hitPos = hit.point + hit.normal * 5;
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

            if (clicked && CheckNearby(hit.point, widthBetweenTraps, heightBetweenTraps) && trap != null)
            {
                Instantiate(trap, hitPos, hitRot);
                trap = null;
                Destroy(ghostTrap);
                ghostTrap = null;
            }
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

    private void OnClickTrap1()
    {
        trap = traps[0];
        SetGhost();
    }

    private void OnClickTrap2()
    {
        trap = traps[1];
        SetGhost();
    }

    private void OnClickTrap3()
    {
        trap = traps[2];
        SetGhost();
    }

    private void OnClickTrap4()
    {
        trap = traps[3];
        SetGhost();
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
        }
    }
}
