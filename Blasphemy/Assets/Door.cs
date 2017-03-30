using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public int doorType;
    public float timeCount;
    public float Timer;
    public int speed;
    public bool isOpened;
    public bool start;

    //door type
    // 0 = floating up to wall and destroy
    //
    //

	// Use this for initialization
	void Start () {
        timeCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (start == true)
        {
            open();
        }
	}

    public void open()
    {
        if (doorType == 0)
        {
            start = true;
            transform.Translate(Vector3.up * speed * Time.deltaTime);
            timeCount += Time.deltaTime;
            if (timeCount > Timer)
            {
                Destroy(gameObject);
            }
        }

    }
}
