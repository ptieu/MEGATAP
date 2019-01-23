using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// a base class for traps to build on
public class TrapBase : MonoBehaviour {

    // apply knockback to inputted
    public void KnockBack(Collision obj, int knockDistance) 
    {
        Debug.Log("Knockback");
    }

    // apply knockback to inputted
    public void Stun(Collision obj, int stunDuration)
    {
        Debug.Log("Knockback");
    }

    // apply knockback to inputted
    public void Slow(Collision obj, int slowDuration)
    {
        Debug.Log("Knockback");
    }

    // apply knockback to inputted
    public void RestartFace(Collision obj)
    {
        Debug.Log("Knockback");
    }
}
