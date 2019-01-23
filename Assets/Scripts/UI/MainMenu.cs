using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
	public GameObject popup;

	void Start () {
		popup = GameObject.Find("Panel");
		popup.gameObject.SetActive(false);
	}
    public void OnClickPlay()
    {
        SceneManager.LoadScene("Tower1");
        SceneManager.LoadScene("Tower1_Platforms", LoadSceneMode.Additive);
    }
    
    public void QuitGame()
    {
		popup.gameObject.SetActive(true);
    }
    
    public void YesButton()
    {
    	Debug.Log("Quit");
    	Application.Quit();
    }
    
    public void NoButton()
    {
    	popup.gameObject.SetActive(false);
    }	
}
