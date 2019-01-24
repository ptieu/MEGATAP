using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {
	public static bool GameIsPaused = false;
	public PauseMenu pause;
	
	
    //Game Over Status
    [SerializeField] private PlayerOne player;
    private bool lose = false;


    private string[] joysticks;

    private bool controllerOne;
    private bool controllerTwo;


    private void Awake()
    {
        joysticks = Input.GetJoystickNames();
        CheckControllers();
        SceneManager.LoadScene("Tower1_Platforms", LoadSceneMode.Additive);
        Debug.Log("Player 1 controller: " + controllerOne);
        Debug.Log("Player 2 controller: " + controllerTwo);
    }

    private void Update () {

		if(Input.GetButton("Cancel"))
        {
            PauseMenu pause = gameObject.GetComponent(typeof(PauseMenu)) as PauseMenu;
            if(GameIsPaused){
				pause.Resume();
			}
			else{
				pause.Pause();
			}
            
        }

        //Game Over from timer
        lose = player.GameOver();
        if(lose == true)
        {
            SceneManager.LoadScene("Menu");
        }

        CheckControllers();

	}

    private void CheckControllers()
    {
        if (Input.GetJoystickNames().Length > 0)
        {
            for (int i = 0; i < joysticks.Length; i++)
            {
                if (!string.IsNullOrEmpty(joysticks[i]))
                {
                    //Debug.Log("Controller " + i + " is connected using: " + joysticks[i]);
                    if (i == 0)
                    {
                        controllerOne = true;
                    }
                    if (i == 1)
                    {
                        controllerTwo = true;
                    }
                }
                else
                {
                    //Debug.Log("Controller " + i + " is disconnected.");
                    if (i == 0)
                    {
                        controllerOne = false;
                    }
                    if(i == 1)
                    {
                        controllerTwo = false;
                    }
                }
            }
        }
        else
        {
            controllerOne = false;
            controllerTwo = false;
        }
    }

    public bool GetControllerTwoState()
    {
        return controllerTwo;
    }

    public float GetInputAxis()
    {
        if (controllerOne)
        {
            if(Mathf.Abs(Input.GetAxis("Horizontal_Joy_1")) > 0.4f)
            {
                return Input.GetAxis("Horizontal_Joy_1");
            }
            else
            {
                return 0;
            }
        }
        else
        {
            return Input.GetAxis("Horizontal_Keyboard");
        }
    }
}
