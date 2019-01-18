using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// a base class for traps to build on
public class TrapBase : MonoBehaviour {

    // dictate where trap can be placed
    [SerializeField] private bool canPlaceAbove;
    [SerializeField] private bool canPlaceBelow;
    [SerializeField] private bool canPlaceLeft;
    [SerializeField] private bool canPlaceRight;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
