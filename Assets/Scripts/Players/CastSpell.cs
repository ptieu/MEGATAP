using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastSpell : MonoBehaviour {
    [SerializeField] private GameManager gm;
    [SerializeField] private Image controllerCursor;

	
	void Update () {
        bool controller = gm.GetControllerTwoState();
        float cursorX, cursorY;
        if (controller)
        {
            cursorX = controllerCursor.transform.position.x;
            cursorY = controllerCursor.transform.position.y;
        }
        else
        {
            cursorX = Input.mousePosition.x;
            cursorY = Input.mousePosition.y;
        }

        //Get rid of cursorY != 0
        if(cursorX < Screen.width / 2 && cursorY != 0)
        {
            //Debug.Log(cursorX + ", " + cursorY);
        }
	}
}
