using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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



            if(ghostTrap != null)
            {
                Debug.Log("move");
                ghostTrap.transform.position = hitPos;
                ghostTrap.transform.rotation = hitRot;
            }
        }
    }
    public void OnClickTower()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 99999, ~LayerMask.NameToLayer("Tower")))
        {
            Vector3 hitPos = hit.point + hit.normal * 5;
            Quaternion hitRot = Quaternion.identity;

            if(hit.normal.x == -1 || hit.normal.x == 1)
            {
                hitRot = Quaternion.Euler(0, 90, 0);
            }

           

            if (CheckNearby(hit.point, widthBetweenTraps, heightBetweenTraps) && trap != null)
            {
                Instantiate(trap, hitPos, hitRot);
                trap = null;
                Destroy(ghostTrap);
                ghostTrap = null;
            }
        }
    }

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
