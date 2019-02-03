using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapOverlap : MonoBehaviour {
    [HideInInspector]
    public bool nearbyTrap;


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("TrapOverlap") || other.tag == "Platform")
        {
            nearbyTrap = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        nearbyTrap = false;
    }
}
