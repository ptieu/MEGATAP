using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// a base class for traps to build on
public class TrapBase : MonoBehaviour {

    // apply knockback to inputted
    // must be used in a FixedUpdate method, will apply velocity per frame. Use a timing
    // method to decide how many frames force is applied.
    public void KnockBack(GameObject obj, int knockBackDistance, int knockUpDistance) 
    {
        // find out which way the player is facing so the knock can 
        int playerDirection = obj.gameObject.GetComponent<PlayerOneMovement>().GetState();
        Rigidbody rb = obj.gameObject.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * -1 * knockBackDistance);
        switch (playerDirection)
        {
            case 1:
                rb.velocity = new Vector3(-knockBackDistance, knockUpDistance, 0);
                break;
            case 2:
                rb.velocity = new Vector3(0, knockUpDistance, -knockBackDistance);
                break;
            case 3:
                rb.velocity = new Vector3(knockBackDistance, knockUpDistance, 0);
                break;
            case 4:
                rb.velocity = new Vector3(0, knockUpDistance, knockBackDistance);
                break;
        }
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
