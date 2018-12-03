using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialText : MonoBehaviour
{
    public bool playOnStart;
    TextMeshPro text;
    void Start()
    {
        if(playOnStart)
        {
            Destroy(gameObject, 6);
            gameObject.SetActive(true);
        }
    }
    public TextMeshPro Text
    {
        get
        {
            if (text == null)
                text = GetComponentInChildren<TextMeshPro>();
            return text;
        }

        set
        {
            text = value;
        }
    }

    public void Show(string t, float duration, Vector3 pos, float y, float delay = 0)
    {
        Destroy(gameObject, duration+delay);
        Text.text = t;
        transform.position = pos + Vector3.up * y;
        Invoke("Activate", delay);
    }
    void Activate()
    {
        gameObject.SetActive(true);
    }
}
