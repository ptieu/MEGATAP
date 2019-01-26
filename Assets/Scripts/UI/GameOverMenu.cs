using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour {

    public void onClickRetry()
    {
        SceneManager.LoadScene("Tower1");
    }

    public void onClickMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
