using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {
    private TrapBase trapBase;

    // custom to this trap
    [SerializeField] private int knockBackValue = 75;
    [SerializeField] private int knockUpValue = 25;

    // let the FixedUpdate method know that there was a collision
    private bool hit = false;
    // the player (or whatever collided with this trap)
    private GameObject player = null;
    // keep track of how many frames of knockback have passed
    private int knockTimer = 0;

    // Use this for initialization
    void Start () {
        trapBase = GetComponent<TrapBase>();
	}
	
	// Update is called once per frame
    // knockback has a knockback velocity, knockup velocity, and a knockTimer to 
    // force the knockback into an arc shape.
	void FixedUpdate () {
        if (player != null)
        {
            if (hit && knockTimer < 7 && knockTimer >= 5)
            {
                trapBase.KnockBack(player, knockBackValue, 0);
                knockTimer++;
            }
            else if (hit && knockTimer < 7)
            {
                trapBase.KnockBack(player, 0, knockUpValue);
                knockTimer++;
            }
            else
            {
                hit = false;
                knockTimer = 0;
            }
        }
       
    }

    void OnTriggerEnter(Collider other)
    {
        hit = true;
        player = other.gameObject;
        trapBase.UpdatePlayerVelocities(other.gameObject);
    }
}
