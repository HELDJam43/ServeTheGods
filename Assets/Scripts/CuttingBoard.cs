using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingBoard : MonoBehaviour {

    public bool activated = false;

    float activeRamp = 0;
    static float ACTIVESPEED = 10f;

    Animator animator;

    AudioSource sound;

    float soundFalloff = 0;
    static float SOUNDTIME = 3;
    float repeatTimer = 0;
    static float REPEATSPEED = .14f;

    static float[] PENTATONIC = { 1, 9 / 8f, 5 / 4f, 3 / 2f, 5 / 3f, 2 };

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();
        if (Global.FirstCuttingBoard)
        {
            Global.FirstCuttingBoard = false;
            TutorialText tt = Instantiate(Global.TutorialPrefab).GetComponent<TutorialText>();
            tt.Show("Use Appliances to create new foods", 3, transform.position, 1);
        }
    }
	
	// Update is called once per frame
	void Update () {
		if(activated)
        {
            soundFalloff += Time.deltaTime;
            repeatTimer -= Time.deltaTime;
            if (repeatTimer <= 0)
                repeatTimer = REPEATSPEED;

            activeRamp += ACTIVESPEED * Time.deltaTime;
        } else
        {
            soundFalloff = 0;
            activeRamp -= ACTIVESPEED * Time.deltaTime;
        }
        activeRamp = Mathf.Clamp01(activeRamp);

        if (soundFalloff > 0 && soundFalloff < SOUNDTIME && repeatTimer == REPEATSPEED)
        {
            sound.volume = Mathf.Pow((1 - (soundFalloff / SOUNDTIME)), 2);
            sound.pitch = PENTATONIC[Random.Range(0, PENTATONIC.Length)];
            sound.Play();
        }

        animator.SetFloat("Chopping", activeRamp);
	}
}
