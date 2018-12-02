﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingBoard : MonoBehaviour {

    public bool activated = false;

    float activeRamp = 0;
    static float ACTIVESPEED = 10f;

    Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(activated)
        {
            activeRamp += ACTIVESPEED * Time.deltaTime;
        } else
        {
            activeRamp -= ACTIVESPEED * Time.deltaTime;
        }
        activeRamp = Mathf.Clamp01(activeRamp);

        animator.SetFloat("Chopping", activeRamp);
	}
}
