using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// a base class for traps to build on
public class TrapBase : MonoBehaviour {

    // keeps track of which direction the player was moving at the moment of this save
    // be sure to call UpdatePlayerVelocities() before using these variables
    private int playerx = 0;
    private int playery = 0;
    private int playerz = 0;

    // call before using the playerx playery playerz variables
    public void UpdatePlayerVelocities( GameObject obj )
    {
        // find out the velocity of the player
        if (obj.gameObject.GetComponent<Rigidbody>().velocity.x > 0)
        {
            playerx = 1;
        }
        else if (obj.gameObject.GetComponent<Rigidbody>().velocity.x < 0)
        {
            playerx = -1;
        }
        if (obj.gameObject.GetComponent<Rigidbody>().velocity.y > 0)
        {
            playery = 1;
        }
        else if (obj.gameObject.GetComponent<Rigidbody>().velocity.y < 0)
        {
            playery = -1;
        }
        if (obj.gameObject.GetComponent<Rigidbody>().velocity.z > 0)
        {
            playerz = 1;
        }
        else if (obj.gameObject.GetComponent<Rigidbody>().velocity.z < 0)
        {
            playerz = -1;
        }
    }

    // apply knockback to inputted
    // must be used in a FixedUpdate method, will apply velocity per frame. Use a timing
    // method to decide how many frames force is applied.
    public void KnockBack(GameObject obj, int knockBackDistance, int knockUpDistance) 
    {
        // find out which way the player is facing (on faces of the tower) so the knock can do accordingly
        int playerDirection = obj.gameObject.GetComponent<PlayerOneMovement>().GetState();
        Rigidbody rb = obj.gameObject.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * -1 * knockBackDistance);

        switch (playerDirection)
        {
            case 1:
                rb.velocity = new Vector3(-knockBackDistance * playerx, knockUpDistance * -playery, 0);
                break;
            case 2:
                rb.velocity = new Vector3(0, knockUpDistance * -playery, -knockBackDistance * playerz);
                break;
            case 3:
                rb.velocity = new Vector3(knockBackDistance * -playerx, knockUpDistance * playery, 0);
                break;
            case 4:
                rb.velocity = new Vector3(0, knockUpDistance * -playery, knockBackDistance * playerz);
                break;
        }
        Debug.Log(playerx + " " + playery + " " + playerz);
    }

    // apply knockback to inputted
    public void Stun(GameObject obj, int stunDuration)
    {
        Debug.Log("Stun");
    }

    // apply knockback to inputted
    // input directions: both "percents" should be between 0 and 1
    public void Slow(GameObject obj, float slowPercent, float jumpReductionPercent)
    {
        obj.gameObject.GetComponent<PlayerOneMovement>().SetJumpHeight(obj.gameObject.GetComponent<PlayerOneMovement>().GetJumpHeight() * jumpReductionPercent);
        obj.gameObject.GetComponent<PlayerOneMovement>().SetSpeed(obj.gameObject.GetComponent<PlayerOneMovement>().GetSpeed() * slowPercent);
    }

    // apply knockback to inputted
    public void RestartFace(GameObject obj)
    {
        Debug.Log("RestartFace");
    }
}
