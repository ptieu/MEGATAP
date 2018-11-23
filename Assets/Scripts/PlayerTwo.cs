using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTwo : MonoBehaviour {
    [SerializeField] private Button[] trapButtons;
    [SerializeField] private GameObject[] traps;
    [SerializeField] private GameObject tower;
    [SerializeField] private Camera cam;

    private GameObject trap;



    private void Start()
    {
        trapButtons[0].onClick.AddListener(OnClickTrap1);
        trapButtons[1].onClick.AddListener(OnClickTrap2);
        trapButtons[2].onClick.AddListener(OnClickTrap3);
        trapButtons[3].onClick.AddListener(OnClickTrap4);
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

            Instantiate(trap, hitPos, hitRot);
        }
    }

    private void OnClickTrap1()
    {
        trap = traps[0];
    }

    private void OnClickTrap2()
    {
        trap = traps[1];
    }

    private void OnClickTrap3()
    {
        trap = traps[2];
    }

    private void OnClickTrap4()
    {
        trap = traps[3];
    }
}
