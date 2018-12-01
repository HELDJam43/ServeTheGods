using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Image))]
public class InputImage : MonoBehaviour
{
    public string actionName = "INVALID";

    public string ActionName
    {
        get
        {
            return actionName;
        }

        set
        {
            actionName = value;
            UpdateImage();
        }
    }

    // Use this for initialization
    void OnEnable()
    {
        UpdateImage();
        ControllerHandler.OnControllerChanged += UpdateImage;
    }
    private void OnDisable()
    {
        ControllerHandler.OnControllerChanged -= UpdateImage;
    }

    public void UpdateImage()
    {
        if (actionName != "INVALID")
            GetComponent<Image>().sprite = InputHandler.GetButtonImage(ActionName);
    }

}
