using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScene : MonoBehaviour {
    private FadeZero fz ;
    private float timeCount;
    public AudioClip intro;
    public AudioSource aso;
    public bool start;
    public float clipDuration;
	// Use this for initialization
	void Start () {
        fz = FindObjectOfType<FadeZero>();
        timeCount = 0.0f;
        
        if (start == true)
        {
            aso.clip = intro;
            aso.Play();
            GetComponent<selfDestruct>().enabled = true;
        }
             
    }
	
	// Update is called once per frame
	void Update () {

        if (start == true)
        {
            timeCount += Time.deltaTime;
            if (0.0f < timeCount && timeCount < clipDuration)
            {
                fz.setVlack();
                fz.startnewFade();
            }
            if (clipDuration < timeCount && timeCount < clipDuration + 3.0f)
            {
                if (fz.fadeFinish == true)
                {
                    fz.fadeIn();
                }
            }
        }
        
        


    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            start = true;
            aso.clip = intro;
            aso.Play();
            GetComponent<selfDestruct>().enabled = true;
        }

    }
}
