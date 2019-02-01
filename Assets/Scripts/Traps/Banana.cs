using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : MonoBehaviour {
    private TrapBase trapBase;

    // let the FixedUpdate method know that there was a collision
    private bool hit = false;
    // the player (or whatever collided with this trap)
    private GameObject player = null;
    // keep track of how many frames of knockback have passed

    private void Start()
    {
        trapBase = GetComponent<TrapBase>();
    }
    // Stun has player object, stun time in seconds, trap itself
    // player has normal y velocity but is stopped in all other velocities and cannot move controls
    void FixedUpdate()
    {
        if (player != null)
        {
            if (hit)
            {
                trapBase.Stun(player, 2, this.gameObject);
            }
            else
            {
                hit = false;
            }
            
        }
        Debug.Log(player);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            hit = true;
            player = other.gameObject;
        }
    }
}
