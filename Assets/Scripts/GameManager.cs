using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {

    //Game Over Status
    [SerializeField] private PlayerOne player;
    private bool dead = false;

	void Update () {
		if(Input.GetButton("Cancel"))
        {
            SceneManager.LoadScene("Menu");
        }

        //Game Over from timer
        dead = player.GameOver();
        if(dead == true)
        {
            SceneManager.LoadScene("Menu");
        }
	}
}
