using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selfDestruct : MonoBehaviour {

    public float time;
    private float count;

	// Use this for initialization
	void Start () {
        count = 0;
	}
	void timer()
    {
        count += Time.deltaTime;
        if (count > time)
        {
            Destroy(gameObject);
        }
    }
	// Update is called once per frame
	void Update () {
        timer();
	}

    public void destroyNow()
    {
        Destroy(gameObject);
    }

}
