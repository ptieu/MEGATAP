using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneLose : MonoBehaviour {
    private bool lose;
    [SerializeField] Timer timer;
    private float time;

	// Use this for initialization
	void Start () {
        lose = false;
	}
	
	// Update is called once per frame
	void Update () {
        time = timer.getTime();
		if(time < 0)
        {
            lose = true;
            GameOver();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //check collision with Rising walls that are tagged with "rise"
        if (other.tag == "rise")
        {
            lose = true;
            GameOver();
        }
    }

    public bool GameOver()
    {
        return lose;
    }
}
