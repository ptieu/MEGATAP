using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rise : MonoBehaviour {
    //Vine colliders
    [SerializeField] private GameObject rightWall;
    [SerializeField] private GameObject leftWall;
    [SerializeField] private GameObject botWall;
    [SerializeField] private GameObject topWall;

    //For speed of vines
    private float targetPosition = 42f;
    private float targetSize = 85f;
    private float currentPosition;
    private float currentSize;
    private float position;
    private float scale;
    [SerializeField] float yMov = 0.01f;

    //when we have the vines animation, keep the visual 2 from colliders.

    //Timing
    private float time;


    // Use this for initialization
    void Start () {
        //references
        time = 0;
        currentPosition = rightWall.transform.position.x;
        currentSize = rightWall.transform.localScale.z;
        position = (currentPosition - targetPosition) / 30f;
        scale = (currentSize - targetSize) / 30f;
	}
	
	// Update is called once per frame
	void Update () {

        time += Time.deltaTime;

        //start vines after certain time
        if (time >= 10 && time <= 11f)
        {
            rightWall.transform.position = new Vector3(140f, 1f, 0);
            leftWall.transform.position = new Vector3(-140f, 1f, 0);
            botWall.transform.position = new Vector3(0, 1f, -140f);
            topWall.transform.position = new Vector3(0, 1f, 140f);
        }

        //Vines begin to move in over a certain period of time
        if (time >= 11f && time <= 41f)
        {
            moveIn();
        }

        //Vines crawl up until tower height is reached
        if (time >= 41f && rightWall.transform.localScale.y <= 400)
        {
            moveUp();
        }
    }

    void moveIn()
    {
        //move inwards
        rightWall.transform.Translate(Vector3.left * Time.deltaTime * position, Space.World);
        leftWall.transform.Translate(Vector3.right * Time.deltaTime * position, Space.World);
        botWall.transform.Translate(Vector3.forward * Time.deltaTime * position, Space.World);
        topWall.transform.Translate(-Vector3.forward * Time.deltaTime * position, Space.World);
        
        //sized dinwards appropriately
        rightWall.transform.localScale -= new Vector3(0, 0, scale * Time.deltaTime);
        leftWall.transform.localScale -= new Vector3(0, 0, scale * Time.deltaTime);
        botWall.transform.localScale -= new Vector3(scale * Time.deltaTime, 0, 0);
        topWall.transform.localScale -= new Vector3(scale * Time.deltaTime, 0, 0);
    }

    void moveUp()
    {
        //sized up on the y-axis
        rightWall.transform.localScale += new Vector3(0, yMov, 0);
        leftWall.transform.localScale += new Vector3(0, yMov, 0);
        botWall.transform.localScale += new Vector3(0, yMov, 0);
        topWall.transform.localScale += new Vector3(0, yMov, 0);
    }
}
