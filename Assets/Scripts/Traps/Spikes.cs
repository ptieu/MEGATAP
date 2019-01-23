﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {

    // dictate where trap can be placed
    [SerializeField] private bool canPlaceAbove;
    [SerializeField] private bool canPlaceBelow;
    [SerializeField] private bool canPlaceLeft;
    [SerializeField] private bool canPlaceRight;
    [SerializeField] private TrapBase trapBase;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision other)
    {
        trapBase.KnockBack(other, 5);
    }
}