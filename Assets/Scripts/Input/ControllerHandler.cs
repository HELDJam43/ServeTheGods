using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_STANDALONE_WIN
using XInputDotNetPure;
#endif
public class ControllerHandler : MonoBehaviour {
    public GameObject xboxPrefab, pcPrefab;
    public GameObject currentController;
#if UNITY_STANDALONE_WIN
    GamePadState state;
#endif
    public static bool usingController=false;
    public delegate void OnControllerChangedEvent();
    public static OnControllerChangedEvent OnControllerChanged;
    // Use this for initialization
    void Awake () {
        UsePC();
	}
#if UNITY_STANDALONE_WIN
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
#endif
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
