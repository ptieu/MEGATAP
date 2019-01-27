using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    [SerializeField] private float time = 500f;
    [SerializeField] Text TimerText;


    void Start () {
        SetTimerText();
	}
	
	// Update is called once per frame
	void Update ()
    { 
        //timer countdown
        time -= Time.deltaTime;
        SetTimerText();
    }

    void SetTimerText()
    {
        TimerText.text = "Time: " + time.ToString("F0");
    }


    public float getTime()
    {
        return time;
    }
}
