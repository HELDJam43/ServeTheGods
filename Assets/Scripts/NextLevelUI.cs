using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class NextLevelUI : MonoBehaviour
{
    public bool finalLevel = false;
    TextMeshProUGUI text;
    // Use this for initialization
    void Start()
    {
        if (finalLevel)
        {
            Destroy(this);
            text.text = "Thanks for playing";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ActionSystem.OnActionDown(ActionSystem.NEXT))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
