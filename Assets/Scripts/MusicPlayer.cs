using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

    AudioSource audio;
    static MusicPlayer instance = null;

	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
        if(instance != null)
        {
            Destroy(gameObject);
        } else
        {
            instance = this;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
