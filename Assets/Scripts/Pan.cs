﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pan : MonoBehaviour {

    public bool activated = false;
    bool lastActivated = false;

    float activeRamp = 0;
    static float ACTIVESPEED = 3f;

    Animator animator;

    AudioSource sound;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();
        if (Global.FirstPAN)
        {
            Global.FirstPAN = false;
            TutorialText tt = Instantiate(Global.TutorialPrefab).GetComponent<TutorialText>();
            tt.Show("Use Appliances to create new foods", 3, transform.position, 2);
        }
    }
	
	// Update is called once per frame
	void Update () {
		if(activated)
        {
            if(activated != lastActivated)
            {
                sound.Play();
            }
            activeRamp += ACTIVESPEED * Time.deltaTime;
        } else
        {
            activeRamp -= ACTIVESPEED * Time.deltaTime;
        }
        lastActivated = activated;
        activeRamp = Mathf.Clamp01(activeRamp);

        animator.SetFloat("Frying", activeRamp);
	}
}
