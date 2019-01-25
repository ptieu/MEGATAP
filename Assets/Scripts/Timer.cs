using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    [SerializeField] private float time = 500f;
    private bool lose;
    [SerializeField] Text TimerText;


    void Start () {
        lose = false;
        SetTimerText();
	}
	
	// Update is called once per frame
	void Update ()
    { 
        //timer countdown
        time -= Time.deltaTime;
        SetTimerText();
        if (time < 0)
        {
            lose = true;
            GameOver();
        }
    }

    void SetTimerText()
    {
        TimerText.text = "Time: " + time.ToString("F0");
    }

    public bool GameOver()
    {
        return lose;
    }
}
