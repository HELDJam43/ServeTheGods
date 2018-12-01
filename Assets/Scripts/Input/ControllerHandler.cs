using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;
public class ControllerHandler : MonoBehaviour {
    public GameObject xboxPrefab, pcPrefab;
    public GameObject currentController; 

    GamePadState state;
    public static bool usingController=false;
    public delegate void OnControllerChangedEvent();
    public static OnControllerChangedEvent OnControllerChanged;
    // Use this for initialization
    void Awake () {
        UsePC();
	}
	
	// Update is called once per frame
	void Update () {
       
        state = GamePad.GetState((PlayerIndex)0);
        if (usingController)
        {
            if (!state.IsConnected)
                UsePC();
        }
        else
        {
            if (state.IsConnected)
                UseXbox();
        }
    }

    void UsePC()
    {
        usingController = false;
        Destroy(currentController);
        currentController = GameObject.Instantiate(pcPrefab);
        currentController.GetComponent<InputHandler>().RegisterActions();
        if (OnControllerChanged != null)
            OnControllerChanged();
    }

    void UseXbox()
    {
        usingController = true;
        Destroy(currentController);
        currentController = GameObject.Instantiate(xboxPrefab);
        currentController.GetComponent<InputHandler>().RegisterActions();
        if (OnControllerChanged != null)
            OnControllerChanged();
    }
}
