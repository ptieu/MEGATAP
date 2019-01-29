using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : MonoBehaviour {

    // dictate where trap can be placed
    [SerializeField] private bool canPlaceAbove;
    [SerializeField] private bool canPlaceBelow;
    [SerializeField] private bool canPlaceLeft;
    [SerializeField] private bool canPlaceRight;
    [SerializeField] private TrapBase trapBase;

    // let the FixedUpdate method know that there was a collision
    private bool hit = false;
    // the player (or whatever collided with this trap)
    private GameObject player = null;
    // keep track of how many frames of knockback have passed

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
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
