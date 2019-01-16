using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {

    //Game Over Status
    [SerializeField] private PlayerOne player;
    private bool lose = false;

	void Update () {
		if(Input.GetButton("Cancel"))
        {
            SceneManager.LoadScene("Menu");
        }

        //Game Over from timer
        lose = player.GameOver();
        if(lose == true)
        {
            SceneManager.LoadScene("Menu");
        }
	}
}
