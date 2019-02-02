using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Flags]
public enum Location
{
    FreeFloat = 1, //Doesn't need to be attached to a surface
    Ceiling = 2,
    Floor = 4,
    LeftWall = 8,
    RightWall = 16,

    AnyWall = LeftWall | RightWall,
    AnySurface = LeftWall | RightWall | Floor | Ceiling,
    FloorAndCeiling = Floor | Ceiling
}

// a base class for traps to build on
public class TrapBase : MonoBehaviour {
    [EnumFlag] [SerializeField]
    public Location ValidLocations;


    //CameraTwoRotator cam;
    //[SerializeField] Vector3 overlapBoxOffset;
    //[SerializeField] Vector3 overlapBoxScale;
    //private Vector3 offset, scale; //Change these instead of serialized field

    //For drawing overlap box
   // bool m_Started;
    private void Start()
    {
      //  m_Started = true;
      //  cam = GameObject.Find("Player 2 Camera").GetComponent<CameraTwoRotator>();
    }
    //private void Update()
    //{
    //    switch (cam.GetState())
    //    {
    //        case 1:
    //        case 3:
    //            offset = overlapBoxOffset;
    //            scale = overlapBoxScale;
    //            break;
    //        case 2:
    //        case 4:
    //            offset = new Vector3(overlapBoxOffset.z, overlapBoxOffset.y, overlapBoxOffset.x);
    //            scale = new Vector3(overlapBoxScale.z, overlapBoxScale.y, overlapBoxScale.x);
    //            break;

    //    }
    //}

    //public Collider[] OverlapBox()
    //{
    //    ////Debug.Log((scale / 4) + "; " + (scale.x / 4) + ", " + (scale.y) + ", " + (scale.z / 4));
    //    //Vector3 overlapScale = new Vector3(scale.x / 2, scale.y / 4, scale.z / 2);
    //    //Collider[] hitColliders = Physics.OverlapBox(this.transform.position + offset, overlapScale, Quaternion.identity);

    //    //return hitColliders;
    //}
    public GameObject InstantiateTrap(Vector3 position)
    {
        return Instantiate(this.gameObject, position, this.transform.rotation);
    }

    public GameObject InstantiateTrap(Vector3 position, Quaternion rotation)
    {
        return Instantiate(this.gameObject, position, rotation);
    }

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

    private float time;

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
                rb.velocity = new Vector3(knockBackDistance * -playerx, knockUpDistance * -playery, 0);
                break;
            case 4:
                rb.velocity = new Vector3(0, knockUpDistance * -playery, knockBackDistance * playerz);
                break;
        }
        Debug.Log(playerx + " " + playery + " " + playerz);
    }

    // apply stun to inputted
    public void Stun(GameObject obj, int stunTime, GameObject trap)
    {
        obj.gameObject.GetComponent<PlayerOneMovement>().SetMove(false);
        obj.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, obj.gameObject.GetComponent<Rigidbody>().velocity.y, 0);
        time += Time.deltaTime;
        if(time >= stunTime)
        {
            obj.gameObject.GetComponent<PlayerOneMovement>().SetMove(true);
            time = 0;
            Destroy(trap);
        }
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


    //This is to draw the overlap box around the object, to help with debugging
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    //Check that it is being run in Play Mode, so it doesn't try to draw this in Editor mode
    //    if (m_Started)
    //    {
    //        //Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size)
    //        Gizmos.DrawWireCube(this.transform.position + offset, scale);
    //    }
    //}
}
