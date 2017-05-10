using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selfDestruct : MonoBehaviour {

    public float time;
    private float count;
    public bool isParticle;
 

	// Use this for initialization
	void Start () {
        count = 0;
	}
	void timer()
    {
        count += Time.deltaTime;
        if (count > time)
        {
            if (isParticle == true)
            {
                this.gameObject.GetComponent<ParticleSystem>().Stop();               
            }
            else
            {
                Destroy(gameObject);
            }
           
        }
        if (count > time+4.0f && isParticle == true)
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
